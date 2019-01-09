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
    public class ExtractText : PDFTextStripper
    {
        public List<Node> _nodes = null;
        public string _text = string.Empty;

        public void Extract(PDDocument document, int pageNumber)
        {
            //开始页与结束页相同，只解析一页
            this.setStartPage(pageNumber);
            this.setEndPage(pageNumber);
            this.document = document;
            _nodes = new List<Node>();
            Writer dumpy = new OutputStreamWriter(new ByteArrayOutputStream());
            writeText(document, dumpy);
        }

        protected override void writeString(string text, List textPositions)
        {
            for (int i = 0; i < textPositions.size(); i++)
            {
                TextPosition tp = (TextPosition)textPositions.get(i);
                Node node = new Node();
                node.Text = tp.getCharacter();
                node.x = tp.getXDirAdj();
                node.y = tp.getYDirAdj();
                _nodes.Add(node);
                _text += node.Text;
            }
        }

    }

    public class Node : Rectangle
    {
        public string Text { get; set; }

        public Node() { }

        public Node(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public Node createUnion(Node r)
        {
            //需先算width，height，因为this的想，y会被改变
            this.width = Math.Max(r.x, this.x) - Math.Min(r.x, this.x);
            this.height = Math.Max(r.y, this.y) - Math.Min(r.y, this.y);
            this.x = Math.Min(r.x, this.x);
            this.y = Math.Min(r.y, this.y);
            return this;
        }


        public bool IsEmpty()
        {
            return (width <= 0.0f) || (height <= 0.0f);
        }

        public override string ToString()
        {
            return this.Text + "<x=" + this.x + ",y=" + this.y + ">";
        }

    }


    public abstract class Rectangle
    {
        public float x { get; set; }

        public float y { get; set; }

        public float width { get; set; }

        public float height { get; set; }
    }

}
