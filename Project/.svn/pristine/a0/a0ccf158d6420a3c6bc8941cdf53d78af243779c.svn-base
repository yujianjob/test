using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Express.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Express.Business.Business
{
    public partial class ManageExpress
    {
        /// <summary>
        /// 仓库搜索
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="iName"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Exp_StorageDC>> Exp_Storage_SELECT_List(
               int? iType, string iName,
               DateTime? iStartDate, DateTime? iEndDate,
               int iPageIndex, int iPageSize)
        {
            var rtn = expressDAL.Exp_Storage_SELECT_List(iType, iName, iStartDate, iEndDate, iPageIndex, iPageSize);

            return new ReturnEntity<RecordCountEntity<Exp_StorageDC>>(rtn);
        }

        /// <summary>
        /// 在库搜索
        /// </summary>
        /// <param name="iStorageID"></param>
        /// <param name="iNumber"></param>
        /// <param name="iOtherNumber"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Exp_StorageItemDC>> Exp_StorageItem_SELECT_List(
            int? iStorageID, string iNumber, string iOtherNumber,
            int? iItemType, int? iNodeID, int? iLineID, int? iTargetType, int? iStatus,
            int iPageIndex, int iPageSize)
        {
            var rtn = expressDAL.Exp_StorageItem_SELECT_List(iStorageID, iNumber, iOtherNumber,
                iItemType, iNodeID, iLineID, iTargetType, iStatus, iPageIndex, iPageSize);

            return new ReturnEntity<RecordCountEntity<Exp_StorageItemDC>>(rtn);
        }

        /// <summary>
        /// 库存流转搜索
        /// </summary>
        /// <param name="iStorageID"></param>
        /// <param name="iType"></param>
        /// <param name="iNumber"></param>
        /// <param name="iOtherNumber"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Exp_StorageLogDC>> Exp_StorageLog_SELECT_List(
             int? iStorageID, int? iType, string iNumber, string iOtherNumber,
             DateTime? iStartDate, DateTime? iEndDate,
             int iPageIndex, int iPageSize)
        {
            var rtn = expressDAL.Exp_StorageLog_SELECT_List(iStorageID, iType, iNumber, iOtherNumber, null, iStartDate, iEndDate, iPageIndex, iPageSize);

            return new ReturnEntity<RecordCountEntity<Exp_StorageLogDC>>(rtn);
        }

        /// <summary>
        /// 出入库
        /// </summary>
        /// <param name="iItemID"></param>
        /// <param name="iSourceStorageID"></param>
        /// <param name="iTargetStorageID"></param>
        /// <param name="iTargetType"></param>
        /// <param name="iAdmin"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<int> Exp_StorageItem_IO(int iItemID, int iSourceStorageID, int iTargetStorageID, int iTargetType, bool iAdmin, int iMuser)
        {
            var rtn = expressDAL.Exp_StorageItem_IO(iItemID, iSourceStorageID, iTargetStorageID, iTargetType, iAdmin, iMuser);

            return new ReturnEntity<int>(rtn);
        }

        /// <summary>
        /// 根据ID查询Exp_StorageItem
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public ReturnEntity<Exp_StorageItemDC> Exp_StorageItem_SELECT_Entity(int iID)
        {
            var rtn = expressDAL.Exp_StorageItem_SELECT_Entity(iID);

            return new ReturnEntity<Exp_StorageItemDC>(rtn);
        }

    }
}
