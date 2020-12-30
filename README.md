# OpenCL Ray Tracer

This repo contains the code for a Raytracer written in C# using Windows Forms. Raytracing is highly parallelizable task so allowing the code to run on the GPU, using OpenCL, was a necessity for fast realtime results. The ray tracer supports backgrounds, spheres, disks, reflections, and refractions.

<img src="https://i.imgur.com/MirRgbT.png" alt="drawing"/>

## What is Ray Tracing

Ray tracing is a 3D rendering technique. Some alrternatives are rasterazation and ray marching. Ray tracing works by simulating individual light rays. Instead of originating the light rays at the light source, where they start their hourney in real world, the light rays start from camera. They then shoot out into the scene until they hit an object. If the object is reflective then that ray is reflected off of the surface and bounces again through the scene. If the object is refractive, think glass, then the ray is refracted through the object. The maximum number of bounces of light allowed is called the max depth.

With a depth of 1 you can see objects 

With a depth of 2 you can see reflections of objects 

With a depth of 3 you can see reflections of reflections of objects 

<img src="https://i.imgur.com/vFPRXtK.png" alt="drawing" width="500"/>
<img src="https://i.imgur.com/snGcC5M.png" alt="drawing" width="500"/>
<img src="https://i.imgur.com/fyDcyYl.png" alt="drawing" width="500"/>
<img src="https://i.imgur.com/71oleRr.png" alt="drawing" width="500"/>
<img src="https://i.imgur.com/s8t9RxR.png" alt="drawing" width="500"/>
<img src="https://i.imgur.com/QbZMhBt.png" alt="drawing" width="500"/>

## Lisence

Feel free to use the code whatever purposes you like.

Please note that the background photo is not mine.

Photo Credit: Sitoo <a href="http://www.flickr.com/photos/7470842@N04/49659142176">Serra del Cavall Bernat, Pollença, Mallorca</a> via <a href="http://photopin.com">photopin</a> <a href="https://creativecommons.org/licenses/by-nc-nd/2.0/">(license)</a>