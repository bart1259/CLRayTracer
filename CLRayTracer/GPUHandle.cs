using OpenCL.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CLRayTracer
{

    /// <summary>
    /// A class represnting a handle to conenct with the GPU
    /// </summary>
    class GPUHandle : IDisposable
    {

        /// <summary>
        /// OpenCL Context
        /// </summary>
        public Context Context {
            get
            {
                return _context;
            }}

        /// <summary>
        /// OpenCL Device
        /// </summary>
        public Device Device {
            get
            {
                return _device;
            }}

        /// <summary>
        /// OpenCL Command Queue
        /// </summary>
        public CommandQueue Queue { get
            {
                return _queue;
            }}

        private ErrorCode _error;
        private CommandQueue _queue;
        private Device _device;
        private Context _context;

        /// <summary>
        /// Initializes handle
        /// </summary>
        /// <returns> whether or not the handle was intialized succesfully </returns>
        public void Init()
        {
            Platform[] platforms = Cl.GetPlatformIDs(out _error);
            CLException.CheckException(_error);

            Console.WriteLine("Found " + platforms.Length + " platform(s)");

            InfoBuffer platformNameBuffer = Cl.GetPlatformInfo(platforms[0], PlatformInfo.Name, out _error);
            CLException.CheckException(_error);
            Console.WriteLine(platformNameBuffer.ToString());

            Device[] devices = Cl.GetDeviceIDs(platforms[0], DeviceType.Gpu, out _error);
            CLException.CheckException(_error); 
            _device = devices[0];
            InfoBuffer deviceNameBuffer = Cl.GetDeviceInfo(Device, DeviceInfo.Platform, out _error);
            CLException.CheckException(_error);
            Console.WriteLine(deviceNameBuffer.ToString());

            _context = Cl.CreateContext(null, 1, new[] { devices[0] }, null, IntPtr.Zero, out _error);
            CLException.CheckException(_error);
            _queue = Cl.CreateCommandQueue(Context, Device, CommandQueueProperties.None, out _error);
            CLException.CheckException(_error);

        }


        /// <summary>
        /// Waits until the queue is empty
        /// </summary>
        public void WaitUntilDone()
        {
            _error = Cl.Finish(Queue);
            CLException.CheckException(_error);
        }

        /// <summary>
        /// Clean up memory on GPU
        /// </summary>
        public void Dispose()
        {
            _error = Cl.ReleaseCommandQueue(Queue);
            CLException.CheckException(_error);
        }
    }
}
