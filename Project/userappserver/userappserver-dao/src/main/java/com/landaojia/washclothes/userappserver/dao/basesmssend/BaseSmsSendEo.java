package com.landaojia.washclothes.userappserver.dao.basesmssend;

import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;

/**
 * 短信发送
 * @author liuxi
 */
@Entity
@Table(name = "base_smssend")
public class BaseSmsSendEo {
	private Long id;
	private String mobile;
	private String content;
	private Integer sendStatus;
	private Date sendTime;
	private Date runTime;
	private Integer priority;
	private Integer type;
	private Integer channel;
	private Integer source;
	private String sourceValue;
	private Integer reTry;
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
	
	@Column(name = "Mobile")
	public String getMobile() {
		return mobile;
	}
	public void setMobile(String mobile) {
		this.mobile = mobile;
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
	
	@Column(name = "Priority")
	public Integer getPriority() {
		return priority;
	}
	public void setPriority(Integer priority) {
		this.priority = priority;
	}
	
	@Column(name = "Type")
	public Integer getType() {
		return type;
	}
	public void setType(Integer type) {
		this.type = type;
	}
	
	@Column(name = "Channel")
	public Integer getChannel() {
		return channel;
	}
	public void setChannel(Integer channel) {
		this.channel = channel;
	}
	
	@Column(name = "Source")
	public Integer getSource() {
		return source;
	}
	public void setSource(Integer source) {
		this.source = source;
	}
	
	@Column(name = "SourceValue")
	public String getSourceValue() {
		return sourceValue;
	}
	public void setSourceValue(String sourceValue) {
		this.sourceValue = sourceValue;
	}
	
	@Column(name = "ReTry")
	public Integer getReTry() {
		return reTry;
	}
	public void setReTry(Integer reTry) {
		this.reTry = reTry;
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
