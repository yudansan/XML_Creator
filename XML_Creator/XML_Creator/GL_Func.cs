using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace XML_Creator
{
    public static class GL_Func
    {
        public static string Get_Size(string size)
        {
            string size_final;
            if (double.Parse(size) > 1024 * 1024 * 1024)
            {
                size_final = (double.Parse(size) / 1024 / 1024 / 1024).ToString("f2") + "GB";
            }
            else if (double.Parse(size) > 1024 * 1024)
            {
                size_final = (double.Parse(size) / 1024 / 1024).ToString("f2") + "MB";
            }
            else if (double.Parse(size) > 1024)
            {
                size_final = (double.Parse(size) / 1024).ToString("f2") + "KB";
            }
            else
            {
                size_final = size + "B";
            }
            return size_final;
        }

        public static string Get_Size(string size, bool type)
        {
            if (type)
            {
                string size_final;
                if (double.Parse(size) > 1024 * 1024 * 1024)
                {
                    size_final = (double.Parse(size) / 1024 / 1024 / 1024).ToString("f2") + " GB";
                }
                else if (double.Parse(size) > 1024 * 1024)
                {
                    size_final = (double.Parse(size) / 1024 / 1024).ToString("f2") + " MB";
                }
                else if (double.Parse(size) > 1024)
                {
                    size_final = (double.Parse(size) / 1024).ToString("f2") + " KB";
                }
                else
                {
                    size_final = size + " 字节";
                }
                return size_final;
            } 
            else
            {
                string size_final = "";
                string tmp = new string(size.ToCharArray().Reverse().ToArray());
                int count = size.Length;
                int i = 0;
                while ((i + 3) < count)
                {
                    size_final += tmp.Substring(i, 3) + ",";
                    i += 3;
                }
                size_final += tmp.Substring(i);
                return new string(size_final.ToCharArray().Reverse().ToArray()) + " 字节";
            }
        }

        public static double Resume_Size(string size)
        {
            Double size_final;
            //Console.WriteLine(size.Length);
            if (size.Length == 0)
            {
                size_final = 0;
            }
            else if (size.Length <= 2)
            {
                size_final = Double.Parse(size.Substring(0, size.Length - 2));
            }
            else if (size.Substring(size.Length - 2) == "GB")
            {
                size_final = (Double.Parse(size.Substring(0, size.Length - 3)) * 1024 * 1024 * 1024);
            }
            else if (size.Substring(size.Length - 2) == "MB")
            {
                size_final = (Double.Parse(size.Substring(0, size.Length - 3)) * 1024 * 1024);
            }
            else if (size.Substring(size.Length - 2) == "kB")
            {
                size_final = (Double.Parse(size.Substring(0, size.Length - 3)) * 1024);
            }
            else
            {
                size_final = Double.Parse(size.Substring(0, size.Length - 2));
            }
            return size_final;
        }

        public static string Get_Type(string type)
        {
            string file_type;
            int pos = type.LastIndexOf(".");
            file_type = type.Substring(pos+1);
            return file_type;
        }

        public static string Get_Type(FileInfo file)
        {
            return file.Extension;
        }

        public static string Get_Name(FileInfo file)
        {
            return file.Name.Replace(file.Extension,"");
        }
    }

}
