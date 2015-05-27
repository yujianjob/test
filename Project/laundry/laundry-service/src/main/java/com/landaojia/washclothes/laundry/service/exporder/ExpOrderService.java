package com.landaojia.washclothes.laundry.service.exporder;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.landaojia.washclothes.laundry.dao.exporder.ExpOrderDao;
import com.landaojia.washclothes.laundry.dao.exporder.ExpOrderEo;

/**
 * 物流订单
 * @author liuxi
 */
@Service
public class ExpOrderService {
	
	@Autowired
	private ExpOrderDao expOrderDao;
	
	/**
	 * 根据物流单号获取
	 */
	public ExpOrderEo getByExpNumber( String expNumber ){
		return expOrderDao.getByProperties( new String[]{ "expNumber", "objStatus" }, new Object[]{ expNumber, 1 }, "id desc");
	}
	
	/**
	 * 根据订单号查询(送还)
	 */
	public ExpOrderEo getByOutNumberSendBack( String outNumber ){
		return expOrderDao.getByProperties( new String[]{ "outNumber", "transportType" }, new Object[]{ outNumber, 2 }, "id desc");
	}
}
