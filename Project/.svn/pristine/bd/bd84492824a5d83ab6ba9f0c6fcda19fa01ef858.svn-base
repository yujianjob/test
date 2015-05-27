package com.landaojia.washclothes.userappserver.dao.orderstep;

import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;

/**
 * 订单步骤
 * @author liuxi
 */
@Entity
@Table(name = "order_step")
public class OrderStepEo {
	private Long id;
	private Long oId;
	private Integer type;
	private String content;
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
	
	@Column(name = "Type")
	public Integer getType() {
		return type;
	}
	public void setType(Integer type) {
		this.type = type;
	}
	
	@Column(name = "Content")
	public String getContent() {
		return content;
	}
	public void setContent(String content) {
		this.content = content;
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
