using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.DataHelper;

namespace CommandLineParse
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(Run);
            Console.ReadKey();
        }

        static void Run(Options options)
        {
            if (options.File.IsEmpty())
            {
                Log.Error("error.");
            }
            else
            {
                Log.Info(options.File);
            }
        }

        static void HandleParseError(IEnumerable<Error> errs)
        {
            ;
        }

    }


    class Options
    {
        [Option('f', "file", Required = true, HelpText = "需要处理的文件。")]
        public string File { get; set; }

        [Option('o', "override", Required = false, HelpText = "是否覆盖原有文件。")]
        public bool Override { get; set; }


    }
}
