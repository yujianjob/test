using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Configuration;

namespace LazyAtHome.Core.Infrastructure.WCF.Client
{
    /// <summary>
    /// 客户端代理类基类
    /// </summary>
    /// <typeparam name="TChannel"></typeparam>
    public abstract class ClientProxyBase<TChannel> : ClientBase<TChannel> where TChannel : class
    {
        private static Dictionary<string, IList<string>> endpointNameList = new Dictionary<string, IList<string>>();

        public TChannel Proxy
        {
            get { return Channel; }
        }

        public ClientProxyBase()
            : base(GetEndPoint())
        {
            //CredentialsSetting();
        }

        public ClientProxyBase(string endpointConfigurationName)
            : base(endpointConfigurationName)
        {
            //CredentialsSetting();
        }

        public ClientProxyBase(System.ServiceModel.Channels.Binding binding, EndpointAddress remoteAddress)
            : base(binding, remoteAddress)
        {
            //CredentialsSetting();
        }

        /// <summary>
        /// 设置客户端X509证书
        /// </summary>
        private void CredentialsSetting()
        {
            //ClientCredentials.ClientCertificate.Certificate = new X509Certificate2(@"d:\VeloPublic.cer");
            //ClientCredentials.ClientCertificate.SetCertificate("CN=VELO", StoreLocation.CurrentUser, StoreName.My);
            //ClientCredentials.ServiceCertificate.SetDefaultCertificate("CN=VELO", StoreLocation.CurrentUser, StoreName.My);
            //ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;
        }

        private static string GetEndPoint()
        {
            string rtn = null;
            string typeName = typeof(TChannel).ToString();
            IList<string> xlist = null;
            if (endpointNameList.ContainsKey(typeName))
                xlist = endpointNameList[typeName];

            if (xlist == null)
            {
                xlist = new List<string>();
                Configuration config = null;

                if (System.Web.HttpContext.Current != null)
                    config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(System.Web.HttpContext.Current.Request.ApplicationPath + "\\web.config");
                else
                    config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetEntryAssembly().Location);
                var svc = System.ServiceModel.Configuration.ServiceModelSectionGroup.GetSectionGroup(config);

                foreach (System.ServiceModel.Configuration.ChannelEndpointElement item in svc.Client.Endpoints)
                {
                    if (item.Contract == typeName)
                        xlist.Add(item.Name);
                }
                if (xlist.Count > 0)
                    endpointNameList[typeName] = xlist;
            }
            if (xlist.Count == 0)
                throw new Exception("配置文件内未找到" + typeName + "的EndPoint");

            rtn = xlist[new Random().Next(0, xlist.Count)];
            return rtn;
        }

        public new void Close()
        {
            //Console.WriteLine("Cust Close Exec");

            if (State != CommunicationState.Closed)
            {
                try
                {
                    base.Close();
                    //Console.WriteLine("CCE Close()");
                }
                catch (Exception ex)
                {
                    Abort();
                    Console.WriteLine("CCE Abort():" + ex.Message);
                }
            }
        }
    }
}
