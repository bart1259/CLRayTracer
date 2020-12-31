using OpenCL.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CLRayTracer
{
    /// <summary>
    /// Struct representing a camera
    /// </summary>

    struct Camera
    {
        public float fov;
        public int depth;
        public float dummy0;
        public float dummy1;
        public float4 position;
        public float4 rotation;

        public Camera(float3 position, float3 rotation, float fov, int depth)
        {
            this.position = new float4(position.x, position.y, position.z, 1.0f);
            this.rotation = new float4(rotation.x, rotation.y, rotation.z, 1.0f);
            this.fov = fov;
            this.depth = depth;
            dummy0 = 0;
            dummy1 = 0;
        }
    }
}
