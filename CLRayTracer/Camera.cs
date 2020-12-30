using OpenCL.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLRayTracer
{
    /// <summary>
    /// Struct representing a camera
    /// </summary>
    struct Camera
    {
        public float3 position;
        public float dummy0;
        public float3 rotation;
        public float dummy1;
        public float fov;
        //Number of ray bounces
        public int depth;

        public Camera(float3 position, float3 rotation, float fov, int depth)
        {
            this.position = position;
            this.rotation = rotation;
            this.fov = fov;
            this.depth = depth;

            dummy0 = 0;
            dummy1 = 0;
        }
    }
}
