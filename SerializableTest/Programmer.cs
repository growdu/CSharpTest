using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerializableTest
{
    [Serializable]
    public class Programmer : Person

    {

        private string Language;//编程语言

        public Programmer(string name, bool sex, string language) : base(name, sex)

        {

            this.Language = language;

        }

        public override string ToString()

        {

            return base.ToString() + "\t编程语言：" + this.Language;

        }

    }
}
