package com.landaojia.washclothes.userappserver.dao.basepush;

import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;

/**
 * 
 * @author liuxi
 */
@Entity
@Table(name = "base_push")
public class BasePushEo {
	private Long id;
	private String title;
	private String content;
	private Integer sendStatus;
	private Date sendTime;
	private Date runTime;
	private String tag;
	private Integer type;
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
	
	@Column(name = "Title")
	public String getTitle() {
		return title;
	}
	public void setTitle(String title) {
		this.title = title;
	}
	
	@Column(name = "Content")
	public String getContent() {
		return content;
	}
	public void setContent(String content) {
		this.content = content;
	}
	
	@Column(name = "SendStatus")
	public Integer getSendStatus() {
		return sendStatus;
	}
	public void setSendStatus(Integer sendStatus) {
		this.sendStatus = sendStatus;
	}
	
	@Column(name = "SendTime")
	public Date getSendTime() {
		return sendTime;
	}
	public void setSendTime(Date sendTime) {
		this.sendTime = sendTime;
	}
	
	@Column(name = "RunTime")
	public Date getRunTime() {
		return runTime;
	}
	public void setRunTime(Date runTime) {
		this.runTime = runTime;
	}
	
	@Column(name = "Tag")
	public String getTag() {
		return tag;
	}
	public void setTag(String tag) {
		this.tag = tag;
	}
	
	@Column(name = "Type")
	public Integer getType() {
		return type;
	}
	public void setType(Integer type) {
		this.type = type;
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
