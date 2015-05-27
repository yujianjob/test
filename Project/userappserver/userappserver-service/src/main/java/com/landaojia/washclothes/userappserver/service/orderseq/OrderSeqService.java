package com.landaojia.washclothes.userappserver.service.orderseq;

import java.text.SimpleDateFormat;
import java.util.Date;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.landaojia.washclothes.userappserver.common.util.TextUtil;
import com.landaojia.washclothes.userappserver.dao.orderseq.OrderSeqDao;
import com.landaojia.washclothes.userappserver.dao.orderseq.OrderSeqEo;

/**
 * 订单流水
 * @author liuxi
 */
@Service
public class OrderSeqService {
	
	@Autowired
	private OrderSeqDao orderSeqDao;
	
	/**
	 * 生成订单号
	 */
	public String generateOrderNumber(){
		OrderSeqEo eo = new OrderSeqEo();
		orderSeqDao.save( eo );
		return "2" + new SimpleDateFormat( "yyMMdd" ).format( new Date() ) + TextUtil.lPadForLen( eo.getId().toString(), '0', 8 );
	}
	
}
