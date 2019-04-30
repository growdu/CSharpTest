using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectTest
{
    class ExtendMethodTest
    {
       static void Main(string[] args)
        {
            User user = new User();
            user.id = "1";
            user.name = "test";
            user.password = "xx";
            Console.WriteLine(user.toString());
        }
    }

    public class User
    {
        public string id { get; set; }

        public string name { get; set; }

        public string password { get; set; }
    }

    public static class UserExtend
    {
        public static string toString(this User user)
        {
            return user.id + user.name + user.password;
        }
    }

}
