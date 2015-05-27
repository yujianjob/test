package com.landaojia.washclothes.laundry.dao.exporder;

import java.math.BigDecimal;
import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;

/**
 * 物流订单
 * @author liuxi
 */
@Entity
@Table(name = "exp_order")
public class ExpOrderEo {
	private Long id;
	private String expNumber;
	private String outNumber;
	private Integer transportType;
	private Integer quickType;
	private Long districtId;
	private String address;
	private String contacts;
	private String mpno;
	private Date expTime;
	private Long operatorId;
	private Long nodeId;
	private Date operatorTime;
	private Date completeTime;
	private Integer allotStatus;
	private String packageInfo;
	private Integer packageCount;
	private BigDecimal chargeFee;
	private Integer step;
	private String stepRemark;
	private String userRemark;
	private String csRemark;
	private BigDecimal latitude;
	private BigDecimal longitude;
	private String systemRemark;
	private Integer objStatus;
	private String objRemark;
	private Date objCdate;
	private Long objCuser;
	private Date objMdate;
	private Long objMuser;
	private Date takeTime;
	private Date allotTime;
	private Integer callUserStatus;
	private Date callUserTime;
	private Integer callUserSecond;
	private String inviteCode;
	private Integer alarm;
	private Integer waitProcess;
	
	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	@Column(name = "id")
	public Long getId() {
		return id;
	}
	
	public void setId(Long id) {
		this.id = id;
	}

	@Column(name = "expNumber")
	public String getExpNumber() {
		return expNumber;
	}

	public void setExpNumber(String expNumber) {
		this.expNumber = expNumber;
	}

	@Column(name = "outNumber")
	public String getOutNumber() {
		return outNumber;
	}

	public void setOutNumber(String outNumber) {
		this.outNumber = outNumber;
	}

	@Column(name = "transportType")
	public Integer getTransportType() {
		return transportType;
	}

	public void setTransportType(Integer transportType) {
		this.transportType = transportType;
	}

	@Column(name = "quickType")
	public Integer getQuickType() {
		return quickType;
	}

	public void setQuickType(Integer quickType) {
		this.quickType = quickType;
	}

	@Column(name = "districtId")
	public Long getDistrictId() {
		return districtId;
	}

	public void setDistrictId(Long districtId) {
		this.districtId = districtId;
	}

	@Column(name = "address")
	public String getAddress() {
		return address;
	}

	public void setAddress(String address) {
		this.address = address;
	}

	@Column(name = "contacts")
	public String getContacts() {
		return contacts;
	}

	public void setContacts(String contacts) {
		this.contacts = contacts;
	}

	@Column(name = "mpno")
	public String getMpno() {
		return mpno;
	}

	public void setMpno(String mpno) {
		this.mpno = mpno;
	}

	@Column(name = "expTime")
	public Date getExpTime() {
		return expTime;
	}

	public void setExpTime(Date expTime) {
		this.expTime = expTime;
	}

	@Column(name = "operatorId")
	public Long getOperatorId() {
		return operatorId;
	}

	public void setOperatorId(Long operatorId) {
		this.operatorId = operatorId;
	}

	@Column(name = "nodeId")
	public Long getNodeId() {
		return nodeId;
	}

	public void setNodeId(Long nodeId) {
		this.nodeId = nodeId;
	}

	@Column(name = "operatorTime")
	public Date getOperatorTime() {
		return operatorTime;
	}

	public void setOperatorTime(Date operatorTime) {
		this.operatorTime = operatorTime;
	}

	@Column(name = "completeTime")
	public Date getCompleteTime() {
		return completeTime;
	}

	public void setCompleteTime(Date completeTime) {
		this.completeTime = completeTime;
	}

	@Column(name = "allotStatus")
	public Integer getAllotStatus() {
		return allotStatus;
	}

	public void setAllotStatus(Integer allotStatus) {
		this.allotStatus = allotStatus;
	}

	@Column(name = "packageInfo")
	public String getPackageInfo() {
		return packageInfo;
	}

	public void setPackageInfo(String packageInfo) {
		this.packageInfo = packageInfo;
	}

	@Column(name = "packageCount")
	public Integer getPackageCount() {
		return packageCount;
	}

	public void setPackageCount(Integer packageCount) {
		this.packageCount = packageCount;
	}

	@Column(name = "chargeFee")
	public BigDecimal getChargeFee() {
		return chargeFee;
	}

	public void setChargeFee(BigDecimal chargeFee) {
		this.chargeFee = chargeFee;
	}

	@Column(name = "step")
	public Integer getStep() {
		return step;
	}

	public void setStep(Integer step) {
		this.step = step;
	}

	@Column(name = "stepRemark")
	public String getStepRemark() {
		return stepRemark;
	}

	public void setStepRemark(String stepRemark) {
		this.stepRemark = stepRemark;
	}

	@Column(name = "userRemark")
	public String getUserRemark() {
		return userRemark;
	}

	public void setUserRemark(String userRemark) {
		this.userRemark = userRemark;
	}

	@Column(name = "csRemark")
	public String getCsRemark() {
		return csRemark;
	}

	public void setCsRemark(String csRemark) {
		this.csRemark = csRemark;
	}

	@Column(name = "latitude")
	public BigDecimal getLatitude() {
		return latitude;
	}

	public void setLatitude(BigDecimal latitude) {
		this.latitude = latitude;
	}

	@Column(name = "longitude")
	public BigDecimal getLongitude() {
		return longitude;
	}

	public void setLongitude(BigDecimal longitude) {
		this.longitude = longitude;
	}

	@Column(name = "systemRemark")
	public String getSystemRemark() {
		return systemRemark;
	}

	public void setSystemRemark(String systemRemark) {
		this.systemRemark = systemRemark;
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

	@Column(name = "takeTime")
	public Date getTakeTime() {
		return takeTime;
	}

	public void setTakeTime(Date takeTime) {
		this.takeTime = takeTime;
	}

	@Column(name = "allotTime")
	public Date getAllotTime() {
		return allotTime;
	}

	public void setAllotTime(Date allotTime) {
		this.allotTime = allotTime;
	}

	@Column(name = "callUserStatus")
	public Integer getCallUserStatus() {
		return callUserStatus;
	}

	public void setCallUserStatus(Integer callUserStatus) {
		this.callUserStatus = callUserStatus;
	}

	@Column(name = "callUserTime")
	public Date getCallUserTime() {
		return callUserTime;
	}

	public void setCallUserTime(Date callUserTime) {
		this.callUserTime = callUserTime;
	}

	@Column(name = "callUserSecond")
	public Integer getCallUserSecond() {
		return callUserSecond;
	}

	public void setCallUserSecond(Integer callUserSecond) {
		this.callUserSecond = callUserSecond;
	}

	@Column(name = "inviteCode")
	public String getInviteCode() {
		return inviteCode;
	}

	public void setInviteCode(String inviteCode) {
		this.inviteCode = inviteCode;
	}

	@Column(name = "alarm")
	public Integer getAlarm() {
		return alarm;
	}

	public void setAlarm(Integer alarm) {
		this.alarm = alarm;
	}

	@Column(name = "waitProcess")
	public Integer getWaitProcess() {
		return waitProcess;
	}

	public void setWaitProcess(Integer waitProcess) {
		this.waitProcess = waitProcess;
	}
}
