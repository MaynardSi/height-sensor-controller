using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HeightSensor
{
    internal class Program
    {
        private const int ControlPort = 5011;
        private const int DataTransmissionPort = 5010;

        private static void Main(string[] args)
        {
            UdpClient ControlClient = new UdpClient();
            UdpClient DataTransmissionClient = new UdpClient();
        }
    }
}