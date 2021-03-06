package com.landaojia.washclothes.laundry.client.washfactory.yibeijie.ws;

import javax.jws.WebMethod;
import javax.jws.WebParam;
import javax.jws.WebResult;
import javax.jws.WebService;
import javax.xml.bind.annotation.XmlSeeAlso;
import javax.xml.ws.Action;
import javax.xml.ws.RequestWrapper;
import javax.xml.ws.ResponseWrapper;

/**
 * This class was generated by Apache CXF 3.0.4
 * 2015-02-28T17:18:17.195+08:00
 * Generated source version: 3.0.4
 * 
 */
@WebService(targetNamespace = "http://tempuri.org/", name = "IOrderService")
@XmlSeeAlso({ObjectFactory.class})
public interface IOrderService {

    @WebResult(name = "OrderFeedbackResult", targetNamespace = "http://tempuri.org/")
    @Action(input = "http://tempuri.org/IOrderService/OrderFeedback", output = "http://tempuri.org/IOrderService/OrderFeedbackResponse")
    @RequestWrapper(localName = "OrderFeedback", targetNamespace = "http://tempuri.org/", className = "com.landaojia.washclothes.laundry.client.washfactory.yibeijie.ws.OrderFeedback")
    @WebMethod(operationName = "OrderFeedback", action = "http://tempuri.org/IOrderService/OrderFeedback")
    @ResponseWrapper(localName = "OrderFeedbackResponse", targetNamespace = "http://tempuri.org/", className = "com.landaojia.washclothes.laundry.client.washfactory.yibeijie.ws.OrderFeedbackResponse")
    public java.lang.String orderFeedback(
        @WebParam(name = "arg_no", targetNamespace = "http://tempuri.org/")
        java.lang.String argNo,
        @WebParam(name = "arg_order", targetNamespace = "http://tempuri.org/")
        java.lang.String argOrder,
        @WebParam(name = "arg_pw", targetNamespace = "http://tempuri.org/")
        java.lang.String argPw
    );

    @WebResult(name = "QuotedPriceResult", targetNamespace = "http://tempuri.org/")
    @Action(input = "http://tempuri.org/IOrderService/QuotedPrice", output = "http://tempuri.org/IOrderService/QuotedPriceResponse")
    @RequestWrapper(localName = "QuotedPrice", targetNamespace = "http://tempuri.org/", className = "com.landaojia.washclothes.laundry.client.washfactory.yibeijie.ws.QuotedPrice")
    @WebMethod(operationName = "QuotedPrice", action = "http://tempuri.org/IOrderService/QuotedPrice")
    @ResponseWrapper(localName = "QuotedPriceResponse", targetNamespace = "http://tempuri.org/", className = "com.landaojia.washclothes.laundry.client.washfactory.yibeijie.ws.QuotedPriceResponse")
    public java.lang.Integer quotedPrice(
        @WebParam(name = "arg_phone", targetNamespace = "http://tempuri.org/")
        java.lang.String argPhone,
        @WebParam(name = "arg_order", targetNamespace = "http://tempuri.org/")
        java.lang.String argOrder,
        @WebParam(name = "arg_pw", targetNamespace = "http://tempuri.org/")
        java.lang.String argPw
    );

    @WebResult(name = "OrderUpLoadResult", targetNamespace = "http://tempuri.org/")
    @Action(input = "http://tempuri.org/IOrderService/OrderUpLoad", output = "http://tempuri.org/IOrderService/OrderUpLoadResponse")
    @RequestWrapper(localName = "OrderUpLoad", targetNamespace = "http://tempuri.org/", className = "com.landaojia.washclothes.laundry.client.washfactory.yibeijie.ws.OrderUpLoad")
    @WebMethod(operationName = "OrderUpLoad", action = "http://tempuri.org/IOrderService/OrderUpLoad")
    @ResponseWrapper(localName = "OrderUpLoadResponse", targetNamespace = "http://tempuri.org/", className = "com.landaojia.washclothes.laundry.client.washfactory.yibeijie.ws.OrderUpLoadResponse")
    public java.lang.Integer orderUpLoad(
        @WebParam(name = "arg_order", targetNamespace = "http://tempuri.org/")
        java.lang.String argOrder,
        @WebParam(name = "arg_pw", targetNamespace = "http://tempuri.org/")
        java.lang.String argPw
    );

    @Action(input = "http://tempuri.org/IOrderService/DoWork", output = "http://tempuri.org/IOrderService/DoWorkResponse")
    @RequestWrapper(localName = "DoWork", targetNamespace = "http://tempuri.org/", className = "com.landaojia.washclothes.laundry.client.washfactory.yibeijie.ws.DoWork")
    @WebMethod(operationName = "DoWork", action = "http://tempuri.org/IOrderService/DoWork")
    @ResponseWrapper(localName = "DoWorkResponse", targetNamespace = "http://tempuri.org/", className = "com.landaojia.washclothes.laundry.client.washfactory.yibeijie.ws.DoWorkResponse")
    public void doWork();
}
