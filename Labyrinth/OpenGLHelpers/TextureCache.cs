using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Labyrinth.OpenGLHelpers
{
    public class TextureCache : IDisposable
    {
        Hashtable cachetbl;

        public TextureCache()
        {
            cachetbl = new Hashtable();
        }

        public void Dispose()
        {
            foreach (DictionaryEntry de in cachetbl)
            {
                if (GL.IsTexture((int)de.Value)) GL.DeleteTexture((int)de.Value);
            }

            cachetbl = null;
        }

        public int this[string Name]
        {
            get { return (int)cachetbl[Name]; }
        }

        public void Load(string Name, Bitmap Image)
        {
            int glid = -1;

            if (Image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    /* Get image data */
                    BitmapData bmpdata = Image.LockBits(new Rectangle(0, 0, Image.Width, Image.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    byte[] data = new byte[bmpdata.Stride * Image.Height];
                    Marshal.Copy(bmpdata.Scan0, data, 0, data.Length);

                    /* Generate GL texture */
                    glid = GL.GenTexture();
                    GL.BindTexture(TextureTarget.Texture2D, glid);
                    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmpdata.Width, bmpdata.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmpdata.Scan0);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

                    /* Finish things up */
                    Image.UnlockBits(bmpdata);
                    ms.Close();
                }
            }

            /* Check cache for texture, add if not already cached */
            if (!cachetbl.ContainsKey(Name))
            {
                cachetbl.Add(Name, glid);
            }
        }
    }
}
