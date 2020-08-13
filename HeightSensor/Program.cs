using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            OD5000Controller C30T05Controller = new OD5000Controller
            {
                ControlClient = new UdpClient(),
                ControlEndPoint = new IPEndPoint(IPAddress.Parse("169.254.137.141"), 5011),
                DataTransmissionClient = new UdpClient(),
                DataTransmissionEndPoint = new IPEndPoint(IPAddress.Any, 0) //Any
            };
        }
    }
}