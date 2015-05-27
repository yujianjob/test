using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;

namespace LazyAtHome.Core.Infrastructure.WCF.Service
{
    /// <summary>
    /// 服务器实现类基类
    /// </summary>
    public abstract class ServiceBase : IServiceBehavior, IErrorHandler
    {
        #region IServiceBehavior 成员

        public void AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
            {
                channelDispatcher.ErrorHandlers.Add(this);
            }
        }

        public void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
        }

        #endregion

        #region IErrorHandler 成员

        public virtual bool HandleError(Exception error)
        {
            //此处用于记录错误日志

            //var stackTrace = new System.Diagnostics.StackTrace(error, true);
            //string className = null;
            //string methodName = null;
            //if (stackTrace.FrameCount > 0)
            //{
            //    var method = stackTrace.GetFrame(0).GetMethod();
            //    className = method.DeclaringType.Name;
            //    methodName = method.Name;
            //}

            if (error is FaultException)
            {
                var err = (FaultException)error;
                Console.WriteLine(err.Code.Name);
                Console.WriteLine(err.Message);
                Console.WriteLine(err.Source);
            }
            else
                Console.WriteLine(error.Message);
            return true;
        }

        public virtual void ProvideFault(Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {
            MessageFault messageFault = null;
            string strFault = null;
            FaultException err = null;
            if (error is FaultException)
                err = error as FaultException;
            else
                err = new FaultException(error.Message, new FaultCode("100000"));

            messageFault = err.CreateMessageFault();
            strFault = err.Action;

            fault = Message.CreateMessage(version, messageFault, strFault);
        }

        #endregion
    }
}
