using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Corex.Mini;

namespace DataTransmissionProgram
{
    public class DataTransmissionProgram
    {
        public bool IsTransmitting { get; set; }
        private static async Task Main(string[] args)
        {
            UdpClient DataTransmissionListener = new UdpClient(5010);
            // Listen to any IP. Change if necessary.
            IPEndPoint DataTransmissionEP = new IPEndPoint(IPAddress.Any, 0);
            try
            {
                //DataTransmissionProgram p = new DataTransmissionProgram() { IsTransmitting = true};
                //Task.Run(() => p.GetDataTransmissionAsync(DataTransmissionListener).ConfigureAwait(false));

                //Thread.Sleep(1000);
                //p.IsTransmitting = false;
                //Console.WriteLine("wow");
                //Console.ReadLine();
                while (true)
                {
                    Console.WriteLine("Waiting for broadcast");
                    byte[] bytes = DataTransmissionListener.Receive(ref DataTransmissionEP);
                    Console.WriteLine($"Received broadcast from {DataTransmissionEP} :");
                    Console.WriteLine(BitConverter.ToString(bytes));
                    //Console.WriteLine(BitConverter.ToInt16(bytes.Skip(2).ToArray(), 2));
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
                DataTransmissionListener.Close();
            }
        }
        public async Task GetDataTransmissionAsync(UdpClient DataTransmissionClient)
        {
            StringBuilder DataTransmissionLog = new StringBuilder();
            while (IsTransmitting)
            {
                UdpReceiveResult receiveBytes = await DataTransmissionClient.ReceiveAsync().ConfigureAwait(false);
                Console.WriteLine(BitConverter.ToInt16(receiveBytes.Buffer, 2));
            }
        }
    }
}
