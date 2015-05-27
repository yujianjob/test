using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data;
using System.Reflection;

namespace LazyAtHome.Core.Infrastructure.WCF
{
    [Serializable]
    [DataContract]
    public abstract class EntityBase
    {
        #region Updateparam

        protected IList<UpdateParamEntity> _UpdateParam;
        protected bool _ModifyEnable = false;
        protected Core.Enumerate.UpdateModel _UpdateFlag;
        Hashtable _list = new Hashtable();

        [DataMember]
        public virtual IList<UpdateParamEntity> UpdateParam
        {
            get
            {
                if (_UpdateParam == null)
                    _UpdateParam = new List<UpdateParamEntity>();
                return _UpdateParam;
            }
            protected set
            {
                _UpdateParam = value;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        [DataMember]
        public Core.Enumerate.UpdateModel UpdateFlag
        {
            get { return _UpdateFlag; }
            set { _UpdateFlag = value; }
        }

        [DataMember]
        public bool ModifyEnable
        {
            get { return _ModifyEnable; }
            set { _ModifyEnable = value; }
        }

        protected virtual void AddUpdateParam(UpdateParamEntity param)
        {
            if (param == null)
                return;
            if (_UpdateParam == null)
                _UpdateParam = new List<UpdateParamEntity>();
            else if (!_UpdateParam.GetType().Equals(typeof(List<UpdateParamEntity>)))
            {
                _UpdateParam = _UpdateParam.ToList();
            }
            if (_UpdateParam.Count(p => p.Name == param.Name) > 0)
                _UpdateParam.Remove(UpdateParam.First(p => p.Name == param.Name));

            _UpdateParam.Add(param);
        }

        protected virtual void ValueChanged(String propertyName, object oldValue, object newValue, System.Data.DbType dbType, bool isKey = false)
        {
            if (!_ModifyEnable)
                return;

            UpdateParamEntity param = new UpdateParamEntity();
            param.Name = propertyName;
            param.IsPrimaryKey = isKey;
            if (oldValue == null)
                param.OldValue = DBNull.Value;
            else
                param.OldValue = oldValue;
            if (newValue == null)
                param.NewValue = DBNull.Value;
            else
                param.NewValue = newValue;
            param.DbType = dbType;
            AddUpdateParam(param);
        }

        #endregion

        public EntityBase()
        {
            Obj_Status = 1;
        }

        #region 基础属性

        string _Obj_Remark;
        byte _Obj_Status;
        int _Obj_Cuser;
        DateTime? _Obj_Cdate;
        int _Obj_Muser;
        DateTime? _Obj_Mdate;

        [DataMember]
        public string Obj_Remark
        {
            get { return _Obj_Remark; }
            set
            {
                if (_Obj_Remark != value)
                {
                    ValueChanged("Obj_Remark", _Obj_Remark, value, System.Data.DbType.String);
                    _Obj_Remark = value;
                }
            }
        }

        [DataMember]
        public byte Obj_Status
        {
            get { return _Obj_Status; }
            set
            {
                if (_Obj_Status != value)
                {
                    ValueChanged("Obj_Status", _Obj_Status, value, System.Data.DbType.Byte);
                    _Obj_Status = value;
                }
            }
        }

        [DataMember]
        public int Obj_Cuser
        {
            get { return _Obj_Cuser; }
            set
            {
                if (_Obj_Cuser != value)
                {
                    ValueChanged("Obj_Cuser", _Obj_Cuser, value, System.Data.DbType.Int32);
                    _Obj_Cuser = value;
                }
            }
        }

        [DataMember]
        public string Obj_CuserName { get; set; }

        [DataMember]
        public DateTime? Obj_Cdate
        {
            get { return _Obj_Cdate; }
            set
            {
                if (_Obj_Cdate != value)
                {
                    ValueChanged("Obj_Cdate", _Obj_Cdate, value, System.Data.DbType.DateTime);
                    _Obj_Cdate = value;
                }
            }
        }

        [DataMember]
        public int Obj_Muser
        {
            get { return _Obj_Muser; }
            set
            {
                if (_Obj_Muser != value)
                {
                    ValueChanged("Obj_Muser", _Obj_Muser, value, System.Data.DbType.Int32);
                    _Obj_Muser = value;
                }
            }
        }

        [DataMember]
        public string Obj_MuserName { get; set; }

        [DataMember]
        public DateTime? Obj_Mdate
        {
            get { return _Obj_Mdate; }
            set
            {
                if (_Obj_Mdate != value)
                {
                    ValueChanged("Obj_Mdate", _Obj_Mdate, value, System.Data.DbType.DateTime);
                    _Obj_Mdate = value;
                }
            }
        }

        #endregion

        ///// <summary>
        ///// 克隆一个自己的，返回一个新对象
        ///// 注意：此方法不会复制对象的引用，只会复制引用关系
        ///// </summary>
        ///// <returns>一个新的对象副本</returns>
        //public virtual EntityBase Clone()
        //{
        //    return this.MemberwiseClone() as EntityBase;
        //}

        public void CopyCreateInfo(EntityBase model)
        {
            this.Obj_Cdate = model.Obj_Cdate;
            this.Obj_Cuser = model.Obj_Cuser;
            this.Obj_Mdate = model.Obj_Mdate;
            this.Obj_Muser = model.Obj_Muser;
        }

        public void CopyUpdateInfo(EntityBase model)
        {
            this.Obj_Mdate = model.Obj_Mdate;
            this.Obj_Muser = model.Obj_Muser;
        }

        public void SetBaseInfo(IDataReader reader)
        {
            SetBaseInfo(reader, null);
        }

        public void SetBaseInfo(IDataReader reader, string[] cols)
        {
            if (cols == null) cols = GetReaderColumnsName(reader);

            if (cols.Contains("Obj_Cdate"))
                if (reader["Obj_Cdate"] != DBNull.Value) Obj_Cdate = (DateTime?)reader["Obj_Cdate"];
            if (cols.Contains("Obj_Cuser"))
                if (reader["Obj_Cuser"] != DBNull.Value) Obj_Cuser = (int)reader["Obj_Cuser"];
            if (cols.Contains("Obj_Mdate"))
                if (reader["Obj_Mdate"] != DBNull.Value) Obj_Mdate = (DateTime?)reader["Obj_Mdate"];
            if (cols.Contains("Obj_Muser"))
                if (reader["Obj_Muser"] != DBNull.Value) Obj_Muser = (int)reader["Obj_Muser"];
            if (cols.Contains("Obj_Remark"))
                if (reader["Obj_Remark"] != DBNull.Value) Obj_Remark = reader["Obj_Remark"].ToString();
            if (cols.Contains("Obj_Status"))
                if (reader["Obj_Status"] != DBNull.Value) Obj_Status = (byte)reader["Obj_Status"];

            if (cols.Contains("Obj_CuserName"))
                if (reader["Obj_CuserName"] != DBNull.Value) Obj_CuserName = (string)reader["Obj_CuserName"];
            if (cols.Contains("Obj_MuserName"))
                if (reader["Obj_MuserName"] != DBNull.Value) Obj_MuserName = (string)reader["Obj_MuserName"];
        }

        public void SetBaseInfo(EntityBase item, IDataReader reader, string[] cols)
        {
            if (cols == null) cols = GetReaderColumnsName(reader);

            if (cols.Contains("Obj_Cdate"))
                if (reader["Obj_Cdate"] != DBNull.Value) item.Obj_Cdate = (DateTime?)reader["Obj_Cdate"];
            if (cols.Contains("Obj_Cuser"))
                if (reader["Obj_Cuser"] != DBNull.Value) item.Obj_Cuser = (int)reader["Obj_Cuser"];
            if (cols.Contains("Obj_Mdate"))
                if (reader["Obj_Mdate"] != DBNull.Value) item.Obj_Mdate = (DateTime?)reader["Obj_Mdate"];
            if (cols.Contains("Obj_Muser"))
                if (reader["Obj_Muser"] != DBNull.Value) item.Obj_Muser = (int)reader["Obj_Muser"];
            if (cols.Contains("Obj_Remark"))
                if (reader["Obj_Remark"] != DBNull.Value) item.Obj_Remark = reader["Obj_Remark"].ToString();
            if (cols.Contains("Obj_Status"))
                if (reader["Obj_Status"] != DBNull.Value) item.Obj_Status = (byte)reader["Obj_Status"];

            if (cols.Contains("Obj_CuserName"))
                if (reader["Obj_CuserName"] != DBNull.Value) Obj_CuserName = (string)reader["Obj_CuserName"];
            if (cols.Contains("Obj_MuserName"))
                if (reader["Obj_MuserName"] != DBNull.Value) Obj_MuserName = (string)reader["Obj_MuserName"];
        }

        /// <summary>
        /// 获取Reader对象包含的列
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected static string[] GetReaderColumnsName(System.Data.IDataReader reader)
        {
            Hashtable ht = new Hashtable(System.StringComparer.Create(System.Globalization.CultureInfo.CurrentCulture, true));
            string[] cols = new string[reader.FieldCount];
            for (int i = 0; i < reader.FieldCount; i++)
                cols[i] = reader.GetName(i);
            return cols;
        }

        /// <summary>
        /// 获取实体类
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public void AutoGetEntity(System.Data.IDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                _list.Add(reader.GetName(i).ToUpper(), reader.GetValue(i));
            }

            Type type = this.GetType();

            PropertyInfo[] plist = type.GetProperties();


            foreach (PropertyInfo info in plist)
            {
                try
                {
                    if (!info.CanWrite)
                        continue;
                    object value = _list[info.Name.ToUpper()];

                    if (info.PropertyType.Name == "String")
                    {
                        //手机客户端专用
                        if (value == null)
                        {
                            //没有值,是String类型,赋值为string.Empty
                            info.SetValue(this, string.Empty, null);
                        }
                        else
                        {
                            info.SetValue(this, value.ToString(), null);
                        }
                    }
                    else if (info.PropertyType.Name == "Double")
                    {
                        info.SetValue(this, Convert.ToDouble(value), null);
                    }
                    else if (value != null && value != DBNull.Value)
                    {
                        //有值则赋值
                        info.SetValue(this, value, null);
                    }
                }
                catch
                {
                    Console.WriteLine("AutoGetEntity " + info.Name + "  " + _list[info.Name]);
                    throw;
                }

            }


        }

        /// <summary>
        /// 判断SQL查询中是否有该列
        /// </summary>
        /// <param name="iColumnName"></param>
        /// <returns></returns>
        public bool ColumnsContains(string iColumnName)
        {
            return _list.ContainsKey(iColumnName.ToUpper());
        }

    }
}
