using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace XML_Creator
{
    class XML_Builder
    {
        XDocument myXDoc;

        public XML_Builder()
        {
            //----构造函数
            myXDoc = new XDocument();
        }

        ~XML_Builder()
        {
            //----析构函数
        }

    }
}
