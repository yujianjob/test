using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Common.Contract.DataContract.File;
using LazyAtHome.WCF.Common.Contract.InterfaceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Common.Business.Portal
{
    public partial class CommonPortal : IFile
    {
        protected LazyAtHome.WCF.Common.Business.Business.File FileInstance = LazyAtHome.WCF.Common.Business.Business.File.Instance;

        public ReturnEntity<UploadResultDC> File_Upload(UploadInfoDC iUploadInfoDC)
        {
            Log_DeBug("File_Upload", iUploadInfoDC);
            try
            {
                return FileInstance.File_Upload(iUploadInfoDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("File_Upload", ex);
                return new ReturnEntity<UploadResultDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("File_Upload");
            }
        }

        public ReturnEntity<int> File_Delete(Guid iSignKey)
        {
            Log_DeBug("File_Delete", iSignKey);
            try
            {
                return FileInstance.File_Delete(iSignKey);
            }
            catch (Exception ex)
            {
                Log_Fatal("File_Delete", ex);
                return new ReturnEntity<int>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("File_Delete");
            }
        }
    }
}
