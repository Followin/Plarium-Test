using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using DirectoryScan.Concrete;
using DirectoryScan.Models;
using Timer = System.Windows.Forms.Timer;

namespace DirectoryScan
{

    
    public partial class Form1 : Form
    {
        private Thread t;
        private Timer timer;
        public Form1()
        {
            InitializeComponent();
        }

        //Подвязываем все обработчики
        private void Form1_Load(object s, EventArgs e)
        {
            changeFolderButton.Click += (sender, args) =>
            {
                folderPathLaber.Text = "Folder to scan: " + FormHelper.ChangeDirFolder();
            };
            changeXmlButton.Click += (sender, args) =>
            {
                xmlPathLabel.Text = "Xml file to save: " + FormHelper.ChangeXmlPath();
            };
            startbutton.Click += (sender, args) =>
            {
                var mode = bwMode.Checked ? FormHelper.TreeWriteModes.BWorker : FormHelper.TreeWriteModes.Invoke;
                if (FormHelper.Start(this, treeView1, bw_ProgressChanged, mode))
                {


                    startbutton.Enabled = false;
                    timer = new Timer();
                    timer.Interval = 100;
                    timer.Tick += timer_Tick;
                    timer.Start();
                }
            };
        }

        //Обработчик информации, поставляющейся в дерево
        void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var info = e.UserState as TreeInfo;
            var parentDir = treeView1.Nodes.Find(info.ParentPath, true);
            if (parentDir.Length != 0)
                parentDir[0].Nodes.Insert(1,info.Element);
            else
                treeView1.Nodes.Add("Directories").Nodes.Add(info.Element);


        }

        public void timer_Tick(object sender, EventArgs e)
        {
            label1.Text = FormHelper.QueueCount.ToString();
        }

        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!FormHelper.Processing) return;
            if (MessageBox.Show("Process is still running. Are you sure you wanna stop it?", "Question",
                MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
            FormHelper.CancelProcess();
            
        }

        



        

        


        
    }
}
