package com.landaojia.washclothes.userappserver.service.order;

/**
 * 一键下单，请求
 * @author liuxi
 */
public class OneKeyOrderPo {
	
	/**
	 * 用户id
	 */
	private String userId;
	
	/**
	 * 用户姓名
	 */
	private String userName;
	
	/**
	 * 手机号
	 */
	private String mpNo;
	
	/**
	 * 地址
	 */
	private String address;
	
	/**
	 * 期望收衣时间(yyyy-MM-dd HH:mm:ss)
	 */
	private String expectTime;
	
	/**
	 * 用户备注
	 */
	private String userRemark;

	/**
	 * 下单渠道
	 */
	private Integer channel;
	
	public String getUserId() {
		return userId;
	}

	public void setUserId(String userId) {
		this.userId = userId;
	}

	public String getUserName() {
		return userName;
	}

	public void setUserName(String userName) {
		this.userName = userName;
	}

	public String getMpNo() {
		return mpNo;
	}

	public void setMpNo(String mpNo) {
		this.mpNo = mpNo;
	}

	public String getAddress() {
		return address;
	}

	public void setAddress(String address) {
		this.address = address;
	}

	public String getExpectTime() {
		return expectTime;
	}

	public void setExpectTime(String expectTime) {
		this.expectTime = expectTime;
	}

	public String getUserRemark() {
		return userRemark;
	}

	public void setUserRemark(String userRemark) {
		this.userRemark = userRemark;
	}

	public Integer getChannel() {
		return channel;
	}

	public void setChannel(Integer channel) {
		this.channel = channel;
	}
}
