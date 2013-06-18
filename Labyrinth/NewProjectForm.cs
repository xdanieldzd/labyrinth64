using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Labyrinth
{
    public partial class NewProjectForm : Form
    {
        public NewProjectForm()
        {
            InitializeComponent();
        }//end constructor


        public void createform()
        {
            this.ShowDialog();
        }//end method


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (newProjectListType.SelectedIndex == 0)
                labelNewFormDescription.Text = "Create a new map that only has one room. Very handy" + "\n" + "for simple maps that do not require a large area.";
            else if (newProjectListType.SelectedIndex == 1)
                labelNewFormDescription.Text = "Create a new map that has more that one room. Useful" + "\n" + "for dungeons or large maps that have too many" + "\n" + "polygons for one room";
            else if (newProjectListType.SelectedIndex == 2)
                labelNewFormDescription.Text = "Advanced feature which allows greater control over the" + "\n" + "map but requires an extensive knowledge of maps" + "\n" + "and their headers";
        }//end method

        private void buttonSelectTypeOfNewProject_Click(object sender, EventArgs e)
        {
            returnProjectType = newProjectListType.SelectedIndex;
            this.Dispose();
        }//end method


        public int returnProjectType { get; set; }//end method


    }//end class
}//end namespace
