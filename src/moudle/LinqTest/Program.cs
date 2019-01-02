using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var obj = new { Guid.Empty, myTitle = "匿名类型", myOtherParam = new int[] { 1, 2, 3, 4 } };
            Console.WriteLine(obj.Empty);//另一个对象的属性名字，被原封不动的拷贝到匿名对象中来了。
            Console.WriteLine(obj.myTitle);
            Console.ReadKey();
            List<int> array = new List<int> { 1, 2, 3, 4, 5 };
            var greatThan3 =
                from a in array
                where a > 3
                select a;
            foreach(int s in greatThan3)
            {
                Console.WriteLine(s);
            }

            int[] arrays = { 1, 2, 3, 100, 7, 9, 5, 10, 22, 4, 5 };
            //筛选数据并排序
            var result =
                from a in arrays
                where a > 3 && a % 2 == 0
                orderby a
                select a;
            foreach (int s in result)
            {
                Console.WriteLine(s);
            }
            

            var res =
                from a in arrays
                group a by a % 2;
            foreach (var group in res)
            {
                Console.WriteLine("--------" + group.ToString() + "-------");
                foreach(var s in group)
                {
                    Console.WriteLine(s);
                }  
            }
            Console.ReadKey();
        }
    }
}
