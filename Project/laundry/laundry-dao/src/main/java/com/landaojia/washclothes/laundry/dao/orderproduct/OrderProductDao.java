package com.landaojia.washclothes.laundry.dao.orderproduct;

import java.util.List;

import org.springframework.stereotype.Repository;

import com.landaojia.washclothes.laundry.dao.LazyHomeDbHibernateBaseDao;

/**
 * 订单产品
 * @author liuxi
 */
@Repository
public class OrderProductDao extends LazyHomeDbHibernateBaseDao<OrderProductEo, Long>{
	
	/**
	 * 查询出库单信息
	 */
	public OrderProductOutBoundVo queryOrderProductOutBound( Long id ){
		
		String sql = 
		"SELECT " +
		"en.DistrictID as districtID, en.Address as nodeAddress, en.No as nodeNo, op.Code as washCode, gx.No as gxNo,  " +
		"eo.Contacts as contacts, eo.Mpno as contactsMpNo, '' AS recipientsAddr1, eo.Address as expAddress, op.Price as price,  " +
		"po.Name as operatorName, oo.UserRemark as userRemark, oo.CSRemark as csRemark, en.name as nodeName " +
		"FROM Order_Product op, Order_Order oo, Exp_Order eo, Exp_Node en, Exp_Node gx, PR_Operator po " +
		"WHERE op.OID=oo.ID " +
		"AND oo.OrderNumber=eo.OutNumber " +
		"AND eo.NodeID=en.ID " +
		"AND en.ParentID=gx.ID " +
		"AND eo.OperatorID=po.ID " +
		"AND eo.TransportType=1 " +
		"AND op.Obj_Status=1 " +
		"AND oo.Obj_Status=1 " +
		"AND eo.Obj_Status=1 " +
		"AND op.ID=? " +
		"ORDER BY op.ID DESC ";
		
		List<?> list = this.findBySql( sql, new Object[]{ id } );
		if( list.size() > 0 ){
			Object[] objs = ( Object[] )list.get( 0 );
			OrderProductOutBoundVo vo = new OrderProductOutBoundVo();
			vo.setBillNo( "" );//单号?
			vo.setStationAddr1( objs[13] == null? "" : String.valueOf( objs[13] ) );//站点地址1:站点名称
			vo.setStationAddr2( ( objs[1] == null? "" : String.valueOf( objs[1] ) ) );//站点地址2:
			vo.setStationNo( ( objs[2] == null? "" : String.valueOf( objs[2] ) ) );//站点号
			vo.setOrderNo( ( objs[3] == null? "" : String.valueOf( objs[3] ) ) );//洗涤条码号
			vo.setLineNo( ( objs[4] == null? "" : String.valueOf( objs[4] ) ) );//干线号
			vo.setRecipients( objs[5] == null? "" : String.valueOf( objs[5] ) );//收件人
			vo.setPhoneNo( objs[6] == null? "" : String.valueOf( objs[6] ) );//收件人电话
			vo.setRecipientsAddr1( ( objs[7] == null? "" : String.valueOf( objs[7] ) ) );//收件人地址1:
			vo.setRecipientsAddr2( objs[8] == null? "" : String.valueOf( objs[8] ) );//收件人地址2:
			vo.setAmount( objs[9] == null? "" : String.valueOf( objs[9] ) );//应收金额
			vo.setCourier( objs[10] == null? "" : String.valueOf( objs[10] ) );//派件员
			vo.setMemo1( "" );//备注1，objs[11] == null? "" : String.valueOf( objs[11] )
			vo.setMemo2( "" );//备注2，objs[12] == null? "" : String.valueOf( objs[12] )
			return vo;
		}else{
			return null;
		}
		
	}
	
}
