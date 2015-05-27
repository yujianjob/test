
package com.landaojia.washclothes.laundry.client.washfactory.yibeijie.ws;

import javax.xml.bind.JAXBElement;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElementRef;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;


/**
 * <pre>
 * &lt;complexType&gt;
 *   &lt;complexContent&gt;
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType"&gt;
 *       &lt;sequence&gt;
 *         &lt;element name="arg_phone" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/&gt;
 *         &lt;element name="arg_order" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/&gt;
 *         &lt;element name="arg_pw" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/&gt;
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
    "argPhone",
    "argOrder",
    "argPw"
})
@XmlRootElement(name = "QuotedPrice")
public class QuotedPrice {

    @XmlElementRef(name = "arg_phone", namespace = "http://tempuri.org/", type = JAXBElement.class, required = false)
    protected JAXBElement<String> argPhone;
    @XmlElementRef(name = "arg_order", namespace = "http://tempuri.org/", type = JAXBElement.class, required = false)
    protected JAXBElement<String> argOrder;
    @XmlElementRef(name = "arg_pw", namespace = "http://tempuri.org/", type = JAXBElement.class, required = false)
    protected JAXBElement<String> argPw;

    /**
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public JAXBElement<String> getArgPhone() {
        return argPhone;
    }

    /**
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public void setArgPhone(JAXBElement<String> value) {
        this.argPhone = value;
    }

    /**
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public JAXBElement<String> getArgOrder() {
        return argOrder;
    }

    /**
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public void setArgOrder(JAXBElement<String> value) {
        this.argOrder = value;
    }

    /**
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public JAXBElement<String> getArgPw() {
        return argPw;
    }

    /**
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public void setArgPw(JAXBElement<String> value) {
        this.argPw = value;
    }

}
