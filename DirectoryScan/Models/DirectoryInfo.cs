using System;
using System.Collections.Generic;
using System.Linq;

namespace DirectoryScan.Models
{
    public class DirectoryViewModel
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastWriteTime { get; set; }
        public DateTime LastAccessTime { get; set; }
        public List<FileViewModel> Files { get; set; }

        public AccessInfo AccessInfo { get; set; }
        public AttributesInfo AttributesInfo { get; set; }

        public double Size
        {
            get { return Files.Sum(_ => _.Size); }
        }

        public DirectoryViewModel()
        {
            AccessInfo = new AccessInfo();
            AttributesInfo = new AttributesInfo();
            Files = new List<FileViewModel>();
        }

    }

    public class FileViewModel
    {
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastWriteTime { get; set; }
        public DateTime LastAccessTime { get; set; }
        public double Size { get; set; }

        public AccessInfo AccessInfo { get; set; }
        public AttributesInfo AttributesInfo { get; set; }

        public FileViewModel()
        {
            AccessInfo = new AccessInfo();
            AttributesInfo = new AttributesInfo();
        }

    }

    public class AccessInfo
    {
        public string Owner { get; set; }
        public bool ReadExecute { get; set; }
        public bool Modify { get; set; }
        public bool Read { get; set; }
        public bool Write { get; set; }

    }

    public class AttributesInfo
    {
        public bool ReadOnly { get; set; }
        public bool Encrypted { get; set; }
        public bool Hidden { get; set; }
        public bool Compresed { get; set; }
        public bool Archive { get; set; }
        public bool System { get; set; }
    }

}
