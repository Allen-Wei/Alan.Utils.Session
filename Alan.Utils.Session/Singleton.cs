using Alan.Utils.Session.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alan.Utils.Session
{
    /// <summary>
    /// Session单例
    /// </summary>
    public class Singleton
    {
        /// <summary>
        /// 容器
        /// </summary>
        public static SessionContainer Container { get;private set; }
        static Singleton()
        {
            Container = new SessionContainer();
        }


    }
}