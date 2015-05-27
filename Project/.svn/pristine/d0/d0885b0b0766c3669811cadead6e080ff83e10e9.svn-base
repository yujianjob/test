package com.landaojia.washclothes.userappserver.dao.userconsigneeaddress;

import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;

/**
 * 用户收货地址
 * @author liuxi
 */
@Entity
@Table(name = "user_consigneeaddress")
public class UserConsigneeAddressEo{
	private Long id;
	private String userId;
	private String consignee;
	private Long districtId;
	private String address;
	private String mpNo;
	private String phone;
	private Boolean isDefault;
	private String zipCode;
	private String email;
	private Integer objStatus;
	private String objRemark;
	private Date objCdate;
	private Long objCuser;
	private Date objMdate;
	private Long objMuser;
	
	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	@Column(name = "id")
	public Long getId() {
		return id;
	}
	public void setId(Long id) {
		this.id = id;
	}
	
	@Column(name = "UserID")
	public String getUserId() {
		return userId;
	}
	public void setUserId(String userId) {
		this.userId = userId;
	}
	
	@Column(name = "Consignee")
	public String getConsignee() {
		return consignee;
	}
	public void setConsignee(String consignee) {
		this.consignee = consignee;
	}
	
	@Column(name = "DistrictID")
	public Long getDistrictId() {
		return districtId;
	}
	public void setDistrictId(Long districtId) {
		this.districtId = districtId;
	}
	
	@Column(name = "Address")
	public String getAddress() {
		return address;
	}
	public void setAddress(String address) {
		this.address = address;
	}
	
	@Column(name = "Mpno")
	public String getMpNo() {
		return mpNo;
	}
	public void setMpNo(String mpNo) {
		this.mpNo = mpNo;
	}
	
	@Column(name = "Phone")
	public String getPhone() {
		return phone;
	}
	public void setPhone(String phone) {
		this.phone = phone;
	}
	
	@Column(name = "IsDefault")
	public Boolean getIsDefault() {
		return isDefault;
	}
	public void setIsDefault(Boolean isDefault) {
		this.isDefault = isDefault;
	}
	
	@Column(name = "ZipCode")
	public String getZipCode() {
		return zipCode;
	}
	public void setZipCode(String zipCode) {
		this.zipCode = zipCode;
	}
	
	@Column(name = "Email")
	public String getEmail() {
		return email;
	}
	public void setEmail(String email) {
		this.email = email;
	}
	
	@Column(name = "Obj_Status")
	public Integer getObjStatus() {
		return objStatus;
	}

	public void setObjStatus(Integer objStatus) {
		this.objStatus = objStatus;
	}

	@Column(name = "Obj_Remark")
	public String getObjRemark() {
		return objRemark;
	}

	public void setObjRemark(String objRemark) {
		this.objRemark = objRemark;
	}

	@Column(name = "Obj_Cdate")
	public Date getObjCdate() {
		return objCdate;
	}

	public void setObjCdate(Date objCdate) {
		this.objCdate = objCdate;
	}

	@Column(name = "Obj_Cuser")
	public Long getObjCuser() {
		return objCuser;
	}

	public void setObjCuser(Long objCuser) {
		this.objCuser = objCuser;
	}

	@Column(name = "Obj_Mdate")
	public Date getObjMdate() {
		return objMdate;
	}

	public void setObjMdate(Date objMdate) {
		this.objMdate = objMdate;
	}

	@Column(name = "Obj_Muser")
	public Long getObjMuser() {
		return objMuser;
	}

	public void setObjMuser(Long objMuser) {
		this.objMuser = objMuser;
	}
}
