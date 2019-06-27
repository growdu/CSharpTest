using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineParse
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
            .WithParsed<Options>(opts => RunOptionsAndReturnExitCode(opts))
            .WithNotParsed<Options>((errs) => HandleParseError(errs));
            Console.ReadKey();
        }

        static int RunOptionsAndReturnExitCode(Options options)
        {
            
            return 0;
        }

        static void HandleParseError(IEnumerable<Error> errs)
        {

        }

    }


    class Options
    {
        [Option('s', "source", MetaValue = "FILE", Required = true, HelpText = "输入数据文件")]
        public string InputFile { get; set; }

        [Option('t', "target", MetaValue = "FILE", Required = false, HelpText = "输出数据文件")]
        public string OutputFile { get; set; }


        [Option('s', "start-time", MetaValue = "STARTTIME", Required = true, HelpText = "开始时间")]
        public DateTime StartTime { get; set; }

        [Option('e', "end-time", MetaValue = "ENDTIME", Required = true, HelpText = "结束时间")]
        public DateTime EndTime { get; set; }


    }
}
