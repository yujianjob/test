using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.ApplicationServer.Caching;

namespace LazyAtHome.Core.Helper
{
    /// <summary>
    /// 缓存服务
    /// </summary>
    public static class CacheHelper
    {
        private static DataCache _Cache = null;
        static DataCache Cache
        {
            get
            {
                if (_Cache == null)
                {
                    Infrastructure.Cache.AppFabricService cacheService = new Infrastructure.Cache.AppFabricService();
                    _Cache = cacheService.GetCache();
                }
                return _Cache;
            }
        }

        #region Put相关操作

        public static void Put(string key, object obj)
        {
            Cache.Put(key, obj);
        }

        public static void Put(string key, object obj, TimeSpan ts)
        {
            Cache.Put(key, obj, ts);
        }

        public static void Put(string key, object obj, string region)
        {
            Cache.Put(key, obj, region);
        }

        public static void Put(string key, object obj, TimeSpan ts, string region)
        {
            Cache.Put(key, obj, ts, region);
        }

        public static bool PutTags(string key, object obj, string tags)
        {
            if (string.IsNullOrEmpty(tags))
                return false;

            DataCacheTag[] dcTag = ConvertTag(tags);
            Cache.Put(key, obj, dcTag);
            return true;
        }

        public static bool PutTags(string key, object obj, string tags, string regionName)
        {
            if (string.IsNullOrEmpty(tags))
                return false;

            DataCacheTag[] dcTag = ConvertTag(tags);
            Cache.Put(key, obj, dcTag, regionName);
            return true;
        }

        public static bool PutTags(string key, object obj, TimeSpan ts, string tags)
        {
            if (string.IsNullOrEmpty(tags))
                return false;

            DataCacheTag[] dcTag = ConvertTag(tags);
            Cache.Put(key, obj, ts, dcTag);
            return true;
        }

        public static bool PutTags(string key, object obj, TimeSpan ts, string tags, string regionName)
        {
            if (string.IsNullOrEmpty(tags))
                return false;

            DataCacheTag[] dcTag = ConvertTag(tags);
            Cache.Put(key, obj, ts, dcTag, regionName);
            return true;
        }

        #endregion

        /// <summary>
        /// 从字符串生成DataCacheTag对象数组
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        private static DataCacheTag[] ConvertTag(string tags)
        {
            var arrTags = tags.Split(',');
            DataCacheTag[] dcTag = new DataCacheTag[arrTags.Length];
            for (int i = 0; i < arrTags.Length; i++)
                dcTag[i] = new DataCacheTag(arrTags[i]);
            return dcTag;
        }

        public static object Get(string key)
        {
            return Cache.Get(key);
        }

        public static object Get(string key, string regionName)
        {
            return Cache.Get(key, regionName);
        }

        /// <summary>
        /// 检索匹配Tag的数据,Tag只能为一个值
        /// </summary>
        /// <param name="tag">Tag只能为一个值</param>
        /// <param name="regionName"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, object>> GetByTag(string tag, string regionName)
        {
            return Cache.GetObjectsByTag(new DataCacheTag(tag), regionName);
        }

        /// <summary>
        /// 检索匹配所有Tag的数据,Tags以逗号分隔
        /// </summary>
        /// <param name="tags">Tags以逗号分隔</param>
        /// <param name="regionName"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, object>> GetByAllTags(string tags, string regionName)
        {
            return Cache.GetObjectsByAllTags(ConvertTag(tags), regionName);
        }

        /// <summary>
        /// 检索匹配所有Tag中的任意一个即可,Tags以逗号分隔
        /// </summary>
        /// <param name="tags">Tags以逗号分隔</param>
        /// <param name="regionName"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, object>> GetByAnyTag(string tags, string regionName)
        {
            return Cache.GetObjectsByAnyTag(ConvertTag(tags), regionName);
        }

        public static bool Remove(string key)
        {
            return Cache.Remove(key);
        }

        public static bool Remove(string key, string region)
        {
            return Cache.Remove(key, region);
        }

        #region 分区相关操作

        /// <summary>
        /// 创建分区
        /// </summary>
        /// <param name="regionName"></param>
        /// <returns></returns>
        public static bool CreateRegion(string regionName)
        {
            return Cache.CreateRegion(regionName);
        }

        /// <summary>
        /// 清空分区
        /// </summary>
        /// <param name="regionName"></param>
        public static void ClearRegion(string regionName)
        {
            Cache.ClearRegion(regionName);
        }

        /// <summary>
        /// 删除分区
        /// </summary>
        /// <param name="regionName"></param>
        /// <returns></returns>
        public static bool RemoveRegion(string regionName)
        {
            return Cache.RemoveRegion(regionName);
        }

        #endregion
    }
}
