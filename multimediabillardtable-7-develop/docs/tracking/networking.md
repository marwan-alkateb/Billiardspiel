# Networking System
The networking system has been developed to transfer the data coming from the tracking system to a program that will process this data. Currently, this data is passed to the project in Unity.

## Implementation
This system consists of three classes: `Client`, `Server` and `Serializer`. The `Client` class must be created in the program that receives the data, and the `Server` class must be created in the program that sends the data.
The `Server` class sends the `Frame` data specified in the `Tracking.Model` project to the client asynchronously by serializing it with JSON. The client then receives this data in a separate thread.

### Server
The tracking system also includes a server program controlled from the console. With this program, the data coming from the system can be easily sent to the client created in another program.

### Client
After the client class is created, it can be connected to the server at a specific IP address and port with the `ConnectToServer()` method. Then it can start receiving data with the `BeginListening()` method. This function creates a new Thread and updates the `LastFrame` property of the `Client` class as data comes in.

## Usage example

```csharp
Client client = new Client();
client.ConnectToServer("127.0.0.1", 3200);
client.BeginListening();

while (true)
{
    Console.WriteLine(client.LastFrame?.ToString());
}
```
