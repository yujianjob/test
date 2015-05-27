package com.landaojia.washclothes.laundry.dao.orderorder;

import java.math.BigDecimal;
import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;

/**
 * 订单
 * @author liuxi
 */
@Entity
@Table(name = "order_order")
public class OrderOrderEo {
	private Long id;
	private String orderNumber;
	private Long orderClass;
	private Integer orderType;
	private String userId;
	private String title;
	private BigDecimal sourceAmount;
	private BigDecimal totalAmount;
	private BigDecimal payAmount;
	private Integer payStatus;
	private Integer payType;
	private String userRemark;
	private Date expectTime;
	private String csRemark;
	private String systemRemark;
	private String printRemark;
	private Long relationId;
	private String relationNo;
	private Integer orderStatus;
	private Integer site;
	private Date completeTime;
	private Date allFinishTime;
	private Integer channel;
	private String getExpressNumber;
	private Integer getExpressType;
	private String sendexPressNumber;
	private Integer step;
	private Date step1Time;
	private Date step2Time;
	private Date step3Time;
	private Date step4Time;
	private Date step5Time;
	private Date step6Time;
	private Date step7Time;
	private Integer expressStatus;
	private Date pushExpressTime;
	private Integer objStatus;
	private String objRemark;
	private Date objCdate ;
	private Long objCuser;
	private Date objMdate;
	private Long objMuser;
	private Integer sendType;
	private String inviteCode;
	private Integer commissionStatus;
	private Date commissionTime;
	private BigDecimal tmpPayAmount;
	
	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	@Column(name = "id")
	public Long getId() {
		return id;
	}
	public void setId(Long id) {
		this.id = id;
	}
	
	@Column(name = "orderNumber")
	public String getOrderNumber() {
		return orderNumber;
	}
	public void setOrderNumber(String orderNumber) {
		this.orderNumber = orderNumber;
	}
	
	@Column(name = "orderClass")
	public Long getOrderClass() {
		return orderClass;
	}
	public void setOrderClass(Long orderClass) {
		this.orderClass = orderClass;
	}
	
	@Column(name = "orderType")
	public Integer getOrderType() {
		return orderType;
	}
	public void setOrderType(Integer orderType) {
		this.orderType = orderType;
	}
	
	@Column(name = "userId")
	public String getUserId() {
		return userId;
	}
	public void setUserId(String userId) {
		this.userId = userId;
	}
	
	@Column(name = "title")
	public String getTitle() {
		return title;
	}
	public void setTitle(String title) {
		this.title = title;
	}
	
	@Column(name = "sourceAmount")
	public BigDecimal getSourceAmount() {
		return sourceAmount;
	}
	public void setSourceAmount(BigDecimal sourceAmount) {
		this.sourceAmount = sourceAmount;
	}
	
	@Column(name = "totalAmount")
	public BigDecimal getTotalAmount() {
		return totalAmount;
	}
	public void setTotalAmount(BigDecimal totalAmount) {
		this.totalAmount = totalAmount;
	}
	
	@Column(name = "payAmount")
	public BigDecimal getPayAmount() {
		return payAmount;
	}
	public void setPayAmount(BigDecimal payAmount) {
		this.payAmount = payAmount;
	}
	
	@Column(name = "payStatus")
	public Integer getPayStatus() {
		return payStatus;
	}
	public void setPayStatus(Integer payStatus) {
		this.payStatus = payStatus;
	}
	
	@Column(name = "payType")
	public Integer getPayType() {
		return payType;
	}
	public void setPayType(Integer payType) {
		this.payType = payType;
	}
	
	@Column(name = "userRemark")
	public String getUserRemark() {
		return userRemark;
	}
	public void setUserRemark(String userRemark) {
		this.userRemark = userRemark;
	}
	
	@Column(name = "expectTime")
	public Date getExpectTime() {
		return expectTime;
	}
	public void setExpectTime(Date expectTime) {
		this.expectTime = expectTime;
	}
	
	@Column(name = "csRemark")
	public String getCsRemark() {
		return csRemark;
	}
	public void setCsRemark(String csRemark) {
		this.csRemark = csRemark;
	}
	
	@Column(name = "systemRemark")
	public String getSystemRemark() {
		return systemRemark;
	}
	public void setSystemRemark(String systemRemark) {
		this.systemRemark = systemRemark;
	}
	
	@Column(name = "printRemark")
	public String getPrintRemark() {
		return printRemark;
	}
	public void setPrintRemark(String printRemark) {
		this.printRemark = printRemark;
	}
	
	@Column(name = "relationId")
	public Long getRelationId() {
		return relationId;
	}
	public void setRelationId(Long relationId) {
		this.relationId = relationId;
	}
	
	@Column(name = "relationNo")
	public String getRelationNo() {
		return relationNo;
	}
	public void setRelationNo(String relationNo) {
		this.relationNo = relationNo;
	}
	
	@Column(name = "orderStatus")
	public Integer getOrderStatus() {
		return orderStatus;
	}
	public void setOrderStatus(Integer orderStatus) {
		this.orderStatus = orderStatus;
	}
	
	@Column(name = "site")
	public Integer getSite() {
		return site;
	}
	public void setSite(Integer site) {
		this.site = site;
	}
	
	@Column(name = "completeTime")
	public Date getCompleteTime() {
		return completeTime;
	}
	public void setCompleteTime(Date completeTime) {
		this.completeTime = completeTime;
	}
	
	@Column(name = "allFinishTime")
	public Date getAllFinishTime() {
		return allFinishTime;
	}
	public void setAllFinishTime(Date allFinishTime) {
		this.allFinishTime = allFinishTime;
	}
	
	@Column(name = "channel")
	public Integer getChannel() {
		return channel;
	}
	public void setChannel(Integer channel) {
		this.channel = channel;
	}
	
	@Column(name = "getExpressNumber")
	public String getGetExpressNumber() {
		return getExpressNumber;
	}
	public void setGetExpressNumber(String getExpressNumber) {
		this.getExpressNumber = getExpressNumber;
	}
	
	@Column(name = "getExpressType")
	public Integer getGetExpressType() {
		return getExpressType;
	}
	public void setGetExpressType(Integer getExpressType) {
		this.getExpressType = getExpressType;
	}
	
	@Column(name = "sendexPressNumber")
	public String getSendexPressNumber() {
		return sendexPressNumber;
	}
	public void setSendexPressNumber(String sendexPressNumber) {
		this.sendexPressNumber = sendexPressNumber;
	}
	
	@Column(name = "step")
	public Integer getStep() {
		return step;
	}
	public void setStep(Integer step) {
		this.step = step;
	}
	
	@Column(name = "step1Time")
	public Date getStep1Time() {
		return step1Time;
	}
	public void setStep1Time(Date step1Time) {
		this.step1Time = step1Time;
	}
	
	@Column(name = "step2Time")
	public Date getStep2Time() {
		return step2Time;
	}
	public void setStep2Time(Date step2Time) {
		this.step2Time = step2Time;
	}
	
	@Column(name = "step3Time")
	public Date getStep3Time() {
		return step3Time;
	}
	public void setStep3Time(Date step3Time) {
		this.step3Time = step3Time;
	}
	
	@Column(name = "step4Time")
	public Date getStep4Time() {
		return step4Time;
	}
	public void setStep4Time(Date step4Time) {
		this.step4Time = step4Time;
	}
	
	@Column(name = "step5Time")
	public Date getStep5Time() {
		return step5Time;
	}
	public void setStep5Time(Date step5Time) {
		this.step5Time = step5Time;
	}
	
	@Column(name = "step6Time")
	public Date getStep6Time() {
		return step6Time;
	}
	public void setStep6Time(Date step6Time) {
		this.step6Time = step6Time;
	}
	
	@Column(name = "step7Time")
	public Date getStep7Time() {
		return step7Time;
	}
	public void setStep7Time(Date step7Time) {
		this.step7Time = step7Time;
	}
	
	@Column(name = "expressStatus")
	public Integer getExpressStatus() {
		return expressStatus;
	}
	public void setExpressStatus(Integer expressStatus) {
		this.expressStatus = expressStatus;
	}
	
	@Column(name = "pushExpressTime")
	public Date getPushExpressTime() {
		return pushExpressTime;
	}
	public void setPushExpressTime(Date pushExpressTime) {
		this.pushExpressTime = pushExpressTime;
	}
	
	@Column(name = "obj_Status")
	public Integer getObjStatus() {
		return objStatus;
	}
	public void setObjStatus(Integer objStatus) {
		this.objStatus = objStatus;
	}
	
	@Column(name = "obj_Remark")
	public String getObjRemark() {
		return objRemark;
	}
	public void setObjRemark(String objRemark) {
		this.objRemark = objRemark;
	}
	
	@Column(name = "obj_Cdate")
	public Date getObjCdate() {
		return objCdate;
	}
	public void setObjCdate(Date objCdate) {
		this.objCdate = objCdate;
	}
	
	@Column(name = "obj_Cuser")
	public Long getObjCuser() {
		return objCuser;
	}
	public void setObjCuser(Long objCuser) {
		this.objCuser = objCuser;
	}
	
	@Column(name = "obj_Mdate")
	public Date getObjMdate() {
		return objMdate;
	}
	public void setObjMdate(Date objMdate) {
		this.objMdate = objMdate;
	}
	
	@Column(name = "obj_Muser")
	public Long getObjMuser() {
		return objMuser;
	}
	public void setObjMuser(Long objMuser) {
		this.objMuser = objMuser;
	}
	
	@Column(name = "sendType")
	public Integer getSendType() {
		return sendType;
	}
	public void setSendType(Integer sendType) {
		this.sendType = sendType;
	}
	
	@Column(name = "inviteCode")
	public String getInviteCode() {
		return inviteCode;
	}
	public void setInviteCode(String inviteCode) {
		this.inviteCode = inviteCode;
	}
	
	@Column(name = "commissionStatus")
	public Integer getCommissionStatus() {
		return commissionStatus;
	}
	public void setCommissionStatus(Integer commissionStatus) {
		this.commissionStatus = commissionStatus;
	}
	
	@Column(name = "commissionTime")
	public Date getCommissionTime() {
		return commissionTime;
	}
	public void setCommissionTime(Date commissionTime) {
		this.commissionTime = commissionTime;
	}
	
	@Column(name = "tmp_PayAmount")
	public BigDecimal getTmpPayAmount() {
		return tmpPayAmount;
	}
	public void setTmpPayAmount(BigDecimal tmpPayAmount) {
		this.tmpPayAmount = tmpPayAmount;
	}
}
