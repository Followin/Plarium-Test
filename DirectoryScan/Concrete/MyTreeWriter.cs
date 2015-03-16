using System;
using System.Windows.Forms;
using DirectoryScan.Models;

namespace DirectoryScan.Concrete
{

    public class TreeInfo
    {
        public string ParentPath { get; set; }
        public TreeNode Element { get; set; }

    }
    public class MyDirectoryToTreeWriter
    {
        public static void Write(DirectoryViewModel directoryModel, TreeView tree)
        {
            var info = GetInfo(directoryModel, tree);
            var parentDir = tree.Nodes.Find(info.ParentPath, true);

            //Проверка на необходимость Invoke и запись в дерево
            if (parentDir.Length != 0)
            {
                if (tree.InvokeRequired)
                    tree.Invoke(new Action(() => parentDir[0].Nodes.Insert(1, info.Element)));
                else
                    parentDir[0].Nodes.Insert(1, info.Element);
            }
            else
            {
                if (tree.InvokeRequired)
                    tree.Invoke(new Action(() =>
                    {
                        var dir = tree.Nodes.Add("Directories");
                        dir.Nodes.Add(info.Element);
                    }));
                else
                {
                    var dir = tree.Nodes.Add("Directories");
                    dir.Nodes.Add(info.Element);
                }
            }

        }

        public static TreeInfo GetInfo(DirectoryViewModel directoryModel, TreeView tree)
        {
            var directoryElement = new TreeNode(directoryModel.Name);
            directoryElement.Name = directoryModel.Path;
            var directoryAttrs = new[]
            {
                new TreeNode("Path: " + directoryModel.Path),
                new TreeNode("Size: " + directoryModel.Size),
                new TreeNode("Creation Time: " + directoryModel.CreationTime),
                new TreeNode("Last Write Time: " + directoryModel.LastWriteTime),
                new TreeNode("Last Access Time: " + directoryModel.LastAccessTime),
                new TreeNode("Owned: " + directoryModel.AccessInfo.Owner),
                new TreeNode("ReadExecute: " + directoryModel.AccessInfo.ReadExecute),
                new TreeNode("Read: " + directoryModel.AccessInfo.Read),
                new TreeNode("Modify: " + directoryModel.AccessInfo.Modify),
                new TreeNode("Write: " + directoryModel.AccessInfo.Write),
                new TreeNode("Read Only: " + directoryModel.AttributesInfo.ReadOnly),
                new TreeNode("Encrypted: " + directoryModel.AttributesInfo.Encrypted),
                new TreeNode("Hidden: " + directoryModel.AttributesInfo.Hidden),
                new TreeNode("Compresed: " + directoryModel.AttributesInfo.Compresed),
                new TreeNode("Archive: " + directoryModel.AttributesInfo.Archive),
                new TreeNode("System: " + directoryModel.AttributesInfo.System),
            };
            var directoryAttrsWrapper = new TreeNode("DIRECTORY INFO");
            directoryAttrsWrapper.Nodes.AddRange(directoryAttrs);
            directoryElement.Nodes.Add(directoryAttrsWrapper);

            foreach (var fileModel in directoryModel.Files)
            {
                var fileElement = new TreeNode(fileModel.Name);
                var fileAttrs = new[]
                {
                    new TreeNode("Size: " + fileModel.Size),
                    new TreeNode("Creation Time: " + fileModel.CreationTime),
                    new TreeNode("Last Write Time: " + fileModel.LastWriteTime),
                    new TreeNode("Last Access Time: " + fileModel.LastAccessTime),
                    new TreeNode("Owned: " + fileModel.AccessInfo.Owner),
                    new TreeNode("ReadExecute: " + fileModel.AccessInfo.ReadExecute),
                    new TreeNode("Read: " + fileModel.AccessInfo.Read),
                    new TreeNode("Modify: " + fileModel.AccessInfo.Modify),
                    new TreeNode("Write: " + fileModel.AccessInfo.Write),
                    new TreeNode("Read Only: " + fileModel.AttributesInfo.ReadOnly),
                    new TreeNode("Encrypted: " + fileModel.AttributesInfo.Encrypted),
                    new TreeNode("Hidden: " + fileModel.AttributesInfo.Hidden),
                    new TreeNode("Compresed: " + fileModel.AttributesInfo.Compresed),
                    new TreeNode("Archive: " + fileModel.AttributesInfo.Archive),
                    new TreeNode("System: " + fileModel.AttributesInfo.System),
                };
                fileElement.Nodes.AddRange(fileAttrs);
                directoryElement.Nodes.Add(fileElement);
            }



            return new TreeInfo
            {
                ParentPath = directoryModel.Path.Remove(directoryModel.Path.LastIndexOf('\\')),
                Element = directoryElement
            };



        }
    }
}
