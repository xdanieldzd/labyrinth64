using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Labyrinth.OpenGLHelpers
{
    class Initialization
    {
        public static void SetDefaults()
        {
            GL.ShadeModel(ShadingModel.Smooth);
            GL.Enable(EnableCap.PointSmooth);
            GL.Hint(HintTarget.PointSmoothHint, HintMode.Nicest);

            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            GL.ClearColor(System.Drawing.Color.LightBlue);
            GL.ClearDepth(5.0f);

            GL.DepthFunc(DepthFunction.Lequal);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthMask(true);

            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            GL.Light(LightName.Light0, LightParameter.Ambient, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
            GL.Light(LightName.Light0, LightParameter.Diffuse, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
            GL.Light(LightName.Light0, LightParameter.Specular, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
            GL.Light(LightName.Light0, LightParameter.Position, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
            GL.Enable(EnableCap.Light0);

            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Normalize);

            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
        }//end method

        public static void SetViewport(int Width, int Height)
        {
            if (Width == 0 || Height == 0) 
                return;

            GL.Viewport(0, 0, Width, Height);

            double aspect = Width / (double)Height;
            Matrix4 PerspMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect, 0.1f, 15000);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.MultMatrix(ref PerspMatrix);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }//end method
    }//end class
}//end namespace
