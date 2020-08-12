using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace HeightSensor
{
    internal class DataTransmissionProgram
    {
        private const int DataTransmissionPort = 5008;

        private static void Main(string[] args)
        {
            UdpClient DataTransmissionListener = new UdpClient(DataTransmissionPort);
            // Listen to any IP. Change if necessary.
            IPEndPoint DataTransmissionEP = new IPEndPoint(IPAddress.Any, 0);
            try
            {
                while (true)
                {
                    Console.WriteLine("Waiting for broadcast");
                    byte[] bytes = DataTransmissionListener.Receive(ref DataTransmissionEP);

                    Console.WriteLine($"Received broadcast from {DataTransmissionEP} :");
                    Console.WriteLine($" {Encoding.ASCII.GetString(bytes, 0, bytes.Length)}");

                    Console.WriteLine("Continue? (y/n)");
                    var input = Console.ReadLine();
                    if (input.Equals("n", StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                DataTransmissionListener.Close();
            }
        }
    }
}