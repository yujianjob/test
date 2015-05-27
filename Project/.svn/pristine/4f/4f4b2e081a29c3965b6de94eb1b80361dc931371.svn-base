package com.landaojia.washclothes.laundry.dao.washclass;

import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;

/**
 * 洗衣产品大类
 * @author liuxi
 */
@Entity
@Table(name = "wash_class")
public class WashClassEo {
	private Long id;
	private String name;
	private Long parentId;
	private String code;
	private Boolean isDefault;
	private Integer sort;
	private Boolean enable;
	private String pictureL;
	private String pictureM;
	private String pictureS;
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
	
	@Column(name = "Name")
	public String getName() {
		return name;
	}
	
	public void setName(String name) {
		this.name = name;
	}
	
	@Column(name = "ParentID")
	public Long getParentId() {
		return parentId;
	}
	
	public void setParentId(Long parentId) {
		this.parentId = parentId;
	}
	
	@Column(name = "Code")
	public String getCode() {
		return code;
	}
	
	public void setCode(String code) {
		this.code = code;
	}
	
	@Column(name = "IsDefault")
	public Boolean getIsDefault() {
		return isDefault;
	}
	
	public void setIsDefault(Boolean isDefault) {
		this.isDefault = isDefault;
	}
	
	@Column(name = "Sort")
	public Integer getSort() {
		return sort;
	}
	
	public void setSort(Integer sort) {
		this.sort = sort;
	}
	
	@Column(name = "Enable")
	public Boolean getEnable() {
		return enable;
	}
	
	public void setEnable(Boolean enable) {
		this.enable = enable;
	}
	
	@Column(name = "PictureL")
	public String getPictureL() {
		return pictureL;
	}
	
	public void setPictureL(String pictureL) {
		this.pictureL = pictureL;
	}
	
	@Column(name = "PictureM")
	public String getPictureM() {
		return pictureM;
	}
	
	public void setPictureM(String pictureM) {
		this.pictureM = pictureM;
	}
	
	@Column(name = "PictureS")
	public String getPictureS() {
		return pictureS;
	}
	
	public void setPictureS(String pictureS) {
		this.pictureS = pictureS;
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
