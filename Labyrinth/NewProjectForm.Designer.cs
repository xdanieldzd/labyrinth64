namespace Labyrinth
{
    partial class NewProjectForm
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
            this.newProjectListType = new System.Windows.Forms.ComboBox();
            this.labelNewFormDescription = new System.Windows.Forms.Label();
            this.buttonSelectTypeOfNewProject = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // newProjectListType
            // 
            this.newProjectListType.FormattingEnabled = true;
            this.newProjectListType.Items.AddRange(new object[] {
            "Single Room Scene",
            "Multi Room Scene",
            "Blank Project"});
            this.newProjectListType.Location = new System.Drawing.Point(12, 12);
            this.newProjectListType.Name = "newProjectListType";
            this.newProjectListType.Size = new System.Drawing.Size(260, 21);
            this.newProjectListType.TabIndex = 0;
            this.newProjectListType.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // labelNewFormDescription
            // 
            this.labelNewFormDescription.AutoSize = true;
            this.labelNewFormDescription.Location = new System.Drawing.Point(11, 64);
            this.labelNewFormDescription.Name = "labelNewFormDescription";
            this.labelNewFormDescription.Size = new System.Drawing.Size(28, 13);
            this.labelNewFormDescription.TabIndex = 1;
            this.labelNewFormDescription.Text = "       ";
            // 
            // buttonSelectTypeOfNewProject
            // 
            this.buttonSelectTypeOfNewProject.Location = new System.Drawing.Point(99, 131);
            this.buttonSelectTypeOfNewProject.Name = "buttonSelectTypeOfNewProject";
            this.buttonSelectTypeOfNewProject.Size = new System.Drawing.Size(75, 23);
            this.buttonSelectTypeOfNewProject.TabIndex = 2;
            this.buttonSelectTypeOfNewProject.Text = "Select";
            this.buttonSelectTypeOfNewProject.UseVisualStyleBackColor = true;
            this.buttonSelectTypeOfNewProject.Click += new System.EventHandler(this.buttonSelectTypeOfNewProject_Click);
            // 
            // NewProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 163);
            this.Controls.Add(this.buttonSelectTypeOfNewProject);
            this.Controls.Add(this.labelNewFormDescription);
            this.Controls.Add(this.newProjectListType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "NewProjectForm";
            this.Text = "Select the Type of Project";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox newProjectListType;
        private System.Windows.Forms.Label labelNewFormDescription;
        private System.Windows.Forms.Button buttonSelectTypeOfNewProject;
    }
}