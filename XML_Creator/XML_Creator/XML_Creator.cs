using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Windows.Forms;

namespace XML_Creator
{
    public partial class XML_Creator : Form
    {
        private static DirectoryInfo Select_Dir;
        private static DriveInfo Select_Dri;
        private static FileInfo Select_File;
        private string Select_Floder;
        private IniFiles Config_INI ;

        public XML_Creator()
        {
            InitializeComponent();
        }

        private void MenuItem_Open_Click(object sender, EventArgs e)
        {
            Floder_Select_Dlg.ShowDialog();
            Select_Floder = Floder_Select_Dlg.SelectedPath;
            Console.WriteLine(Select_Floder);
            this.StatusLabel1.Text = Select_Floder;
            if (Select_Floder != null && Select_Floder != "")
            {
                TreeV.Nodes.Clear();
                DirectoryInfo tmp = new DirectoryInfo(Select_Floder);
                if (tmp.ToString() == tmp.Root.ToString())
                {
                    TreeNode root = new TreeNode(tmp.Root.Name);
                    TreeV.Nodes.Add(root);
                    Traverse(root, tmp.Root);
                    this.StatusLabel2.Text = "List.";
                }
                else
                {
                    TreeNode root = new TreeNode(tmp.Name);
                    TreeV.Nodes.Add(root);
                    Traverse(root, tmp);
                    this.StatusLabel2.Text = "List.";
                }
            }
        }

        private void MenuItem_Build_Click(object sender, EventArgs e)
        {
            if (Select_Floder != null && Select_Floder != "")
            {
                Select_Dir = new DirectoryInfo(Select_Floder);
                if (Select_Dir.ToString() == Select_Dir.Root.ToString())
                {
                    Console.WriteLine("It is a Driver!");
                    Select_Dri = new DriveInfo(Select_Dir.Root.Name);
                    XElement dirXml = Select_Dri.LToXml();//显示所有文件
                    string XML_Name = Select_Dri.VolumeLabel + Select_Dri.Name.Substring(0, 1) + "(" + (Select_Dri.TotalSize / 1024 / 1024 / 1024).ToString() + "G)";
                    string XML_Path = Config_INI.ReadString("XML", "XML_Build_Path", "");
                    if (XML_Path == null || XML_Path == "")
                    {
                        MessageBox.Show("ERROR 101 :\nDO NOT Find XML_Build_Path in XML!");
                        return;
                    }
                    string XML_FullPath = XML_Path + XML_Name + ".xml";
                    if (File.Exists(XML_FullPath))
                    {
                        //MessageBox.Show("Existed!So Rewrite!");
                        this.StatusLabel1.Text = "Existed!So Rewrite.";
                        File.Delete(XML_FullPath);
                        dirXml.Save(XML_FullPath);
                        this.StatusLabel2.Text = "Created.(Driver)";
                    }
                    else
                    {
                        dirXml.Save(XML_FullPath);
                        this.StatusLabel2.Text = "Created.(Driver)";
                    }
                }
                else
                {
                    XElement dirXml = Select_Dir.LToXml();//显示所有文件
                    string XML_Name = Select_Dir.Name;
                    string XML_Path = Config_INI.ReadString("XML", "XML_Build_Path", "");
                    if (XML_Path == null || XML_Path == "")
                    {
                        MessageBox.Show("ERROR 101 :\nDO NOT Find XML_Build_Path in XML!");
                        return;
                    }
                    string XML_FullPath = XML_Path + XML_Name + ".xml";
                    if (File.Exists(XML_FullPath))
                    {
                        //MessageBox.Show("Existed!So Rewrite!");
                        this.StatusLabel1.Text = "Existed!So Rewrite.";
                        File.Delete(XML_FullPath);
                        dirXml.Save(XML_FullPath);
                        this.StatusLabel2.Text = "Created.(Floder)";
                    }
                    else
                    {
                        dirXml.Save(XML_FullPath);
                        this.StatusLabel2.Text = "Created.(Floder)";
                    }
                }
            }
            else
                this.StatusLabel1.Text = "Path is NULL!";
        }

        private void MenuItem_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TreeV_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void TreeV_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            //e.Node.Nodes.Cast<TreeNode>().ToList().ForEach(x =>
            //{
            //    try
            //    {
            //        if (x.Nodes.Count == 0)
            //        {
            //            TreeNode[] nodes = new TreeNode[] { };
            //            if (x.Tag.GetType() == typeof(DriveInfo))
            //            {
            //                var item = x.Tag as DriveInfo;
            //                //Console.WriteLine(item.Name);
            //                nodes = Directory.GetDirectories(item.Name)
            //                    .Select(y => new DirectoryInfo(y))
            //                    .Select(y => new TreeNode(y.Name) { Tag = y })
            //                    .ToArray();
            //            }
            //            if (x.Tag.GetType() == typeof(DirectoryInfo))
            //            {
            //                var item = x.Tag as DirectoryInfo;
            //                //Console.WriteLine(item.FullName);
            //                nodes = Directory.GetDirectories(item.FullName)
            //                    .Select(y => new DirectoryInfo(y))
            //                    .Select(y => new TreeNode(y.Name) { Tag = y })
            //                    .ToArray();
            //            }
                        
            //            x.Nodes.AddRange(nodes);
            //        }
            //    }
            //    catch { }
            //});
        }

        private void XML_Creator_Load(object sender, EventArgs e)
        {
            try
            {
                Config_INI = new IniFiles(System.Environment.CurrentDirectory + "/config.ini");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("ERROR 100 :\nGet INI failed!");
                this.Close();
            }
        }

        public void Traverse(TreeNode node, DirectoryInfo dir)
        {
            if (dir == null)
            {
                // 如果目录为空，则说明没有子目录，应该返回到上一层   
                return;
            }
            else
            {
                // TreeNode treeNode = new TreeNode(dir.Name);
                // node.Nodes.Add(treeNode); // 添加结点   
                // 子目录不空，则优先遍历所有的子目录，再遍历每个子目录中的文件   
                DirectoryInfo[] subDir = dir.GetDirectories();
                foreach (DirectoryInfo sub in subDir)
                {
                    if (sub.Attributes.ToString().LastIndexOf("System") == -1)
                    {
                        TreeNode treeNode = new TreeNode(sub.Name);
                        node.Nodes.Add(treeNode); // 添加结点   
                        Traverse(treeNode, sub); // 先遍历当前目录的子目录 
                    }
                }
                // 遍历当前目录的文件   
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo f in files)
                {
                    string fileName = f.Name;
                    TreeNode fileNode = new TreeNode(fileName);
                    node.Nodes.Add(fileNode);
                }
            }
        }

        private void ToolStripMenuItem_About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Author :dansan\nVersion:1.0\nDate    :13-6-9");
        }  
    }
}
