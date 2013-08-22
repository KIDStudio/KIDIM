using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = GetConfigFile();

            TcpListener server = null;
            try
            {
                // Set the TcpListener on port.
                Int32 port = Convert.ToInt32(config.Port);
                IPAddress localAddr = IPAddress.Parse(config.IP);

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[1024];
                String data = null;

                // Enter the listening loop.
                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("{0} Connected", client.Client.LocalEndPoint);

                    data = null;

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.
                    if ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received LogIn request: {0}", data);

                        // Process the data sent by the client.
                        data = data.ToUpper();

                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                        // Send back a response.
                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Sent: {0}", data);
                    }

                    // Shutdown and end connection
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }


            Console.WriteLine("\nHit enter to continue...");
            Console.Read();

        }

        private static ServerConfig GetConfigFile()
        {

            var configManager = ConfigurationManager.AppSettings;
            var serverConfig = new ServerConfig();
            try
            {
                serverConfig.IP = configManager["IP"];
                serverConfig.Port = configManager["Port"];

            }
            catch (Exception ex)
            {
                Console.WriteLine("获取配置失败，使用默认值");
            }
            return serverConfig;
        }

        private static bool LogIn(TcpClient client, int id, string password)
        {
            bool result = false;
            try
            {
                NetworkStream stream = client.GetStream();
                while()
            }
        }
    }
}
