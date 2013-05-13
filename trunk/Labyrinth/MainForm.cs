using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Labyrinth
{
    public partial class MainForm : Form
    {
        bool GLReady;


        //testing
        ModelImport.WavefrontObj obj;

        public MainForm()
        {
            InitializeComponent();
            this.Text = Application.ProductName;

            Application.Idle += new EventHandler(Application_Idle);

            //testing!
         //   ModelImport.Common.Vector4 tmp = new ModelImport.Common.Vector4(0.5, 0.5, 0.5, 0.5);
          //  obj = new ModelImport.WavefrontObj(@"C:\Users\haddocdx\Downloads\Death_Mountain_2.obj");
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            if (GLReady)
            {
                glControl1.Invalidate();
            }
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            GL.ClearColor(Color.LightBlue);
            //setup gl params

            GLReady = true;
        }

        private void glControl1_Resize(object sender, EventArgs e)
        {
            //resize viewport
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (!GLReady) return;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            ((GLControl)sender).SwapBuffers();
        }


        // File -> New opens this form which displays the new map types
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewProjectForm newProjForm = new NewProjectForm();
            newProjForm.createform();
            int projectType = newProjForm.returnProjectType;
            if (projectType == 0)
                createNewSingleRoomScene();
            else if (projectType == 1)
                createNewMultiRoomScene();
            else if (projectType == 2)
                createNewBlankProject();
            else
                MessageBox.Show("What happened? ProjectType should never be " + projectType.ToString(), "Caught an error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }//end method


        private void createNewSingleRoomScene()
        {
            
        }//end method


        private void createNewMultiRoomScene()
        {
            
        }//end method


        private void createNewBlankProject()
        {
            
        }//end method


    }
}
