using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LazyAtHome.WCF.Common.Interface;
using LazyAtHome.WCF.Common.Contract.DataContract.File;
using LazyAtHome.Core.Infrastructure.WCF;

namespace LazyAtHome.WCF.Common.Business.Business
{
    public class File
    {
        private LazyAtHome.WCF.Common.Interface.IDAL.IFileDAL fileDAL;
        public File()
        {
            if (fileDAL == null)
                fileDAL = new LazyAtHome.WCF.Common.DAL.FileDAL();
        }

        static File _File;
        public static File Instance
        {
            get
            {
                if (_File == null)
                {
                    _File = new File();
                }
                return _File;
            }
        }

        #region [允许上传的文件扩展名]

        private readonly string[] Comm_File_UploadFile_AllowFileExtension = new[]
            {
                ".txt",
                ".doc",
                ".docx",
                ".xls",
                ".xlsx",
                ".dat",
                ".png",
                ".jpg",
                ".jpeg",
                ".gif",
            };

        #endregion


        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="iUploadFileInfoDC"></param>
        /// <returns></returns>
        public ReturnEntity<UploadResultDC> File_Upload(UploadInfoDC iUploadFileInfoDC)
        {
            if (iUploadFileInfoDC == null)
            {
                throw new NotSupportedException("上传对象空!");
            }
            //返回值
            UploadResultDC rtnEntiy = new UploadResultDC();
            rtnEntiy.Result = -1;

            var uploadConfigPath = fileDAL.File_GetModuleConfig(iUploadFileInfoDC.ProjectType);
            if (string.IsNullOrWhiteSpace(uploadConfigPath))
            {
                throw new Exception("未知的上传路径配置");
            }
            //var directoryPath = uploadPath[0];

            try
            {
                #region [参数检查]

                if (!string.IsNullOrEmpty(iUploadFileInfoDC.CustomPath))
                {
                    uploadConfigPath = Path.Combine(uploadConfigPath, iUploadFileInfoDC.CustomPath.TrimStart('\\').TrimStart('/'));
                    uploadConfigPath = uploadConfigPath.Replace('/', '\\');
                }

                //路径不存在,创建路径
                if (!Directory.Exists(uploadConfigPath))
                {
                    Directory.CreateDirectory(uploadConfigPath);
                }

                //路径为空
                if (string.IsNullOrEmpty(uploadConfigPath))
                {
                    throw new System.IO.DirectoryNotFoundException("路径不存在");

                }

                //源文件名为空
                if (string.IsNullOrEmpty(iUploadFileInfoDC.FileName))
                {
                    throw new System.ArgumentException("文件名错误");
                }

                //获取文件名
                iUploadFileInfoDC.FileName = new FileInfo(iUploadFileInfoDC.FileName).Name;

                //文件扩展名不允许上传
                var fileExtension = new FileInfo(iUploadFileInfoDC.FileName).Extension;
                if (!string.IsNullOrEmpty(fileExtension))
                {
                    //* 下述限制上传文件扩展功能已注释  Edit By Ljli [2012/07/16]
                    //if (!Comm_File_UploadFile_AllowFileExtension.Contains(fileExtension.ToLower()))
                    //{
                    //    throw new System.ArgumentException("文件名错误");
                    //}
                }

                #endregion
                #region [预先处理]

                rtnEntiy.SignKey = Guid.NewGuid();

                //生成新文件名
                if (iUploadFileInfoDC.NewFileName)
                {
                    iUploadFileInfoDC.FileName = rtnEntiy.SignKey.ToString() + fileExtension;
                }

                //文件名为空
                if (string.IsNullOrEmpty(iUploadFileInfoDC.FileName))
                {
                    throw new System.ArgumentException("文件名错误");
                }


                //文件名不合法
                foreach (var item in Path.GetInvalidFileNameChars())
                {
                    if (iUploadFileInfoDC.FileName.Contains(item))
                    {
                        throw new System.ArgumentException("文件名错误" + iUploadFileInfoDC.FileName);
                    }
                }

                //检查文件是否存在
                if (!iUploadFileInfoDC.Overwrite)
                {
                    if (System.IO.File.Exists(Path.Combine(uploadConfigPath, iUploadFileInfoDC.FileName)))
                    {
                        throw new System.IO.IOException("文件已存在");
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("参数处理遇到错误:" + iUploadFileInfoDC.ToString(), ex);
            }

            #region [写入文件]

            var md5 = string.Empty;

            FileStream postStream = null;
            try
            {
                postStream = new FileStream(Path.Combine(uploadConfigPath, iUploadFileInfoDC.FileName), FileMode.Create);
                if (postStream.CanWrite)
                {
                    postStream.Write(iUploadFileInfoDC.PostArray, 0, iUploadFileInfoDC.PostArray.Length);
                }
                else
                {
                    throw new System.Security.SecurityException("文件为只读,不可覆盖");
                }

                try
                {
                    //MD5计算
                    System.Security.Cryptography.MD5CryptoServiceProvider get_md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                    byte[] hash_byte = get_md5.ComputeHash(iUploadFileInfoDC.PostArray);
                    md5 = System.BitConverter.ToString(hash_byte);
                    md5 = md5.Replace("-", "");
                }
                catch (Exception e)
                {
                    md5 = e.Message;
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                if (postStream != null)
                {
                    postStream.Close();
                }
            }
            #endregion


            //返回成功
            rtnEntiy.Result = 1;
            rtnEntiy.FileName = iUploadFileInfoDC.FileName;

            //存入数据库
            #region [存入数据库]
            fileDAL.File_AddUpLoadLog(new UpLoadLogDC()
            {
                ProjectType = iUploadFileInfoDC.ProjectType,

                FileSignKey = rtnEntiy.SignKey,

                FileCustomKey = iUploadFileInfoDC.CustomKey,

                SaveFilePath = Path.Combine(uploadConfigPath, iUploadFileInfoDC.FileName),

                FileMD5 = md5,

                Obj_Cdate = iUploadFileInfoDC.Obj_Cdate,
                Obj_Cuser = iUploadFileInfoDC.Obj_Cuser,
                Obj_CuserName = iUploadFileInfoDC.Obj_CuserName,
                Obj_Mdate = iUploadFileInfoDC.Obj_Mdate,
                Obj_MuserName = iUploadFileInfoDC.Obj_MuserName,
                Obj_Remark = iUploadFileInfoDC.Obj_Remark,
                Obj_Status = iUploadFileInfoDC.Obj_Status,
            });
            #endregion

            return new ReturnEntity<UploadResultDC>(rtnEntiy);
        }

        ///// <summary>
        ///// 下载文件
        ///// </summary>
        ///// <param name="iUploadFileInfoDC"></param>
        ///// <returns></returns>
        //public UploadInfoDC File_DownloadFile(Guid iSignKey)
        //{
        //    var uploadLog = fileDAL.Common_UpLoadFileLog_GetBySignKey(iSignKey);
        //    if (uploadLog == null)
        //    {
        //        throw new FileNotFoundException("文件配置不存在");
        //    }
        //    if (!System.IO.File.Exists(uploadLog.SaveFilePath))
        //    {
        //        throw new FileNotFoundException("文件不存在");
        //    }

        //    //返回对象
        //    UploadInfoDC uploadFileInfoDC = new UploadInfoDC();
        //    //文件名
        //    uploadFileInfoDC.FileName = Path.GetFileName(uploadLog.SaveFilePath);

        //    try
        //    {
        //        using (FileStream fs = new FileStream(uploadLog.SaveFilePath, FileMode.OpenOrCreate, FileAccess.Read))
        //        {
        //            if (fs.CanRead)
        //            {
        //                uploadFileInfoDC.PostArray = new byte[fs.Length];
        //                fs.Read(uploadFileInfoDC.PostArray, 0, (int)fs.Length);
        //            }
        //            else
        //            {
        //                throw new IOException("文件不可读");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return uploadFileInfoDC;

        //}

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="iSignKey"></param>
        /// <returns></returns>
        public ReturnEntity<int> File_Delete(Guid iSignKey)
        {
            var uploadLog = fileDAL.File_UpLoadLog_GetBySignKey(iSignKey);
            if (uploadLog == null)
            {
                throw new FileNotFoundException("文件配置不存在");
            }
            if (!System.IO.File.Exists(uploadLog.SaveFilePath))
            {
                return new ReturnEntity<int>(2);
            }
            try
            {
                System.IO.File.Delete(uploadLog.SaveFilePath);
                return new ReturnEntity<int>(1);
            }
            catch
            {
                throw;
            }
        }

        //public void Comm_File_DeleteFile(int iProjectType, string iFullPath)
        //{
        //    var uploadFileConfig = fileDAL.UpLoadFile_GetModuleConfig(iProjectType);
        //    if (uploadFileConfig == null)
        //    {
        //        throw new Exception("未知的上传路径配置");
        //    }
        //    try
        //    {
        //        System.IO.File.Delete(uploadFileConfig.TrimEnd('\\') + "\\" + iFullPath.TrimStart('\\'));
        //        return;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        ///// <summary>
        ///// 生成短链
        ///// </summary>
        ///// <param name="url"></param>
        ///// <returns></returns>
        //public string TinyUrl_Create(string url)
        //{
        //    return fileDAL.TinyUrl_Create(url);
        //}

        ///// <summary>
        ///// 获取短链源
        ///// </summary>
        ///// <param name="code"></param>
        ///// <returns></returns>
        //public string TinyUrl_Get(string code)
        //{
        //    return fileDAL.TinyUrl_Get(code);
        //}

    }
}
