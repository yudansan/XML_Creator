using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace XML_Creator
{
    public static class LinqToXml
    {
        public static XElement LToXml(this DirectoryInfo dir)
        {
            XElement root;
            if (dir.Attributes.ToString().LastIndexOf("System") != -1)
            {
                return null;
            } 
            else
            {
                /*root = new XElement("Floder",
                    new XAttribute("Name", dir.Name), new XAttribute("Date", dir.LastWriteTimeUtc.ToString()),
                    from subDir in dir.GetDirectories() select LToXml(subDir),
                    from file in dir.GetFiles() select new XElement("File", file.Name,
                        new XAttribute("Date", file.LastWriteTimeUtc.ToString()), new XAttribute("Size", file.Length.ToString())));*/
                root = new XElement("Floder",
                    new XAttribute("Name", dir.Name), new XAttribute("Date", dir.LastWriteTime.ToString()),
                    from subDir in dir.GetDirectories() select LToXml(subDir),
                    from file in dir.GetFiles() select new XElement("File", new XAttribute("Name", file.Name),
                        new XElement("Date", file.LastWriteTime.ToString()),
                        new XElement("Size", file.Length.ToString())
                        /*,new XElement("URL", file.FullName.ToString())*/));
                return root;
            }
        }
        public static XElement LToXml(this DriveInfo drive)
        {
            DirectoryInfo dir = drive.RootDirectory;
            XElement root = new XElement("Floder",
            new XAttribute("Name", dir.Name),
            from subDir in dir.GetDirectories() select LToXml(subDir),
            from file in dir.GetFiles() select new XElement("File", file.Name));
            return root;
        }
    }
}
