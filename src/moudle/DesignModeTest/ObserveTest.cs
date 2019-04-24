using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignModeTest
{
    /***********************************************************************
* 文 件 名：
* CopyRight(C) 2019-2029 https://www.github.com/growdu
* 创 建 人：growdu
* 创建日期：2019-04-24
* 修 改 人：
* 修改日期：
* 描    述：观察者模式（热水器、加热器、报警器实例）（不符合.NET框架规范）
***********************************************************************/
    class ObserveTest
    {

        static void Main(string[] args)
        {//将发布者和订阅者串联起来的方法
            //发布者（拥有事件）
            Heater heater = new Heater();
            //订阅者（拥有具体的执行方法）
            Alarm alarm = new Alarm();
            //订阅事件
            heater.BoilEvent += alarm.MakeAlert;
            heater.BoilEvent += Display.ShowMsg;
            heater.BoilWater();
        }

        /// <summary>
        /// 热水器（发布者，事件属于发布者，发布者规定了订阅方法的签名）
        /// </summary>
        public class Heater
        {
            private int temperature;
            //定义委托
            public  delegate void BoilHandler(int param);
            //定义事件
            public event BoilHandler BoilEvent;

            public void BoilWater()
            {
                for(int i = 0; i < 100; i++)
                {
                    temperature = i;

                    if (temperature > 95)
                    {
                        if (BoilEvent != null)
                        {
                            BoilEvent(temperature);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 警报器（订阅者，订阅者是当某一事件发生时，需要执行什么行为，具体表现为与事件委托签名一致的方法）
        /// </summary>
        public class Alarm
        {
            public void MakeAlert(int param)
            {
                Console.WriteLine("Alarm：嘀嘀嘀，水已经 {0} 度了：", param);
            }
        }

        /// <summary>
        /// 显示器（订阅者）
        /// </summary>        
        public class Display
        {
            public static void ShowMsg(int param) {
                //静态方法                        
                Console.WriteLine("Display：水快烧开了，当前温度：{0}度。", param);
            }
        }

    }
}
