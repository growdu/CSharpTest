using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xceed.Words.NET;

namespace Tools.DataHelper
{
    class HtmlToDocx
    {
        Dictionary<PreTable, Table> pre_table;

        public HtmlToDocx()
        {
            pre_table = new Dictionary<PreTable, Table>();
        }

        public void LoadHtml(string path)
        {
            string target = path.Replace(".html", ".docx");
            var doc = new HtmlDocument();
            doc.Load(path, Encoding.UTF8);
            var root = doc.DocumentNode;
            List<HtmlNode> tables = new List<HtmlNode>();
            var nodes = root.SelectNodes("//table");
            foreach (var node in nodes)
            {
                if (HasTable(node))
                    continue;

                tables.Add(node);
            }
            List<PreTable> headers = new List<PreTable>();
            bool lastIsHeader = false;
            int lastHeaderIndex = -1;
            int noHeaderKey = 0;
            foreach (var table in tables)
            {
                if (IsTitle(table))
                {//标题
                    var header = JudgeTitleOrText(table);
                    if (header == null)
                        continue;

                    if (lastIsHeader && lastHeaderIndex > -1)
                    {//上一个表格也是表格头需要进行合并
                        var lastHeader = headers[lastHeaderIndex];
                        //先更新上一个表格头的title添加content，再把当前header的content添加
                        lastHeader.Content += lastHeader.Title + header.Content;
                        lastHeader.Title = header.Title;
                    }
                    else
                    {
                        headers.Add(header);
                        lastIsHeader = true;
                        lastHeaderIndex++;
                    }
                }
                else
                {//表格
                    Table localTable = new Table(table);
                    if (localTable == null || localTable.Matrix == null)
                        continue;

                    if (!lastIsHeader)
                    {//表格没有表头
                        PreTable noHeader = new PreTable();
                        noHeader.Title = "没有表头" + noHeaderKey.ToString();
                        noHeader.Content = "";
                        pre_table.Add(noHeader, localTable);
                    }
                    else
                    {
                        pre_table.Add(headers[lastHeaderIndex], localTable);
                    }
                    lastIsHeader = false;
                }
            }
            using (DocX document = DocX.Create(target))
            {
                foreach (var key in pre_table.Keys)
                {
                    var table = pre_table[key].Matrix;
                    int rowCount = table.GetLength(0);
                    int columnCount = table.GetLength(1);
                    var docxTable = document.AddTable(rowCount, columnCount);
                    docxTable.AutoFit = AutoFit.Contents;
                    for (int i = 0; i < rowCount; i++)
                    {
                        for (int j = 0; j < columnCount; j++)
                        {
                            docxTable.Rows[i].Cells[j].Paragraphs[0].Append(table[i, j]);
                        }
                    }
                    if (!key.Title.Contains("没有表头"))
                    {
                        document.InsertParagraph(key.Content);
                        if (!key.Title.IsEmpty())
                        {
                            var title = document.InsertParagraph().Append(key.Title).Heading(HeadingType.Heading1);
                        }
                    }
                    MergeCell(docxTable);
                    document.InsertTable(docxTable);
                }
                document.Save();
            }

            //SaveAsDocx(tables);
        }

        void MergeCell(Xceed.Words.NET.Table docxTable)
        {
            int rowCount = docxTable.RowCount;
            int columnCount = docxTable.ColumnCount;
            #region 处理跨行
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    //Console.WriteLine("i:" + i + ",j:" + j);
                    var ps = docxTable.Rows[i].Cells[j].Paragraphs;
                    if (ps == null || ps.Count < 1)
                        continue;

                    string text = docxTable.Rows[i].Cells[j].Paragraphs[0].Text;
                    if (docxTable.Rows[i].Cells[j].Paragraphs[0].Text == "|" && i > 0)
                    {
                        docxTable.MergeCellsInColumn(j, i - 1, i);
                    }
                }
            }
            #endregion

            #region 处理跨列
            for (int i = 0; i < rowCount; i++)
            {
                int colnum = columnCount;
                for (int j = 0; j < colnum;)
                {
                    //Console.WriteLine("i:" + i + ",j:" + j);
                    var ps = docxTable.Rows[i].Cells[j].Paragraphs;
                    if (ps == null || ps.Count < 1)
                        continue;

                    string text = docxTable.Rows[i].Cells[j].Paragraphs[0].Text;
                    if (docxTable.Rows[i].Cells[j].Paragraphs[0].Text == "||" && j > 0)
                    {
                        docxTable.Rows[i].Cells[j].RemoveParagraphAt(0);
                        docxTable.Rows[i].MergeCells(j - 1, j);
                        colnum--;
                    }
                    else
                    {
                        j++;
                    }
                }
            }
            #endregion
        }


        bool HasTable(HtmlNode node)
        {
            while (node != null)
            {
                if (node.SelectNodes("table") != null)
                    return true;

                foreach (var n in node.ChildNodes)
                {
                    if (n.SelectNodes("table") != null)
                        return true;

                    return HasTable(n);
                }
                node = node.NextSibling;
            }

            return false;
        }

        public string RemoveSplitChar(string input)
        {
            return input.Replace("\t", "").Replace(" ", "").Replace("&nbsp;", " ");
            //return input.Replace("\r\n", "").Replace("\n", "").Replace("\t", "").Replace(" ", "").Replace("&nbsp;", " ");
        }

        bool IsTitle(HtmlNode htmlTable)
        {
            var trs = htmlTable.SelectNodes("./tr");
            if (trs == null)
            {
                trs = htmlTable.SelectNodes("tbody/tr");
            }
            if (trs == null || trs.Count < 1)
                return false;

            var tds = trs[0].SelectNodes("td");
            if (trs.Count == 1)
            {
                if (tds.Count == 1)
                    return true;

                int i = 0;
                foreach (var td in tds)
                {
                    if (td.InnerText.IsEmpty())
                        continue;

                    i++;
                }
                if (i < 2)
                    return true;
            }
            return false;
        }

        PreTable JudgeTitleOrText(HtmlNode htmlTable)
        {
            PreTable header = new PreTable();
            var trs = htmlTable.SelectNodes("./tr");
            if (trs == null)
            {
                trs = htmlTable.SelectNodes("tbody/tr");
            }
            if (trs == null)
                return null;

            //if (trs.Count == 1 && trs[0].SelectNodes("td").Count == 1)
            //{
            //var td = trs[0].SelectNodes("td");
            string text = trs[0].InnerText.Replace(" ", "");
            string[] texts = trs[0].InnerText.Split('\n');
            texts = texts.Where(t => !t.IsEmpty()).ToArray();
            if (texts.Length < 1)
                return null;

            if (texts.Length == 1)
            {
                if (texts[0].IsEmpty())
                {
                    header.Content = texts[0];
                }
                else
                {
                    header.Title = texts[0].Replace("\r\n", "").Replace("\t", "").Trim();
                    header.Content = "";
                }
            }
            else
            {
                header.Title = texts[texts.Length - 1];
                for (int i = 0; i < texts.Length - 1; i++)
                {
                    header.Content += texts[i];
                }
            }
            //}
            return header;
        }


    }

    public class Table
    {
        Func<HtmlNodeCollection, int> GetMax = (nodes) => {
            int max = 0;
            foreach (var node in nodes)
            {
                var tds = node.SelectNodes("./td");
                if (tds.Count > max)
                    max = tds.Count;

            }
            return max;
        };

        public Table(HtmlNode htmlTable)
        {
            var trs = htmlTable.SelectNodes("./tr");
            if (trs == null)
            {
                trs = htmlTable.SelectNodes("tbody/tr");
            }
            if (trs == null)
                return;

            int rowCount = trs.Count;
            int columnCount = GetMax(trs);
            Matrix = new string[rowCount, columnCount];
            for (int i = 0; i < rowCount; i++)
            {
                int j = 0;
                var tr = trs[i];
                var tds = tr.SelectNodes("./td");
                foreach (var td in tds)
                {

                    int colspan = td.GetAttributeValue("colspan", "0").ToInt();
                    int rowspan = td.GetAttributeValue("rowspan", "0").ToInt();
                    while (Matrix[i, j] != null && Matrix[i, j].Contains("|"))
                    {
                        j = j + 1;
                    }
                    Matrix[i, j] = td.InnerText.Replace("\r\n", "").Replace("\t", "").Trim().Replace("&nbsp;", "");
                    if (colspan > 0)
                    {
                        for (int row = 1; row < colspan; row++)
                        {
                            Matrix[i, j + row] = "||";
                        }
                    }
                    else if (rowspan > 0)
                    {
                        for (int col = 1; col < rowspan; col++)
                        {
                            Matrix[i + col, j] = "|";
                        }
                    }
                    j += 1;
                }
            }
        }

        //public PreTable Header { get; private set; } 

        /// <summary>
        /// 表格矩阵（跨行用“||”占位，跨列用“|”占位）
        /// </summary>
        public string[,] Matrix { get; set; }

    }

    public class PreTable
    {
        /// <summary>
        /// 正文
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
    }
}
