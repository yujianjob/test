
package com.landaojia.washclothes.laundry.client.washfactory.yibeijie.ws;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;


/**
 * <pre>
 * &lt;complexType&gt;
 *   &lt;complexContent&gt;
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType"&gt;
 *       &lt;sequence&gt;
 *         &lt;element name="QuotedPriceResult" type="{http://www.w3.org/2001/XMLSchema}int" minOccurs="0"/&gt;
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
    "quotedPriceResult"
})
@XmlRootElement(name = "QuotedPriceResponse")
public class QuotedPriceResponse {

    @XmlElement(name = "QuotedPriceResult")
    protected Integer quotedPriceResult;

    /**
     * @return
     *     possible object is
     *     {@link Integer }
     *     
     */
    public Integer getQuotedPriceResult() {
        return quotedPriceResult;
    }

    /**
     * @param value
     *     allowed object is
     *     {@link Integer }
     *     
     */
    public void setQuotedPriceResult(Integer value) {
        this.quotedPriceResult = value;
    }

}
