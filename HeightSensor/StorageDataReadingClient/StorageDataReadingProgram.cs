using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace StorageDataReadingClient
{
    public static class StorageDataReadingProgram
    {
        private static void Main(string[] args)
        {
            UdpClient StorageDataReadingClient = new UdpClient(5008);
            // Listen to any IP. Change if necessary.
            IPEndPoint StorageDataReadingEP = new IPEndPoint(IPAddress.Any, 5008);
            try
            {
                while (true)
                {
                    Console.WriteLine("Waiting for broadcast");
                    byte[] bytes = StorageDataReadingClient.Receive(ref StorageDataReadingEP);
                    Console.WriteLine($"Received broadcast from {StorageDataReadingEP} :");
                    Console.WriteLine(BitConverter.ToString(bytes));
                    //Console.WriteLine("Continue? (y/n)");
                    //var input = Console.ReadLine();
                    //if (input.Equals("n", StringComparison.OrdinalIgnoreCase))
                    //{
                    //    break;
                    //}
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                StorageDataReadingClient.Close();
            }
        }
    }
}
