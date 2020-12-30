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
    /// Represents a spgere that can be raytraced
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    struct Sphere
    {
        public float radius;
        public float reflectivity;
        public float ior;
        float dummy5;
        public float4 pos;
        public float4 color;

        public Sphere(float radius, float reflectivity, float ior, float3 pos, float3 color)
        {
            this.radius = radius;
            this.pos = new float4(pos.x, pos.y, pos.z, 0.0f);
            this.color = new float4(color.x, color.y, color.z, 1.0f); ;
            this.reflectivity = reflectivity;
            this.ior = ior;
            dummy5 = 0;
        }
    }
}
