using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.WCF.Common.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LazyAtHome.WCF.Common.Interface;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Common.Contract.Enumerate;

namespace LazyAtHome.WCF.Common.Business.Business
{
    public class PR
    {
        private LazyAtHome.WCF.Common.Interface.IDAL.IPRDAL prDAL;
        public PR()
        {
            if (prDAL == null)
                prDAL = new PRDAL();

        }

        static PR _PR;
        public static PR Instance
        {
            get
            {
                if (_PR == null)
                {
                    _PR = new PR();
                }
                return _PR;
            }
        }


        public void SetCache()
        {
            Console.WriteLine("获取人员信息缓存..........");
            SetCachePROperator();
            Console.WriteLine("获取人员信息缓存完成......");
        }

        /// <summary>
        /// 人员信息缓存
        /// </summary>
        private void SetCachePROperator()
        {
            IList<OperatorDC> hrm = prDAL.PR_Operator_SELECT_List_ALL();
            if (hrm != null)
            {
                LazyAtHome.Core.Helper.CacheHelper.Put("PR_Operator_List", hrm, CacheTimer.GetTimeSpanOneDay);
                Console.WriteLine("\t获取人员信息缓存" + hrm.Count.ToString() + "条");
            }
            else
                Console.WriteLine("\t获取人员信息缓存0条");
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="iOperatorDC"></param>
        /// <returns></returns>
        public ReturnEntity<int> PR_Operator_ADD(OperatorDC iOperatorDC)
        {
            //检查登录名是否重复
            if (prDAL.PR_Operator_HasLoginName(-1, iOperatorDC.LoginName))
                return new ReturnEntity<int>(-1, "用户登录名重复！");

            //把明码进行MD5加密
            iOperatorDC.LoginPwd = LazyAtHome.Core.Helper.CryptoHelper.Encrypt(iOperatorDC.LoginPwd, LazyAtHome.Core.Enumerate.CryptoMode.MD5);

            //创建用户登录信息
            int id = prDAL.PR_Operator_ADD(iOperatorDC);
            if (id <= 0)
            {
                return new ReturnEntity<int>(-2, "创建用户失败！");
            }
            //更新人员缓存
            SetCachePROperator();

            return new ReturnEntity<int>(id);
        }

        /// <summary>
        /// 编辑用户登录信息
        /// </summary>
        /// <param name="iEntity"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> PR_Operator_UPDATE(OperatorDC iEntity)
        {
            //根据员工ID登录信息
            OperatorDC _operator = prDAL.PR_Operator_SELECT_BYID(iEntity.ID);
            if (_operator == null)
                return new ReturnEntity<bool>(-1, "用户不存在！");
            //检查登录名是否重复
            if (prDAL.PR_Operator_HasLoginName(iEntity.ID, iEntity.LoginName))
                return new ReturnEntity<bool>(-2, "用户登录名重复！");

            if (!prDAL.PR_Operator_UPDATE(iEntity))
                return new ReturnEntity<bool>(-2, "更新数据库失败");

            if (!string.IsNullOrWhiteSpace(iEntity.LoginPwd))
            {
                if (iEntity.Obj_Muser != 1)
                {
                    return new ReturnEntity<bool>(-1, "非管理员不能修改密码");
                }

                iEntity.LoginPwd = LazyAtHome.Core.Helper.CryptoHelper.Encrypt(iEntity.LoginPwd, LazyAtHome.Core.Enumerate.CryptoMode.MD5);

                //共通更新方法角色表
                if (!prDAL.PR_Operator_UPDATE_Password(iEntity.ID, iEntity.LoginPwd, iEntity.Obj_Muser))
                {
                    return new ReturnEntity<bool>(-2, "更新数据库失败");
                }
            }

            //更新人员缓存
            SetCachePROperator();


            return new ReturnEntity<bool>(true);

        }

        /// <summary>
        /// 上岗下岗
        /// </summary>
        /// <param name="iOperatorID"></param>
        /// <param name="iOnDuty"></param>
        /// <returns></returns>
        public ReturnEntity<bool> PR_Operator_UPDATE_OnDuty(int iOperatorID, int iOnDuty)
        {
            var rtn = prDAL.PR_Operator_UPDATE_OnDuty(iOperatorID, iOnDuty);
            return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="iOperatorID"></param>
        /// <param name="iNewPassword"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> PR_Operator_UPDATE_Password(int iOperatorID, string iNewPassword, int iMuser)
        {
            //根据员工ID登录信息
            OperatorDC _operator = prDAL.PR_Operator_SELECT_BYID(iOperatorID);
            if (_operator == null)
                return new ReturnEntity<bool>(-1, "用户不存在！");

            _operator.LoginPwd = LazyAtHome.Core.Helper.CryptoHelper.Encrypt(iNewPassword, LazyAtHome.Core.Enumerate.CryptoMode.MD5);
            _operator.Obj_Muser = iMuser;

            //共通更新方法角色表
            if (!prDAL.PR_Operator_UPDATE_Password(iOperatorID, iNewPassword, iMuser))
                return new ReturnEntity<bool>(-2, "更新数据库失败");

            return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="iOperatorID"></param>
        /// <param name="iOldPassword"></param>
        /// <param name="iNewPassword"></param>
        /// <returns></returns>
        public ReturnEntity<bool> PR_Operator_UPDATE_Password(int iOperatorID, string iOldPassword, string iNewPassword)
        {
            //根据员工ID登录信息
            OperatorDC _operator = prDAL.PR_Operator_SELECT_BYID(iOperatorID);
            if (_operator == null)
                return new ReturnEntity<bool>(-1, "用户不存在！");

            iOldPassword = LazyAtHome.Core.Helper.CryptoHelper.Encrypt(iOldPassword, LazyAtHome.Core.Enumerate.CryptoMode.MD5);
            iNewPassword = LazyAtHome.Core.Helper.CryptoHelper.Encrypt(iNewPassword, LazyAtHome.Core.Enumerate.CryptoMode.MD5);

            //修改用户密码
            if (!prDAL.PR_Operator_UPDATE_Password(iOperatorID, iOldPassword, iNewPassword))
                return new ReturnEntity<bool>(-2, "原密码错误");

            return new ReturnEntity<bool>(true);
        }

        /// <summary>								
        /// 操作员登录								
        /// </summary>								
        /// <param name="iLoginName">登录名</param>								
        /// <param name="iPassword">密码</param>								
        /// <returns></returns>
        public ReturnEntity<OperatorDC> PR_Operator_Login(string iLoginName, string iPassword, OperatorType iOperatorType)
        {
            //把明码进行MD5加密
            iPassword = LazyAtHome.Core.Helper.CryptoHelper.Encrypt(iPassword, LazyAtHome.Core.Enumerate.CryptoMode.MD5);

            //获得员工信息
            OperatorDC entity = prDAL.PR_Operator_SELECT_Entity(iLoginName, iPassword, iOperatorType);
            if (entity == null)
            {
                return new ReturnEntity<OperatorDC>(-1, "用户名或密码错误，请重新输入！");
            }
            //if (iIsGetAllFunction)
            //{
            //    //获得权限列表
            //    entity.lstFunctionDC = prDAL.VELOUnion_Function_SELECT_List_BYPersonnelID(entity.COL_ID);
            //}
            return new ReturnEntity<OperatorDC>(entity);
        }

        /// <summary>								
        /// 获取人员列表								
        /// </summary>								
        /// <param name="iName">名称</param>	
        /// <param name="iMpNo">手机</param>
        /// <param name="iEmail">邮箱</param>
        /// <param name="iEnableType">登陆状态</param>
        /// <param name="oInfo">分页信息</param>
        /// <returns></returns>								
        public ReturnEntity<RecordCountEntity<OperatorDC>> PR_Operator_SELECT_List(string iName, string iMpNo,
            string iEmail, int? iEnableType,
            DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize,
            int? iNodeID, string iKeyword
            )
        {
            var entity = prDAL.PR_Operator_SELECT_List(iName, iMpNo, iEmail, iEnableType, iStartDate, iEndDate,
                iPageIndex, iPageSize, iNodeID, iKeyword);

            if (entity != null && entity.ReturnList != null)
            {
                foreach (var item in entity.ReturnList)
                {
                    if (item != null)
                    {
                        var role = RoleDC.CONFIG_LIST.FirstOrDefault(p => p.ID == item.RoleID);
                        if (role != null)
                        {
                            item.RoleName = role.Name;
                        }
                    }
                }
                if (iNodeID.HasValue && iPageIndex == 1)
                {
                    var manage = prDAL.PR_Operator_SELECT_BYNodeManagerID(iNodeID.Value);
                    if (manage != null)
                    {
                        manage.Name += "(站点负责人)";
                        entity.ReturnList.Insert(0, manage);
                    }
                }
            }

            return new ReturnEntity<RecordCountEntity<OperatorDC>>(entity);
        }

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="iVUL_ID">PRL_ID</param>
        /// <returns></returns>
        public ReturnEntity<OperatorDC> PR_Operator_SELECT_BYID(int iID)
        {
            var rtn = prDAL.PR_Operator_SELECT_BYID(iID);

            if (rtn != null)
            {
                var role = RoleDC.CONFIG_LIST.FirstOrDefault(p => p.ID == rtn.RoleID);
                if (role != null)
                {
                    rtn.RoleName = role.Name;
                }
            }

            return new ReturnEntity<OperatorDC>(rtn);
        }

        /// <summary>
        /// 新增操作员日志
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="iOperatorID"></param>
        /// <param name="iOperatorName"></param>
        /// <param name="iLogContent"></param>
        /// <returns></returns>
        public ReturnEntity<bool> PR_OperatorLog_ADD(int iType, int iOperatorID, string iOperatorName, string iLogContent)
        {
            var entity = new OperatorLogDC()
            {
                Type = iType,
                OperatorID = iOperatorID,
                OperatorName = iOperatorName,
                LogContent = iLogContent,
            };
            var rtn = prDAL.PR_OperatorLog_ADD(entity);
            if (rtn > 0)
            {
                return new ReturnEntity<bool>(true);
            }
            else
            {
                return new ReturnEntity<bool>(-1, "添加日志失败");
            }
        }

        /// <summary>
        /// 查询日志列表
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="iOperatorName"></param>
        /// <param name="iLogContent"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<OperatorLogDC>> PR_OperatorLog_SELECT_List(int? iType, string iOperatorName,
            string iLogContent, DateTime iStartDate, DateTime iEndDate, int iPageIndex, int iPageSize)
        {
            return new ReturnEntity<RecordCountEntity<OperatorLogDC>>(prDAL.PR_OperatorLog_SELECT_List(
              iType, iOperatorName, iLogContent, iStartDate, iEndDate, iPageIndex, iPageSize));
        }

        /// <summary>
        ///// 创建角色信息
        ///// </summary>
        ///// <param name="iRole">角色信息</param>
        ///// <returns></returns>
        //public int VELOUnion_PR_CreateRole(RoleDC iRole)
        //{
        //    //检查角色编号是否重复
        //    if (prDAL.VELOUnion_Role_HasRoleCode(-1, iRole.COR_Code))
        //        throw new FaultException("角色编号重复！", new FaultCode("000050002"));

        //    int id = prDAL.VELOUnion_Role_ADD(iRole);
        //    if (id <= 0)
        //    {
        //        throw new FaultException("创建角色失败！", new FaultCode("000050003"));
        //    }
        //    return id;
        //}

        ///// <summary>
        ///// 修改角色信息
        ///// </summary>
        ///// <param name="iRole">角色信息</param>
        ///// <param name="iMuser">操作人</param>
        ///// <returns></returns>
        //public bool VELOUnion_PR_UpdateRole(RoleDC iRole, int iMuser)
        //{
        //    //根据ID查询角色表
        //    RoleDC role = prDAL.VELOUnion_Role_SELECT_BYCOR_ID(iRole.COR_ID);
        //    if (role == null)
        //        throw new FaultException("角色信息不存在！", new FaultCode("000060002"));

        //    //检查角色编号是否重复
        //    if (prDAL.VELOUnion_Role_HasRoleCode(iRole.COR_ID, iRole.COR_Code))
        //        throw new FaultException("角色编号重复！", new FaultCode("000060003"));

        //    //打开修改数据收集器
        //    role.ModifyEnable = true;
        //    //角色编号
        //    role.COR_Code = iRole.COR_Code;
        //    //角色名称
        //    role.COR_Name = iRole.COR_Name;
        //    //状态
        //    role.COR_Enable = iRole.COR_Enable;
        //    //操作人
        //    role.Obj_Muser = iMuser;
        //    //备注
        //    role.Obj_Remark = iRole.Obj_Remark;
        //    //关闭修改数据收集器
        //    role.ModifyEnable = false;
        //    if (role.UpdateParam.Count > 1)
        //        //共通更新方法角色表
        //        return prDAL.VELOUnion_Role_Update_Common(role.UpdateParam);
        //    else
        //        return true;
        //}

        ///// <summary>
        ///// 批量删除角色信息
        ///// </summary>
        ///// <param name="iIdList">角色信息列表</param>
        ///// <param name="iMuser">操作人</param>
        ///// <returns></returns>
        //public bool VELOUnion_PR_DeleteRoleList(IList<int> iIdList, int iMuser)
        //{
        //    foreach (int id in iIdList)
        //    {
        //        prDAL.VELOUnion_Role_DELETE(id, iMuser);
        //    }
        //    return true;
        //}

        ///// <summary>
        ///// 创建角色关联人员(人员选择角色)
        ///// </summary>
        ///// <param name="iList">角色关联人员列表</param>
        ///// <param name="iPersonnelID">员工ID</param>
        ///// <param name="iMuser">操作员</param>
        ///// <returns></returns>
        //public bool VELOUnion_PR_CreateRolePersonnelList_ByPersonnelID(IList<int> iAddList, IList<int> iDelList, int iPersonnelID, int iMuser)
        //{
        //    if (iDelList != null)
        //    {
        //        //删除人员关联
        //        foreach (int item in iDelList)
        //        {
        //            prDAL.VELOUnion_RolePersonnel_DELETE(item, iPersonnelID, iMuser);
        //        }
        //    }

        //    if (iAddList != null)
        //    {
        //        //通过员工ID获取角色关联人员列表
        //        IList<RolePersonnelDC> list = prDAL.VELOUnion_RolePersonnel_SELECT_List_ByPersonnelID(iPersonnelID);
        //        bool insertFlag = true;
        //        foreach (int item in iAddList)
        //        {
        //            insertFlag = true;
        //            for (int i = 0; i < list.Count; i++)
        //            {
        //                //存在的关联
        //                if (list[i].COR_RoleID == item)
        //                {
        //                    insertFlag = false;
        //                    break;
        //                }
        //            }

        //            //取消的关联
        //            if (insertFlag)
        //            {
        //                RolePersonnelDC rp = new RolePersonnelDC();
        //                rp.COR_RoleID = item;
        //                rp.COP_PersonnelID = iPersonnelID;
        //                prDAL.VELOUnion_RolePersonnel_ADD(rp);
        //            }
        //        }
        //    }
        //    return true;
        //}

        ///// <summary>
        ///// 创建角色关联人员(角色选择人员)
        ///// </summary>
        ///// <param name="iList">角色关联人员列表</param>
        ///// <param name="iRoleID">角色ID</param>
        ///// <param name="iMuser">操作员</param>
        ///// <returns></returns>
        //public bool VELOUnion_PR_CreateRolePersonnelList_ByRoleID(IList<int> iAddList, IList<int> iDelList, int iRoleID, int iMuser)
        //{
        //    if (iDelList != null)
        //    {
        //        //删除人员关联
        //        foreach (int item in iDelList)
        //        {
        //            prDAL.VELOUnion_RolePersonnel_DELETE(iRoleID, item, iMuser);
        //        }
        //    }

        //    if (iAddList != null)
        //    {
        //        //通过角色ID获取角色关联人员列表	
        //        IList<RolePersonnelDC> list = prDAL.VELOUnion_RolePersonnel_SELECT_List_ByRoleID(iRoleID);
        //        bool insertFlag = true;
        //        foreach (int item in iAddList)
        //        {
        //            insertFlag = true;
        //            for (int i = 0; i < list.Count; i++)
        //            {
        //                //存在的关联
        //                if (list[i].COP_PersonnelID == item)
        //                {
        //                    insertFlag = false;
        //                    break;
        //                }
        //            }

        //            //取消的关联
        //            if (insertFlag)
        //            {
        //                RolePersonnelDC rp = new RolePersonnelDC();
        //                rp.COR_RoleID = iRoleID;
        //                rp.COP_PersonnelID = item;
        //                prDAL.VELOUnion_RolePersonnel_ADD(rp);
        //            }
        //        }
        //    }
        //    return true;
        //}

        ///// <summary>
        ///// 创建角色关联功能
        ///// </summary>
        ///// <param name="iList">角色关联功能列表</param>
        ///// <param name="iRoleID">角色ID</param>
        ///// <param name="iMuser">操作员</param>
        ///// <returns></returns>
        //public bool VELOUnion_PR_CreateRoleFunctionList(IList<RoleFunctionDC> iList, int iRoleID, int iMuser)
        //{
        //    //通过角色ID获取角色关联功能列表	
        //    IList<RoleFunctionDC> list = prDAL.VELOUnion_RoleFunction_SELECT_List_ByRoleID(iRoleID);
        //    IList<RoleFunctionDC> insert = new List<RoleFunctionDC>();
        //    iList.ToList().Where(i => i.COF_FunctionID != 0).ToList().ForEach(i => insert.Add(i));
        //    bool delFlag = true;
        //    int index = -1;
        //    foreach (RoleFunctionDC item in list)
        //    {
        //        index = -1;
        //        delFlag = true;
        //        for (int i = 0; i < insert.Count; i++)
        //        {
        //            //存在的关联
        //            if (insert[i].COF_FunctionID == item.COF_FunctionID)
        //            {
        //                index = i;
        //                delFlag = false;
        //            }
        //        }

        //        //取消的关联
        //        if (delFlag)
        //        {
        //            prDAL.VELOUnion_RoleFunction_DELETE(item.CORF_ID, iMuser);
        //        }
        //        else
        //        {
        //            insert.RemoveAt(index);
        //        }
        //    }

        //    //添加的关联
        //    foreach (RoleFunctionDC item in insert)
        //    {
        //        prDAL.VELOUnion_RoleFunction_ADD(item);
        //    }
        //    return true;
        //}

        ///// <summary>
        ///// 根据ID获取角色信息
        ///// </summary>
        ///// <param name="iID">角色ID</param>
        ///// <returns></returns>
        //public RoleDC VELOUnion_PR_GetRoleByID(int iID)
        //{
        //    return prDAL.VELOUnion_Role_SELECT_BYCOR_ID(iID);
        //}

        ///// <summary>
        ///// 根据员工ID获取角色列表
        ///// </summary>
        ///// <param name="iPersonnelID">员工ID</param>
        ///// <returns></returns>
        //public IList<RoleDC> VELOUnion_PR_GetRoleListByPersonnelID(int iPersonnelID)
        //{
        //    return prDAL.VELOUnion_Role_SELECT_List_ByPersonnelID(iPersonnelID);
        //}

        ///// <summary>
        ///// 根据角色ID查询角色关联人员列表
        ///// </summary>
        ///// <param name="iRoleID">角色ID</param>
        ///// <returns></returns>
        //public IList<LoginDC> VELOUnion_PR_GetRolePersonnelListByRoleID(int iRoleID)
        //{
        //    return prDAL.VELOUnion_Login_SELECT_List_ByRoleID(iRoleID);
        //}

        ///// <summary>
        ///// 根据角色ID查询角色关联功能权限列表
        ///// </summary>
        ///// <param name="iRoleID">角色ID</param>
        ///// <returns></returns>
        //public IList<FunctionDC> VELOUnion_PR_GetRoleFunctionListByRoleID(int iRoleID)
        //{
        //    return prDAL.VELOUnion_Function_SELECT_List_ByRoleID(iRoleID);
        //}

        ///// <summary>
        ///// 创建功能权限信息
        ///// </summary>
        ///// <param name="iFunction">功能权限信息</param>
        ///// <returns></returns>
        //public int VELOUnion_PR_CreateFunction(FunctionDC iFunction)
        //{
        //    //检查角色编号是否重复
        //    if (prDAL.VELOUnion_Function_HasFunctionCode(-1, iFunction.COF_Code))
        //        throw new FaultException("功能权限编号重复！", new FaultCode("000110002"));

        //    int id = prDAL.VELOUnion_Function_ADD(iFunction);
        //    if (id <= 0)
        //    {
        //        throw new FaultException("创建功能权限失败！", new FaultCode("000110003"));
        //    }
        //    iFunction.COF_ID = id;
        //    return id;
        //}

        ///// <summary>
        ///// 修改功能权限信息
        ///// </summary>
        ///// <param name="iFunction">功能权限信息</param>
        ///// <param name="iMuser">操作人</param>
        ///// <returns></returns>
        //public bool VELOUnion_PR_UpdateFunction(FunctionDC iFunction, int iMuser)
        //{
        //    //根据ID查询菜单/界面/功能信息表
        //    FunctionDC function = prDAL.VELOUnion_Function_SELECT_BYCOF_ID(iFunction.COF_ID);
        //    if (function == null)
        //        throw new FaultException("功能权限未找到！", new FaultCode("000120002"));

        //    //检查角色编号是否重复
        //    if (prDAL.VELOUnion_Function_HasFunctionCode(iFunction.COF_ID, iFunction.COF_Code))
        //        throw new FaultException("功能权限编号重复！", new FaultCode("000120003"));

        //    //打开修改数据收集器
        //    function.ModifyEnable = true;
        //    //父节点ID
        //    function.COF_ParentID = iFunction.COF_ParentID;
        //    //编号
        //    function.COF_Code = iFunction.COF_Code;
        //    //名称
        //    function.COF_Name = iFunction.COF_Name;
        //    //界面等级
        //    function.COF_Level = iFunction.COF_Level;
        //    //数据类型（1项目 2菜单 3页面 4操作）
        //    function.COF_DataType = iFunction.COF_DataType;
        //    //操作类型(1添加 2编辑 3删除）
        //    function.COF_OperateType = iFunction.COF_OperateType;
        //    //URL
        //    function.COF_URL = iFunction.COF_URL;
        //    //状态（1启用0未启用）
        //    function.COF_Enable = iFunction.COF_Enable;
        //    //排序
        //    function.COF_OrderIndex = iFunction.COF_OrderIndex;
        //    //业务自动编号
        //    function.COF_IdentifierNo = iFunction.COF_IdentifierNo;
        //    //备注
        //    function.Obj_Remark = iFunction.Obj_Remark;
        //    //修改人
        //    function.Obj_Muser = iMuser;
        //    //关闭修改数据收集器
        //    function.ModifyEnable = false;
        //    if (function.UpdateParam.Count > 1)
        //    {
        //        //共通更新方法角色表
        //        if (prDAL.VELOUnion_Function_Update_Common(function.UpdateParam))
        //            return true;
        //        else
        //            throw new FaultException("更新功能权限失败！", new FaultCode("000120004"));
        //    }
        //    else
        //        return true;
        //}

        ///// <summary>
        ///// 批量删除功能权限信息
        ///// </summary>
        ///// <param name="iIdList">功能权限信息列表</param>
        ///// <param name="iMuser">操作人</param>
        ///// <returns></returns>
        //public bool VELOUnion_PR_DeleteFunctionList(IList<int> iIdList, int iMuser)
        //{
        //    foreach (int id in iIdList)
        //    {
        //        //删除所有权限
        //        prDAL.VELOUnion_Function_DELETE(id, iMuser);
        //    }
        //    return true;
        //}

        ///// <summary>
        ///// 根据ID获取功能权限信息
        ///// </summary>
        ///// <param name="iID">ID</param>
        ///// <returns></returns>
        //public FunctionDC VELOUnion_PR_GetFunctionByID(int iID)
        //{
        //    return prDAL.VELOUnion_Function_SELECT_BYCOF_ID(iID);
        //}

        ///// <summary>
        ///// 根据父节点ID获取功能权限信息列表
        ///// </summary>
        ///// <param name="iParentID">父节点ID</param>
        ///// <param name="iDataType">数据类型</param>
        ///// <param name="iEnableType">状态(null全部true启用false不启用)</param>
        ///// <param name="iCode">编号</param>	
        ///// <param name="iName">名称</param>
        ///// <param name="iOperateType">操作类型(0全部 1添加 2编辑 3删除）</param>
        ///// <param name="iInfo">数据分页信息</param>
        ///// <returns></returns>
        //public IList<FunctionDC> VELOUnion_PR_GetFunctionListByParentID(int iParentID, VeloUnion_FunctionDataTypeEnum iDataType, bool? iEnableType, string iCode, string iName, VeloUnion_FunctionOperateTypeEnum iOperateType, ref PageInfo iInfo)
        //{
        //    return prDAL.VELOUnion_Function_SELECT_List_ByParentIDANDDataTypeANDEnableType(iParentID, iDataType, iEnableType, iCode, iName, iOperateType, ref iInfo);
        //}

        ///// <summary>
        ///// 查询权限树桩结构
        ///// </summary>
        ///// <param name="iDataType">数据类型（1项目 2菜单 3页面 4操作）</param>
        ///// <returns></returns>
        //public IList<FunctionDC> VELOUnion_PR_GetFunctionListByDataType(IList<int> iDataType)
        //{
        //    return prDAL.VELOUnion_Function_SELECT_List_ByDataType(iDataType);
        //}

        ///// <summary>
        ///// 查询全部HRM人员信息表
        ///// </summary>
        ///// <returns></returns>
        //public IList<LoginDC> VELOUnion_PR_GetLoginList()
        //{
        //    return prDAL.VELOUnion_Login_SELECT_List_ALL();
        //}

        ///// <summary>
        ///// 根据组织机构编号获取角色列表
        ///// </summary>
        ///// <param name="iRoleCode">角色关键字</param>
        ///// <param name="iRoleName">角色名称</param>
        ///// <param name="iEnableType">角色状态(null全部true启用false不启用)</param>
        ///// <param name="iSearchType">是否递规获取</param>
        ///// <param name="iInfo">数据分页信息</param>
        ///// <returns></returns>
        //public IList<RoleDC> VELOUnion_PR_GetRoleListByRoleCodeANDEnableType(string iRoleCode, string iRoleName, bool? iEnableType, int iSearchType, ref PageInfo iInfo)
        //{
        //    return prDAL.VELOUnion_Role_SELECT_List_ByRoleCodeANDEnableType(iRoleCode, iRoleName, iEnableType, iSearchType, ref iInfo);
        //}

        ///// <summary>
        ///// 查询全部HRM人员信息表
        ///// </summary>
        ///// <returns></returns>
        //public IList<RoleDC> VELOUnion_PR_GetRoleList()
        //{
        //    return prDAL.VELOUnion_Role_SELECT_List();
        //}

        ///// <summary>								
        ///// 通过员工ID获取权限关联列表								
        ///// </summary>								
        ///// <param name="iPersonnelID">员工ID</param>								
        ///// <returns></returns>								
        //public IList<FunctionDC> VELOUnion_PR_GetPersonnelFunctionByPersonnelID(int iPersonnelID)
        //{
        //    return prDAL.VELOUnion_PersonnelFunction_SELECT_List_ByPersonnelIDForFunction(iPersonnelID);
        //}

        ///// <summary>
        ///// 创建人员单独关联权限
        ///// </summary>
        ///// <param name="iPerFunList">关联权限</param>
        ///// <param name="iPersonnelID">员工ID</param>
        ///// <param name="iMuser">操作员</param>
        ///// <returns></returns>
        //public bool VELOUnion_PR_CreatePersonnelFunctionList(IList<PersonnelFunctionDC> iPerFunList, int iPersonnelID, int iMuser)
        //{
        //    //通过员工ID获取权限关联人员列表
        //    IList<PersonnelFunctionDC> list = prDAL.VELOUnion_PersonnelFunction_SELECT_List_ByPersonnelID(iPersonnelID);
        //    IList<PersonnelFunctionDC> insert = new List<PersonnelFunctionDC>();
        //    iPerFunList.ToList().Where(i => i.COF_FunctionID != 0).ToList().ForEach(i => insert.Add(i));
        //    bool delFlag = true;
        //    int index = -1;
        //    foreach (PersonnelFunctionDC item in list)
        //    {
        //        delFlag = true;
        //        index = -1;
        //        for (int i = 0; i < insert.Count; i++)
        //        {
        //            //存在的关联
        //            if (insert[i].COF_FunctionID == item.COF_FunctionID)
        //            {
        //                //打开修改数据收集器
        //                item.ModifyEnable = true;
        //                //开始时间
        //                item.COPF_BeginDate = insert[i].COPF_BeginDate;
        //                //结束时间
        //                item.COPF_EndDate = insert[i].COPF_EndDate;
        //                //操作人
        //                item.Obj_Muser = iMuser;
        //                //关闭修改数据收集器
        //                item.ModifyEnable = false;
        //                if (item.UpdateParam.Count > 1)
        //                {
        //                    prDAL.VELOUnion_PersonnelFunction_Update_Common(item.UpdateParam);
        //                }
        //                index = i;
        //                delFlag = false;
        //                break;
        //            }
        //        }

        //        //取消的关联
        //        if (delFlag)
        //        {
        //            prDAL.VELOUnion_PersonnelFunction_DELETE(item.COPF_ID, iMuser);
        //        }
        //        else
        //        {
        //            insert.RemoveAt(index);
        //        }
        //    }

        //    //添加的关联
        //    foreach (PersonnelFunctionDC item in insert)
        //    {
        //        prDAL.VELOUnion_PersonnelFunction_ADD(item);
        //    }
        //    return true;
        //}
    }
}
