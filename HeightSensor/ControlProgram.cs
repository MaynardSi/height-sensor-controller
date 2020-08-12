using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace HeightSensor
{
    internal class ControlProgram
    {
        private const int ControlPort = 5011;

        // TODO: Do we need to fix conversion from bytes to readable characters?
        //       Is the format of the commands we are sending to the device wrong
        //
        private static void Main(string[] args)
        {
            UdpClient ControlClient = new UdpClient();
            // Change if necessary.
            IPAddress DeviceIP = IPAddress.Parse("169.254.137.141");
            try
            {
                // Maybe can be omitted since UDP is connectionless (check UdpClient Send function).
                ControlClient.Connect(DeviceIP, ControlPort);

                Console.WriteLine("Sending Get displacement from channel A command(short)");
                //byte[] getDisplacementCommand = new byte[] { 0x40, 0x02, 0x0C, 0xF8, 0x00, 0x04, 0x00, 0x00, 0x00, 0x01 };
                //byte[] getDisplacementCommand = new byte[] { 0x30, 0x02, 0x0D, 0x60 };
                byte[] getDisplacementCommand = new byte[] { 0x40, 0x02, 0x0C, 0x10, 0x00, 0x04, 0x00, 0x00, 0x00, 0x02 };
                //0x4002 0x0C10 0x0004 0x00000003

                ControlClient.Send(getDisplacementCommand, getDisplacementCommand.Length);

                // If what we read from the manual is correct, device responses should be sent
                // to port 5011. But uncomment this to check if client receives a response
                // from the device.

                #region Receive datagram

                //IPEndPoint object will allow us to read datagrams sent from any source.
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

                // Blocks until a message returns on this socket from a remote host.
                Byte[] receiveBytes = ControlClient.Receive(ref RemoteIpEndPoint);
                string returnData = Encoding.ASCII.GetString(receiveBytes);

                // Uses the IPEndPoint object to determine which of these two hosts responded.
                Console.WriteLine("This is the message you received " +
                                             BitConverter.ToString(receiveBytes));
                Console.WriteLine("This message was sent from " +
                                            RemoteIpEndPoint.Address.ToString() +
                                            " on their port number " +
                                            RemoteIpEndPoint.Port.ToString());
                Console.ReadLine();

                #endregion Receive datagram
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                ControlClient.Close();
            }
        }
    }
}