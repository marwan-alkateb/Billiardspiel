using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Threading.Tasks;
using Tracking.Model;
using System.IO;

namespace Tracking.API
{
    public class Server : IDisposable
    {
        TcpListener server;
        List<TcpClient> clientList;
        NetworkStream stream;
        public event EventHandler<string> LogMessageAvailable;
        public event EventHandler<string> ErrorMessageAvailable;
        public int BufferSize { get; set; }
        private bool IsListening = false;
        public Server(int port)
        {
            server = new TcpListener(IPAddress.Any, port);
            clientList = new List<TcpClient>();
        }

        /// <summary>
        /// Start a new thread that listen connection requests from clients.
        /// </summary>
        public async void Start()
        {
            server.Start();
            IsListening = true;
            while (IsListening)
            {
                await AcceptClient();
            }
        }

        /// <summary>
        /// Stop the server
        /// </summary>
        public void Stop()
        {
            IsListening = false;
            server.Stop();
        }

        /// <summary>
        /// Send a frame to all clients asynchronously
        /// </summary>
        /// <param name="frame"></param>
        public async void SendAsyncFrame(Frame frame)
        {
            TcpClient[] localClients;
            lock (clientList)
            {
                localClients = new TcpClient[clientList.Count];
                clientList.CopyTo(localClients, 0);
            }

            foreach (TcpClient client in localClients)
            {
                if (client != null && client.Connected)
                {
                    stream = client.GetStream();
                    var buffer = await Serializer.Serialize<Frame>(frame);

                    //If the client shut down while sending data, the System.IO.IOException exception is thrown.
                    //This method is written in the try catch block so that the server continues to run even if a client is shut down.
                    try
                    {
                        await stream.WriteAsync(buffer, 0, buffer.Length);
                        LogMessageAvailable.Invoke(this, "A frame sended");
                    }
                    catch (IOException exception)
                    {
                        ErrorMessageAvailable.Invoke(this, $"Error when sending data: {exception.Message}");
                        Console.Error.WriteLine(exception.StackTrace);
                    }
                    await Task.Delay(20);
                }
                else
                {
                    lock (clientList)
                    {
                        clientList.Remove(client);
                        LogMessageAvailable.Invoke(this, "A client removed.");
                    }
                }
            }
        }

        private async Task AcceptClient()
        {
            TcpClient client = await server.AcceptTcpClientAsync();
            lock (clientList)
            {
                clientList.Add(client);
            }
            LogMessageAvailable.Invoke(this, "A client accepted.");
        }
        public void Dispose()
        {
            server.Stop();
            server.Server.Dispose();
            foreach (TcpClient client in clientList)
            {
                client.Dispose();
            }
        }
    }
}