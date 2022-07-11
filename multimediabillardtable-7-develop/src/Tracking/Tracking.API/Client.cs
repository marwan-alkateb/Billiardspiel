using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using Tracking.Model;

namespace Tracking.API
{
    public class Client : IDisposable
    {
        public TcpClient client { get; set; }
        private Thread listeningThread;
        private NetworkStream stream;
        public Frame LastFrame { get; private set; }
        public bool IsConnected => client.Connected;
        private bool IsListening = false;
        public bool IsFrameReady = false;
        public Client()
        {
            client = new TcpClient();
        }

        /// <summary>
        /// Connect to server.
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public void ConnectToServer(String ip, int port)
        {
            client.Connect(ip, port);
        }

        /// <summary>
        /// Connect to a server asynchronously
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public void ConnectToServerAsync(String ip, int port)
        {
            client.BeginConnect(ip, port, OnConnected, this);
        }

        /// <summary>
        /// Read one frame data from stream and update the last frame afterwards
        /// </summary>
        public async void ReadOneFrame()
        {
            if (client.GetStream().DataAvailable)
            {
                var frame = await Serializer.Deserialize<Frame>(client.GetStream());
                LastFrame = frame;
            }
        }

        /// <summary>
        /// Start a new thread that continously read the stream and update last frmae.
        /// </summary>
        public void BeginListening()
        {
            listeningThread = new Thread(ListenFunction);
            IsListening = true;
            listeningThread.Start();
        }

        /// <summary>
        /// Stop the listening thread
        /// </summary>
        public void stopListening()
        {
            IsListening = false;
        }
       

        private void ListenFunction()
        {
            while (client.Connected && IsListening)
            {
                ReadOneFrame();
            }
        }

        private void OnConnected(IAsyncResult result)
        {
            client.EndConnect(result);
            stream = client.GetStream();
        }
        public void Dispose()
        {
            client.Dispose();
            stream.Dispose();
            listeningThread.Abort();
        }

    }
}
