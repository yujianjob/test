using ifunction.JPush;
using ifunction.JPush.V3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Common.Business.Business
{
    public class JPush
    {
        private LazyAtHome.WCF.Common.Interface.IDAL.ISmsDAL smsDAL;

        public JPush()
        {
            if (smsDAL == null)
                smsDAL = new LazyAtHome.WCF.Common.DAL.SmsDAL();
        }

        static JPush _JPush;

        public static JPush Instance
        {
            get
            {
                if (_JPush == null)
                {
                    _JPush = new JPush();
                }
                return _JPush;
            }
        }

        /// <summary>
        /// AppKey
        /// </summary>
        static String ExAPP_APP_KEY = "57a00b5ec9c62b54d0008a92";

        /// <summary>
        /// API 主密码	
        /// </summary>
        static String ExAPP_MASTER_SECRET = "0086d336f085b474602e8052";

        //HashSet<DeviceEnum> ExAPP_DeviceSet = new HashSet<DeviceEnum>(){
        //    DeviceEnum.Android,
        //    DeviceEnum.IOS,
        //};

        JPushClientV3 Ex_PUSH_Client = new JPushClientV3(ExAPP_APP_KEY, ExAPP_MASTER_SECRET);

        public void Process()
        {
            //读取表
            var pushList = smsDAL.Base_Push_SELECT_List();
            if (pushList == null || pushList.Count == 0)
            {
                return;
            }
            foreach (var item in pushList)
            {
                Console.WriteLine("PUSH:\t" + item.Type + " " + item.Title + " " + item.Content + " " + item.Tag);
                if (item.Type == 1)
                {
                    //懒到家APP
                    smsDAL.Base_Push_UPDATE(item.ID, -1);
                }
                else if (item.Type == 2)
                {

                    //用户自定义键值对
                    Dictionary<string, string> customizedValues = new Dictionary<string, string>();
                    customizedValues.Add("PlaySound", "order.mp3");
                    //customizedValues.Add("CK2", "CV2");


                    Audience audience = new Audience();
                    // In JPush V3, tag can be multiple added with different values.
                    // In following code, it is to send push to those who are in ((Tag1 AND Tag2) AND (Tag3 OR Tag4))
                    // If you want to send to all, please use: audience.Add(PushTypeV3.Broadcast, null);
                    //audience.Add(PushTypeV3.ByTagWithinAnd, new List<string>(new string[] { "Tag1", "Tag2" }));
                    //audience.Add(PushTypeV3.ByTagWithinOr, new List<string>(new string[] { "Tag3", "Tag4" }));

                    if (string.IsNullOrEmpty(item.Tag))
                    {
                        //广播
                        audience.Add(PushTypeV3.Broadcast, null);
                    }
                    else
                    {
                        audience.Add(PushTypeV3.ByTagWithinOr, new List<string>(new string[] { item.Tag }));
                    }


                    // In JPush V3, Notification would not be display on screen, it would be transferred to app instead.
                    // And different platform can provide different notification data.
                    Notification notification = new Notification
                    {
                        AndroidNotification = new AndroidNotificationParameters
                        {
                            //Title = "JPush provides V3.",
                            Alert = item.Content,
                            CustomizedValues = customizedValues
                        },

                        //iOSNotification = new iOSNotificationParameters
                        //{
                        //    //如果不填，表示不改变角标数字；否则把角标数字改为指定的数字；为 0 表示清除。
                        //    //Badge = 1,

                        //    //这里指定了，将会覆盖上级统一指定的 alert 信息；内容为空则不展示到通知栏。
                        //    //支持 emoji 表情。这里不指定则上级 notification 必须指定。
                        //    Alert = item.Content,

                        //    Sound = "YourSound",
                        //    CustomizedValues = customizedValues
                        //}
                    };

                    var response = Ex_PUSH_Client.SendPushMessage(new PushMessageRequestV3
                    {
                        //推送平台设置
                        Platform = PushPlatform.AndroidAndiOS,
                        //推送设备指定
                        Audience = audience,

                        //APNs是否生产环境 默认设置为推送 “开发环境”。
                        IsTestEnvironment = false,

                        AppMessage = new AppMessage
                        {
                            Content = item.Content,
                            CustomizedValue = customizedValues
                        },

                        Notification = notification,

                        //离线消息保留时长
                        LifeTime = 3600,


                    });


                    //Console.WriteLine(response.ResponseCode.ToString() + ":" + response.ResponseMessage);
                    //Console.WriteLine("Push sent.");
                    //Console.WriteLine(response.ResponseCode.ToString() + ":" + response.ResponseMessage);


                    //List<string> idToCheck = new List<string>();
                    //idToCheck.Add(response.MessageId);
                    //var statusList = Ex_PUSH_Client.QueryPushMessageStatus(idToCheck);
                    //Console.WriteLine("Status track is completed.");
                    //if (statusList != null)
                    //{
                    //    foreach (var one in statusList)
                    //    {
                    //        Console.WriteLine(string.Format("Id: {0}, Android: {1}, iOS: {2}", one.MessageId, one.AndroidDeliveredCount, one.ApplePushNotificationDeliveredCount));
                    //    }
                    //}
                    //Console.WriteLine("Press any key to exit.");
                    //Console.Read();


                    //快递APP
                    smsDAL.Base_Push_UPDATE(item.ID, 2);


                    ////初始推送设备类型
                    //JPushClient client = new JPushClient(ExAPP_APP_KEY, ExAPP_MASTER_SECRET, 0, ExAPP_DeviceSet, true);

                    //MessageResult result = null;

                    //NotificationParams notifyParams = new NotificationParams();
                    //CustomMessageParams customParams = new CustomMessageParams();

                    //String extras = null;


                    //extras = "{\"ios\":{\"badge\":88, \"sound\":\"happy\"}}";

                    //notifyParams.ReceiverType = ReceiverTypeEnum.ALIAS;

                    ////options 推送可选项。

                    ////推送序号 纯粹用来作为 API 调用标识，API 返回时被原样返回，以方便 API 调用方匹配请求与返回。
                    ////notifyParams.SendNo = item.ID;

                    ////要覆盖的消息ID
                    ////notifyParams.OverrideMsgId = item.ID.ToString();

                    ////APNs是否生产环境 True 表示推送生产环境，False 表示要推送开发环境；如果不指定则为推送生产环境。
                    ////notifyParams.ApnsProduction = 1;

                    ////离线消息保留时长 默认 86400 （1 天） 最长 10 天。
                    ////设置为 0 表示不保留离线消息，只有推送当前在线的用户可以收到。
                    //notifyParams.TimeToLive = 3600;

                    //result = client.sendNotification("酷派tag111111", notifyParams, extras);

                    //Console.WriteLine("sendNotification by tag:**返回状态：" + result.getErrorCode().ToString() +
                    //              "  **返回信息:" + result.getErrorMessage() +
                    //              "  **Send No.:" + result.getSendNo() +
                    //              "  msg_id:" + result.getMessageId() +
                    //              "  频率次数：" + result.getRateLimitQuota() +
                    //              "  可用频率：" + result.getRateLimitRemaining() +
                    //              "  重置时间：" + result.getRateLimitReset());

                }
                else
                {
                    smsDAL.Base_Push_UPDATE(item.ID, -1);
                }
            }
        }
    }
}
