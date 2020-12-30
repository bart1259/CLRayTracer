using OpenCL.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLRayTracer
{
    class GPUBuffer<T> : IDisposable where T:struct
    {

        /// <summary>
        /// Returns the memory handle of the buffer
        /// </summary>
        public IMem BufferHandle
        {
            get
            {
                return _buffer;
            }
        }

        private IMem<T> _buffer;
        private GPUHandle _handle;

        /// <summary>
        /// Creates a new GPU Buffer with data
        /// </summary>
        /// <param name="handle">What GPU Instance to create it on</param>
        /// <param name="flags">What Read/Write flags to use</param>
        /// <param name="data">The data to store in the buffer</param>
        public GPUBuffer(GPUHandle handle, MemFlags flags, T[] data) : this(handle)
        {
            ErrorCode error;
            _buffer = Cl.CreateBuffer<T>(handle.Context, flags, data, out error);
            CLException.CheckException(error);
        }

        /// <summary>
        /// Creates a new GPU Buffer or predefined size
        /// </summary>
        /// <param name="handle">What GPU Instance to create it on</param>
        /// <param name="flags">What Read/Write flags to use</param>
        /// <param name="size">Size of memory of the buffer</param>
        public GPUBuffer(GPUHandle handle, MemFlags flags, int size) : this(handle)
        {
            ErrorCode error;
            _buffer = Cl.CreateBuffer<T>(handle.Context, flags, size, out error);
            CLException.CheckException(error);
        }

        private GPUBuffer(GPUHandle handle)
        {
            _handle = handle;
        }

        /// <summary>
        /// Write data to GPU Buffer
        /// </summary>
        /// <param name="data">array of data to write to buffer</param>
        public void WriteData(T[] data)
        {
            Event e;
            Cl.EnqueueWriteBuffer(_handle.Queue, _buffer, Bool.True, data, 0, null, out e);
        }

        /// <summary>
        /// Reads data from GPU Buffer
        /// </summary>
        /// <param name="data">where to store the read data</param>
        public void ReadData(T[] data)
        {
            Event e;
            Cl.EnqueueReadBuffer(_handle.Queue, _buffer, Bool.True, 0, data.Length, data, 0, null, out e);
        }

        /// <summary>
        /// Clean up memory
        /// </summary>
        public void Dispose()
        {
            Cl.ReleaseMemObject(_buffer);
        }
    }
}
