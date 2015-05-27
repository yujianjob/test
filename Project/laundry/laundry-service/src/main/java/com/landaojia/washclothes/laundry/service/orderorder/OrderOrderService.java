package com.landaojia.washclothes.laundry.service.orderorder;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.landaojia.washclothes.laundry.dao.orderorder.OrderOrderDao;
import com.landaojia.washclothes.laundry.dao.orderorder.OrderOrderEo;

/**
 * 订单
 * @author liuxi
 */
@Service
public class OrderOrderService {
	
	@Autowired
	private OrderOrderDao orderOrderDao;
	
	/**
	 * 根据id查询
	 */
	public OrderOrderEo getById( Long id ){
		return orderOrderDao.get( id );
	}
	
	/**
	 * 根据订单号查询
	 */
	public OrderOrderEo getByOrderNumber( String orderNumber ){
		return orderOrderDao.getByProperties( new String[]{ "orderNumber", "objStatus" }, new Object[]{ orderNumber, 1 }, "id desc" );
	}
}
