struct Ray {
	float3 origin;
	float3 dir;
};

struct Sphere {
	float radius;
	float reflectivity;
	float ior;
	float3 pos;
	float3 color;
};

struct Disk {
	float radius;
	float reflectivity;
	float3 pos;
	float3 normal;
	float3 color;
};

struct Camera {
	float fov;
	int depth;
	float3 position;
	float3 rotation;
};


__constant float3 lightPosition = (float3)(-5.0f, 10.0f, 5.0f);
const float pi = 3.14159265358979f;
const sampler_t bgSampler = CLK_NORMALIZED_COORDS_TRUE | CLK_ADDRESS_REPEAT | CLK_FILTER_LINEAR;

//https://www.scratchapixel.com/lessons/3d-basic-rendering/minimal-ray-tracer-rendering-simple-shapes/ray-plane-and-ray-disk-intersection
float intersect_ray_plane(const struct Ray* ray, const struct Disk* disk, struct Ray* normal) 
{ 
    // assuming vectors are all normalized
    float denom = dot(disk->normal, ray->dir); 
    if (denom > 1e-6f) { 
        float3 p0l0 = disk->pos - ray->origin; 
        float t = dot(p0l0, disk->normal) / denom; 

		normal->origin = ray->origin + (t * ray->dir);
		normal->dir = disk->normal;

        return t; 
    } 
	if (denom < -1e-6f) { 
        float3 p0l0 = disk->pos - ray->origin; 
        float t = dot(p0l0, disk->normal) / denom; 

		normal->origin = ray->origin + (t * ray->dir);
		normal->dir = -disk->normal;

        return t; 
    } 
 
    return -1; 
} 

//https://www.scratchapixel.com/lessons/3d-basic-rendering/minimal-ray-tracer-rendering-simple-shapes/ray-plane-and-ray-disk-intersection
float intersect_ray_disk(const struct Ray* ray, const struct Disk* disk, struct Ray* normal) 
{ 
	float t = intersect_ray_plane(ray, disk, normal);
	if(t >= 0){
		float3 p = normal->origin;
		float3 v = p - disk->pos;
        float d2 = dot(v, v); 
		if(d2 <= disk->radius * disk->radius){
			return t;
		}
	}
 
    return -1; 
} 

//https://www.scratchapixel.com/lessons/3d-basic-rendering/minimal-ray-tracer-rendering-simple-shapes/ray-sphere-intersection
float intersect_ray_sphere(const struct Ray* ray, const struct Sphere* sphere, struct Ray* normal)
{
	float t0, t1; // solutions for t if the ray intersects 
	float radius2 = sphere->radius * sphere->radius;    

	// geometric solution
    float3 L = sphere->pos - ray->origin; 
    float tca = dot(L,ray->dir); 
    // if (tca < 0) return false;
    float d2 = dot(L,L) - (tca * tca); 
    if (d2 > radius2) return -1; 
    float thc = sqrt(radius2 - d2); 
    t0 = tca - thc; 
    t1 = tca + thc; 

    if (t0 > t1) {
		float temp = t0;
		t0 = t1;
		t1 = temp;
	} 
 
    if (t0 < 0) { 
        t0 = t1; // if t0 is negative, let's use t1 instead 
        if (t0 < 0) return -1; // both t0 and t1 are negative 
    } 
 
	normal->origin = ray->origin + (ray->dir * (t0));
	normal->dir = normalize(normal->origin - sphere->pos);

	return t0;
}


float2 getSphereUV(float3 dir){

	float u = 0.5f + (atan2(dir.x, dir.z) / (2 * pi));
	float v = 0.5f - (asin(dir.y) / pi);
	return (float2)(u, v);
}

float3 refractRay(float3 normal, float3 incident, float ior){

    float cosI = fabs(dot(normal, incident));
    float sinT2 = ior * ior * (1.0f - cosI * cosI);
    //if(sinT2 > 1.0f) return Vector::invalid; // TIR
    float cosT = sqrt(1.0f - sinT2);
    return normalize(ior * incident + (ior * cosI - cosT) * normal);
}

float4 traceScene(struct Ray* ray,
	 __constant struct Sphere* spheres,
	 int sphereCount,
	 __constant struct Disk* disks,
	 int diskCount,
	 image2d_t backgroundImage,
	 int maxDepth){

	struct Ray currentRay;
	struct Ray newRay = (*ray);
	
	float currentIOR = 1.0f;
	float residualIntensity = 1.0f;
	float totalIntensity = 0.0f;
	float4 color = (float4)(0.0f, 0.0f, 0.0f, 1.0f);

	for(int depth = 0; depth < maxDepth; depth++){
		
		currentRay = newRay;

		//Object that was hit's reflectivity
		float objectReflectivity = 0.0f;
		float4 objectColor = (float4)(0.0f, 0.0f, 1.0f, 1.0f);

		bool hit = false;
		float tn = 1e20f;

		//Ray trace spheres
		for(int i = 0; i < sphereCount; i++) {
			struct Sphere sphere = spheres[i];

			struct Ray normal;
			float ti = intersect_ray_sphere(&currentRay, &sphere, &normal);

			if (ti > 0) {
				if(ti < tn){
					//Ray hit a Sphere!
					hit = true;
					objectReflectivity = sphere.reflectivity;
					
					float brightness = dot(normal.dir, normalize(lightPosition - normal.origin));
					if (brightness < 0) {
						objectColor = (float4)(0.0f, 0.0f, 0.0f, 1.0f);
					} else {
						objectColor = (float4)(brightness * sphere.color, 1.0f);
					}
					
					if(sphere.ior <= 1){
						struct Ray reflectRay;
						reflectRay.origin = normal.origin;
						reflectRay.dir = normalize(currentRay.dir - (2 * dot(currentRay.dir, normal.dir) * normal.dir));
						reflectRay.origin += 0.001f * reflectRay.dir;
					
						newRay = reflectRay;
					} else {

						float objectIOR = dot(currentRay.dir, currentRay.origin - sphere.pos) > 0 ? sphere.ior : 1.0f / sphere.ior;
								
						struct Ray refractedRay;
						refractedRay.origin = normal.origin;
						refractedRay.dir = refractRay(normal.dir, currentRay.dir, objectIOR);
						refractedRay.origin += 0.001f * refractedRay.dir;

						newRay = refractedRay;
					
					}
					tn = ti;
				}
			}
		}

		for(int i = 0; i < diskCount; i++) {
			struct Disk disk = disks[i];

			struct Ray normal;
			float ti = intersect_ray_disk(&currentRay, &disk, &normal);

			if (ti > 0) {
				if(ti < tn){
					//Ray hit a Disk!
					hit = true;
					objectReflectivity = disk.reflectivity;
					
					float brightness = dot(normal.dir, normalize(lightPosition - normal.origin));
					if (brightness < 0) {
						objectColor = (float4)(0.0, 0.0, 0.0, 1.0f);
					} else {
						objectColor = (float4)(brightness * disk.color, 1.0f);
					}
					
					struct Ray reflectRay;
					reflectRay.origin = normal.origin;
					reflectRay.dir = normalize(currentRay.dir - (2 * dot(currentRay.dir, normal.dir) * normal.dir));
					reflectRay.origin += 0.001f * reflectRay.dir;

					newRay = reflectRay;
					tn = ti;
				}
			}
		}
		
		if(hit) {
			
			color += residualIntensity * objectColor;
			totalIntensity += residualIntensity;
			residualIntensity *= objectReflectivity;

		} else {
			color += residualIntensity * read_imagef(backgroundImage, bgSampler, getSphereUV(currentRay.dir));
			totalIntensity += residualIntensity;
			depth = 999;	
		}
	}
	return color / totalIntensity;

}

__kernel void render(int width,
	 int height,
	 __global float4* output,
	 __constant struct Sphere* spheres,
	 int sphereCount,
	 __constant struct Disk* disks,
	 int diskCount,
	 struct Camera camera,
	 image2d_t backgroundImage)
{
	//Get our global thread ID               
	const int id = get_global_id(0);

	//Calculate x and y coordinate of pixel
	int x = id % width;
	int y = id / width;

	const float fov = camera.fov;

	//Calculate aspect ratio
	float aspectRatio = (float)width / (float)height;

	float fieldOfView = tan(fov / 2 * pi / 180.0f);

	if (x < width && y < height) {
		//Calculate screen mapping, -0.5 being left and +0.5 being right
		float fx = aspectRatio * fieldOfView * (2 * ((x + 0.5f) / width) - 1);
		float fy = (1 - (2 * (y + 0.5f) / height)) * fieldOfView; 

		//Create camera ray
		struct Ray ray;
		ray.origin = camera.position;
		ray.dir = (float3)(fx, fy, -1);
		
		//Rotate ray
		float oldx = ray.dir.x;
		float oldy = ray.dir.y;
		float oldz = ray.dir.z;

		ray.dir.y = (oldy * cos(camera.rotation.x)) − (oldz * sin(camera.rotation.x));
		ray.dir.z = (oldy * sin(camera.rotation.x)) + (oldz * cos(camera.rotation.x));

		ray.dir = normalize(ray.dir);

		oldx = ray.dir.x;
		oldz = ray.dir.z;

		ray.dir.x = (oldx * cos(camera.rotation.y)) − (oldz * sin(camera.rotation.y));
		ray.dir.z = (oldx * sin(camera.rotation.y)) + (oldz * cos(camera.rotation.y));

		ray.dir = normalize(ray.dir);

		output[id] = traceScene(&ray, spheres, sphereCount, disks, diskCount, backgroundImage, camera.depth);
		
	}
}