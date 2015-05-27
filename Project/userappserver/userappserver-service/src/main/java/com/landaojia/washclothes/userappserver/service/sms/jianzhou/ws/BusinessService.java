package com.landaojia.washclothes.userappserver.service.sms.jianzhou.ws;

import javax.jws.WebMethod;
import javax.jws.WebParam;
import javax.jws.WebResult;
import javax.jws.WebService;
import javax.jws.soap.SOAPBinding;
import javax.xml.bind.annotation.XmlSeeAlso;
import javax.xml.ws.RequestWrapper;
import javax.xml.ws.ResponseWrapper;

/**
 * This class was generated by Apache CXF 3.0.4
 * 2015-03-16T15:53:48.242+08:00
 * Generated source version: 3.0.4
 * 
 */
@WebService(targetNamespace = "http://service.nineorange.com", name = "BusinessService")
@XmlSeeAlso({ObjectFactory.class})
public interface BusinessService {

    @WebMethod
    @RequestWrapper(localName = "validateUser", targetNamespace = "http://service.nineorange.com", className = "com.landaojia.washclothes.userappserver.service.sms.jianzhou.ws.ValidateUser")
    @ResponseWrapper(localName = "validateUserResponse", targetNamespace = "http://service.nineorange.com", className = "com.landaojia.washclothes.userappserver.service.sms.jianzhou.ws.ValidateUserResponse")
    @WebResult(name = "validateUserReturn", targetNamespace = "http://service.nineorange.com")
    public int validateUser(
        @WebParam(name = "account", targetNamespace = "http://service.nineorange.com")
        java.lang.String account,
        @WebParam(name = "password", targetNamespace = "http://service.nineorange.com")
        java.lang.String password
    );

    @WebMethod
    @RequestWrapper(localName = "sendBatchMessage", targetNamespace = "http://service.nineorange.com", className = "com.landaojia.washclothes.userappserver.service.sms.jianzhou.ws.SendBatchMessage")
    @ResponseWrapper(localName = "sendBatchMessageResponse", targetNamespace = "http://service.nineorange.com", className = "com.landaojia.washclothes.userappserver.service.sms.jianzhou.ws.SendBatchMessageResponse")
    @WebResult(name = "sendBatchMessageReturn", targetNamespace = "http://service.nineorange.com")
    public int sendBatchMessage(
        @WebParam(name = "account", targetNamespace = "http://service.nineorange.com")
        java.lang.String account,
        @WebParam(name = "password", targetNamespace = "http://service.nineorange.com")
        java.lang.String password,
        @WebParam(name = "destmobile", targetNamespace = "http://service.nineorange.com")
        java.lang.String destmobile,
        @WebParam(name = "msgText", targetNamespace = "http://service.nineorange.com")
        java.lang.String msgText
    );

    @WebMethod
    @RequestWrapper(localName = "getReceivedMsg", targetNamespace = "http://service.nineorange.com", className = "com.landaojia.washclothes.userappserver.service.sms.jianzhou.ws.GetReceivedMsg")
    @ResponseWrapper(localName = "getReceivedMsgResponse", targetNamespace = "http://service.nineorange.com", className = "com.landaojia.washclothes.userappserver.service.sms.jianzhou.ws.GetReceivedMsgResponse")
    @WebResult(name = "getReceivedMsgReturn", targetNamespace = "http://service.nineorange.com")
    public java.lang.String getReceivedMsg(
        @WebParam(name = "account", targetNamespace = "http://service.nineorange.com")
        java.lang.String account,
        @WebParam(name = "password", targetNamespace = "http://service.nineorange.com")
        java.lang.String password,
        @WebParam(name = "param", targetNamespace = "http://service.nineorange.com")
        int param
    );

    @WebMethod
    @RequestWrapper(localName = "sendGjBatchMessage", targetNamespace = "http://service.nineorange.com", className = "com.landaojia.washclothes.userappserver.service.sms.jianzhou.ws.SendGjBatchMessage")
    @ResponseWrapper(localName = "sendGjBatchMessageResponse", targetNamespace = "http://service.nineorange.com", className = "com.landaojia.washclothes.userappserver.service.sms.jianzhou.ws.SendGjBatchMessageResponse")
    @WebResult(name = "return", targetNamespace = "http://service.nineorange.com")
    public int sendGjBatchMessage(
        @WebParam(name = "account", targetNamespace = "http://service.nineorange.com")
        java.lang.String account,
        @WebParam(name = "password", targetNamespace = "http://service.nineorange.com")
        java.lang.String password,
        @WebParam(name = "sendDateTime", targetNamespace = "http://service.nineorange.com")
        java.lang.String sendDateTime,
        @WebParam(name = "destmobile", targetNamespace = "http://service.nineorange.com")
        java.lang.String destmobile,
        @WebParam(name = "msgText", targetNamespace = "http://service.nineorange.com")
        java.lang.String msgText
    );

    @WebMethod
    @RequestWrapper(localName = "sendAudio", targetNamespace = "http://service.nineorange.com", className = "com.landaojia.washclothes.userappserver.service.sms.jianzhou.ws.SendAudio")
    @ResponseWrapper(localName = "sendAudioResponse", targetNamespace = "http://service.nineorange.com", className = "com.landaojia.washclothes.userappserver.service.sms.jianzhou.ws.SendAudioResponse")
    @WebResult(name = "return", targetNamespace = "http://service.nineorange.com")
    public int sendAudio(
        @WebParam(name = "account", targetNamespace = "http://service.nineorange.com")
        java.lang.String account,
        @WebParam(name = "password", targetNamespace = "http://service.nineorange.com")
        java.lang.String password,
        @WebParam(name = "sendDateTime", targetNamespace = "http://service.nineorange.com")
        java.lang.String sendDateTime,
        @WebParam(name = "destmobile", targetNamespace = "http://service.nineorange.com")
        java.lang.String destmobile,
        @WebParam(name = "msgText", targetNamespace = "http://service.nineorange.com")
        java.lang.String msgText,
        @WebParam(name = "srcMobile", targetNamespace = "http://service.nineorange.com")
        java.lang.String srcMobile
    );

    @WebMethod
    @RequestWrapper(localName = "getUserInfo", targetNamespace = "http://service.nineorange.com", className = "com.landaojia.washclothes.userappserver.service.sms.jianzhou.ws.GetUserInfo")
    @ResponseWrapper(localName = "getUserInfoResponse", targetNamespace = "http://service.nineorange.com", className = "com.landaojia.washclothes.userappserver.service.sms.jianzhou.ws.GetUserInfoResponse")
    @WebResult(name = "getUserInfoReturn", targetNamespace = "http://service.nineorange.com")
    public java.lang.String getUserInfo(
        @WebParam(name = "account", targetNamespace = "http://service.nineorange.com")
        java.lang.String account,
        @WebParam(name = "password", targetNamespace = "http://service.nineorange.com")
        java.lang.String password
    );

    @WebMethod
    @RequestWrapper(localName = "sendPersonalMessages", targetNamespace = "http://service.nineorange.com", className = "com.landaojia.washclothes.userappserver.service.sms.jianzhou.ws.SendPersonalMessages")
    @ResponseWrapper(localName = "sendPersonalMessagesResponse", targetNamespace = "http://service.nineorange.com", className = "com.landaojia.washclothes.userappserver.service.sms.jianzhou.ws.SendPersonalMessagesResponse")
    @WebResult(name = "sendPersonalMessagesReturn", targetNamespace = "http://service.nineorange.com")
    public int sendPersonalMessages(
        @WebParam(name = "account", targetNamespace = "http://service.nineorange.com")
        java.lang.String account,
        @WebParam(name = "password", targetNamespace = "http://service.nineorange.com")
        java.lang.String password,
        @WebParam(name = "destMobiles", targetNamespace = "http://service.nineorange.com")
        java.lang.String destMobiles,
        @WebParam(name = "msgContents", targetNamespace = "http://service.nineorange.com")
        java.lang.String msgContents
    );

    @WebMethod
    @RequestWrapper(localName = "sendBatchMessageTimelyExt", targetNamespace = "http://service.nineorange.com", className = "com.landaojia.washclothes.userappserver.service.sms.jianzhou.ws.SendBatchMessageTimelyExt")
    @ResponseWrapper(localName = "sendBatchMessageTimelyExtResponse", targetNamespace = "http://service.nineorange.com", className = "com.landaojia.washclothes.userappserver.service.sms.jianzhou.ws.SendBatchMessageTimelyExtResponse")
    @WebResult(name = "return", targetNamespace = "http://service.nineorange.com")
    public int sendBatchMessageTimelyExt(
        @WebParam(name = "account", targetNamespace = "http://service.nineorange.com")
        java.lang.String account,
        @WebParam(name = "password", targetNamespace = "http://service.nineorange.com")
        java.lang.String password,
        @WebParam(name = "sendDateTime", targetNamespace = "http://service.nineorange.com")
        java.lang.String sendDateTime,
        @WebParam(name = "destmobile", targetNamespace = "http://service.nineorange.com")
        java.lang.String destmobile,
        @WebParam(name = "msgText", targetNamespace = "http://service.nineorange.com")
        java.lang.String msgText,
        @WebParam(name = "ext", targetNamespace = "http://service.nineorange.com")
        java.lang.String ext
    );

    @WebMethod
    @RequestWrapper(localName = "modifyPassword", targetNamespace = "http://service.nineorange.com", className = "com.landaojia.washclothes.userappserver.service.sms.jianzhou.ws.ModifyPassword")
    @ResponseWrapper(localName = "modifyPasswordResponse", targetNamespace = "http://service.nineorange.com", className = "com.landaojia.washclothes.userappserver.service.sms.jianzhou.ws.ModifyPasswordResponse")
    @WebResult(name = "modifyPasswordReturn", targetNamespace = "http://service.nineorange.com")
    public int modifyPassword(
        @WebParam(name = "account", targetNamespace = "http://service.nineorange.com")
        java.lang.String account,
        @WebParam(name = "oldPassword", targetNamespace = "http://service.nineorange.com")
        java.lang.String oldPassword,
        @WebParam(name = "newPassword", targetNamespace = "http://service.nineorange.com")
        java.lang.String newPassword
    );

    @WebMethod
    @SOAPBinding(parameterStyle = SOAPBinding.ParameterStyle.BARE)
    @WebResult(name = "gxmtResponse", targetNamespace = "http://service.nineorange.com", partName = "parameters")
    public GxmtResponse gxmt(
        @WebParam(partName = "parameters", name = "gxmt", targetNamespace = "http://service.nineorange.com")
        Gxmt parameters
    );

    @WebMethod
    @RequestWrapper(localName = "sendMessage", targetNamespace = "http://service.nineorange.com", className = "com.landaojia.washclothes.userappserver.service.sms.jianzhou.ws.SendMessage")
    @ResponseWrapper(localName = "sendMessageResponse", targetNamespace = "http://service.nineorange.com", className = "com.landaojia.washclothes.userappserver.service.sms.jianzhou.ws.SendMessageResponse")
    @WebResult(name = "sendMessageReturn", targetNamespace = "http://service.nineorange.com")
    public int sendMessage(
        @WebParam(name = "account", targetNamespace = "http://service.nineorange.com")
        java.lang.String account,
        @WebParam(name = "password", targetNamespace = "http://service.nineorange.com")
        java.lang.String password,
        @WebParam(name = "destmobile", targetNamespace = "http://service.nineorange.com")
        java.lang.String destmobile,
        @WebParam(name = "msgText", targetNamespace = "http://service.nineorange.com")
        java.lang.String msgText
    );

    @WebMethod
    @RequestWrapper(localName = "sendMmsMessages", targetNamespace = "http://service.nineorange.com", className = "com.landaojia.washclothes.userappserver.service.sms.jianzhou.ws.SendMmsMessages")
    @ResponseWrapper(localName = "sendMmsMessagesResponse", targetNamespace = "http://service.nineorange.com", className = "com.landaojia.washclothes.userappserver.service.sms.jianzhou.ws.SendMmsMessagesResponse")
    @WebResult(name = "sendMmsMessagesReturn", targetNamespace = "http://service.nineorange.com")
    public int sendMmsMessages(
        @WebParam(name = "account", targetNamespace = "http://service.nineorange.com")
        java.lang.String account,
        @WebParam(name = "password", targetNamespace = "http://service.nineorange.com")
        java.lang.String password,
        @WebParam(name = "phones", targetNamespace = "http://service.nineorange.com")
        java.lang.String phones,
        @WebParam(name = "mmsString", targetNamespace = "http://service.nineorange.com")
        java.lang.String mmsString
    );

    @WebMethod
    @RequestWrapper(localName = "sendTimelyMessage", targetNamespace = "http://service.nineorange.com", className = "com.landaojia.washclothes.userappserver.service.sms.jianzhou.ws.SendTimelyMessage")
    @ResponseWrapper(localName = "sendTimelyMessageResponse", targetNamespace = "http://service.nineorange.com", className = "com.landaojia.washclothes.userappserver.service.sms.jianzhou.ws.SendTimelyMessageResponse")
    @WebResult(name = "sendTimelyMessageReturn", targetNamespace = "http://service.nineorange.com")
    public int sendTimelyMessage(
        @WebParam(name = "account", targetNamespace = "http://service.nineorange.com")
        java.lang.String account,
        @WebParam(name = "password", targetNamespace = "http://service.nineorange.com")
        java.lang.String password,
        @WebParam(name = "sendDateTime", targetNamespace = "http://service.nineorange.com")
        java.lang.String sendDateTime,
        @WebParam(name = "destmobile", targetNamespace = "http://service.nineorange.com")
        java.lang.String destmobile,
        @WebParam(name = "msgText", targetNamespace = "http://service.nineorange.com")
        java.lang.String msgText
    );

    @WebMethod
    @RequestWrapper(localName = "getReceipt", targetNamespace = "http://service.nineorange.com", className = "com.landaojia.washclothes.userappserver.service.sms.jianzhou.ws.GetReceipt")
    @ResponseWrapper(localName = "getReceiptResponse", targetNamespace = "http://service.nineorange.com", className = "com.landaojia.washclothes.userappserver.service.sms.jianzhou.ws.GetReceiptResponse")
    @WebResult(name = "getReceiptReturn", targetNamespace = "http://service.nineorange.com")
    public java.lang.String getReceipt(
        @WebParam(name = "account", targetNamespace = "http://service.nineorange.com")
        java.lang.String account,
        @WebParam(name = "password", targetNamespace = "http://service.nineorange.com")
        java.lang.String password,
        @WebParam(name = "taskID", targetNamespace = "http://service.nineorange.com")
        int taskID
    );
}
