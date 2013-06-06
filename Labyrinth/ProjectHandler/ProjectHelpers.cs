using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Labyrinth.ProjectHandler
{
    class ProjectHelpers
    {

        public ProjectHelpers()
        {
            didErrorOccur = false;
        }//end constructor

        public static string errorMessage { get; set; }//end method

        public static bool didErrorOccur { get; set; }//end method

        public static ModelImport.I3DModel OpenModelByExtension(string fn)
        {
            ModelImport.I3DModel model = null;
            didErrorOccur = false;

            try
            {
                string ext = System.IO.Path.GetExtension(fn);

                switch (ext)
                {
                    case ".obj":
                        model = new ModelImport.WavefrontObj(fn);
                        break;

                    default:
                        errorMessage = "Unsupported model format";
                        didErrorOccur = true;
                        throw new Exception("Unsupported model format");
                }//end switch
            }//end try
            catch (Exception e)
            {
                didErrorOccur = true;
                errorMessage = "There was an issue getting the file extension from file: " + fn + "\r\n" + "Error message: " + e.Message;
            }//end catch

            return model;
        }//end method
    }//end class
}//end method
