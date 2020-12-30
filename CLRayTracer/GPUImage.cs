using OpenCL.Net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CLRayTracer
{
    class GPUImage : IDisposable
    {

        /// <summary>
        /// The image memory buffer handle
        /// </summary>
        public IMem ImageBufferHandle
        {
            get
            {
                return _image;
            }
        }

        private GPUHandle _handle;
        private IMem _image;

        public GPUImage(GPUHandle handle)
        {
            _handle = handle;
        }

        /// <summary>
        /// Writes an image to GPU memeory
        /// </summary>
        /// <param name="image">the image to write</param>
        /// <param name="memFlags">the memory flags</param>
        public void WriteImage(Bitmap image, MemFlags memFlags)
        {
            ErrorCode error;
            _image = Cl.CreateImage2D(_handle.Context, memFlags, new OpenCL.Net.ImageFormat(ChannelOrder.RGBA, ChannelType.Unorm_Int8),
                new IntPtr(image.Width), new IntPtr(image.Height), new IntPtr(0), new IntPtr(0), out error);
            CLException.CheckException(error);

            byte[] data = BmpToBytes(image);
            IntPtr[] originArray = new IntPtr[] { new IntPtr(0), new IntPtr(0), new IntPtr(0) };
            IntPtr[] sizeArray = new IntPtr[] { new IntPtr(image.Width), new IntPtr(image.Height), new IntPtr(1) };

            Event e;
            error = Cl.EnqueueWriteImage(_handle.Queue, _image, Bool.True,
                originArray, sizeArray, new IntPtr(0), new IntPtr(0),
                data, 0, null, out e);
            CLException.CheckException(error);

        }

        /// <summary>
        /// Converts a bitmap to an array of bytes
        /// </summary>
        /// <param name="bmp">The bitmap to convert to an byte array</param>
        /// <returns>the byte array representing the bitmap</returns>
        private static byte[] BmpToBytes(Bitmap bmp)
        {
            BitmapData bData = bmp.LockBits(new Rectangle(new Point(), bmp.Size),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            int byteCount = bData.Stride * (bmp.Height);

            byte[] data = new byte[byteCount];

            Marshal.Copy(bData.Scan0, data, 0, data.Length);

            bmp.UnlockBits(bData);

            //Swap A and B cahnnels
            for (int i = 0; i < data.Length/4; i++)
            {
                byte temp = data[(i * 4)];
                data[(i * 4)] = data[(i * 4) + 2];
                data[(i * 4) + 2] = temp;
            }

            return data;
        }


        /// <summary>
        /// Clean up memory on GPU
        /// </summary>
        public void Dispose()
        {
            Cl.ReleaseMemObject(_image);
        }
    }
}
