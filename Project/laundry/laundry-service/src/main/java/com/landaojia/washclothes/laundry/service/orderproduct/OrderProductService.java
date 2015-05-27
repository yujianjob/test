package com.landaojia.washclothes.laundry.service.orderproduct;

import java.util.Date;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Propagation;
import org.springframework.transaction.annotation.Transactional;

import com.landaojia.washclothes.laundry.dao.exporder.ExpOrderDao;
import com.landaojia.washclothes.laundry.dao.orderorder.OrderOrderDao;
import com.landaojia.washclothes.laundry.dao.orderorder.OrderOrderEo;
import com.landaojia.washclothes.laundry.dao.orderproduct.OrderProductDao;
import com.landaojia.washclothes.laundry.dao.orderproduct.OrderProductEo;
import com.landaojia.washclothes.laundry.dao.orderproduct.OrderProductOutBoundVo;

/**
 * 订单产品
 * @author liuxi
 */
@Service
public class OrderProductService {
	
	@Autowired
	private OrderProductDao orderProductDao;
	
	@Autowired 
	private ExpOrderDao expOrderDao;
	
	@Autowired
	private OrderOrderDao orderOrderDao;
	
	/**
	 * 根据订单id查询
	 */
	public List<OrderProductEo> getListByOrderId( Long orderId ){
		return orderProductDao.findByProperties( new String[]{ "oId", "objStatus" }, new Object[]{ orderId, 1 }, "id asc" );
	}
	
	/**
	 * 根据洗衣条码查询
	 */
	public OrderProductEo getByCode( String code ){
		List<OrderProductEo> list = orderProductDao.findByProperties( new String[]{ "code" }, new Object[]{ code }, "id desc" );
		return list.size() > 0? list.get( 0 ) : null;
	}
	
	/**
	 * 添加订单产品
	 */
	@Transactional(propagation=Propagation.REQUIRED )
	public OrderProductEo addOrderProduct( EditOrderProductPo po ){
		OrderProductEo eo = new OrderProductEo();
		//eo.setId( po.getId() );
		eo.setOId( po.getOrderId() );
		eo.setProductId( po.getProductId() );
		eo.setName( po.getProductName() );
		eo.setType( 1 );//0:自定义产品 1:干洗产品
		eo.setPrice( po.getPrice() );
		eo.setAttribute( po.getAttr() );
		eo.setCode( po.getCode() );
		eo.setOtherCode( po.getOtherCode() );
		eo.setReturnStatus( 0 );
		eo.setFactoryWash( 1 );
		eo.setUserMpno( null );
		eo.setUserName( null );
		eo.setUserSignType( 0 );
		eo.setUserSignTime( null );
		eo.setStep( 0 );
		eo.setStep5Time( null );
		eo.setObjStatus( 1 );
		eo.setObjRemark( null );
		eo.setObjCdate( new Date() );
		eo.setObjCuser( 2L );//TODO...登录的用户
		eo.setObjMdate( new Date() );
		eo.setObjMuser( 2L );//TODO...登录的用户
		orderProductDao.save( eo );
		
		return eo;
	}
	
	/**
	 * (逻辑)删除订单产品
	 */
	public void deleteOrderProduct( Long id ){
		OrderProductEo eo = orderProductDao.get( id );
		eo.setObjStatus( 6 );
		orderProductDao.update( eo );
	}
	
	/**
	 * 订单产品出库
	 * @return OrderProductOutBoundRo 用于打印出库单的值对象包装
	 */
	//@Transactional(propagation=Propagation.REQUIRED )
	public OrderProductOutBoundVo orderProductOutBound( Long id ){
		OrderProductOutBoundVo vo = orderProductDao.queryOrderProductOutBound( id );
		OrderProductEo opEo = orderProductDao.get( id );
		opEo.setStep( 5 );
		opEo.setStep5Time( new Date() );
		orderProductDao.update( opEo );
		
		//----查找库中是否仍然有未出库的衣物，表明当前这件衣物的出库描述-----
		OrderProductEo opEo2 = orderProductDao.getByProperties( new String[]{ "oId", "step", "objStatus" }, new Object[]{ opEo.getOId(), 0, 1 }, null );
		if( opEo2 != null ){
			vo.setRecipientsAddr2( vo.getRecipientsAddr2() + "(等待出库)" );
		}else{
			vo.setRecipientsAddr2( vo.getRecipientsAddr2() + "(完成出库)" );
			
			OrderOrderEo orderEo = orderOrderDao.get( opEo.getOId() );
			orderEo.setStep( 5 );//出库中
			orderEo.setStep5Time( new Date() );
			orderOrderDao.update( orderEo );
		}
		
		//----查询件数----
		Integer count = orderProductDao.countByProperties( new String[]{ "oId", "objStatus" }, new Object[]{ opEo.getOId(), 1 } );
		vo.setMemo2( opEo.getName() + "(订单共" + count + "件)" );
		
		return vo;
	}
}
