using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using LazyAtHome.Core.Infrastructure.WCF;

namespace LazyAtHome.WCF.Common.Contract.DataContract.Base
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class Base_NotifyDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string EventNumber { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string OrderNumber { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int RoleID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int PersonnelID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int Class { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Title { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Content { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int Level { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int Status { set; get; }


        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int NotifyStatus
        {
            set
            {

            }
            get
            {
                return this.Status;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Result { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string NotifyUserList { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool IsSms { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool IsEmail { set; get; }

        #endregion Model

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string PersonnelName { set; get; }

        /// <summary>
        /// 根据Reader生成Base_NotifyDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Base_NotifyDC GetEntity(IDataReader reader)
        {
            Base_NotifyDC entity = null;

            entity = new Base_NotifyDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
