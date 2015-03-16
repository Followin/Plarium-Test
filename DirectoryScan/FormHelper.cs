using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using DirectoryScan.Concrete;
using DirectoryScan.Models;
using Timer = System.Windows.Forms.Timer;

namespace DirectoryScan
{

    
    public static class FormHelper
    {
        public enum TreeWriteModes
        {
            Invoke,
            BWorker
        };
        private static SaveFileDialog fs = new SaveFileDialog();
        private static FolderBrowserDialog fbd = new FolderBrowserDialog();

        private static MyDirectoryBlockingQueue _queue;

        private static Thread scanThread;
        private static Thread fileThread;
        private static Thread treeThread;
        private static BackgroundWorker bw;

        static AutoResetEvent treeWaitHandler = new AutoResetEvent(true);
        static AutoResetEvent fileWaitHandler = new AutoResetEvent(true);
        

        public static string FilePath
        {
            get { return fs.FileName; }
        }

        public static string DirPath
        {
            get { return fbd.SelectedPath; }
        }

        public static int QueueCount
        {
            get { return _queue.Count; }
        }

        public static bool QueueEnded
        {
            get { return _queue.Ended; }
        }

        public static bool Processing { get; private set; }

        public static string ChangeXmlPath()
        {
            
            fs.Filter = "XML|.xml";
            if (fs.ShowDialog() == DialogResult.Cancel) return "";
            return fs.FileName;
        }

        public static string ChangeDirFolder()
        {
            if (fbd.ShowDialog() == DialogResult.Cancel) return "";
            return fbd.SelectedPath;
        }


        public static bool Start(Form1 parent, TreeView tree, ProgressChangedEventHandler bwProgressChanged, TreeWriteModes mode)
        {
            if (!Check())
            {
                MessageBox.Show("Please select directory to scan and xml file to save info.");
                return false;
            }
            MessageBox.Show("Please don't open an xml file you selected");

            Processing = true;

            tree.Nodes.Clear();
            _queue = new MyDirectoryBlockingQueue();

            var xml = new XDocument();
            xml.Add(new XElement("directories"));
            using (var stream = new StreamWriter(fs.FileName))
            {
                xml.Save(stream);
            }

            //Поток сканирования
            scanThread = new Thread(() => MyFolderScanner.GetDirectories(fbd.SelectedPath, _queue));
            scanThread.Start();

            //Поток записи в файл
            fileThread = new Thread(() =>
            {
                DirectoryViewModel model;

                while (!_queue.Ended || _queue.Count != 0)
                {
                    treeWaitHandler.WaitOne();
                    _queue.TryDequeue(out model);
                    MyXmlWriter.Write(model, fs.FileName);
                    fileWaitHandler.Set();
                }
                MessageBox.Show("Done!");
                Processing = false;
                parent.startbutton.Enabled = true;
            });
            fileThread.Start();


            //В зависимости от режима записи в дерево, стартует один из типов потоков
            if (mode == TreeWriteModes.BWorker)
            {
                //Background worker, который отправляет обработанные ветки в дерево
                bw = new BackgroundWorker();
                bw.WorkerReportsProgress = true;
                bw.WorkerSupportsCancellation = true;
                bw.ProgressChanged += bwProgressChanged;
                bw.DoWork += (sender, args) =>
                {
                    var worker = sender as BackgroundWorker;
                    DirectoryViewModel model;
                    while (!_queue.Ended || _queue.Count != 0)
                    {
                        fileWaitHandler.WaitOne();
                        _queue.TryDequeue(out model);
                        if (model != null)
                        {
                            var info = MyDirectoryToTreeWriter.GetInfo(model, tree);
                            worker.ReportProgress(0, info);
                        }
                        treeWaitHandler.Set();
                    }
                };
                bw.RunWorkerAsync();
            }
            else
            {
                //Поток, который пользуется Invoke, чтобы записывать в дерево
                treeThread = new Thread(() =>
                {
                    DirectoryViewModel model;
                    while (!_queue.Ended || _queue.Count != 0)
                    {
                        fileWaitHandler.WaitOne();
                        _queue.TryDequeue(out model);
                        MyDirectoryToTreeWriter.Write(model, tree);
                        treeWaitHandler.Set();
                    }
                });
                treeThread.Start();
            }
            return true;
        }

        static bool Check()
        {
            if (!string.IsNullOrEmpty(FilePath) && !string.IsNullOrEmpty(DirPath))
                return true;
            return false;
        }

        public static void CancelProcess()
        {
            if (Processing)
            {
                scanThread.Abort();
                fileThread.Abort();
                if (treeThread != null && treeThread.IsAlive)
                    treeThread.Abort();
                else if(bw!=null)
                    bw.CancelAsync();
            }
        }

    }
}
