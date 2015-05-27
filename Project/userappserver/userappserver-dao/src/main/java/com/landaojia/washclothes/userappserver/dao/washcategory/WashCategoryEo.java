package com.landaojia.washclothes.userappserver.dao.washcategory;

import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;

/**
 * 洗衣产品小类
 * @author liuxi
 */
@Entity
@Table(name = "wash_category")
public class WashCategoryEo {
	private Long id;
	private String name;
	private String code;
	private Integer classId;
	private String unit;
	private String keyword;
	private String content;
	private Boolean enable;
	private Integer sort;
	private String pictureL;
	private String pictureM;
	private String pictureS;
	private String pictureAlt;
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
	
	@Column(name = "Code")
	public String getCode() {
		return code;
	}
	
	public void setCode(String code) {
		this.code = code;
	}
	
	@Column(name = "ClassID")
	public Integer getClassId() {
		return classId;
	}
	
	public void setClassId(Integer classId) {
		this.classId = classId;
	}
	
	@Column(name = "Unit")
	public String getUnit() {
		return unit;
	}
	
	public void setUnit(String unit) {
		this.unit = unit;
	}
	
	@Column(name = "Keyword")
	public String getKeyword() {
		return keyword;
	}
	
	public void setKeyword(String keyword) {
		this.keyword = keyword;
	}
	
	@Column(name = "Content")
	public String getContent() {
		return content;
	}
	
	public void setContent(String content) {
		this.content = content;
	}
	
	@Column(name = "Enable")
	public Boolean getEnable() {
		return enable;
	}
	
	public void setEnable(Boolean enable) {
		this.enable = enable;
	}
	
	@Column(name = "Sort")
	public Integer getSort() {
		return sort;
	}
	
	public void setSort(Integer sort) {
		this.sort = sort;
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
	
	@Column(name = "PictureAlt")
	public String getPictureAlt() {
		return pictureAlt;
	}
	
	public void setPictureAlt(String pictureAlt) {
		this.pictureAlt = pictureAlt;
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
