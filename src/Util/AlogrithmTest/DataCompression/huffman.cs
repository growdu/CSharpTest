using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 数据压缩
/// </summary>
namespace DC5
{
    /// <summary>
    /// 哈夫曼树:n个权值作为n个叶子结点，构造的带权路径长度最小的二叉树--》哈夫曼编码
    /// </summary>
    class Huff
    {
        /// <summary>
        /// 数据，此处为字符串
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// 某个字符出现的频率
        /// </summary>
        public int Frequency { get; set; }

        /// <summary>
        /// 右子树
        /// </summary>
        public Huff RightChild { get; set; }

        /// <summary>
        /// 左子树
        /// </summary>
        public Huff LeftChild { get; set; }

        /// <summary>
        /// 构造哈夫曼节点
        /// </summary>
        /// <param name="data">节点数据</param>
        /// <param name="frequency">频率（权重）</param>
        public Huff(string data, int frequency)
        {
            Data = data;
            Frequency = frequency;
        }

        /// <summary>
        /// 构建哈夫曼树
        /// </summary>
        /// <param name="leftChild"></param>
        /// <param name="rightChild"></param>
        public Huff(Huff leftChild, Huff rightChild)
        {
            LeftChild = leftChild;
            RightChild = rightChild;
            Data = leftChild.Data + ":" + rightChild.Data;
            Frequency = leftChild.Frequency + rightChild.Frequency;
        }
    }

    internal class Man
    {
        public List<string> Codec { get; set; } = new List<string>();

        public List<string> Data { get; set; } = new List<string>();

    }

    class Program
    {
        static void Run(string[] args)
        {
            IList<Huff> list = new List<Huff>();
            Console.Write("Enter String: ");
            var str = Console.ReadLine().ToLower().Replace(" ", "#");
            Console.WriteLine("Space will be represented by #");
            int n = str.Length, count = 1, pos = 0;
            var flag = false;
            var d = new int[n];
            var d1 = new int[n];
            var c1 = new char[n];
            var c = str.ToCharArray();
            for (var i = 0; i < n; i++)
            {
                for (var j = i + 1; j < n; j++)
                {
                    if (c[i] == c[j])
                    {
                        count++;
                    }
                }
                d[i] = count;
                count = 1;
            }
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    if (c[i] == c1[j])
                    {
                        flag = true;
                    }
                }
                if (!flag)
                {
                    c1[pos] = c[i];
                    d1[pos] = d[i];
                    pos++;
                }
                flag = false;
            }
            for (var i = 0; i < pos; i++)
            {
                Console.Write("{0}\t", c1[i]);
            }
            Console.WriteLine();
            for (var i = 0; i < pos; i++)
            {
                Console.Write("{0}\t", d1[i]);
            }
            int temp;
            char ch;

            // sorts array d1 (frequencies) and array c1
            for (var i = 0; i < pos; i++)
            {
                for (var j = i + 1; j < pos; j++)
                {
                    if (d1[i] > d1[j])
                    {
                        temp = d1[i];
                        ch = c1[i];
                        d1[i] = d1[j];
                        c1[i] = c1[j];
                        d1[j] = temp;
                        c1[j] = ch;
                    }
                }
            }
            Console.WriteLine("\n\nAfter Sorting: ");
            for (var i = pos - 1; i >= 0; i--)
            {
                Console.Write("{0}\t", c1[i]);
            }
            Console.WriteLine();
            for (var i = pos - 1; i >= 0; i--)
            {
                Console.Write("{0}\t", d1[i]);
            }

            // computes the information content in bits
            double infoBit = 0;
            for (var i = 0; i < pos; i++)
            {
                double prob, si;
                prob = d1[i] / (double)n;
                si = -(Math.Log(prob) / Math.Log(2));
                infoBit += si * d1[i];
            }
            Console.WriteLine("\nTotal Information Count: {0}", infoBit + " Bits");
            Console.WriteLine("Number of Bits required before Compression: {0}", n * 8);
            var array = new int[pos];

            // copies array d1 in 'array'
            for (var i = 0; i < pos; i++)
            {
                array[i] = d1[i];
            }

            // fills the list 'list' with Huff-objects
            for (var i = 0; i < pos; i++)
            {
                list.Add(new Huff(c1[i].ToString(), array[i]));
            }
            var stack = GetSortedStack(list);
            while (stack.Count > 1)
            {//递归构造哈夫曼树
                var leftChild = stack.Pop();
                var rightChild = stack.Pop();
                var parentNode = new Huff(leftChild, rightChild);
                stack.Push(parentNode);
                stack = GetSortedStack(stack.ToList<Huff>());
            }

            // generated huffman tree
            var parentNode1 = stack.Pop();
            var man = new Man();

            // generates and displays the huffman code
            Console.WriteLine("\nHuffman Code:");
            GenerateCode(parentNode1, "", man);
            var cStr = " ";
            foreach (var item in str)
            {
                var index = man.Data.IndexOf(item.ToString());
                cStr += man.Codec.ElementAt(index);
            }
            var hfBits = 0;
            foreach (var item in cStr.Replace(" ", ""))
            {
                hfBits++;
            }
            Console.WriteLine("Huffman Bits: " + hfBits);
            Console.WriteLine("\nCoded String: ");
            Console.WriteLine(cStr.Replace(" ", ""));
            Console.ReadKey();
        }

        /// <summary>
        /// 根据权重对哈夫曼节点排序，并入栈
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static Stack<Huff> GetSortedStack(IList<Huff> list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                for (var j = i + 1; j < list.Count; j++)
                {
                    if (list[i].Frequency > list[j].Frequency)
                    {
                        var tempNode = list[j];
                        list[j] = list[i];
                        list[i] = tempNode;
                    }
                }
            }
            var stack = new Stack<Huff>();
            for (var j = 0; j < list.Count; j++)
            {
                stack.Push(list[j]);
            }

            return stack;
        }

        /// <summary>
        /// 生成哈夫曼树（取两棵根结点权值最小的树作为左右子树构造一棵新的二叉树，置新的二叉树的根结点的权值为左右子树根结点的权值之和）
        /// 引向左子树的分支为0，右子树的分支为1
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="code"></param>
        /// <param name="man"></param>
        public static void GenerateCode(Huff parentNode, string code, Man man)
        {
            if (parentNode != null)
            {
                GenerateCode(parentNode.LeftChild, code + "0", man);
                if (parentNode.LeftChild == null && parentNode.RightChild == null)
                {
                    Console.WriteLine(parentNode.Data + "\t" + code);
                    man.Codec.Add(code);
                    man.Data.Add(parentNode.Data);
                }
                GenerateCode(parentNode.RightChild, code + "1", man);
            }
        }
    }
}