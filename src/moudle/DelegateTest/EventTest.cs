using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegateTest
{
 /***********************************************************************
* 文 件 名：
* CopyRight(C) 2019-2029 https://www.github.com/growdu
* 创 建 人：growdu
* 创建日期：2019-04-24
* 修 改 人：
* 修改日期：
* 描    述：观察者模式（热水器、加热器、报警器实例）（符合.NET框架规范）
***********************************************************************/
    class EventTest
    {
        static void Main(string[] args)
        {
            Heater heater = new Heater();
            Alarm alarm = new Alarm();
            heater.Boiled += alarm.MakeAlert;        
            //注册方法                       
            heater.Boiled +=alarm.MakeAlert;
            //为匿名对象注册方法                        
            heater.Boiled += new Heater.BoiledEventHandler(alarm.MakeAlert);
            //注册静态方法
            heater.Boiled += Display.ShowMsg;
            heater.BoiledWater();
            Console.ReadKey();
        }

        class Heater
        {
            private int temperature;
            public string type = "RealFire 001";             
            public string area = "China Xian";
            //事件封装在类里，相当于属性，委托定义相当于私有属性，在类内部访问；事件定义提供对外访问
            public delegate void BoiledEventHandler(object sender, BoliedEventArgs e);
            public event BoiledEventHandler Boiled;

            /// <summary>
            /// 事件数据
            /// </summary>
            public class BoliedEventArgs : EventArgs
            {
                public readonly int temperature;
                public BoliedEventArgs(int temperature)
                {
                    this.temperature = temperature;
                }
            }

            // 可以供继承自 Heater 的类重写，以便继承类拒绝其他对象对它的监视
            protected virtual void OnBolied(BoliedEventArgs e)
            {
                Boiled?.Invoke(this, e);
                //if (Boiled != null)
                //{
                //    Boiled(this,e);
                //}
            }

            public void BoiledWater()
            {
                for (int i=0;i<100;i++)
                {
                    temperature = i;
                    if (temperature > 95)
                    {
                        BoliedEventArgs e = new BoliedEventArgs(temperature);
                        OnBolied(e);
                    }
                }
            }
        }

        // 警报器        
         class Alarm
        {
            public void MakeAlert(Object sender, Heater.BoliedEventArgs e)
            {
                Heater heater = (Heater)sender;                   
                //访问 sender 中的公共字段                        
                Console.WriteLine("Alarm：{0} - {1}: ", heater.area, heater.type);
                Console.WriteLine("Alarm: 嘀嘀嘀，水已经 {0} 度了：", e.temperature);
                Console.WriteLine();
            }
        }

        // 显示器        
        class Display
        {
            public static void ShowMsg(Object sender, Heater.BoliedEventArgs e)
            {
                //静态方法                        
                Heater heater = (Heater)sender;
                Console.WriteLine("Display：{0} - {1}: ", heater.area, heater.type);
                Console.WriteLine("Display：水快烧开了，当前温度：{0}度。", e.temperature);
                Console.WriteLine();
            }
        }

    }
}
