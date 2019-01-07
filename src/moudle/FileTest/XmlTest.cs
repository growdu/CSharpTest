using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FileTest
{
    class XmlTest
    {
        public static void CreateXml()
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "utf-8", null);
            document.AppendChild(declaration);
            //添加根节点
            XmlNode div = document.CreateElement("div");
            document.AppendChild(div);
            int pageNum = 1;
            for(int i = 0; i < 10; i++)
            {
                //添加页号
                XmlElement page = document.CreateElement("page");
                page.SetAttribute("num", i.ToString());
                div.AppendChild(page);
                for(int j= 0; j < 5; j++)
                {
                    XmlElement para = document.CreateElement("P");
                    para.SetAttribute("x","10");
                    para.SetAttribute("y","10");
                    para.InnerText="This is a test"+j;
                    page.AppendChild(para);
                }
            }
            try
            {
                document.Save(@"test.xml");   
            }
            catch (Exception ex){
                ;
            }
        }
        
    }
}
