package com.landaojia.washclothes.userappserver.dao.userinfo;

import java.math.BigDecimal;
import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.Table;

/**
 * 用户信息
 * @author liuxi
 */
@Entity
@Table(name = "User_Info")
public class UserInfoEo{
	private String id;
	private Long seedId;
	private String loginName;
	private String mpNo;
	private String email;
	private String loginPassword;
	private String payPassword;
	private Integer type;
	private Integer level;
	private Integer site;
	private Date lastLoginTime;
	private Integer growth;
	private BigDecimal money;
	private Long score;
	private Integer accountStatus;
	private Integer userStatus;
	private Integer registSource;
	private String recommendedCode;
	private Integer orderCount;
	private BigDecimal latitude;
	private BigDecimal longitude;
	private Long nodeId;
	protected Integer objStatus;
	
	protected String objRemark;
	
	protected Date objCdate;
	
	protected Long objCuser;
	
	protected Date objMdate;
	
	protected Long objMuser;

	
	@Id
	@Column(name = "id")
	public String getId() {
		return id;
	}
	public void setId(String id) {
		this.id = id;
	}
	
	@Column(name = "SeedID")
	public Long getSeedId() {
		return seedId;
	}
	public void setSeedId(Long seedId) {
		this.seedId = seedId;
	}
	
	@Column(name = "LoginName")
	public String getLoginName() {
		return loginName;
	}
	public void setLoginName(String loginName) {
		this.loginName = loginName;
	}
	
	@Column(name = "MPNo")
	public String getMpNo() {
		return mpNo;
	}
	public void setMpNo(String mpNo) {
		this.mpNo = mpNo;
	}
	
	@Column(name = "Email")
	public String getEmail() {
		return email;
	}
	public void setEmail(String email) {
		this.email = email;
	}
	
	@Column(name = "LoginPassword")
	public String getLoginPassword() {
		return loginPassword;
	}
	public void setLoginPassword(String loginPassword) {
		this.loginPassword = loginPassword;
	}
	
	@Column(name = "PayPassword")
	public String getPayPassword() {
		return payPassword;
	}
	public void setPayPassword(String payPassword) {
		this.payPassword = payPassword;
	}
	
	@Column(name = "Type")
	public Integer getType() {
		return type;
	}
	public void setType(Integer type) {
		this.type = type;
	}
	
	@Column(name = "Level")
	public Integer getLevel() {
		return level;
	}
	public void setLevel(Integer level) {
		this.level = level;
	}
	
	@Column(name = "Site")
	public Integer getSite() {
		return site;
	}
	public void setSite(Integer site) {
		this.site = site;
	}
	
	@Column(name = "LastLoginTime")
	public Date getLastLoginTime() {
		return lastLoginTime;
	}
	public void setLastLoginTime(Date lastLoginTime) {
		this.lastLoginTime = lastLoginTime;
	}
	
	@Column(name = "Growth")
	public Integer getGrowth() {
		return growth;
	}
	public void setGrowth(Integer growth) {
		this.growth = growth;
	}
	
	@Column(name = "Money")
	public BigDecimal getMoney() {
		return money;
	}
	public void setMoney(BigDecimal money) {
		this.money = money;
	}
	
	@Column(name = "Score")
	public Long getScore() {
		return score;
	}
	public void setScore(Long score) {
		this.score = score;
	}
	
	@Column(name = "AccountStatus")
	public Integer getAccountStatus() {
		return accountStatus;
	}
	public void setAccountStatus(Integer accountStatus) {
		this.accountStatus = accountStatus;
	}
	
	@Column(name = "UserStatus")
	public Integer getUserStatus() {
		return userStatus;
	}
	public void setUserStatus(Integer userStatus) {
		this.userStatus = userStatus;
	}
	
	@Column(name = "RegistSource")
	public Integer getRegistSource() {
		return registSource;
	}
	public void setRegistSource(Integer registSource) {
		this.registSource = registSource;
	}
	
	@Column(name = "RecommendedCode")
	public String getRecommendedCode() {
		return recommendedCode;
	}
	public void setRecommendedCode(String recommendedCode) {
		this.recommendedCode = recommendedCode;
	}
	
	@Column(name = "OrderCount")
	public Integer getOrderCount() {
		return orderCount;
	}
	public void setOrderCount(Integer orderCount) {
		this.orderCount = orderCount;
	}
	
	@Column(name = "Latitude")
	public BigDecimal getLatitude() {
		return latitude;
	}
	public void setLatitude(BigDecimal latitude) {
		this.latitude = latitude;
	}
	
	@Column(name = "Longitude")
	public BigDecimal getLongitude() {
		return longitude;
	}
	public void setLongitude(BigDecimal longitude) {
		this.longitude = longitude;
	}
	
	@Column(name = "NodeID")
	public Long getNodeId() {
		return nodeId;
	}
	public void setNodeId(Long nodeId) {
		this.nodeId = nodeId;
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
