using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Labyrinth.ProjectHandler
{
    class SceneSetup
    {
        Project parent;
        ModelImport.I3DModel Model;

        public SceneSetup(Project p)
        {
            parent = p;
        }//end constructor

        public void LoadCollisionModel(string fn)
        {
            Model = ProjectHelpers.OpenModelByExtension(fn);
        }//end method
    }//end class
}//end namespace
