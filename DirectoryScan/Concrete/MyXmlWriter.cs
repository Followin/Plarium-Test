using System.Xml.Linq;
using System.Xml.XPath;
using DirectoryScan.Models;

namespace DirectoryScan.Concrete
{
    public class MyXmlWriter
    {
        public static void Write(DirectoryViewModel model, string path)
        {
            var xdoc = XDocument.Load(path);
            
            //var directory = xdoc.Elements("directory").Where(x => x.Attribute("path").Value == path);
            var root = xdoc.Element("directories");
            var attrs = new[]
            {
                new XAttribute("name", model.Name),
                new XAttribute("path", model.Path),
                new XAttribute("size", model.Size),
                new XAttribute("creationTime", model.CreationTime.ToString()),
                new XAttribute("lastWriteTime", model.LastWriteTime.ToString()),
                new XAttribute("lastAccessTime", model.LastAccessTime.ToString()),
                new XAttribute("owner", model.AccessInfo.Owner),
                new XAttribute("readExecute", model.AccessInfo.ReadExecute.ToString()),
                new XAttribute("read", model.AccessInfo.Read.ToString()),
                new XAttribute("modify", model.AccessInfo.Modify.ToString()),
                new XAttribute("write", model.AccessInfo.Write.ToString()),
                new XAttribute("readonly", model.AttributesInfo.ReadOnly.ToString()),
                new XAttribute("encrypted", model.AttributesInfo.Encrypted.ToString()),
                new XAttribute("hidden", model.AttributesInfo.Hidden.ToString()),
                new XAttribute("compresed", model.AttributesInfo.Compresed.ToString()),
                new XAttribute("archive", model.AttributesInfo.Archive.ToString()),
                new XAttribute("system", model.AttributesInfo.System.ToString()),
            };
            var xelement = new XElement("directory");
            xelement.Add(attrs);

            foreach (var file in model.Files)
            {
                var xFileElement = new XElement("file");
                attrs = new[]
                {
                    new XAttribute("name", file.Name),
                    new XAttribute("size", file.Size),
                    new XAttribute("creationTime", file.CreationTime.ToString()),
                    new XAttribute("lastWriteTime", file.LastWriteTime.ToString()),
                    new XAttribute("lastAccessTime", file.LastAccessTime.ToString()),
                    new XAttribute("owner", file.AccessInfo.Owner),
                    new XAttribute("readExecute", file.AccessInfo.ReadExecute.ToString()),
                    new XAttribute("read", file.AccessInfo.Read.ToString()),
                    new XAttribute("modify", file.AccessInfo.Modify.ToString()),
                    new XAttribute("write", file.AccessInfo.Write.ToString()),
                    new XAttribute("readonly", file.AttributesInfo.ReadOnly.ToString()),
                    new XAttribute("encrypted", file.AttributesInfo.Encrypted.ToString()),
                    new XAttribute("hidden", file.AttributesInfo.Hidden.ToString()),
                    new XAttribute("compresed", file.AttributesInfo.Compresed.ToString()),
                    new XAttribute("archive", file.AttributesInfo.Archive.ToString()),
                    new XAttribute("system", file.AttributesInfo.System.ToString()),
                };
                xFileElement.Add(attrs);
                xelement.Add(xFileElement);
            }
            var parentDir = root.XPathSelectElement("directory[@path='" + model.Path.Remove(model.Path.LastIndexOf('\\')) + "']");
            if(parentDir!=null)
                parentDir.AddFirst(xelement);
            else
                root.Add(xelement);
            xdoc.Save(path);

        }
    }
}
