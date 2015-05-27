package com.landaojia.washclothes.userappserver.dao.orderorder;

import com.landaojia.washclothes.userappserver.common.paginquery.PagingQueryParameter;

/**
 * 分页查询订单，参数
 * @author liuxi
 */
public class PagingQueryOrderCo extends PagingQueryParameter{
	
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
