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
    /// Rerpesents a raytacable disk
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    struct Disk
    {
        public float radius;
        public float reflectivity;
        float dummy3;
        float dummy5;
        public float4 pos;
        public float4 normal;
        public float4 color;

        public Disk(float radius, float reflectivity, float3 pos, float3 normal, float3 color)
        {
            this.radius = radius;
            this.pos = new float4(pos.x, pos.y, pos.z, 0.0f);
            this.normal = new float4(normal.x, -normal.y, normal.z, 0.0f);
            this.color = new float4(color.x, color.y, color.z, 1.0f); ;
            this.reflectivity = reflectivity;
            dummy3 = 0;
            dummy5 = 0;
        }
    }
}
