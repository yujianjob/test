
package com.landaojia.washclothes.userappserver.service.sms.jianzhou.ws;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>anonymous complex type的 Java 类。
 * 
 * <p>以下模式片段指定包含在此类中的预期内容。
 * 
 * <pre>
 * &lt;complexType&gt;
 *   &lt;complexContent&gt;
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType"&gt;
 *       &lt;sequence&gt;
 *         &lt;element name="sendPersonalMessagesReturn" type="{http://www.w3.org/2001/XMLSchema}int"/&gt;
 *       &lt;/sequence&gt;
 *     &lt;/restriction&gt;
 *   &lt;/complexContent&gt;
 * &lt;/complexType&gt;
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "", propOrder = {
    "sendPersonalMessagesReturn"
})
@XmlRootElement(name = "sendPersonalMessagesResponse")
public class SendPersonalMessagesResponse {

    protected int sendPersonalMessagesReturn;

    /**
     * 获取sendPersonalMessagesReturn属性的值。
     * 
     */
    public int getSendPersonalMessagesReturn() {
        return sendPersonalMessagesReturn;
    }

    /**
     * 设置sendPersonalMessagesReturn属性的值。
     * 
     */
    public void setSendPersonalMessagesReturn(int value) {
        this.sendPersonalMessagesReturn = value;
    }

}
