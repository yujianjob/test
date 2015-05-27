using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using System.Reflection;
using LazyAtHome.Core.Infrastructure.WorkQueue;

namespace LazyAtHome.Service.WorkQueue.Service
{
    public class WorkQueueTask_ExpressNotify : TaskServer
    {
        Hashtable PluginList = new Hashtable();
        public string QUEUE_URL = string.Empty;//@"FormatName:DIRECT=TCP:192.168.27.83\Private$\Common";
        MessageQueue mq = null;

        public WorkQueueTask_ExpressNotify()
        {
            QUEUE_URL = "FormatName:DIRECT=" + System.Configuration.ConfigurationManager.AppSettings["QUEUE_URL"];
            FindPlugin();
            mq = new MessageQueue(QUEUE_URL);
            mq.Formatter = new BinaryMessageFormatter();
        }

        public void FindPlugin()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "LazyAtHome.Service.WorkQueue.Plugin.dll";
            Console.WriteLine("加载插件 " + path);
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
                    newObj._SqlCommandOperation_NonQuery += new SqlCommandOperation_NonQuery(DataAccess.SqlCommandOperation_NonQuery);
                    newObj._SqlCommandOperation_Scalar += new SqlCommandOperation_Scalar(DataAccess.SqlCommandOperation_Scalar);
                    newObj._SqlCommandOperation_DataTable += new SqlCommandOperation_DataTable(DataAccess.SqlCommandOperation_DataTable);
                    newObj._SqlCommandOperation_Transaction += new SqlCommandOperation_Transaction(DataAccess.SqlCommandOperation_Transaction);
                    PluginList.Add(newObj.TypeName, newObj);
                    Console.WriteLine("TypeName: " + newObj.TypeName);
                }
            }
            Console.WriteLine("QUEUE_URL: " + QUEUE_URL);
        }

        protected override object GetTask()
        {
            Message mes = mq.Receive();
            return mes.Body;
        }

        protected override void Process(object oTask)
        {
            IQueueItem item = (IQueueItem)oTask;
            if (item == null)
            {
                Console.WriteLine("对象未基础IQueueItem对象 ");
                return;
            }
            PluginBase plugin = (PluginBase)PluginList[item.TypeName];
            if (plugin == null)
            {
                Console.WriteLine("类型[" + item.TypeName + "]未加载");
                return;
            }
            try
            {
                Console.WriteLine(item.TypeName + " Execute.. " + DateTime.Now.ToString());
                plugin.Execute(item);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
