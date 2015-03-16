namespace DirectoryScan
{
    partial class Form1
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
            this.changeFolderButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.folderPathLaber = new System.Windows.Forms.Label();
            this.xmlPathLabel = new System.Windows.Forms.Label();
            this.changeXmlButton = new System.Windows.Forms.Button();
            this.startbutton = new System.Windows.Forms.Button();
            this.bwMode = new System.Windows.Forms.RadioButton();
            this.invokeMode = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // changeFolderButton
            // 
            this.changeFolderButton.Location = new System.Drawing.Point(12, 350);
            this.changeFolderButton.Name = "changeFolderButton";
            this.changeFolderButton.Size = new System.Drawing.Size(66, 23);
            this.changeFolderButton.TabIndex = 0;
            this.changeFolderButton.Text = "Change";
            this.changeFolderButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(224, 435);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 4;
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(12, 12);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(313, 323);
            this.treeView1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(265, 435);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "more";
            // 
            // folderPathLaber
            // 
            this.folderPathLaber.AutoSize = true;
            this.folderPathLaber.Location = new System.Drawing.Point(84, 355);
            this.folderPathLaber.Name = "folderPathLaber";
            this.folderPathLaber.Size = new System.Drawing.Size(80, 13);
            this.folderPathLaber.TabIndex = 7;
            this.folderPathLaber.Text = "Folder to scan: ";
            // 
            // xmlPathLabel
            // 
            this.xmlPathLabel.AutoSize = true;
            this.xmlPathLabel.Location = new System.Drawing.Point(83, 384);
            this.xmlPathLabel.Name = "xmlPathLabel";
            this.xmlPathLabel.Size = new System.Drawing.Size(81, 13);
            this.xmlPathLabel.TabIndex = 8;
            this.xmlPathLabel.Text = "Xml file to save:";
            // 
            // changeXmlButton
            // 
            this.changeXmlButton.Location = new System.Drawing.Point(12, 379);
            this.changeXmlButton.Name = "changeXmlButton";
            this.changeXmlButton.Size = new System.Drawing.Size(66, 23);
            this.changeXmlButton.TabIndex = 9;
            this.changeXmlButton.Text = "Change";
            this.changeXmlButton.UseVisualStyleBackColor = true;
            // 
            // startbutton
            // 
            this.startbutton.Location = new System.Drawing.Point(12, 408);
            this.startbutton.Name = "startbutton";
            this.startbutton.Size = new System.Drawing.Size(313, 23);
            this.startbutton.TabIndex = 10;
            this.startbutton.Text = "START";
            this.startbutton.UseVisualStyleBackColor = true;
            // 
            // bwMode
            // 
            this.bwMode.AutoSize = true;
            this.bwMode.Location = new System.Drawing.Point(12, 463);
            this.bwMode.Name = "bwMode";
            this.bwMode.Size = new System.Drawing.Size(118, 17);
            this.bwMode.TabIndex = 11;
            this.bwMode.Text = "Background worker";
            this.bwMode.UseVisualStyleBackColor = true;
            // 
            // invokeMode
            // 
            this.invokeMode.AutoSize = true;
            this.invokeMode.Checked = true;
            this.invokeMode.Location = new System.Drawing.Point(136, 463);
            this.invokeMode.Name = "invokeMode";
            this.invokeMode.Size = new System.Drawing.Size(58, 17);
            this.invokeMode.TabIndex = 12;
            this.invokeMode.TabStop = true;
            this.invokeMode.Text = "Invoke";
            this.invokeMode.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 492);
            this.Controls.Add(this.invokeMode);
            this.Controls.Add(this.bwMode);
            this.Controls.Add(this.startbutton);
            this.Controls.Add(this.changeXmlButton);
            this.Controls.Add(this.xmlPathLabel);
            this.Controls.Add(this.folderPathLaber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.changeFolderButton);
            this.Name = "Form1";
            this.Text = "Simple";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button changeFolderButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label folderPathLaber;
        private System.Windows.Forms.Label xmlPathLabel;
        private System.Windows.Forms.Button changeXmlButton;
        private System.Windows.Forms.RadioButton bwMode;
        private System.Windows.Forms.RadioButton invokeMode;
        protected internal System.Windows.Forms.Button startbutton;

    }
}

