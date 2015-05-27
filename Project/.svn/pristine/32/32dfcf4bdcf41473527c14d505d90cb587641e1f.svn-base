package com.landaojia.washclothes.userappserver.dao.orderorder;

import java.util.Date;
import java.util.LinkedList;
import java.util.List;

import org.springframework.stereotype.Repository;

import com.landaojia.washclothes.userappserver.common.util.DateUtil;
import com.landaojia.washclothes.userappserver.dao.LazyHomeDbHibernateBaseDao;

/**
 * 订单
 * @author liuxi
 */
@Repository
public class OrderOrderDao extends LazyHomeDbHibernateBaseDao<OrderOrderEo, Long>{
	
	/**
	 * 查询用户订单条数
	 */
	public int countUserOrder( String userId ){
		String hql = " select count(*) from " + OrderOrderEo.class.getName() + " where userId=? and objStatus=? ";
		Object o = this.getObject( hql, new Object[]{ userId, 1 } );
		return Integer.parseInt( String.valueOf( o ) );
	}
	
	/**
	 * 查询用户订单
	 */
	public List<OrderSimpleVo> queryUserOrder( String userId, int pageSize, int pageNo  ){
		String sql = 
		"SELECT oo.id, oo.userId, eo.contacts, oo.orderNumber, oo.step1Time, eo.mpno, eo.address, oo.orderStatus, oo.step " + 
		"FROM order_order oo " +
		"LEFT JOIN exp_order eo ON " +
		"oo.orderNumber=eo.outNumber " +
		"WHERE oo.userId=? " +
		"AND oo.obj_Status=1 " +
		"AND eo.obj_Status=1 " +
		"AND eo.transportType=1 " +
		"ORDER BY id DESC " +
		"LIMIT ?, ? ";
		
		List<?> list = this.findBySql( sql, new Object[]{ userId, ( pageNo - 1 ) * pageSize, pageSize });
		List<OrderSimpleVo> voList = new LinkedList<OrderSimpleVo>();
		for( Object o : list ){
			Object[] objs = ( Object[] )o;
			OrderSimpleVo vo = new OrderSimpleVo();
			vo.setUserId( objs[1] == null? "" : String.valueOf( objs[1] ) );
			vo.setUserName( objs[2] == null? "" : String.valueOf( objs[2] ) );
			vo.setOrderNumber( objs[3] == null? "" : String.valueOf( objs[3] ) );
			vo.setOrderTime( objs[4] == null? "" : DateUtil.dateToStr( (Date)( objs[4] ), "yyyy-MM-dd HH:mm:ss") );
			vo.setMpNo( objs[5] == null? "" : String.valueOf( objs[5] ) );
			vo.setAddress( objs[6] == null? "" : String.valueOf( objs[6] ) );
			vo.setOrderStatus( objs[7] == null? 2 : Integer.parseInt( String.valueOf( objs[7] ) ) );
			vo.setOrderStatusDesc( "" );//TODO...
			vo.setOrderStep( objs[8] == null? 2 : Integer.parseInt( String.valueOf( objs[8] ) ) );
			vo.setOrderStepDesc( "" );//TODO...
			voList.add( vo );
		}
		return voList;
	}

}
