using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfServiceTest
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 Service1.svc 或 Service1.svc.cs，然后开始调试。
    public class ServiceTest : IServiceTest
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }



        public class PeopleInfo : IPeopleInfo
        {
            public int GetAge(string name)
            {
                List<People> peopleList = DataFactory.GetPeopleList();
                return peopleList.Find(t => t.Name == name).Age;
            }
        }

        public class DataFactory
        {
            public static List<People> GetPeopleList()
            {
                List<People> peopleList = new List<People>();
                peopleList.AddRange(new People[] {
                new People{Name="tjm",Age=18},
                new People{Name="lw",Age=20},
                new People{Name="tj",Age=22},
            });
                return peopleList;
            }
        }
    }
}
