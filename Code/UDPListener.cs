using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MNG.Hubs;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MNG
{
    public class UDPListener
    {
        private const int listenPort = 9995;

        private IHubContext<LoggerHub> Hub { get; set; }

        public UDPListener(IHubContext<LoggerHub> hub)
        {
            Hub = hub;
            StartListening();
        }

        private async Task BroadcastNotification(string messages)
        {
            await Hub.Clients.All.SendAsync("notify",messages);
        }


        public void StartListening()
        {
            bool done = false;
            UdpClient listener = new UdpClient(listenPort);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
            string received_data;
            byte[] receive_byte_array;
            try
            {
                while (!done)
                {
                    Console.WriteLine("Waiting for broadcast");
                    receive_byte_array = listener.Receive(ref groupEP);
                    Console.WriteLine("Received a broadcast from {0}", groupEP.ToString());
                    received_data = Encoding.ASCII.GetString(receive_byte_array, 0, receive_byte_array.Length);
                    Console.WriteLine("data follows \n{0}\n\n", received_data);
                    BroadcastNotification(received_data).Wait();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            listener.Close();
        }
    }
}
