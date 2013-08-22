using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ServerConfig
    {
        public ServerConfig()
        {
            IP = "127.0.0.1";
            Port = "12014";
        }
        public string IP { get; set; }
        public string Port { get; set; }
    }
}
