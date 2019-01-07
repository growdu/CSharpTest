using java.io;
using java.util;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileTest
{
    /// <summary>
    /// 用于测试C#使用pdfbox对pdf文件进行读写
    /// </summary>
    public class PdfParse
    {
        public static void Extract(string file)
        {
            File doc = new File(file);
            PDDocument pdf = PDDocument.load(doc);
            Test test = new Test();
            test.extract(pdf,0);
        }
    }

    /// <summary>
    /// 主要是为了测试是否可以读取到内容，如果可以读取到的话就能够进行使用
    /// </summary>
    public class Test : PDFTextStripper
    {
        public Test()
        {

        }

        public void extract(PDDocument document, int pageNumber)
        {
            ////开始页与结束页相同，只解析一页
            this.setStartPage(pageNumber);
            this.setEndPage(pageNumber);
            this.document = document;
            PDFTextStripper stripper = new Test();
            Writer dumpy = new OutputStreamWriter(new ByteArrayOutputStream());
            stripper.writeText(document, dumpy);
        }

        public void Process(int pageNum, File file)
        {
            try
            {
                PDDocument document = PDDocument.load(file);
                //              PDFTextStripper stripper = new TextPositonExtracter();
                if (pageNum == -1)
                {
                    this.setStartPage(1);
                    this.setEndPage(document.getNumberOfPages());
                }
                else
                {
                    this.setStartPage(pageNum);
                    this.setEndPage(pageNum);
                }
                Writer dumpy = new OutputStreamWriter(new ByteArrayOutputStream());
                this.writeText(document, dumpy);
            }
            catch
            {

            }
        }

        protected override void writeString(string text, List textPositions)
        {
            for (int i = 0; i < textPositions.size(); i++)
            {
                TextPosition textPosition = (TextPosition)textPositions.get(i);
                string content = textPosition.getCharacter();
                float x = textPosition.getXDirAdj();
                System.Console.WriteLine(content + "\nx:" + x + "\ty:" + textPosition.getY());
            }
        }
    }

}
