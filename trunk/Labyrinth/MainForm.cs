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

using Labyrinth.ModelImport;
using Labyrinth.OpenGLHelpers;

namespace Labyrinth
{
    public partial class MainForm : Form
    {
        /* OpenGL related variables */
        bool GLReady;
        bool[] KeysDown = new bool[256];
        Camera Cam;
        TextureCache TexCache;
        FPSMonitor FPSMonitor;
        QuickFontWrapper GLText;

        //TEMPORARY for testing
        int fragprog;
        ModelImport.I3DModel obj;

        public MainForm()
        {
            InitializeComponent();
            this.Text = Application.ProductName;

            Application.Idle += new EventHandler(Application_Idle);
        }//end constructor

        private void Application_Idle(object sender, EventArgs e)
        {
            if (GLReady)
            {
                Cam.KeyUpdate(KeysDown);
                glControl1.Invalidate();
            }//end if
        }//end method

        private void MainForm_Load(object sender, EventArgs e)
        {
            //ALL TEMPORARY until more classes and GUI is working
            //obj = new WavefrontObj(@"C:\Users\haddocdx\Downloads\Death_Mountain_2.obj");
            //obj = ProjectHandler.ProjectHelpers.OpenModelByExtension(@"E:\oot-obj\crash_maps\water\water.obj");
            obj = ProjectHandler.ProjectHelpers.OpenModelByExtension(@"C:\Users\Daniel\OoT Obj Testing\plane_grouped_modified-VTXHACK.obj");
            foreach (Common.Material mat in obj.Model.Materials) TexCache.Load(System.IO.Path.GetFileName(mat.TextureMap), mat.TextureMapImage);

            // shader test
            string pstring =
                "!!ARBfp1.0\n" +
                "\n" +
                "TEMP Tex0; TEMP CACombined;\n" +
                "\n" +
                "PARAM ColorMaterial = program.env[0];\n" +
                "ATTRIB ColorVertex = fragment.color;\n" +
                "\n" +
                "TEX Tex0, fragment.texcoord[0], texture[0], 2D;\n" +
                "\n" +
                "OUTPUT Output = result.color;\n" +
                "\n";

            pstring += "MUL CACombined, ColorMaterial, ColorVertex;\n";
            pstring += "MUL Output, Tex0, CACombined;\n";
            pstring += "END\n";

            byte[] bytes = Encoding.ASCII.GetBytes(pstring);
            IntPtr ptr = System.Runtime.InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0);
            GL.Arb.GenProgram(1, out fragprog);
            GL.Arb.BindProgram(AssemblyProgramTargetArb.FragmentProgram, fragprog);
            GL.Arb.ProgramString(AssemblyProgramTargetArb.FragmentProgram, ArbVertexProgram.ProgramFormatAsciiArb, bytes.Length, ptr);
        }//end method

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            TexCache.Dispose();
        }//end method

        private void glControl1_Load(object sender, EventArgs e)
        {
            /* OpenGL stuff */
            OpenGLHelpers.Initialization.SetDefaults();

            /* QuickFont stuff */
            GLText = new QuickFontWrapper(new Font("Verdana", 10.0f, FontStyle.Bold));

            /* Program stuff */
            Cam = new Camera();
            TexCache = new TextureCache();
            FPSMonitor = new FPSMonitor();

            /* Ready */
            GLReady = true;
        }//end method

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (!GLReady) return;

            RenderInit(((GLControl)sender).Width, ((GLControl)sender).Height);
            RenderScene();
            RenderText();

            ((GLControl)sender).SwapBuffers();
        }//end method

        private void RenderInit(int width, int height)
        {
            FPSMonitor.Update();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            OpenGLHelpers.Initialization.SetViewport(width, height);
            Cam.Position();
            GL.Scale(0.02, 0.02, 0.02);
        }//end method

        private void RenderScene()
        {
            // THESE NOTES ARE TEMPORARY
            //rendering test; very VERY barebones
            //---NOTE: write down lighting/material/color setup somewhere, pretty good for the most part, HOWEVER:
            //---   transparency is only possible in DIFFUSE color, means per-surface AND per-vertex alpha CANNOT WORK like this
            //---   => this will likely need some fragment program for rendering
            //---   for now leaving at ambient from GL.Color and diffuse from GL.Material, for alpha from obj material (water and stuff)

            GL.PushAttrib(AttribMask.AllAttribBits);
            GL.Enable(EnableCap.ColorMaterial);

            #if NOSHADERS
                GL.ColorMaterial(MaterialFace.FrontAndBack, ColorMaterialParameter.Ambient);        //ambient color from GL.Color, everything else from GL.Material
            #endif

            int glid;
            foreach (Common.Group g in obj.Model.Groups)
            {
                foreach (Common.Polygon p in g.Polygons)
                {
                    #if NOSHADERS
                        // material tint as diffuse
                        GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, new float[] { (float)p.Material.Color.R, (float)p.Material.Color.G, (float)p.Material.Color.B, (float)p.Material.Color.A });
                    #else
                        GL.Enable((EnableCap)All.FragmentProgram);
                        GL.Arb.ProgramEnvParameter4(AssemblyProgramTargetArb.FragmentProgram, 0, p.Material.Color.R, p.Material.Color.G, p.Material.Color.B, p.Material.Color.A);
                    #endif

                    if (p.Material.TextureMap != string.Empty && (glid = TexCache[System.IO.Path.GetFileName(p.Material.TextureMap)]) != -1)
                    {
                        GL.Enable(EnableCap.Texture2D);
                        GL.BindTexture(TextureTarget.Texture2D, glid);
                    }//end if
                    else
                        GL.Disable(EnableCap.Texture2D);

                    GL.Begin(BeginMode.Triangles);

                    foreach (Common.Vertex v in p.Vertices)
                    {
                        // vertex color as ambient
                        GL.Color4(v.Color.R, v.Color.G, v.Color.B, v.Color.A);

                        GL.Normal3(v.Normals.X, v.Normals.Y, v.Normals.Z);
                        GL.TexCoord2(v.TextureCoordinates.X, v.TextureCoordinates.Y);
                        GL.Vertex3(v.Position.X, v.Position.Y, v.Position.Z);
                    }//end common.vertex foreach
                    GL.End();
                }//end common.polygon foreach
            }//end common.group foreach
            GL.PopAttrib();
        }//end method

        private void RenderText()
        {
            GLText.Begin();
            GLText.Print(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:0.00} FPS", FPSMonitor.Value), new Vector2d(10.0, 10.0));
            GLText.End();
        }//end method

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

        private void glControl1_MouseDown(object sender, MouseEventArgs e)
        {
            Cam.ButtonsDown |= e.Button;

            if (Convert.ToBoolean(Cam.ButtonsDown & System.Windows.Forms.MouseButtons.Left) == true)
                Cam.MouseCenter(new Vector2d(e.X, e.Y));
        }//end method

        private void glControl1_MouseUp(object sender, MouseEventArgs e)
        {
            Cam.ButtonsDown &= ~e.Button;
        }//end method

        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Convert.ToBoolean(e.Button & MouseButtons.Left) == true) Cam.MouseMove(new Vector2d(e.X, e.Y));
        }//end method

        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            KeysDown[e.KeyValue] = true;
        }//end method

        private void glControl1_KeyUp(object sender, KeyEventArgs e)
        {
            KeysDown[e.KeyValue] = false;
        }//end method
    }//end class
}//end constructor
