using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace HeightSensor
{
    public class OD5000Controller : IHeightSensorController
    {
        /// <summary>
        /// UDP client responsible for sending commands to a
        /// given port. Commands for reading and writing are
        /// transmitted to port 5011 (default) via the UDP.
        /// </summary>
        public UdpClient DataTransmissionClient { get; set; }

        /// <summary>
        /// IP address and port destination for receiving
        /// continuous data.
        /// Default IP for receiving process data: 192.168.0.200
        /// Default Por for receving process data: 5010
        /// </summary>
        public IPEndPoint DataTransmissionEndPoint { get; set; }

        /// <summary>
        /// UDP client that listens for traffic on a given
        /// port. The device permits measurement data to be
        /// continually obtained via the Ethernet User Datagram
        /// Protocol (UDP to port 5010 (default)).
        /// </summary>
        public UdpClient ControlClient { get; set; }

        /// <summary>
        /// IP address and port destination corresponding to the
        /// height sensor/device.
        /// Default Device IP Address: 192.168.0.100
        /// Default Port: 5011
        /// </summary>
        public IPEndPoint ControlEndPoint { get; set; }

        #region Controller Interface Methods
        public bool Startup()
        {
            try
            {
                // TODO: Reevaluate. Connect call since UDP is a connectionless protocol.
                // You can specify the destination address when you call Send.
                DataTransmissionClient.Connect(DataTransmissionEndPoint);
                // TODO: Maybe consider only starting up
                ControlClient.Connect(ControlEndPoint);
                return true;
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
        }

        public bool Shutdown()
        {
            try
            {
                DataTransmissionClient.Close();
                ControlClient.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
        }

        public bool Reset()
        {
            try
            {
                return Shutdown() && Startup();
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
        }
        #endregion

        #region Height Sensor Interface Methods
        public int ReadHeight()
        {
            try
            {
                ControlClient.Send(ByteCommandEnum.SINGLE_MEASURE, ByteCommandEnum.SINGLE_MEASURE.Length);
                //IPEndPoint object will allow us to read datagrams sent from any source.
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

                // Blocks until a message returns on this socket from a remote host.
                byte[] receivedBytes = ControlClient.Receive(ref RemoteIpEndPoint);
                if (receivedBytes.SequenceEqual(ByteResponseEnum.COMMAND_ERROR))
                {
                    throw new Exception();
                }
                if (receivedBytes.SequenceEqual(ByteResponseEnum.ADDRESS_ERROR))
                {
                    throw new Exception();
                }
                if (receivedBytes.SequenceEqual(ByteResponseEnum.OVERFLOW_ERROR))
                {
                    throw new Exception();
                }
                #region Test
                Debug.WriteLine("This is the message you received " +
                                             BitConverter.ToString(receivedBytes));
                Debug.WriteLine("This message was sent from " +
                                            RemoteIpEndPoint.Address.ToString() +
                                            " on their port number " +
                                            RemoteIpEndPoint.Port.ToString());
                #endregion
                return BitConverter.ToInt32(receivedBytes, 1); //maybe 2
            }
            catch (Exception)
            {
                // Robot must catch this exception.
                throw;
            }
        }

        public bool StartBufferRecord()
        {
            throw new NotImplementedException();
        }
        public bool StopBufferRecord()
        {
            throw new NotImplementedException();
        }

        public bool StartMeasure()
        {
            throw new NotImplementedException();
        }

        public bool StopMeasure()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}