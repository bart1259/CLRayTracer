namespace CLRayTracer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.renderedScene = new System.Windows.Forms.PictureBox();
            this.timePerFrameLabel = new System.Windows.Forms.Label();
            this.fpsLabel = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.fovTrackBar = new System.Windows.Forms.TrackBar();
            this.fovValueLabel = new System.Windows.Forms.Label();
            this.depthValueLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.depthTrackBar = new System.Windows.Forms.TrackBar();
            this.pitchTrackBar = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.pitchValueLabel = new System.Windows.Forms.Label();
            this.generateSpheresButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cameraPosLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cameraRotationLabel = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.renderedScene)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fovTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.depthTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pitchTrackBar)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // renderedScene
            // 
            this.renderedScene.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renderedScene.Location = new System.Drawing.Point(0, 0);
            this.renderedScene.Name = "renderedScene";
            this.renderedScene.Size = new System.Drawing.Size(879, 627);
            this.renderedScene.TabIndex = 1;
            this.renderedScene.TabStop = false;
            // 
            // timePerFrameLabel
            // 
            this.timePerFrameLabel.AutoSize = true;
            this.timePerFrameLabel.BackColor = System.Drawing.Color.Transparent;
            this.timePerFrameLabel.ForeColor = System.Drawing.Color.Lime;
            this.timePerFrameLabel.Location = new System.Drawing.Point(3, 24);
            this.timePerFrameLabel.Name = "timePerFrameLabel";
            this.timePerFrameLabel.Size = new System.Drawing.Size(80, 13);
            this.timePerFrameLabel.TabIndex = 2;
            this.timePerFrameLabel.Text = "Time per Frame";
            // 
            // fpsLabel
            // 
            this.fpsLabel.AutoSize = true;
            this.fpsLabel.BackColor = System.Drawing.Color.Transparent;
            this.fpsLabel.ForeColor = System.Drawing.Color.Lime;
            this.fpsLabel.Location = new System.Drawing.Point(3, 9);
            this.fpsLabel.Name = "fpsLabel";
            this.fpsLabel.Size = new System.Drawing.Size(33, 13);
            this.fpsLabel.TabIndex = 3;
            this.fpsLabel.Text = "FPS: ";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(-1, 573);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(299, 23);
            this.startButton.TabIndex = 4;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "FOV:";
            // 
            // fovTrackBar
            // 
            this.fovTrackBar.LargeChange = 25;
            this.fovTrackBar.Location = new System.Drawing.Point(3, 24);
            this.fovTrackBar.Maximum = 175;
            this.fovTrackBar.Minimum = 5;
            this.fovTrackBar.Name = "fovTrackBar";
            this.fovTrackBar.Size = new System.Drawing.Size(292, 45);
            this.fovTrackBar.SmallChange = 5;
            this.fovTrackBar.TabIndex = 6;
            this.fovTrackBar.TickFrequency = 25;
            this.fovTrackBar.Value = 90;
            this.fovTrackBar.Scroll += new System.EventHandler(this.FovTrackBar_Scroll);
            // 
            // fovValueLabel
            // 
            this.fovValueLabel.AutoSize = true;
            this.fovValueLabel.Location = new System.Drawing.Point(47, 8);
            this.fovValueLabel.Name = "fovValueLabel";
            this.fovValueLabel.Size = new System.Drawing.Size(19, 13);
            this.fovValueLabel.TabIndex = 7;
            this.fovValueLabel.Text = "90";
            // 
            // depthValueLabel
            // 
            this.depthValueLabel.AutoSize = true;
            this.depthValueLabel.Location = new System.Drawing.Point(53, 72);
            this.depthValueLabel.Name = "depthValueLabel";
            this.depthValueLabel.Size = new System.Drawing.Size(13, 13);
            this.depthValueLabel.TabIndex = 9;
            this.depthValueLabel.Text = "5";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Depth: ";
            // 
            // depthTrackBar
            // 
            this.depthTrackBar.Location = new System.Drawing.Point(6, 88);
            this.depthTrackBar.Maximum = 16;
            this.depthTrackBar.Name = "depthTrackBar";
            this.depthTrackBar.Size = new System.Drawing.Size(292, 45);
            this.depthTrackBar.TabIndex = 10;
            this.depthTrackBar.Value = 5;
            this.depthTrackBar.Scroll += new System.EventHandler(this.DepthTrackBar_Scroll);
            // 
            // pitchTrackBar
            // 
            this.pitchTrackBar.Location = new System.Drawing.Point(6, 152);
            this.pitchTrackBar.Maximum = 90;
            this.pitchTrackBar.Minimum = -90;
            this.pitchTrackBar.Name = "pitchTrackBar";
            this.pitchTrackBar.Size = new System.Drawing.Size(292, 45);
            this.pitchTrackBar.TabIndex = 11;
            this.pitchTrackBar.TickFrequency = 10;
            this.pitchTrackBar.Scroll += new System.EventHandler(this.PitchTrackBar_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Pitch:";
            // 
            // pitchValueLabel
            // 
            this.pitchValueLabel.AutoSize = true;
            this.pitchValueLabel.Location = new System.Drawing.Point(47, 136);
            this.pitchValueLabel.Name = "pitchValueLabel";
            this.pitchValueLabel.Size = new System.Drawing.Size(13, 13);
            this.pitchValueLabel.TabIndex = 13;
            this.pitchValueLabel.Text = "0";
            // 
            // generateSpheresButton
            // 
            this.generateSpheresButton.Location = new System.Drawing.Point(0, 544);
            this.generateSpheresButton.Name = "generateSpheresButton";
            this.generateSpheresButton.Size = new System.Drawing.Size(298, 23);
            this.generateSpheresButton.TabIndex = 14;
            this.generateSpheresButton.Text = "Randomize Spheres";
            this.generateSpheresButton.UseVisualStyleBackColor = true;
            this.generateSpheresButton.Click += new System.EventHandler(this.GenerateSpheresButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Camera Position:";
            // 
            // cameraPosLabel
            // 
            this.cameraPosLabel.AutoSize = true;
            this.cameraPosLabel.Location = new System.Drawing.Point(16, 217);
            this.cameraPosLabel.Name = "cameraPosLabel";
            this.cameraPosLabel.Size = new System.Drawing.Size(0, 13);
            this.cameraPosLabel.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 246);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Camera Rotation:";
            // 
            // cameraRotationLabel
            // 
            this.cameraRotationLabel.AutoSize = true;
            this.cameraRotationLabel.Location = new System.Drawing.Point(16, 259);
            this.cameraRotationLabel.Name = "cameraRotationLabel";
            this.cameraRotationLabel.Size = new System.Drawing.Size(0, 13);
            this.cameraRotationLabel.TabIndex = 18;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.richTextBox1.Enabled = false;
            this.richTextBox1.Location = new System.Drawing.Point(3, 275);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(292, 246);
            this.richTextBox1.TabIndex = 19;
            this.richTextBox1.Text = "Press Start to Start RayTracing\n\nTo move: WASD Shift + Space\nTo move faster hold " +
    "ctrl\nTo Rotate use Q & E";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.fovTrackBar);
            this.panel1.Controls.Add(this.richTextBox1);
            this.panel1.Controls.Add(this.startButton);
            this.panel1.Controls.Add(this.cameraRotationLabel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.fovValueLabel);
            this.panel1.Controls.Add(this.cameraPosLabel);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.depthValueLabel);
            this.panel1.Controls.Add(this.generateSpheresButton);
            this.panel1.Controls.Add(this.depthTrackBar);
            this.panel1.Controls.Add(this.pitchValueLabel);
            this.panel1.Controls.Add(this.pitchTrackBar);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(305, 627);
            this.panel1.TabIndex = 20;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.timePerFrameLabel);
            this.splitContainer1.Panel1.Controls.Add(this.fpsLabel);
            this.splitContainer1.Panel1.Controls.Add(this.renderedScene);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2MinSize = 305;
            this.splitContainer1.Size = new System.Drawing.Size(1188, 627);
            this.splitContainer1.SplitterDistance = 879;
            this.splitContainer1.TabIndex = 21;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 627);
            this.Controls.Add(this.splitContainer1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "OpenCL Raytracer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownCallback);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpCallback);
            ((System.ComponentModel.ISupportInitialize)(this.renderedScene)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fovTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.depthTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pitchTrackBar)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox renderedScene;
        private System.Windows.Forms.Label timePerFrameLabel;
        private System.Windows.Forms.Label fpsLabel;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar fovTrackBar;
        private System.Windows.Forms.Label fovValueLabel;
        private System.Windows.Forms.Label depthValueLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar depthTrackBar;
        private System.Windows.Forms.TrackBar pitchTrackBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label pitchValueLabel;
        private System.Windows.Forms.Button generateSpheresButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label cameraPosLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label cameraRotationLabel;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}

