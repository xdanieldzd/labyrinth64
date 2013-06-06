using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Labyrinth.ModelImport
{
    interface I3DModel
    {
        void Open(string fn);
        Common.Model Model { get; set; } //end method
    }//end interface
}//end namespace