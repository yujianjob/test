
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
 *         &lt;element name="validateUserReturn" type="{http://www.w3.org/2001/XMLSchema}int"/&gt;
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
    "validateUserReturn"
})
@XmlRootElement(name = "validateUserResponse")
public class ValidateUserResponse {

    protected int validateUserReturn;

    /**
     * ��ȡvalidateUserReturn���Ե�ֵ��
     * 
     */
    public int getValidateUserReturn() {
        return validateUserReturn;
    }

    /**
     * ����validateUserReturn���Ե�ֵ��
     * 
     */
    public void setValidateUserReturn(int value) {
        this.validateUserReturn = value;
    }

}
