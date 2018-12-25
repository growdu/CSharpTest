using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GcTest
{
    class GCExample:IDisposable
    {
        public string gcName { get; set; }

        public void Test()
        {
            int hash = gcName.GetHashCode();
        }

        public void Dispose()
        {
            gcName = null;
        }
    }
}
