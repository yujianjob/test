using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Common.Contract.DataContract.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Common.Contract.InterfaceContract
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IFile
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="iUploadFileInfoDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<UploadResultDC> File_Upload(UploadInfoDC iUploadFileInfoDC);

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="iSignKey"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<int> File_Delete(Guid iSignKey);
    }
}
