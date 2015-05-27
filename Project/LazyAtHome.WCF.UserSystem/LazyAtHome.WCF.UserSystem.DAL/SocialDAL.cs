//using LazyAtHome.Core.Helper;
//using LazyAtHome.Core.Infrastructure.WCF;
//using LazyAtHome.WCF.UserSystem.Contract.DataContract;
//using Microsoft.Practices.EnterpriseLibrary.Data;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Common;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace LazyAtHome.WCF.UserSystem.DAL
//{
//    public class SocialDAL : DALBase, LazyAtHome.WCF.UserSystem.Interface.IDAL.ISocialDAL
//    {
//        #region 消息

//        /// <summary>
//        /// 新增
//        /// </summary>
//        /// <param name="iUser_Message_PrivateDC"></param>
//        /// <returns></returns>
//        public int User_Message_Private_ADD(User_Message_PrivateDC iUser_Message_PrivateDC)
//        {
//            StringBuilder sql = new StringBuilder();
//            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
//            sql.Append("INSERT INTO User_Message_Private(UserID, SendUserID, MessageType, Title, Message, ReadStatus, PublicID, ");
//            //字段拼接
//            AddCommonInsertSql(sql);
//            sql.Append(") VALUES (@UserID, @SendUserID, @MessageType, @Title, @Message, @ReadStatus, @PublicID, ");
//            //值拼接
//            AddCommonInsertValues(sql);
//            sql.Append(");SELECT @@IDENTITY;");
//            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
//            //用户ID
//            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUser_Message_PrivateDC.UserID);
//            //源用户ID(用户PM)
//            db.AddInParameter(cmd, "@SendUserID", DbType.Guid, iUser_Message_PrivateDC.SendUserID);
//            //消息类型
//            db.AddInParameter(cmd, "@MessageType", DbType.Int32, iUser_Message_PrivateDC.MessageType);
//            //标题
//            db.AddInParameter(cmd, "@Title", DbType.String, iUser_Message_PrivateDC.Title);
//            //内容
//            db.AddInParameter(cmd, "@Message", DbType.String, iUser_Message_PrivateDC.Message);
//            //读取状态(0:未读 1:已读)
//            db.AddInParameter(cmd, "@ReadStatus", DbType.Int32, iUser_Message_PrivateDC.ReadStatus);
//            //公告ID
//            db.AddInParameter(cmd, "@PublicID", DbType.Int32, iUser_Message_PrivateDC.PublicID);
//            //添加共通参数
//            AddCommonInsertParameter(db, cmd, iUser_Message_PrivateDC);
//            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
//            return id;
//        }

//        /// <summary>
//        /// 新增
//        /// </summary>
//        /// <param name="iUser_Message_PublicDC"></param>
//        /// <returns></returns>
//        public int User_Message_Public_ADD(User_Message_PublicDC iUser_Message_PublicDC)
//        {
//            StringBuilder sql = new StringBuilder();
//            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
//            sql.Append("INSERT INTO User_Message_Public(MessageType, Title, Message, PublishDate, PublishStatus, ");
//            //字段拼接
//            AddCommonInsertSql(sql);
//            sql.Append(") VALUES (@MessageType, @Title, @Message, @PublishDate, @PublishStatus, ");
//            //值拼接
//            AddCommonInsertValues(sql);
//            sql.Append(");SELECT @@IDENTITY;");
//            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
//            //消息类型
//            db.AddInParameter(cmd, "@MessageType", DbType.Int32, iUser_Message_PublicDC.MessageType);
//            //标题
//            db.AddInParameter(cmd, "@Title", DbType.String, iUser_Message_PublicDC.Title);
//            //内容
//            db.AddInParameter(cmd, "@Message", DbType.String, iUser_Message_PublicDC.Message);
//            //发布时间
//            db.AddInParameter(cmd, "@PublishDate", DbType.DateTime, iUser_Message_PublicDC.PublishDate);
//            //发布状态(0:未发布 1:已发布 2:下线)
//            db.AddInParameter(cmd, "@PublishStatus", DbType.Int32, iUser_Message_PublicDC.PublishStatus);
//            //添加共通参数
//            AddCommonInsertParameter(db, cmd, iUser_Message_PublicDC);
//            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
//            return id;
//        }

//        /// <summary>
//        /// 获取公共消息列表
//        /// </summary>
//        /// <param name="iStartDate"></param>
//        /// <param name="iEndDate"></param>
//        /// <param name="iPageIndex"></param>
//        /// <param name="iPageSize"></param>
//        /// <returns></returns>
//        public RecordCountEntity<User_Message_PublicDC> User_Message_Public_SELECT_List(DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
//        {
//            List<User_Message_PublicDC> list = new List<User_Message_PublicDC>();
//            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

//            StringBuilder sqlwhere = new StringBuilder();
//            StringBuilder sqlorder = new StringBuilder();
//            sqlwhere.Append(@"Obj_Status = 1 ");
//            sqlorder.Append(@" ID DESC ");

//            //时间开始
//            if (iStartDate != null)
//                sqlwhere.Append("AND Obj_Cdate >= '" + iStartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
//            //时间结束
//            if (iEndDate != null)
//                sqlwhere.Append("AND Obj_Cdate <= '" + iEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

//            var rtn = new RecordCountEntity<User_Message_PublicDC>();

//            //取得总数量
//            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("User_Message_Public", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
//            //取得功能权限信息列表
//            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("User_Message_Public", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
//            {
//                while (reader.Read())
//                {
//                    list.Add(User_Message_PublicDC.GetEntity(reader));
//                }
//            }
//            rtn.ReturnList = list;
//            return rtn;
//        }

//        /// <summary>
//        /// 获取个人消息列表
//        /// </summary>
//        /// <param name="iStartDate"></param>
//        /// <param name="iEndDate"></param>
//        /// <param name="iPageIndex"></param>
//        /// <param name="iPageSize"></param>
//        /// <returns></returns>
//        public RecordCountEntity<User_Message_PrivateDC> User_Message_Public_SELECT_List(Guid? iUserID, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
//        {
//            List<User_Message_PrivateDC> list = new List<User_Message_PrivateDC>();
//            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

//            StringBuilder sqlwhere = new StringBuilder();
//            StringBuilder sqlorder = new StringBuilder();
//            sqlwhere.Append(@"Obj_Status = 1 ");
//            sqlorder.Append(@" ID DESC ");

//            //用户ID
//            if (iUserID.HasValue)
//                sqlwhere.Append("AND UserID >= '" + iUserID + "' ");
//            //时间开始
//            if (iStartDate.HasValue)
//                sqlwhere.Append("AND Obj_Cdate >= '" + iStartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
//            //时间结束
//            if (iEndDate.HasValue)
//                sqlwhere.Append("AND Obj_Cdate <= '" + iEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

//            var rtn = new RecordCountEntity<User_Message_PrivateDC>();

//            //取得总数量
//            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("User_Message_Private", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
//            //取得功能权限信息列表
//            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("User_Message_Private", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
//            {
//                while (reader.Read())
//                {
//                    list.Add(User_Message_PrivateDC.GetEntity(reader));
//                }
//            }
//            rtn.ReturnList = list;
//            return rtn;
//        }

//        #endregion

//        #region 关注

//        #endregion

//        #region 来访

//        /// <summary>
//        /// 来访新增
//        /// </summary>
//        /// <param name="iUser_VisitsDC"></param>
//        /// <returns></returns>
//        public int User_Visits_ADD(User_VisitsDC iUser_VisitsDC)
//        {
//            StringBuilder sql = new StringBuilder();
//            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
//            sql.Append("INSERT INTO User_Visits(UserID, VisitsUserID, VisitsSeedID, VisitsNickName, ");
//            //字段拼接
//            AddCommonInsertSql(sql);
//            sql.Append(") VALUES (@UserID, @VisitsUserID, @VisitsSeedID, @VisitsNickName, ");
//            //值拼接
//            AddCommonInsertValues(sql);
//            sql.Append(");SELECT @@IDENTITY;");
//            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
//            //用户ID
//            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUser_VisitsDC.UserID);
//            //来访用户ID
//            db.AddInParameter(cmd, "@VisitsUserID", DbType.Guid, iUser_VisitsDC.VisitsUserID);
//            //来访用户ID
//            db.AddInParameter(cmd, "@VisitsSeedID", DbType.Int32, iUser_VisitsDC.VisitsSeedID);
//            //来访昵称
//            db.AddInParameter(cmd, "@VisitsNickName", DbType.String, iUser_VisitsDC.VisitsNickName);
//            //添加共通参数
//            AddCommonInsertParameter(db, cmd, iUser_VisitsDC);
//            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
//            return id;
//        }

//        #endregion

//        #region 晒单

//        /// <summary>
//        /// 新增晒单
//        /// </summary>
//        /// <param name="iUser_ShareDC"></param>
//        /// <returns></returns>
//        public int User_Share_ADD(User_ShareDC iUser_ShareDC)
//        {
//            StringBuilder sql = new StringBuilder();
//            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
//            sql.Append("INSERT INTO User_Share(UserID, Title, Content, Tag, Comment_UP, Comment_Like, Comment_Common, Comment_Down, CommentCount, ItemID, VerifyStatus, AduitEmployeeID, AduitDate, Comment, ");
//            //字段拼接
//            AddCommonInsertSql(sql);
//            sql.Append(") VALUES (@UserID, @Title, @Content, @Tag, @Comment_UP, @Comment_Like, @Comment_Common, @Comment_Down, @CommentCount, @ItemID, @VerifyStatus, @AduitEmployeeID, @AduitDate, @Comment, ");
//            //值拼接
//            AddCommonInsertValues(sql);
//            sql.Append(");SELECT @@IDENTITY;");
//            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
//            //用户ID
//            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUser_ShareDC.UserID);
//            //标题
//            db.AddInParameter(cmd, "@Title", DbType.String, iUser_ShareDC.Title);
//            //内容
//            db.AddInParameter(cmd, "@Content", DbType.String, iUser_ShareDC.Content);
//            //标签
//            db.AddInParameter(cmd, "@Tag", DbType.String, iUser_ShareDC.Tag);
//            //评价(顶)数
//            db.AddInParameter(cmd, "@Comment_UP", DbType.Int32, iUser_ShareDC.Comment_UP);
//            //评价(赞)数
//            db.AddInParameter(cmd, "@Comment_Like", DbType.Int32, iUser_ShareDC.Comment_Like);
//            //评价(平)数
//            db.AddInParameter(cmd, "@Comment_Common", DbType.Int32, iUser_ShareDC.Comment_Common);
//            //评价(踩)数
//            db.AddInParameter(cmd, "@Comment_Down", DbType.Int32, iUser_ShareDC.Comment_Down);
//            //评论数
//            db.AddInParameter(cmd, "@CommentCount", DbType.Int32, iUser_ShareDC.CommentCount);
//            //产品ID
//            db.AddInParameter(cmd, "@ItemID", DbType.Int32, iUser_ShareDC.ItemID);
//            //审核状态(1：已提交 2：审核通过 3：审核失败)
//            db.AddInParameter(cmd, "@VerifyStatus", DbType.Int32, iUser_ShareDC.VerifyStatus);
//            //审核人ID
//            db.AddInParameter(cmd, "@AduitEmployeeID", DbType.Int32, iUser_ShareDC.AduitEmployeeID);
//            //审核时间
//            db.AddInParameter(cmd, "@AduitDate", DbType.DateTime, iUser_ShareDC.AduitDate);
//            //审核人意见
//            db.AddInParameter(cmd, "@Comment", DbType.String, iUser_ShareDC.Comment);
//            //添加共通参数
//            AddCommonInsertParameter(db, cmd, iUser_ShareDC);
//            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
//            return id;
//        }

//        /// <summary>
//        /// 新增晒单图片
//        /// </summary>
//        /// <param name="iUser_SharePictureDC"></param>
//        /// <returns></returns>
//        public int User_SharePicture_ADD(User_SharePictureDC iUser_SharePictureDC)
//        {
//            StringBuilder sql = new StringBuilder();
//            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
//            sql.Append("INSERT INTO User_SharePicture(ShareID, Type, Path, Content, ");
//            //字段拼接
//            AddCommonInsertSql(sql);
//            sql.Append(") VALUES (@ShareID, @Type, @Path, @Content, ");
//            //值拼接
//            AddCommonInsertValues(sql);
//            sql.Append(");SELECT @@IDENTITY;");
//            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
//            //晒单ID
//            db.AddInParameter(cmd, "@ShareID", DbType.Int32, iUser_SharePictureDC.ShareID);
//            //类型(1:洗前 2:洗后)
//            db.AddInParameter(cmd, "@Type", DbType.Int32, iUser_SharePictureDC.Type);
//            //
//            db.AddInParameter(cmd, "@Path", DbType.String, iUser_SharePictureDC.Path);
//            //图片说明
//            db.AddInParameter(cmd, "@Content", DbType.String, iUser_SharePictureDC.Content);
//            //添加共通参数
//            AddCommonInsertParameter(db, cmd, iUser_SharePictureDC);
//            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
//            return id;
//        }

//        /// <summary>
//        /// 新增晒单评论
//        /// </summary>
//        /// <param name="iUser_ShareCommentDC"></param>
//        /// <returns></returns>
//        public int User_ShareComment_ADD(User_ShareCommentDC iUser_ShareCommentDC)
//        {
//            StringBuilder sql = new StringBuilder();
//            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
//            sql.Append("INSERT INTO User_ShareComment(ShareID, ShareUserID, CommentUserID, Content, CommentType, ParentID, VerifyStatus, AduitEmployeeID, AduitDate, Comment, ");
//            //字段拼接
//            AddCommonInsertSql(sql);
//            sql.Append(") VALUES (@ShareID, @ShareUserID, @CommentUserID, @Content, @CommentType, @ParentID, @VerifyStatus, @AduitEmployeeID, @AduitDate, @Comment, ");
//            //值拼接
//            AddCommonInsertValues(sql);
//            sql.Append(");SELECT @@IDENTITY;");
//            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
//            //晒单ID
//            db.AddInParameter(cmd, "@ShareID", DbType.Int32, iUser_ShareCommentDC.ShareID);
//            //晒单人ID
//            db.AddInParameter(cmd, "@ShareUserID", DbType.Guid, iUser_ShareCommentDC.ShareUserID);
//            //CommentUserID
//            db.AddInParameter(cmd, "@CommentUserID", DbType.Guid, iUser_ShareCommentDC.CommentUserID);
//            //评论内容
//            db.AddInParameter(cmd, "@Content", DbType.String, iUser_ShareCommentDC.Content);
//            //评价类型(顶,赞,平,踩)
//            db.AddInParameter(cmd, "@CommentType", DbType.Int32, iUser_ShareCommentDC.CommentType);
//            //上级ID(回复其他人评论)
//            db.AddInParameter(cmd, "@ParentID", DbType.Int32, iUser_ShareCommentDC.ParentID);
//            //审核状态(1：已提交 2：审核通过 3：审核失败)
//            db.AddInParameter(cmd, "@VerifyStatus", DbType.Int32, iUser_ShareCommentDC.VerifyStatus);
//            //审核人ID
//            db.AddInParameter(cmd, "@AduitEmployeeID", DbType.Int32, iUser_ShareCommentDC.AduitEmployeeID);
//            //审核时间
//            db.AddInParameter(cmd, "@AduitDate", DbType.DateTime, iUser_ShareCommentDC.AduitDate);
//            //审核人意见
//            db.AddInParameter(cmd, "@Comment", DbType.String, iUser_ShareCommentDC.Comment);
//            //添加共通参数
//            AddCommonInsertParameter(db, cmd, iUser_ShareCommentDC);
//            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
//            return id;
//        }

//        /// <summary>
//        /// 分享查询列表
//        /// </summary>
//        /// <param name="iTitle"></param>
//        /// <param name="iVerifyStatus"></param>
//        /// <param name="iPageIndex"></param>
//        /// <param name="iPageSize"></param>
//        /// <returns></returns>
//        public RecordCountEntity<User_ShareDC> User_Share_SELECT_List(string iTitle, int? iVerifyStatus,
//            int iPageIndex, int iPageSize)
//        {
//            List<User_ShareDC> list = new List<User_ShareDC>();
//            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

//            StringBuilder sqlwhere = new StringBuilder();
//            StringBuilder sqlorder = new StringBuilder();
//            sqlwhere.Append(@" Obj_Status = 1 ");
//            sqlorder.Append(@" ID DESC ");

//            //标题
//            if (!string.IsNullOrEmpty(iTitle))
//                sqlwhere.Append("AND Title LIKE '%" + iTitle + "%' ");
//            //1：已提交 2：审核通过 3：审核失败
//            if (iVerifyStatus.HasValue)
//                sqlwhere.Append("AND VerifyStatus = '" + iVerifyStatus + "' ");

//            var rtn = new RecordCountEntity<User_ShareDC>();

//            //取得总数量
//            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("User_Share", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
//            //取得功能权限信息列表
//            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("User_Share", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
//            {
//                while (reader.Read())
//                {
//                    list.Add(User_ShareDC.GetEntity(reader));
//                }
//            }
//            rtn.ReturnList = list;
//            return rtn;
//        }

//        /// <summary>
//        /// 晒单评论列表
//        /// </summary>
//        /// <param name="iShareID"></param>
//        /// <param name="iParentID"></param>
//        /// <param name="iPageIndex"></param>
//        /// <param name="iPageSize"></param>
//        /// <returns></returns>
//        public RecordCountEntity<User_ShareCommentDC> User_ShareComment_SELECT_List(int? iShareID, int? iParentID,
//            int iPageIndex, int iPageSize)
//        {
//            List<User_ShareCommentDC> list = new List<User_ShareCommentDC>();
//            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

//            StringBuilder sqlwhere = new StringBuilder();
//            StringBuilder sqlorder = new StringBuilder();
//            sqlwhere.Append(@" Obj_Status = 1 ");
//            sqlorder.Append(@" ID DESC ");

//            if (iShareID.HasValue)
//                sqlwhere.Append("AND ShareID = '" + iShareID + "' ");
//            //上级ID
//            if (iParentID.HasValue)
//                sqlwhere.Append("AND ParentID = '" + iParentID + "' ");

//            var rtn = new RecordCountEntity<User_ShareCommentDC>();

//            //取得总数量
//            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("User_ShareComment", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
//            //取得功能权限信息列表
//            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("User_ShareComment", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
//            {
//                while (reader.Read())
//                {
//                    var entity = User_ShareCommentDC.GetEntity(reader);

//                    entity.ReplyList = User_ShareComment_SELECT_List(null, entity.ID, 1, 10);

//                    list.Add(entity);
//                }
//            }
//            rtn.ReturnList = list;
//            return rtn;
//        }

//        #endregion
//    }
//}
