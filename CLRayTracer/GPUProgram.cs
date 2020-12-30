using OpenCL.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLRayTracer
{
    class GPUProgram : IDisposable
    {

        private ErrorCode _error;
        private GPUHandle _handle;
        private Program _program;
        private Dictionary<string, Kernel> _kernels;

        /// <summary>
        /// Creates and builds a GPU Program from external file
        /// </summary>
        /// <param name="handle">handle to GPU</param>
        /// <param name="path">Path to external program</param>
        public GPUProgram(GPUHandle handle, string path) : this(handle, LoadProgramFromFile(path)) { }

        /// <summary>
        /// Creates and builds a GPU Program from source code
        /// </summary>
        /// <param name="handle">handle to GPU</param>
        /// <param name="sourceCode">source code of program</param>
        public GPUProgram(GPUHandle handle, string[] sourceCode) : this(handle)
        {
            CreateProgram(sourceCode);
        }

        /// <summary>
        /// Creates an empty GPU Program
        /// </summary>
        /// <param name="handle">handle to GPU</param>
        public GPUProgram(GPUHandle handle)
        {
            _handle = handle;

            _kernels = new Dictionary<string, Kernel>();
        }

        /// <summary>
        /// Creates the program from source code
        /// </summary>
        /// <param name="sourceCode">source code</param>
        public void CreateProgram(string[] sourceCode)
        {
            string source = "";
            for (int i = 0; i < sourceCode.Length; i++)
            {
                source += sourceCode[i];
            }

            _program = Cl.CreateProgramWithSource(_handle.Context, 1, 
                new[] { source }, new[] { new IntPtr(source.Length) }, out _error);
            CLException.CheckException(_error);
        }

        /// <summary>
        /// Builds Program 
        /// </summary>
        public void BuildProgram()
        {
            try
            {
                _error = Cl.BuildProgram(_program, 0, null, null, null, IntPtr.Zero);
                CLException.CheckException(_error);
            }
            catch (CLException)
            {
                Console.WriteLine("Could not build program!");
                InfoBuffer buildLog = Cl.GetProgramBuildInfo(_program, _handle.Device, ProgramBuildInfo.Log, out _error);
                CLException.CheckException(_error);
                Console.WriteLine("Build Log: " + buildLog.ToString());
            }
        }

        /// <summary>
        /// Creates a kernel from a program
        /// </summary>
        /// <param name="kernelName">the name of the kernel</param>
        public void CreateKernel(string kernelName)
        {
            Kernel kernel = Cl.CreateKernel(_program, kernelName, out _error);
            CLException.CheckException(_error);
            _kernels.Add(kernelName, kernel);
        }

        /// <summary>
        /// Creates all potential kernels in program 
        /// </summary>
        public void CreateAllKernels()
        {
            Kernel[] kernels = Cl.CreateKernelsInProgram(_program, out _error);
            CLException.CheckException(_error);
            for (int i = 0; i < kernels.Length; i++)
            {
                InfoBuffer name = Cl.GetKernelInfo(kernels[i], KernelInfo.FunctionName, out _error);
                _kernels.Add(name.ToString(), kernels[i]);
            }
        }

        /// <summary>
        /// Sets the argument of the kernel's
        /// </summary>
        /// <param name="kernel">Name of the kernel to set the paramter of</param>
        /// <param name="argIndex">Index of argument to set</param>
        /// <param name="value">Value of argument</param>
        public void SetKernelArg<T>(string kernel, uint argIndex, GPUBuffer<T> buffer) where T : struct
        {
            _error = Cl.SetKernelArg(_kernels[kernel], argIndex, buffer.BufferHandle);
            CLException.CheckException(_error);
        }

        /// <summary>
        /// Sets the argument of the kernel's
        /// </summary>
        /// <param name="kernel">Name of the kernel to set the paramter of</param>
        /// <param name="argIndex">Index of argument to set</param>
        /// <param name="value">Value of argument</param>
        public void SetKernelArg<T>(string kernel, uint argIndex, T value) where T:struct
        {
            _error = Cl.SetKernelArg(_kernels[kernel], argIndex, value);
            CLException.CheckException(_error);
        }

        /// <summary>
        /// Sets an image as a kernel argument
        /// </summary>
        /// <param name="kernel">Name of the kernel to set the image paratmeter of</param>
        /// <param name="argIndex">The argument index to set</param>
        /// <param name="image">The image to set the argument of</param>
        public void SetKernelArg(string kernel, uint argIndex, GPUImage image)
        {
            _error = Cl.SetKernelArg(_kernels[kernel], argIndex, image.ImageBufferHandle);
            CLException.CheckException(_error);
        }


        /// <summary>
        /// Executes a 1D kernel
        /// </summary>
        /// <param name="kernel">Name of the kernel to execute</param>
        /// <param name="globalWorkSize">Total Number of work items</param>
        /// <param name="localWorkSize">Number of work items per local group</param>
        public void ExecuteKernel1D(string kernel, int globalWorkSize, int localWorkSize)
        {
            Event e;
            _error = Cl.EnqueueNDRangeKernel(_handle.Queue, _kernels[kernel], 1, null, new[]{ new IntPtr(globalWorkSize)}, new[] { new IntPtr(localWorkSize) }, 0, null, out e);
            CLException.CheckException(_error);
        }

        public void Dispose()
        {
            foreach (var kernel in _kernels.Values)
            {
                Cl.ReleaseKernel(kernel);
            }
            _error = Cl.ReleaseProgram(_program);
            CLException.CheckException(_error);
        }

        /// <summary>
        /// Loads program source file as array of lines
        /// </summary>
        /// <param name="path">the path to load the program from</param>
        /// <returns>the lines of the program</returns>
        private static string[] LoadProgramFromFile(string path)
        {
            string[] lines = File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] += "\n";
            }
            return lines;
        }
    }
}
