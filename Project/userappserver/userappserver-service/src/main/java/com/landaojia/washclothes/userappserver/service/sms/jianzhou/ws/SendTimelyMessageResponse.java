
package com.landaojia.washclothes.userappserver.service.sms.jianzhou.ws;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>anonymous complex type�� Java �ࡣ
 * 
 * <p>����ģʽƬ��ָ�������ڴ����е�Ԥ�����ݡ�
 * 
 * <pre>
 * &lt;complexType&gt;
 *   &lt;complexContent&gt;
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType"&gt;
 *       &lt;sequence&gt;
 *         &lt;element name="sendTimelyMessageReturn" type="{http://www.w3.org/2001/XMLSchema}int"/&gt;
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
    "sendTimelyMessageReturn"
})
@XmlRootElement(name = "sendTimelyMessageResponse")
public class SendTimelyMessageResponse {

    protected int sendTimelyMessageReturn;

    /**
     * ��ȡsendTimelyMessageReturn���Ե�ֵ��
     * 
     */
    public int getSendTimelyMessageReturn() {
        return sendTimelyMessageReturn;
    }

    /**
     * ����sendTimelyMessageReturn���Ե�ֵ��
     * 
     */
    public void setSendTimelyMessageReturn(int value) {
        this.sendTimelyMessageReturn = value;
    }

}
