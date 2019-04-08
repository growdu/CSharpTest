using System;


/***********************************************************************
 * 文 件 名：ConsoleProgress.cs
 * CopyRight(C) 2019-2029 https://www.github.com/growdu
 * 创 建 人：growdu
 * 创建日期：2019-04-02
 * 修 改 人：
 * 修改日期：
 * 描    述：控制台进度条
 ***********************************************************************/
namespace Util
{

    public enum ProgressBarType
    {
        /// <summary>
        /// 字符
        /// </summary>
        Character,

        /// <summary>
        /// 彩色
        /// </summary>
        Multicolor
    }

    public class ProgressBar
    {
        public int Left
        {
            get;
            set;
        }

        public int Top { get; set; }

        public int Width { get; set; }

        public int Value { get; set; }

        public ProgressBarType BarType { get; set; }

        private ConsoleColor colorBack;

        private ConsoleColor colorFore;

        public ProgressBar() : this(Console.CursorLeft, Console.CursorTop)
        {

        }

        public ProgressBar(int left, int top, int width = 50,
            ProgressBarType ProgressBarType = ProgressBarType.Multicolor)
        {
            this.Left = left;
            this.Top = top;
            this.Width = width;
            this.BarType = ProgressBarType;
            // 清空显示区域；
            Console.SetCursorPosition(Left, Top);
            for(int i = left; ++i < Console.WindowWidth;)
            {
                Console.WriteLine("");
            }
            if (this.BarType == ProgressBarType.Multicolor)
            {//绘制进度条背景
                colorBack = Console.BackgroundColor;
                Console.SetCursorPosition(Left, Top);
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                for (int i = 0; ++i <= width;)
                {
                    Console.Write(" ");
                }
                Console.BackgroundColor = colorBack;
            }
            else
            {
                Console.SetCursorPosition(left, top);
                Console.Write("[");
                Console.SetCursorPosition(left + width - 1, top);
                Console.Write("]");
            }
        }

        public int Display(int value)
        {
            return Display(value, null);
        }

        public int Display(int value, string msg)
        {
            if (this.Value != value)
            {
                this.Value = value;
                if (this.BarType == ProgressBarType.Multicolor)
                {
                    // 保存背景色与前景色；
                    colorBack = Console.BackgroundColor;
                    colorFore = Console.ForegroundColor;
                    //绘制进度条进度
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(this.Left, this.Top);
                    Console.Write(new string(' ', (int)Math.Round(this.Value / (100.0 / this.Width))));
                    Console.BackgroundColor = colorBack;

                    // 更新进度百分比,原理同上. 
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(this.Left + this.Width + 1, this.Top);
                    if (string.IsNullOrWhiteSpace(msg))
                    {
                        Console.Write("{0}%", this.Value);
                    }
                    else
                    {
                        Console.Write(msg);
                    }
                    Console.ForegroundColor = colorFore;
                }
            }
            return value;
        }

    }

}
