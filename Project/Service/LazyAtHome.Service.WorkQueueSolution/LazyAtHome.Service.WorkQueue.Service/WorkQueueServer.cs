using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using LazyAtHome.Core.Infrastructure.WorkQueue;
using System.IO;
using System.Reflection;

namespace LazyAtHome.Service.WorkQueue.Service
{
    public class WorkQueueServer
    {
        internal static Hashtable PluginList = new Hashtable();

        public void DoWork(object item)
        {

        }

        public void FindPlugin()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "LazyAtHome.Service.WorkQueue.Plugin.dll";
            Type PB = typeof(PluginBase);

            //加载dll
            Assembly pluginAssembly = Assembly.LoadFrom(path);

            //判断当前程序集内是否包含实现了PluginBase接口的类型
            Type[] types = pluginAssembly.GetTypes();

            foreach (Type pluginType in types)
            {
                if (pluginType.IsSubclassOf(PB))
                {
                    PluginBase newObj = (PluginBase)Activator.CreateInstance(pluginType);                    
                    PluginList.Add(newObj.TypeName, newObj);
                }
            }
        }
    }
}