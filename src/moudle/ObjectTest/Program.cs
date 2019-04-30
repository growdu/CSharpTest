using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// C sharp 利用反射遍历对象属性测试
/// </summary>
namespace ObjectTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int? a = null;
            Test model = new Test();
            Console.WriteLine(model.ToString());
            ForeachClassProperties<Test>(model);
            Test test = new Test();
            foreach (var propertyInfo in typeof(Test).GetProperties())
            {
                Console.WriteLine(propertyInfo.Name);
            }
            Console.ReadKey();
        }


        /// <summary>
        /// C#反射遍历对象属性
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="model">对象</param>
        public static void ForeachClassProperties<T>(T model)
        {
            Type t = model.GetType();
            PropertyInfo[] PropertyList = t.GetProperties();
            foreach (PropertyInfo item in PropertyList)
            {
                string name = item.Name;
                object value = item.GetValue(model, null);
            }
        }


        public class Test:ToS
        {
            public Test()
            {
                name = "test";
                password = "test";
                age = 18;
            }

            public string name { get; set; }

            public string password { get; set; }

            public int age { get; set; }

            //public override string ToString()
            //{
            //    StringBuilder sb = new StringBuilder();
            //    Type t = this.GetType();
            //    PropertyInfo[] PropertyList = t.GetProperties();
            //    foreach (PropertyInfo item in PropertyList)
            //    {
            //        object value = item.GetValue(this, null);
            //        sb.Append(value.ToString());
            //    }
            //    return sb.ToString();
            //}
        }

        public abstract class ToS
        {
            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                Type t = this.GetType();
                PropertyInfo[] PropertyList = t.GetProperties();
                foreach (PropertyInfo item in PropertyList)
                {
                    object value = item.GetValue(this, null);
                    sb.Append(value.ToString());
                }
                return sb.ToString();
            }
        }

    }
}
