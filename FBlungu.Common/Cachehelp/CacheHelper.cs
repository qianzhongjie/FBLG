using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enyim.Caching;
using System.Configuration;
using System.Collections;

namespace FBlungu.Common
{
    /// <summary>
    /// 页 面 名：分布式缓存<br/>
    /// 说    明：设置、获取、移除Cache<br/>
    /// 作    者：xxx<br/>
    /// 时    间：2012-12-12(神奇的数字,传说中的12K)<br/>
    /// 修 改 者：<br/>
    /// 修改时间：<br/>
    /// </summary>   
    public partial class CacheHelper
    {

        /// <summary>
        /// 全部情况 慎用
        /// </summary>
        public static void FlushAll()
        {
            using (MemcachedClient client = new MemcachedClient("enyim.com/memcached"))
            {
                client.FlushAll();
            }
        }

        /// <summary>
        /// 删除key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool RemvoeKey(string key)
        {
            using (MemcachedClient client = new MemcachedClient("enyim.com/memcached"))
            {
                return client.Remove(key);
            }
        }
        /// <summary>
        /// 往缓存插入值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public static bool SetValue(string key, object data)
        {
            using (MemcachedClient client = new MemcachedClient("enyim.com/memcached"))
            {
                return client.Store(Enyim.Caching.Memcached.StoreMode.Add, key, data);
            }
        }
        /// <summary>
        /// 时间限制
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="limtime"></param>
        /// <returns></returns>
        public static bool SetValue(string key, object data, DateTime limtime)
        {
            using (MemcachedClient client = new MemcachedClient("enyim.com/memcached"))
            {
                return client.Store(Enyim.Caching.Memcached.StoreMode.Add, key, data, limtime);
            }
        }
        /// <summary>
        /// 时间限制
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="limtime"></param>
        /// <returns></returns>
        public static bool SetValue(string key, object data, TimeSpan limtime)
        {
            using (MemcachedClient client = new MemcachedClient("enyim.com/memcached"))
            {
                return client.Store(Enyim.Caching.Memcached.StoreMode.Add, key, data, limtime);
            }
        }
        /// <summary>
        /// 更新值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateValue(string key, object data)
        {
            using (MemcachedClient client = new MemcachedClient("enyim.com/memcached"))
            {
                return client.Store(Enyim.Caching.Memcached.StoreMode.Set, key, data);
            }
            //client.CheckAndSet("", "", "");
        }
        /// <summary>
        /// 时间限制
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="limtime"></param>
        /// <returns></returns>
        public static bool UpdateValue(string key, object data, DateTime limtime)
        {
            using (MemcachedClient client = new MemcachedClient("enyim.com/memcached"))
            {
                return client.Store(Enyim.Caching.Memcached.StoreMode.Set, key, data, limtime);
            }
        }
        /// <summary>
        /// 时间限制
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="limtime"></param>
        /// <returns></returns>
        public static bool UpdateValue(string key, object data, TimeSpan limtime)
        {
            using (MemcachedClient client = new MemcachedClient("enyim.com/memcached"))
            {
                return client.Store(Enyim.Caching.Memcached.StoreMode.Set, key, data, limtime);
            }
        }
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetValue(string key)
        {
            using (MemcachedClient client = new MemcachedClient("enyim.com/memcached"))
            {
                return client.Get(key);
            }
        }
        /// <summary>
        /// 批量获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IDictionary<string, object> GetValueAll(string[] key)
        {
            using (MemcachedClient client = new MemcachedClient("enyim.com/memcached"))
            {
                return client.Get(key);
            }
        }
        /// <summary>
        /// 获取值 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetValue<T>(string key)
        {
            using (MemcachedClient client = new MemcachedClient("enyim.com/memcached"))
            {
                //var states = client.Stats();
                //    if(states.
                T t = default(T);
                t = client.Get<T>(key);
                return t;
            }
        }
    }
}
