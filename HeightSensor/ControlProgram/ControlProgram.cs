using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ControlProgram
{
    public class ControlProgram
    {
        static UdpClient ControlClient = new UdpClient();
        static IPAddress DeviceIP = IPAddress.Parse("169.254.156.20");
        public static void Main(string[] args)
        {
            
            try
            {
                // Maybe can be omitted since UDP is connectionless (check UdpClient Send function).
                ControlClient.Connect(DeviceIP, 5011);

                byte[] laserOff = new byte[] { 0x40, 0x02, 0x0C, 0x98, 0x00, 0x04, 0x00, 0x00, 0x00, 0x01 };
                byte[] laserOn = new byte[] { 0x40, 0x02, 0x0C, 0x98, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00 };
                
                

                byte[] setDataPushIP = { 0x40, 0x02, 0x10, 0xE4, 0x00, 0x04, 0xA9, 0xFE, 0x30, 0xC1 };
                byte[] setDataPushPort = { 0x40, 0x02, 0x10, 0xE0, 0x00, 0x08, 0x00, 0x00, 0x13, 0x92, 0x00, 0x00 };
                byte[] setDataPushChannelToA = { 0x40, 0x02, 0x0C, 0xE0, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00 };
                byte[] setDataPushOff = new byte[] { 0x40, 0x02, 0x01, 0xE0, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00 };
                byte[] setDataPushOn = new byte[] { 0x40, 0x02, 0x01, 0xE0, 0x00, 0x04, 0x00, 0x00, 0x00, 0x01 };

                byte[] setRecordingTriggerChannelToA = new byte[] { 0x40, 0x02, 0x0C, 0xE0, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00 };
                byte[] setRecordingIP = new byte[] { 0x40, 0x02, 0x00, 0x70, 0x00, 0x04, 0xA9, 0xFE, 0x30, 0xC1 };  
                byte[] setRecordingPort = new byte[] { 0x40, 0x02, 0x00, 0x74, 0x00, 0x04, 0x00, 0x00, 0x13, 0x90 }; // Port 5008.
                byte[] setRecordingInterval = new byte[] { 0x40, 0x02, 0x00, 0x78, 0x00, 0x04, 0x00, 0x00, 0x00, 0x02 };
                byte[] setRecordingStartCondtionImmediate = new byte[] { 0x40, 0x02, 0x0C, 0xE0, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00 };
                byte[] setRecordingStartCondtionAlarmOff1 = new byte[] { 0x40, 0x02, 0x0C, 0xE0, 0x00, 0x04, 0x00, 0x00, 0x00, 0x01 };

                byte[] startRecord = new byte[] { 0x40, 0x02, 0x0C, 0xF0, 0x00, 0x04, 0x00, 0x00, 0x00, 0x01 };
                byte[] stopRecord = new byte[] { 0x40, 0x02, 0x0C, 0xF0, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00 };

                byte[] printRecordingIP = new byte[] { 0x30, 0x06, 0x00, 0x00, 0x0C, 0xF0, 0x00, 0x08 };
                byte[] printRecordingPort = new byte[] { 0x30, 0x02, 0x00, 0x74 };

                byte[] startReadStorageData = new byte[] { 0x40, 0x02, 0x0C, 0xF8, 0x00, 0x04, 0x00, 0x00, 0x00, 0x01 };
                byte[] stopReadStorageData = new byte[] { 0x40, 0x02, 0x0C, 0xF8, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00 };
                byte[] deleteStorageData = new byte[] { 0x40, 0x02, 0x00, 0x4C, 0x00, 0x04, 0x00, 0x00, 0x02, 0x4E };

                byte[] printFileSystemCapacity = new byte[] { 0x30, 0x02, 0x01, 0x14 };
                byte[] printSystemFreeSpace = new byte[] { 0x30, 0x02, 0x01, 0x10 };
                byte[] printRecordingState = new byte[] { 0x30, 0x06, 0x00, 0x00, 0x0C, 0xC8, 0x00, 0x00 };
                byte[] printRecordingCount = new byte[] { 0x30, 0x02, 0x0C, 0xC4 };     // Storage count


                //Console.WriteLine("Start buffer record...");
                //ControlClient.Send(startBufferRecordCommand, startBufferRecordCommand.Length);
                //ReceiveDatagram();

                //Console.WriteLine("Stopping data push...");
                //ControlClient.Send(dataPushOffCommand, dataPushOffCommand.Length);
                //ReceiveDatagram();
                //Thread.Sleep(5000);

                //Console.WriteLine("Stop buffer record...");
                //ControlClient.Send(stopBufferRecordCommand, stopBufferRecordCommand.Length);
                //ReceiveDatagram();

                //Console.WriteLine("Setting Recording Trigger Channel to Channel A...");
                //ControlClient.Send(setRecordingTriggerChannel, setRecordingTriggerChannel.Length);
                //ReceiveDatagram();
                //Thread.Sleep(5000);

                //Console.WriteLine("Starting data push...");
                //ControlClient.Send(dataPushOnCommand, dataPushOnCommand.Length);
                //ReceiveDatagram();
                //Thread.Sleep(5000);

                //Console.WriteLine("Stopping data push...");
                //ControlClient.Send(dataPushOffCommand, dataPushOffCommand.Length);
                //ReceiveDatagram();
                //Thread.Sleep(5000);

                //Console.WriteLine("Starting data push...");
                //ControlClient.Send(dataPushOnCommand, dataPushOnCommand.Length);
                //ReceiveDatagram();
                //Console.ReadLine();

                //Console.WriteLine("Start buffer record...");
                //ControlClient.Send(startBufferRecordCommand, startBufferRecordCommand.Length);
                //ReceiveDatagram();
                //Thread.Sleep(5000);

                //Console.WriteLine("Laser off...");
                //ControlClient.Send(laserOffCommand, laserOffCommand.Length);
                //ReceiveDatagram();
                //Thread.Sleep(5000);

                //Console.WriteLine("Laser on...");
                //ControlClient.Send(laserOnCommand, laserOnCommand.Length);
                //ReceiveDatagram();
                //Thread.Sleep(5000);

                //Console.WriteLine("Stop buffer record...");
                //ControlClient.Send(stopBufferRecordCommand, stopBufferRecordCommand.Length);
                //ReceiveDatagram();

                #region Data Push
                //Console.WriteLine("Setting data push IP...");
                //ControlClient.Send(setDataPushIP, setDataPushIP.Length);
                //ReceiveDatagram();
                //Thread.Sleep(5000);

                //Console.WriteLine("Setting data push port...");
                //ControlClient.Send(setDataPushPort, setDataPushPort.Length);
                //ReceiveDatagram();
                //Thread.Sleep(5000);

                //Console.WriteLine("Setting data push channel to Channel A...");
                //ControlClient.Send(setDataPushChannelToA, setDataPushChannelToA.Length);
                //ReceiveDatagram();
                //Thread.Sleep(5000);

                //Console.WriteLine("Starting data push...");
                //ControlClient.Send(setDataPushOn, setDataPushOn.Length);
                //ReceiveDatagram();
                //Thread.Sleep(5000);
                //Console.ReadLine();

                //Console.WriteLine("Stopping data push...");
                //ControlClient.Send(setDataPushOff, setDataPushOff.Length);
                //ReceiveDatagram();
                //Console.ReadLine();
                #endregion

                #region Recording

                //Console.WriteLine("Setting Recording IP...");
                //ControlClient.Send(setRecordingIP, setRecordingIP.Length);
                //ReceiveDatagram();
                //Thread.Sleep(3000);

                //Console.WriteLine("Printing Recording IP...");
                //ControlClient.Send(printRecordingIP, printRecordingIP.Length);
                //ReceiveDatagram();
                //Thread.Sleep(3000);

                //Console.WriteLine("Setting Recording Trigger Channel to Channel A...");
                //ControlClient.Send(setRecordingTriggerChannelToA, setRecordingTriggerChannelToA.Length);
                //ReceiveDatagram();
                //Thread.Sleep(2000);

                //Console.WriteLine("Setting Recording IP...");
                //ControlClient.Send(setRecordingIP, setRecordingIP.Length);
                //ReceiveDatagram();
                //Thread.Sleep(3000);

                //Console.WriteLine("Setting Recording Port...");
                //ControlClient.Send(setRecordingPort, setRecordingPort.Length);
                //ReceiveDatagram();
                //Thread.Sleep(3000);

                //Console.WriteLine("Set Recording Start Condition Immediate:");
                //ControlClient.Send(setRecordingStartCondtionImmediate, setRecordingStartCondtionImmediate.Length);
                //ReceiveDatagram();
                //Thread.Sleep(3000);

                //Console.WriteLine("Setting Interval...");
                //ControlClient.Send(setRecordingInterval, setRecordingInterval.Length);
                //ReceiveDatagram();
                //Thread.Sleep(3000);
                ///
                //Console.WriteLine("Deleting all Recordings...");
                //ControlClient.Send(deleteStorageData, deleteStorageData.Length);
                //ReceiveDatagram();
                //Thread.Sleep(3000);

                Console.WriteLine("Start Recording...");
                ControlClient.Send(startRecord, startRecord.Length);
                ReceiveDatagram();
                Thread.Sleep(20000);

                //Console.WriteLine("Stop Recording...");
                //ControlClient.Send(stopRecording, stopRecording.Length);
                //ReceiveDatagram();
                //Thread.Sleep(2000);

                Console.WriteLine("Start Read data...");
                ControlClient.Send(startReadStorageData, startReadStorageData.Length);
                ReceiveDatagram();
                Thread.Sleep(2000);

                ////Console.WriteLine("Stop Read data...");
                ////ControlClient.Send(stopReadStorageData, stopReadStorageData.Length);
                ////ReceiveDatagram();

                //Console.WriteLine("File system capacity:");
                //ControlClient.Send(printFileSystemCapacity, printFileSystemCapacity.Length);
                //ReceiveDatagram();

                //Console.WriteLine("Recording Count:");
                //ControlClient.Send(printRecordingCount, printRecordingCount.Length);
                //ReceiveDatagram();

                //Console.WriteLine("Recording State:");
                //ControlClient.Send(printRecordingState, printRecordingState.Length);
                //ReceiveDatagram();

                //Console.WriteLine("System free space:");
                //ControlClient.Send(printSystemFreeSpace, printSystemFreeSpace.Length);
                //ReceiveDatagram();



                Console.ReadLine();
                #endregion

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

        public static void ReceiveDatagram()
        {
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

            #endregion Receive datagram
        }

    }
}
