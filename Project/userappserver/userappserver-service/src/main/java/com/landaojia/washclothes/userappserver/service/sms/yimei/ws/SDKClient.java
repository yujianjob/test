package com.landaojia.washclothes.userappserver.service.sms.yimei.ws;

import javax.jws.WebMethod;
import javax.jws.WebParam;
import javax.jws.WebResult;
import javax.jws.WebService;
import javax.xml.bind.annotation.XmlSeeAlso;
import javax.xml.ws.RequestWrapper;
import javax.xml.ws.ResponseWrapper;

/**
 * This class was generated by Apache CXF 3.0.4
 * 2015-03-16T15:52:16.567+08:00
 * Generated source version: 3.0.4
 * 
 */
@WebService(targetNamespace = "http://sdkhttp.eucp.b2m.cn/", name = "SDKClient")
@XmlSeeAlso({ObjectFactory.class})
public interface SDKClient {

    @WebMethod
    @RequestWrapper(localName = "registDetailInfo", targetNamespace = "http://sdkhttp.eucp.b2m.cn/", className = "com.landaojia.washclothes.userappserver.service.sms.yimei.ws.RegistDetailInfo")
    @ResponseWrapper(localName = "registDetailInfoResponse", targetNamespace = "http://sdkhttp.eucp.b2m.cn/", className = "com.landaojia.washclothes.userappserver.service.sms.yimei.ws.RegistDetailInfoResponse")
    @WebResult(name = "return", targetNamespace = "")
    public int registDetailInfo(
        @WebParam(name = "arg0", targetNamespace = "")
        java.lang.String arg0,
        @WebParam(name = "arg1", targetNamespace = "")
        java.lang.String arg1,
        @WebParam(name = "arg2", targetNamespace = "")
        java.lang.String arg2,
        @WebParam(name = "arg3", targetNamespace = "")
        java.lang.String arg3,
        @WebParam(name = "arg4", targetNamespace = "")
        java.lang.String arg4,
        @WebParam(name = "arg5", targetNamespace = "")
        java.lang.String arg5,
        @WebParam(name = "arg6", targetNamespace = "")
        java.lang.String arg6,
        @WebParam(name = "arg7", targetNamespace = "")
        java.lang.String arg7,
        @WebParam(name = "arg8", targetNamespace = "")
        java.lang.String arg8,
        @WebParam(name = "arg9", targetNamespace = "")
        java.lang.String arg9
    );

    @WebMethod
    @RequestWrapper(localName = "getMO", targetNamespace = "http://sdkhttp.eucp.b2m.cn/", className = "com.landaojia.washclothes.userappserver.service.sms.yimei.ws.GetMO")
    @ResponseWrapper(localName = "getMOResponse", targetNamespace = "http://sdkhttp.eucp.b2m.cn/", className = "com.landaojia.washclothes.userappserver.service.sms.yimei.ws.GetMOResponse")
    @WebResult(name = "return", targetNamespace = "")
    public java.util.List<com.landaojia.washclothes.userappserver.service.sms.yimei.ws.Mo> getMO(
        @WebParam(name = "arg0", targetNamespace = "")
        java.lang.String arg0,
        @WebParam(name = "arg1", targetNamespace = "")
        java.lang.String arg1
    );

    @WebMethod
    @RequestWrapper(localName = "getEachFee", targetNamespace = "http://sdkhttp.eucp.b2m.cn/", className = "com.landaojia.washclothes.userappserver.service.sms.yimei.ws.GetEachFee")
    @ResponseWrapper(localName = "getEachFeeResponse", targetNamespace = "http://sdkhttp.eucp.b2m.cn/", className = "com.landaojia.washclothes.userappserver.service.sms.yimei.ws.GetEachFeeResponse")
    @WebResult(name = "return", targetNamespace = "")
    public double getEachFee(
        @WebParam(name = "arg0", targetNamespace = "")
        java.lang.String arg0,
        @WebParam(name = "arg1", targetNamespace = "")
        java.lang.String arg1
    );

    @WebMethod
    @RequestWrapper(localName = "getVersion", targetNamespace = "http://sdkhttp.eucp.b2m.cn/", className = "com.landaojia.washclothes.userappserver.service.sms.yimei.ws.GetVersion")
    @ResponseWrapper(localName = "getVersionResponse", targetNamespace = "http://sdkhttp.eucp.b2m.cn/", className = "com.landaojia.washclothes.userappserver.service.sms.yimei.ws.GetVersionResponse")
    @WebResult(name = "return", targetNamespace = "")
    public java.lang.String getVersion();

    @WebMethod
    @RequestWrapper(localName = "serialPwdUpd", targetNamespace = "http://sdkhttp.eucp.b2m.cn/", className = "com.landaojia.washclothes.userappserver.service.sms.yimei.ws.SerialPwdUpd")
    @ResponseWrapper(localName = "serialPwdUpdResponse", targetNamespace = "http://sdkhttp.eucp.b2m.cn/", className = "com.landaojia.washclothes.userappserver.service.sms.yimei.ws.SerialPwdUpdResponse")
    @WebResult(name = "return", targetNamespace = "")
    public int serialPwdUpd(
        @WebParam(name = "arg0", targetNamespace = "")
        java.lang.String arg0,
        @WebParam(name = "arg1", targetNamespace = "")
        java.lang.String arg1,
        @WebParam(name = "arg2", targetNamespace = "")
        java.lang.String arg2,
        @WebParam(name = "arg3", targetNamespace = "")
        java.lang.String arg3
    );

    @WebMethod
    @RequestWrapper(localName = "getReport", targetNamespace = "http://sdkhttp.eucp.b2m.cn/", className = "com.landaojia.washclothes.userappserver.service.sms.yimei.ws.GetReport")
    @ResponseWrapper(localName = "getReportResponse", targetNamespace = "http://sdkhttp.eucp.b2m.cn/", className = "com.landaojia.washclothes.userappserver.service.sms.yimei.ws.GetReportResponse")
    @WebResult(name = "return", targetNamespace = "")
    public java.util.List<com.landaojia.washclothes.userappserver.service.sms.yimei.ws.StatusReport> getReport(
        @WebParam(name = "arg0", targetNamespace = "")
        java.lang.String arg0,
        @WebParam(name = "arg1", targetNamespace = "")
        java.lang.String arg1
    );

    @WebMethod
    @RequestWrapper(localName = "cancelMOForward", targetNamespace = "http://sdkhttp.eucp.b2m.cn/", className = "com.landaojia.washclothes.userappserver.service.sms.yimei.ws.CancelMOForward")
    @ResponseWrapper(localName = "cancelMOForwardResponse", targetNamespace = "http://sdkhttp.eucp.b2m.cn/", className = "com.landaojia.washclothes.userappserver.service.sms.yimei.ws.CancelMOForwardResponse")
    @WebResult(name = "return", targetNamespace = "")
    public int cancelMOForward(
        @WebParam(name = "arg0", targetNamespace = "")
        java.lang.String arg0,
        @WebParam(name = "arg1", targetNamespace = "")
        java.lang.String arg1
    );

    @WebMethod
    @RequestWrapper(localName = "logout", targetNamespace = "http://sdkhttp.eucp.b2m.cn/", className = "com.landaojia.washclothes.userappserver.service.sms.yimei.ws.Logout")
    @ResponseWrapper(localName = "logoutResponse", targetNamespace = "http://sdkhttp.eucp.b2m.cn/", className = "com.landaojia.washclothes.userappserver.service.sms.yimei.ws.LogoutResponse")
    @WebResult(name = "return", targetNamespace = "")
    public int logout(
        @WebParam(name = "arg0", targetNamespace = "")
        java.lang.String arg0,
        @WebParam(name = "arg1", targetNamespace = "")
        java.lang.String arg1
    );

    @WebMethod
    @RequestWrapper(localName = "getBalance", targetNamespace = "http://sdkhttp.eucp.b2m.cn/", className = "com.landaojia.washclothes.userappserver.service.sms.yimei.ws.GetBalance")
    @ResponseWrapper(localName = "getBalanceResponse", targetNamespace = "http://sdkhttp.eucp.b2m.cn/", className = "com.landaojia.washclothes.userappserver.service.sms.yimei.ws.GetBalanceResponse")
    @WebResult(name = "return", targetNamespace = "")
    public double getBalance(
        @WebParam(name = "arg0", targetNamespace = "")
        java.lang.String arg0,
        @WebParam(name = "arg1", targetNamespace = "")
        java.lang.String arg1
    );

    @WebMethod
    @RequestWrapper(localName = "sendSMS", targetNamespace = "http://sdkhttp.eucp.b2m.cn/", className = "com.landaojia.washclothes.userappserver.service.sms.yimei.ws.SendSMS")
    @ResponseWrapper(localName = "sendSMSResponse", targetNamespace = "http://sdkhttp.eucp.b2m.cn/", className = "com.landaojia.washclothes.userappserver.service.sms.yimei.ws.SendSMSResponse")
    @WebResult(name = "return", targetNamespace = "")
    public int sendSMS(
        @WebParam(name = "arg0", targetNamespace = "")
        java.lang.String arg0,
        @WebParam(name = "arg1", targetNamespace = "")
        java.lang.String arg1,
        @WebParam(name = "arg2", targetNamespace = "")
        java.lang.String arg2,
        @WebParam(name = "arg3", targetNamespace = "")
        java.util.List<java.lang.String> arg3,
        @WebParam(name = "arg4", targetNamespace = "")
        java.lang.String arg4,
        @WebParam(name = "arg5", targetNamespace = "")
        java.lang.String arg5,
        @WebParam(name = "arg6", targetNamespace = "")
        java.lang.String arg6,
        @WebParam(name = "arg7", targetNamespace = "")
        int arg7,
        @WebParam(name = "arg8", targetNamespace = "")
        long arg8
    );

    @WebMethod
    @RequestWrapper(localName = "chargeUp", targetNamespace = "http://sdkhttp.eucp.b2m.cn/", className = "com.landaojia.washclothes.userappserver.service.sms.yimei.ws.ChargeUp")
    @ResponseWrapper(localName = "chargeUpResponse", targetNamespace = "http://sdkhttp.eucp.b2m.cn/", className = "com.landaojia.washclothes.userappserver.service.sms.yimei.ws.ChargeUpResponse")
    @WebResult(name = "return", targetNamespace = "")
    public int chargeUp(
        @WebParam(name = "arg0", targetNamespace = "")
        java.lang.String arg0,
        @WebParam(name = "arg1", targetNamespace = "")
        java.lang.String arg1,
        @WebParam(name = "arg2", targetNamespace = "")
        java.lang.String arg2,
        @WebParam(name = "arg3", targetNamespace = "")
        java.lang.String arg3
    );

    @WebMethod
    @RequestWrapper(localName = "registEx", targetNamespace = "http://sdkhttp.eucp.b2m.cn/", className = "com.landaojia.washclothes.userappserver.service.sms.yimei.ws.RegistEx")
    @ResponseWrapper(localName = "registExResponse", targetNamespace = "http://sdkhttp.eucp.b2m.cn/", className = "com.landaojia.washclothes.userappserver.service.sms.yimei.ws.RegistExResponse")
    @WebResult(name = "return", targetNamespace = "")
    public int registEx(
        @WebParam(name = "arg0", targetNamespace = "")
        java.lang.String arg0,
        @WebParam(name = "arg1", targetNamespace = "")
        java.lang.String arg1,
        @WebParam(name = "arg2", targetNamespace = "")
        java.lang.String arg2
    );

    @WebMethod
    @RequestWrapper(localName = "setMOForwardEx", targetNamespace = "http://sdkhttp.eucp.b2m.cn/", className = "com.landaojia.washclothes.userappserver.service.sms.yimei.ws.SetMOForwardEx")
    @ResponseWrapper(localName = "setMOForwardExResponse", targetNamespace = "http://sdkhttp.eucp.b2m.cn/", className = "com.landaojia.washclothes.userappserver.service.sms.yimei.ws.SetMOForwardExResponse")
    @WebResult(name = "return", targetNamespace = "")
    public int setMOForwardEx(
        @WebParam(name = "arg0", targetNamespace = "")
        java.lang.String arg0,
        @WebParam(name = "arg1", targetNamespace = "")
        java.lang.String arg1,
        @WebParam(name = "arg2", targetNamespace = "")
        java.util.List<java.lang.String> arg2
    );

    @WebMethod
    @RequestWrapper(localName = "sendVoice", targetNamespace = "http://sdkhttp.eucp.b2m.cn/", className = "com.landaojia.washclothes.userappserver.service.sms.yimei.ws.SendVoice")
    @ResponseWrapper(localName = "sendVoiceResponse", targetNamespace = "http://sdkhttp.eucp.b2m.cn/", className = "com.landaojia.washclothes.userappserver.service.sms.yimei.ws.SendVoiceResponse")
    @WebResult(name = "return", targetNamespace = "")
    public java.lang.String sendVoice(
        @WebParam(name = "arg0", targetNamespace = "")
        java.lang.String arg0,
        @WebParam(name = "arg1", targetNamespace = "")
        java.lang.String arg1,
        @WebParam(name = "arg2", targetNamespace = "")
        java.lang.String arg2,
        @WebParam(name = "arg3", targetNamespace = "")
        java.util.List<java.lang.String> arg3,
        @WebParam(name = "arg4", targetNamespace = "")
        java.lang.String arg4,
        @WebParam(name = "arg5", targetNamespace = "")
        java.lang.String arg5,
        @WebParam(name = "arg6", targetNamespace = "")
        java.lang.String arg6,
        @WebParam(name = "arg7", targetNamespace = "")
        int arg7,
        @WebParam(name = "arg8", targetNamespace = "")
        long arg8
    );

    @WebMethod
    @RequestWrapper(localName = "setMOForward", targetNamespace = "http://sdkhttp.eucp.b2m.cn/", className = "com.landaojia.washclothes.userappserver.service.sms.yimei.ws.SetMOForward")
    @ResponseWrapper(localName = "setMOForwardResponse", targetNamespace = "http://sdkhttp.eucp.b2m.cn/", className = "com.landaojia.washclothes.userappserver.service.sms.yimei.ws.SetMOForwardResponse")
    @WebResult(name = "return", targetNamespace = "")
    public int setMOForward(
        @WebParam(name = "arg0", targetNamespace = "")
        java.lang.String arg0,
        @WebParam(name = "arg1", targetNamespace = "")
        java.lang.String arg1,
        @WebParam(name = "arg2", targetNamespace = "")
        java.lang.String arg2
    );
}
