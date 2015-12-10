using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alan.Utils.Session.Core
{
    /// <summary>
    /// Session核心容器类
    /// </summary>
    public class SessionContainer
    {
        /// <summary>
        /// 容器
        /// </summary>
        private Dictionary<string, ContainerModel<object>> Cache { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public SessionContainer()
        {
            this.Empty();
        }

        //private object this[string key]
        //{
        //    get { return this.Cache[key]; }
        //    set { this.Cache[key] = value; }
        //}

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T">返回的数据</typeparam>
        /// <param name="key">键</param>
        /// <param name="fail">失败时的替代值</param>
        /// <returns>缓存的数据</returns>
        public T Get<T>(string key, T fail = default(T))
            where T : class
        {
            if (String.IsNullOrWhiteSpace(key)) { throw new ArgumentNullException("参数 key 不能为空."); }
            if (!this.Cache.ContainsKey(key)) return fail;

            var value = this.Cache[key];
            if (value == null) return fail;
            if (DateTime.Now > value.ExpireDate) return fail;
            if (value.Model == null) return fail;
            return (T)value.Model;
        }

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void Set(string key, object value)
        {
            this.Set(key, value, DateTime.MaxValue);
        }

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expire">绝对过期日期</param>
        public void Set(string key, object value, DateTime expire)
        {
            if (String.IsNullOrWhiteSpace(key)) { throw new ArgumentNullException("参数 key 不能为空."); }
            var model = new ContainerModel<object>() { ExpireDate = expire, Model = value };
            this.Cache[key] = model;
        }

        /// <summary>
        /// 清空所有数据
        /// </summary>
        public void Empty()
        {
            this.Cache = new Dictionary<string, ContainerModel<object>>();
        }

        /// <summary>
        /// 清除过期数据
        /// </summary>
        public void Clean()
        {
            this.Cache.Where(dict => dict.Value != null && DateTime.Now > dict.Value.ExpireDate)
                .Select(dict => dict.Key)
                .ToList()
                .ForEach(k => this.Clean(k));
        }

        /// <summary>
        /// 清除指定键的数据
        /// </summary>
        /// <param name="key">键</param>
        public void Clean(string key)
        {
            if (this.Cache.ContainsKey(key))
            {
                this.Cache.Remove(key);
            }
        }

        /// <summary>
        /// 内部容器
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        private class ContainerModel<TModel>
        {
            /// <summary>
            /// 过期时间
            /// </summary>
            public DateTime ExpireDate { get; set; }

            /// <summary>
            /// 数据
            /// </summary>
            public TModel Model { get; set; }
        }
    }
}