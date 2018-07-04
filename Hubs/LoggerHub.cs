using System;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MNG;

namespace MNG.Hubs
{
    public class LoggerHub : Hub
    {
        private readonly UDPListener _udplistener;

        public LoggerHub(UDPListener udplistener)
        {
            _udplistener = udplistener;
        }

        public string GetLogs()
        {
            return DateTime.Now.ToShortDateString();
        }
    }
}