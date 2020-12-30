using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using OpenCL.Net;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace CLRayTracer
{
    class RenderEngine
    {

        /// <summary>
        /// Width of image created by render engine
        /// </summary>
        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }

        /// <summary>
        /// Height of image created by render engine
        /// </summary>
        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        private GPUHandle _gpuHandle;
        private GPUProgram _renderProgram;

        private int _width;
        private int _height;

        private GPUBuffer<float4> _imgBuffer;
        private GPUBuffer<Sphere> _sphereBuffer;
        private GPUBuffer<Disk> _diskBuffer;
        private GPUImage _backgroundImage;

        private GCHandle _handle;

        public RenderEngine(int width, int height)
        {
            _width = width;
            _height = height;

            _gpuHandle = new GPUHandle();
            _gpuHandle.Init();

            _renderProgram = new GPUProgram(_gpuHandle, "renderer.cl");
            _renderProgram.BuildProgram();
            _renderProgram.CreateKernel("render");

            _renderProgram.SetKernelArg("render", 0, _width);
            _renderProgram.SetKernelArg("render", 1, _height);

            Bitmap bg = (Bitmap)Image.FromFile("background.jpg");
            _backgroundImage = new GPUImage(_gpuHandle);
            _backgroundImage.WriteImage(bg, MemFlags.ReadOnly);

            int totalPixels = _width * _height;
        }


        /// <summary>
        /// Renders a frame
        /// </summary>
        /// <param name="camera">Camera to render from</param>
        /// <param name="spheres">List of Spheres to render</param>
        /// <param name="disks">List of Disks to render</param>
        /// <param name="elapsedTime">Out variable of the elapsed time it took to render frame in seconds</param>
        /// <returns>The rendered frame</returns>
        public Image renderImage(Camera camera, Sphere[] spheres, Disk[] disks, out double elapsedTime)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            int localSize = 64;
            int totalPixels = _width * _height;
            int globalSize = ((int)Math.Ceiling((float)totalPixels / localSize)) * localSize;

            //Must be float4 because of padding
            float4[] pixels = new float4[totalPixels];
            _imgBuffer = new GPUBuffer<float4>(_gpuHandle, MemFlags.WriteOnly, totalPixels);
            _sphereBuffer = new GPUBuffer<Sphere>(_gpuHandle, MemFlags.ReadOnly | MemFlags.AllocHostPtr, spheres.Length);
            _diskBuffer = new GPUBuffer<Disk>(_gpuHandle, MemFlags.ReadOnly | MemFlags.AllocHostPtr, disks.Length);

            _sphereBuffer.WriteData(spheres);
            _diskBuffer.WriteData(disks);

            //Set Arguments
            _renderProgram.SetKernelArg("render", 0, _width);
            _renderProgram.SetKernelArg("render", 1, _height);
            _renderProgram.SetKernelArg("render", 2, _imgBuffer);
            _renderProgram.SetKernelArg("render", 3, _sphereBuffer);
            _renderProgram.SetKernelArg("render", 4, spheres.Length);
            _renderProgram.SetKernelArg("render", 5, _diskBuffer);
            _renderProgram.SetKernelArg("render", 6, disks.Length);
            _renderProgram.SetKernelArg("render", 7, camera);
            _renderProgram.SetKernelArg("render", 8, _backgroundImage);

            //Run Kernal
            _renderProgram.ExecuteKernel1D("render", globalSize, localSize);
            _gpuHandle.WaitUntilDone();

            //Read data
            _imgBuffer.ReadData(pixels);

            //Write data to bmp
            int[] pixelBytes = new int[_width * _height];
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    pixelBytes[(y * _width) + x] = ((255 << 24) | 
                        (byte)(pixels[(y * _width) + x].x * 255) << 16 |
                        (byte)(pixels[(y * _width) + x].y * 255) << 08 |
                        (byte)(pixels[(y * _width) + x].z * 255));
                }
            }

            if (_handle.IsAllocated)
            {
                _handle.Free();
            }

            _handle = GCHandle.Alloc(pixelBytes, GCHandleType.Pinned);
            Bitmap bmp = new Bitmap(_width, _height, _width * 4,
              PixelFormat.Format32bppArgb, _handle.AddrOfPinnedObject());


            _imgBuffer.Dispose();
            pixels = null;
            pixelBytes = null;

            //Compute end time
            stopwatch.Stop();
            elapsedTime = (stopwatch.ElapsedMilliseconds) / 1000.0;

            return bmp;
        }

    }
}
