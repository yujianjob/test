package com.landaojia.washclothes.laundry.dao.orderproduct;

import java.math.BigDecimal;
import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;

/**
 * 订单产品
 * @author liuxi
 */
@Entity
@Table(name = "order_product")
public class OrderProductEo {
	private Long id;
	private Long oId;
	private Long productId;
	private String name;
	private Integer type;
	private BigDecimal price;
	private String attribute;
	private String code;
	private String otherCode;
	private Integer returnStatus;
	private Integer factoryWash;
	private String userMpno;
	private String userName;
	private Integer userSignType;
	private Date userSignTime;
	private Integer step;
	private Date step5Time;
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
	
	@Column(name = "OID")
	public Long getOId() {
		return oId;
	}
	public void setOId(Long oId) {
		this.oId = oId;
	}
	
	@Column(name = "ProductID")
	public Long getProductId() {
		return productId;
	}
	public void setProductId(Long productId) {
		this.productId = productId;
	}
	
	@Column(name = "Name")
	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	
	@Column(name = "Type")
	public Integer getType() {
		return type;
	}
	public void setType(Integer type) {
		this.type = type;
	}
	
	@Column(name = "Price")
	public BigDecimal getPrice() {
		return price;
	}
	public void setPrice(BigDecimal price) {
		this.price = price;
	}
	
	@Column(name = "attribute")
	public String getAttribute() {
		return attribute;
	}
	public void setAttribute(String attribute) {
		this.attribute = attribute;
	}
	
	@Column(name = "code")
	public String getCode() {
		return code;
	}
	public void setCode(String code) {
		this.code = code;
	}
	
	@Column(name = "otherCode")
	public String getOtherCode() {
		return otherCode;
	}
	public void setOtherCode(String otherCode) {
		this.otherCode = otherCode;
	}
	
	@Column(name = "returnStatus")
	public Integer getReturnStatus() {
		return returnStatus;
	}
	public void setReturnStatus(Integer returnStatus) {
		this.returnStatus = returnStatus;
	}
	
	@Column(name = "factoryWash")
	public Integer getFactoryWash() {
		return factoryWash;
	}
	public void setFactoryWash(Integer factoryWash) {
		this.factoryWash = factoryWash;
	}
	
	@Column(name = "userMpno")
	public String getUserMpno() {
		return userMpno;
	}
	public void setUserMpno(String userMpno) {
		this.userMpno = userMpno;
	}
	
	@Column(name = "userName")
	public String getUserName() {
		return userName;
	}
	public void setUserName(String userName) {
		this.userName = userName;
	}
	
	@Column(name = "userSignType")
	public Integer getUserSignType() {
		return userSignType;
	}
	public void setUserSignType(Integer userSignType) {
		this.userSignType = userSignType;
	}
	
	@Column(name = "userSignTime")
	public Date getUserSignTime() {
		return userSignTime;
	}
	public void setUserSignTime(Date userSignTime) {
		this.userSignTime = userSignTime;
	}
	
	@Column(name = "step")
	public Integer getStep() {
		return step;
	}
	public void setStep(Integer step) {
		this.step = step;
	}
	
	@Column(name = "step5Time")
	public Date getStep5Time() {
		return step5Time;
	}
	public void setStep5Time(Date step5Time) {
		this.step5Time = step5Time;
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
}
