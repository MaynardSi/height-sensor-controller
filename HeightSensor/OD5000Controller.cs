using HeightSensor.Utils;
using HeightSensor.Enums;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;
using System.Net.Http.Headers;
using System.Linq;

namespace HeightSensor
{
    public class OD5000Controller : IHeightSensorController
    {
        // TODO: What about Device IP and 
        public OD5000Controller(string IPAddress, )
        {
            
        }

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
        /// <summary>
        /// UDP client responsible for sending commands to a
        /// given port. Commands for reading and writing are
        /// transmitted to port 5011 (default) via the UDP.
        /// </summary>
        public UdpClient DataTransmissionClient { get; set; }

        /// <summary>
        /// IP address and port destination for receiving
        /// continuous data.
        /// </summary>
        public IPEndPoint DataTransmissionEndPoint { get; set; }
        public StringBuilder DataTransmissionLog { get; set; }
        public bool IsTransmitting { get; set; }

        /// <summary>
        /// UDP client responsible for receiving data packets
        /// of saved measurement data/recording.
        /// </summary>
        public UdpClient StorageDataReadingClient { get; set; }

        /// <summary>
        /// IP address and port destination for receiving saved
        /// measurement/recording data packets.
        /// </summary>
        public IPEndPoint StorageDataReadingEndPoint { get; set; }

        public IPAddress DeviceIPAddress { get; set; }
        public IPAddress HostIPAddress { get; set; }

        /// <summary>
        /// Throws an exception if bytes received contain a command error, 
        /// address error, or overflow error.
        /// </summary>
        /// <param name="receivedBytes">The bytes received.</param>
        /// <returns></returns>
        public bool IsDeviceResponseValid(byte[] receivedBytes)
        {
            if (ByteUtils.ByteArrayContains(receivedBytes, ByteResponseEnum.COMMAND_ERROR))
            {
                throw new Exception(); // TODO: Throw command error exception
            }
            if (ByteUtils.ByteArrayContains(receivedBytes, ByteResponseEnum.ADDRESS_ERROR))
            {
                throw new Exception(); // TODO: Throw address error exception
            }
            if (ByteUtils.ByteArrayContains(receivedBytes, ByteResponseEnum.OVERFLOW_ERROR))
            {
                throw new Exception(); // TODO: Throw overflow error exception
            }
            return true;
        }

        /// <summary>
        /// Sends the command at the designated Control EndPoint.
        /// Receives a response from the device at the same EndPoint when possible.
        /// </summary>
        /// <param name="command">The command to send.</param>
        public void SendCommand(byte[] command)
        {
            try
            {
                ControlClient.Send(command, command.Length);
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);     // TODO: Maybe change to read to defined source
                byte[] receivedBytes = ControlClient.Receive(ref RemoteIpEndPoint); // Maybe add timeout here (await/async)
                IsDeviceResponseValid(receivedBytes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Controller Interface Methods
        public bool Startup()
        {
            try
            {
                ControlClient = new UdpClient();
                DataTransmissionClient = new UdpClient();
                StorageDataReadingClient = new UdpClient();

                DataTransmissionListener(DataTransmissionClient);
                StorageDataReadingListener(StorageDataReadingClient);

                ControlClient.Connect(ControlEndPoint);
                DataTransmissionClient.Connect(DataTransmissionEndPoint);
                StorageDataReadingClient.Connect(StorageDataReadingEndPoint);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Shutdown()
        {
            try
            {
                ControlClient.Close();
                DataTransmissionClient.Close();
                StorageDataReadingClient.Close();
                // TODO: Add task cancellation

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

        /// <summary>
        /// Retruns the sensor's measured value in milimeters.
        /// </summary>
        public void ReadHeight()
        {
            SendCommand(ByteCommandEnum.SingleMeasure);
        }

        #region Data Transmission
        public void StartDataTransmission()
        {
            Task.Run(startDataTransmission);
            SendCommand(ByteCommandEnum.SetDataPushOn);

            void startDataTransmission()
            {
                DataTransmissionLog = new StringBuilder();
                IPEndPoint ep = DataTransmissionEndPoint;
                while (IsTransmitting)
                {
                    byte[] receivedBytes = DataTransmissionClient.Receive(ref ep);
                    DataTransmissionLog.Append();
                }
            }
        }
        public void StopDataTransmission()
        {
            SendCommand(ByteCommandEnum.SetDataPushOff);
            IsTransmitting = false;
        }

        public void SetDataTransmissionIP(string ipAddressString)
        {
            byte[] Command = ByteUtils.ConcatByteArray(ByteCommandEnum.SetDataPushIP,
                IPAddress.Parse(ipAddressString).GetAddressBytes());
            SendCommand(Command);
        }
        #endregion
        #region Storage Data Reading
        public void StartStorageDataReading()
        {
            throw new NotImplementedException();
        }

        public void StopStorageDataReading()
        {
            throw new NotImplementedException();
        }

        public void SetStorageDataReadingIP(string ipAddressString)
        {
            byte[] Command = ByteUtils.ConcatByteArray(ByteCommandEnum.SetRecordingIP,
                IPAddress.Parse(ipAddressString).GetAddressBytes());
            SendCommand(Command);
        }
        #endregion
        #endregion
    }
}