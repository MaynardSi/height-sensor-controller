using System;
using System.Net;
using System.Net.Sockets;

namespace HeightSensor
{
    public class OD5000Controller : IHeightSensorController
    {
        public OD5000Controller(
            UdpClient _dataTransmission,
            IPEndPoint _dataTransmissionEndPoint,
            UdpClient _controlClient,
            IPEndPoint _controlEndPoint
            )
        {
            DataTransmissionClient = _dataTransmission;
            DataTransmissionEndPoint = _dataTransmissionEndPoint;
            ControlClient = _controlClient;
            ControlEndPoint = _controlEndPoint;
        }

        /// <summary>
        /// UDP client responsible for sending commands to a
        /// given port. Commands for reading and writing are
        /// transmitted to port 5011 (default) via the UDP.
        /// </summary>
        public UdpClient DataTransmissionClient { get; }

        public IPEndPoint DataTransmissionEndPoint { get; set; }

        /// <summary>
        /// UDP client that listens for traffic on a given
        /// port. The device permits measurement data to be
        /// continually obtained via the Ethernet User Datagram
        /// Protocol (UDP to port 5010 (default)).
        /// </summary>
        public UdpClient ControlClient { get; }

        public IPEndPoint ControlEndPoint { get; set; }

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
                throw;
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
                throw;
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
                throw;
            }
        }
    }
}