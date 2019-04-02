using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttributeTest
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class HelpAttribute : Attribute
    {

    }

    [Help()]
    public class AnyClass
    {

    }

    [HelpAttribute()]
    public class OtherClass
    {

    }

    public class OtherHelpAttibute : Attribute
    {
        protected string description;
        public OtherHelpAttibute(string description)
        {
            this.description = description;
        }

        public string Description
        {
            get{ return description; }
        }
    }

    [OtherHelpAttibute("this is an attribute test")]
    public class SomeClass
    {

    }
}
