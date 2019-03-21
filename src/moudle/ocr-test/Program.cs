using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ocr_test
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            frmOcrTest test = new frmOcrTest();
            test.ShowDialog();
        }
    }
}
