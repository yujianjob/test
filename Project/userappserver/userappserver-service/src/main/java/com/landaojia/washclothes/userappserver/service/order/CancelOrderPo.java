package com.landaojia.washclothes.userappserver.service.order;

/**
 * 取消订单，参数
 * @author liuxi
 */
public class CancelOrderPo {
	/**
	 * 用户id
	 */
	private String userId;
	
	/**
	 * 用户手机号
	 */
	private String mpNo;
	
	/**
	 * 订单号
	 */
	private String orderNumber;
	
	/**
	 * 退单原因，代码
	 */
	private String reasonCode;
	
	/**
	 * 原因描述
	 */
	private String reasonDesc;

	public String getUserId() {
		return userId;
	}

	public void setUserId(String userId) {
		this.userId = userId;
	}

	public String getMpNo() {
		return mpNo;
	}

	public void setMpNo(String mpNo) {
		this.mpNo = mpNo;
	}

	public String getOrderNumber() {
		return orderNumber;
	}

	public void setOrderNumber(String orderNumber) {
		this.orderNumber = orderNumber;
	}

	public String getReasonCode() {
		return reasonCode;
	}

	public void setReasonCode(String reasonCode) {
		this.reasonCode = reasonCode;
	}

	public String getReasonDesc() {
		return reasonDesc;
	}

	public void setReasonDesc(String reasonDesc) {
		this.reasonDesc = reasonDesc;
	}
}
