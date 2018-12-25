using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerializableTest
{
    [Serializable]
    public class Person

    {

        private string Name;//姓名

        private bool Sex;//性别，是否是男

        public Person(string name, bool sex)

        {

            this.Name = name;

            this.Sex = sex;

        }

        public override string ToString()

        {

            return "姓名：" + this.Name + "\t性别：" + (this.Sex ? "男" : "女");

        }

    }

}
