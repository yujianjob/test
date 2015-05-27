using LazyAtHome.WCF.Wash.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Wash.Interface.IDAL
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILuxuryDAL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="iLuxury_ClassDC"></param>
        /// <returns></returns>
        int Luxury_Class_ADD(Luxury_ClassDC iLuxury_ClassDC);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="iLuxury_ClassDC">主键</param>
        /// <returns></returns>
        bool Luxury_Class_UPDATE(Luxury_ClassDC iLuxury_ClassDC);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="iID">主键</param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        bool Luxury_Class_DELETE(int iID, int iMuser);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        IList<Luxury_ClassDC> Luxury_Class_SELECT_ALL();

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        Luxury_ClassDC Luxury_Class_SELECT_Entity(int iID);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="iLuxury_ProductDC"></param>
        /// <returns></returns>
        int Luxury_Product_ADD(Luxury_ProductDC iLuxury_ProductDC);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="iLuxury_ProductDC">主键</param>
        /// <returns></returns>
        bool Luxury_Product_UPDATE(Luxury_ProductDC iLuxury_ProductDC);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="iID">主键</param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        bool Luxury_Product_DELETE(int iID, int iMuser);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        IList<Luxury_ProductDC> Luxury_Product_SELECT_ALL();

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        Luxury_ProductDC Luxury_Product_SELECT_Entity(int iID);
    }
}
