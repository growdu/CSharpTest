using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.DataHelper;
using Xceed.Words.NET;

namespace TCPTest.http
{
    class HtmlToDocx
    {

        public static void SaveAsDocx(List<HtmlNode> tables)
        {
            string fileName = Guid.NewGuid() + ".docx";
            SaveAsDocx(tables, fileName);
        }

        public static void SaveAsDocx(List<HtmlNode> tables, string fileName)
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
            try
            {
                using (DocX document = DocX.Create(fileName))
                {
                    #region 识别表格
                    foreach (var htmlTable in tables)
                    {
                        var trs = htmlTable.SelectNodes("./tr");
                        if (trs == null)
                        {
                            trs = htmlTable.SelectNodes("tbody/tr");
                        }
                        if (trs == null)
                            continue;

                        int rowCount = trs.Count;
                        int columnCount = GetMax(trs);
                        if (rowCount == 1 && columnCount == 1||(rowCount*columnCount<=2))
                        {
                            document.InsertParagraph(RemoveSplitChar(RemoveSplitChar(htmlTable.InnerText)));
                        }
                        else
                        {
                            var docxTable = document.AddTable(rowCount, columnCount);
                            docxTable.AutoFit = AutoFit.Contents;
                            try
                            {
                                for (int i = 0; i < rowCount; i++)
                                {
                                    int j = 0;
                                    var tr = trs[i];
                                    var tds = tr.SelectNodes("./td");
                                    foreach (var td in tds)
                                    {

                                        int colspan = td.GetAttributeValue("colspan", "0").ToInt();
                                        int rowspan = td.GetAttributeValue("rowspan", "0").ToInt();
                                        while (docxTable.Rows[i].Cells[j].Paragraphs[0].Text.Contains("|"))
                                        {
                                            j = j + 1;
                                        }
                                        docxTable.Rows[i].Cells[j].Paragraphs[0].Append(RemoveSplitChar(td.InnerText));
                                        if (colspan > 0)
                                        {
                                            for (int row = 1; row < colspan; row++)
                                            {
                                                docxTable.Rows[i].Cells[j + row].Paragraphs[0].Append("||");
                                            }
                                        }
                                        else if (rowspan > 0)
                                        {
                                            for (int col = 1; col < rowspan; col++)
                                            {
                                                docxTable.Rows[i + col].Cells[j].Paragraphs[0].Append("|");
                                            }
                                        }
                                        j += 1;
                                    }

                                }
                                #endregion

                                #region 处理跨行
                                for (int i = 0; i < rowCount; i++)
                                {
                                    for (int j = 0; j < columnCount; j++)
                                    {
                                        Console.WriteLine("i:" + i + ",j:" + j);
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
                                        Console.WriteLine("i:" + i + ",j:" + j);
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

                                document.InsertTable(docxTable);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }

                        }

                    }
                    document.Save();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        static bool HasTable(HtmlNode node)
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

        public static string RemoveSplitChar(string input)
        {
            return input.Replace("\r\n", "").Replace("\n", "").Replace("\t", "").Replace(" ", "").Replace("&nbsp;", " ");
        }

    }
}
