using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SerializableTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Programmer> list = new List<Programmer>();
            list.Add(new Programmer("李志伟", true, "C#"));
            list.Add(new Programmer("Coder2", false, "C++"));
            list.Add(new Programmer("Coder3", true, "Java"));
            //文件名称与路径
            string fileName = @"C:\users\duanys\desktop\Programmers.dat";
            string file = fileName.Replace('\\','/');
            Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            //创建二进制序列化器
            BinaryFormatter binFormat = new BinaryFormatter();
            binFormat.Serialize(fStream, list);

            //使用二进制反序列化对象
            list.Clear();//清空列表
            fStream.Position = 0;//重置流位置
            //反序列化对象
            list = (List<Programmer>)binFormat.Deserialize(fStream);
            foreach (Programmer p in list)
            {
                Console.WriteLine(p);
            }
            Console.Read();

        }
    }
}
