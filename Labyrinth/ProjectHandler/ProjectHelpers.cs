using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Labyrinth.ProjectHandler
{
    class ProjectHelpers
    {
        public static ModelImport.I3DModel OpenModelByExtension(string fn)
        {
            ModelImport.I3DModel model = null;
            string ext = System.IO.Path.GetExtension(fn);

            switch (ext)
            {
                case ".obj":
                    model = new ModelImport.WavefrontObj(fn);
                    break;

                default:
                    throw new Exception("Unsupported model format");
            }

            return model;
        }
    }
}
