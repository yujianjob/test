using LazyAtHome.WCF.Common.Contract.DataContract.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Common.Interface.IDAL
{
    public interface IFileDAL
    {
        string File_GetModuleConfig(int iProjectType);

        void File_AddUpLoadLog(UpLoadLogDC iUpLoadFileLogDC);

        UpLoadLogDC File_UpLoadLog_GetBySignKey(Guid iSignKey);

    }
}
