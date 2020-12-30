using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

using OpenCL.Net;

namespace CLRayTracer
{
    public partial class Form1 : Form
    {
        private RenderEngine _renderEngine;
        private bool _stopRendering = true;
        private Thread _renderThread;
        private Camera _camera;
        private double _deltaTime;
        private Sphere[] _spheres;
        private Disk[] _disks;
        private Random _random = new Random();

        public Form1()
        {
            InitializeComponent();

            //Ensure labels are drawn over picture box
            fpsLabel.Parent = renderedScene;
            fpsLabel.Location = new Point(5, 10);

            timePerFrameLabel.Parent = renderedScene;
            timePerFrameLabel.Location = new Point(5, 25);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _renderEngine = new RenderEngine(renderedScene.Width, renderedScene.Height);

            //Set exit condition for rendering
            FormClosed += new FormClosedEventHandler(delegate (object o, FormClosedEventArgs a)
            {
                _stopRendering = true;
            });

            GenSpheres();

            //Gen Disks
            _disks = new[] { new Disk(40.0f, 0.5f, new float3(0.0f, 0.0f, 0.0f), new float3(0.0f, 1.0f, 0.0f), new float3(1.0f, 1.0f, 0.0f)) };

            _camera = new Camera(new float3(0, 10, 25), new float3(0, 0, 0), 90, 5);
        }

        /// <summary>
        /// Generates a list of random spheres placed on the start disk
        /// </summary>
        private void GenSpheres()
        {
            //Gen spheres
            List<Sphere> spheres = new List<Sphere>();
            spheres.Add(new Sphere(2.5f, 1.0f, 0.0f, new float3(-2.8f, 2.5f, 0), new float3(1.0f, 0.0f, 0.0f)));
            spheres.Add(new Sphere(2.5f, 1.0f, 0.0f, new float3(2.8f, 2.5f, 0), new float3(0.0f, 1.0f, 0.0f)));
            for (int i = 0; i < 100; i++)
            {
                if (i == 0)
                {
                    spheres.Add(GenSphere());
                    continue;
                }

                while (true)
                {
                    Sphere sphere = GenSphere();

                    bool busted = false;
                    for (int j = 0; j < spheres.Count; j++)
                    {
                        float dx = spheres[j].pos.x - sphere.pos.x;
                        float dy = spheres[j].pos.z - sphere.pos.z;
                        float dist = (float)Math.Sqrt((dx * dx) + (dy * dy));
                        float rad = (spheres[j].radius + sphere.radius);
                        if (dist < rad)
                        {
                            busted = true;
                            break;
                        }
                    }
                    if (busted == false)
                    {
                        spheres.Add(sphere);
                        break;
                    }
                }
            }
            _spheres = spheres.ToArray();
        }

        //Generates a random sphere
        private Sphere GenSphere()
        {
            float genRadius = 30;
            float angle = (float)_random.NextDouble() * 6.28f;

            float distFromCenter = (float)_random.NextDouble();
            float x = genRadius * distFromCenter * (float)Math.Cos(angle);
            float y = genRadius * distFromCenter * (float)Math.Sin(angle);

            float r = (float)_random.NextDouble();
            float g = (float)_random.NextDouble();
            float b = (float)_random.NextDouble();

            float radius = (float)_random.NextDouble() * 7;
            float reflectivity = (float)_random.NextDouble();

            float ior = 0;

            if (_random.NextDouble() < 0.5f)
            {
                ior = 1 + ((float)_random.NextDouble() * 0.3f);
            }

            return new Sphere(radius, reflectivity, ior, new float3(x, radius, y), new float3(r, g, b));
        }

        private void UpdateScene()
        {
            //Update speed based on user input
            float speed = (InputManager.Instance.IsKeyPressed(Keys.ControlKey) ? 5 : 15);
            float rotationSpeed = 1.1f;

            //Claculate camera movment
            float2 forward = new float2((float)Math.Cos(_camera.rotation.y - 1.5707f), (float)Math.Sin(_camera.rotation.y - 1.5707f));
            float2 right = new float2((float)Math.Cos(_camera.rotation.y), (float)Math.Sin(_camera.rotation.y));

            float dx = 0;
            float dz = 0;
            float dy = 0;

            if (InputManager.Instance.IsKeyPressed(Keys.W))
            {
                dx += forward.x * (float)_deltaTime * speed;
                dz += forward.y * (float)_deltaTime * speed;
            }
            if (InputManager.Instance.IsKeyPressed(Keys.S))
            {
                dx += -forward.x * (float)_deltaTime * speed;
                dz += -forward.y * (float)_deltaTime * speed;
            }
            if (InputManager.Instance.IsKeyPressed(Keys.A))
            {
                dx += -right.x * (float)_deltaTime * speed;
                dz += -right.y * (float)_deltaTime * speed;
            }
            if (InputManager.Instance.IsKeyPressed(Keys.D))
            {
                dx += right.x * (float)_deltaTime * speed;
                dz += right.y * (float)_deltaTime * speed;
            }
            if (InputManager.Instance.IsKeyPressed(Keys.ShiftKey))
            {
                dy += (float)_deltaTime * -speed;
            }
            if (InputManager.Instance.IsKeyPressed(Keys.Space))
            {
                dy += (float)_deltaTime * speed;
            }

            if (InputManager.Instance.IsKeyPressed(Keys.Q))
            {
                _camera.rotation = new float3(_camera.rotation.x, _camera.rotation.y - ((float)_deltaTime * rotationSpeed), _camera.rotation.z);
            }
            if (InputManager.Instance.IsKeyPressed(Keys.E))
            {
                _camera.rotation = new float3(_camera.rotation.x, _camera.rotation.y + ((float)_deltaTime * rotationSpeed), _camera.rotation.z);
            }

            _camera.position = new float3(_camera.position.x + dx, _camera.position.y + dy, _camera.position.z + dz);
        }

        /// <summary>
        /// Function used for rendering
        /// </summary>
        private void RenderLoop()
        {
            try
            {
                while (_stopRendering == false)
                {
                    //Update scene before rednering the image
                    UpdateScene();
                    //Render the image
                    _renderEngine.Width = renderedScene.Width;
                    _renderEngine.Height = renderedScene.Height;
                    Image frame = _renderEngine.renderImage(_camera, _spheres, _disks, out _deltaTime);

                    //Update UI
                    if (renderedScene.InvokeRequired)
                    {
                        renderedScene.Invoke(new MethodInvoker(delegate {
                            renderedScene.Image = frame;
                            fpsLabel.Text = "FPS: " + (1 / _deltaTime).ToString("##.00");
                            timePerFrameLabel.Text = "TPF: " + _deltaTime + " s";
                            cameraPosLabel.Text = _camera.position.ToString();
                            cameraRotationLabel.Text = (_camera.rotation).ToString();
                        }));
                    }
                }
            }
            catch (Exception e )
            {
                Console.WriteLine("Error in render thread! : " + e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (_stopRendering == false)
            {
                return;
            }
            _stopRendering = false;

            _renderThread = new Thread(RenderLoop);
            _renderThread.Start();
        }

        private void KeyDownCallback(object sender, KeyEventArgs args)
        {
            InputManager.Instance.ProcessKeyDown(args.KeyCode);
        }

        private void KeyUpCallback(object sender, KeyEventArgs args)
        {
            InputManager.Instance.ProcessKeyUp(args.KeyCode);
        }

        private void FovTrackBar_Scroll(object sender, EventArgs e)
        {
            _camera.fov = fovTrackBar.Value;
            fovValueLabel.Text = fovTrackBar.Value.ToString();
        }

        private void DepthTrackBar_Scroll(object sender, EventArgs e)
        {
            _camera.depth = depthTrackBar.Value;
            depthValueLabel.Text = depthTrackBar.Value.ToString();
        }

        private void PitchTrackBar_Scroll(object sender, EventArgs e)
        {
            _camera.rotation.x = pitchTrackBar.Value * 0.0174533f;
            pitchValueLabel.Text = pitchTrackBar.Value.ToString();
        }

        private void GenerateSpheresButton_Click(object sender, EventArgs e)
        {
            GenSpheres();
        }
    }
}
