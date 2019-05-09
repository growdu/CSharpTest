using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlTest
{
    class Redis
    {
        static void Main(string[] args)
        {
            var client = new RedisClient("127.0.0.1", 6379);
            string name = client.Get<string>("name");
        }
    }
}
