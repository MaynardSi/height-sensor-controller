using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeightSensor
{
    /// <summary>
    /// Interface containing the basic functions of the device.
    /// </summary>
    public interface IController
    {
        bool Startup();

        bool Shutdown();

        bool Reset();
    }
}