package com.landaojia.washclothes.userappserver.service.order;

import com.landaojia.washclothes.userappserver.common.paginquery.PagingQueryParameter;

/**
 * 分页查询订单
 * @author liuxi
 * @deprecated
 */
public class PagingQueryOrderPo extends PagingQueryParameter{
	
	/**
	 * 用户id
	 */
	private String userId;

	public String getUserId() {
		return userId;
	}

	public void setUserId(String userId) {
		this.userId = userId;
	}
}
