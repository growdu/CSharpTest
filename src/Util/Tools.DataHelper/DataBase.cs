using Deduce.Common.Entity;
using Deduce.Common.Utility;
using Deduce.DMIP.Business.Components;
using Deduce.DMIP.Sys.SysData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Tools.DataHelper
{
 /***********************************************************************
 * 文 件 名：
 * CopyRight(C) 2019-2029 数据业务线开发部
 * 创 建 人：duanys
 * 创建日期：2019-05-16
 * 修 改 人：
 * 修改日期：
 * 描    述：非客户端连接聚源相关数据库
 ***********************************************************************/
    public class DataBase:DataClass
    {
        static DataService _dataSrv = null;
        static DateTime _lastRunTime;
        private string _error = "";

        public DataBase()
        {
            Check();
        }

        public void Check()
        {
            if (IsQuit(true))
            {
                DataService.Quit();
            }
        }

        #region 数据库连接
        static DataService DataService
        {
            get
            {
                if (_dataSrv == null)
                {
                    _dataSrv = new DataService(ServiceSetting.EnvironmentType, ServiceSetting.IP);
                }
                return _dataSrv;
            }
        }

        private bool IsQuit(bool auto = false, string sessionID = "")
        {
            if (!auto)
                return false;
            _lastRunTime = DateTime.Now;
            ResultData<bool> result = DataService.Login(ServiceSetting.City, sessionID);
            if (result.Result)
            {
                CheckQuit();
                return false;
            }
            _error = "网络连接失败! ";
            return true;
        }

        private bool CheckQuit()
        {
            if (DataService.IsClosed)
            {
                Utils.WriteLog("网络连接失败退出！！！");
                return true;
            }

            TimeSpan ts1 = new TimeSpan(_lastRunTime.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);
            TimeSpan ts = ts2.Subtract(ts1).Duration();
            int temp = ts.Minutes;
            //无任务运行时的间隔时间，默认30分钟
            return (temp > 30);//"超过30分钟无任务执行，退出！！！"
        }
        #endregion

        #region 数据库访问
        /// <summary>
        /// 获取数据库中表的字段结构
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="conType">数据库类型名称</param>
        /// <returns></returns>
        public DataTable GetStructure(string tableName, ConnectType conType = ConnectType.Jyprime)
        {
            return _data.GetStructure(tableName, GlobalData.CommonMenuID, conType);
        }

        /// <summary>
        /// 生成1个jyp或者中间库的唯一性字段id
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <returns></returns>
        public string GetID(string tableName)
        {
            return GetIDs(1, tableName)[0];
        }

        /// <summary>
        /// 生成指定数量的jyp或者中间库的唯一性字段id
        /// </summary>
        /// <param name="num">所需id数量</param>
        /// <param name="tableName">表名称</param>
        /// <returns></returns>
        public List<string> GetIDs(int num,string tableName)
        {
            return _data.GetIDs(num, tableName);
        }

        /// <summary>
        /// 根据sql语句查询数据库
        /// </summary>
        /// <param name="q">sql语句</param>
        /// <param name="conType">数据库连接类型</param>
        /// <returns></returns>
        public DataTable GetDataTable(string q, ConnectType conType = ConnectType.Jyprime)
        {
            return _data.GetDataTable(q, GlobalData.CommonMenuID, conType);
        }

        /// <summary>
        ///采用CURD更新数据库数据
        /// </summary>
        /// <param name="dt">所需更新的数据</param>
        /// <param name="tableName">表名</param>
        /// <param name="conType">数据库类型</param>
        /// <param name="modifyType">更新方式</param>
        /// <returns></returns>
        public bool Import(DataTable dt, string tableName,ConnectType conType = ConnectType.Jyprime,ModifyType modifyType=ModifyType.Insert)
        {
            CheckQuit();
            bool isOk = _data.DataImport(tableName, dt, modifyType, GlobalData.CommonMenuID, conType);
            if (!isOk)
            {
                Utils.WriteLog(DateTime.Now + "入" + conType.ToString() + "数据库失败。");
            }
            return isOk;
        }
        
        #endregion

    }
}
