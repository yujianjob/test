package com.landaojia.washclothes.userappserver.service.order;

import java.math.BigDecimal;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Propagation;
import org.springframework.transaction.annotation.Transactional;

import com.landaojia.washclothes.userappserver.common.exception.CommonException;
import com.landaojia.washclothes.userappserver.common.exception.CommonExceptionCode;
import com.landaojia.washclothes.userappserver.common.paginquery.PagingQueryResult;
import com.landaojia.washclothes.userappserver.common.util.DateUtil;
import com.landaojia.washclothes.userappserver.dao.basepush.BasePushDao;
import com.landaojia.washclothes.userappserver.dao.basepush.BasePushEo;
import com.landaojia.washclothes.userappserver.dao.basesmssend.BaseSmsSendDao;
import com.landaojia.washclothes.userappserver.dao.basesmssend.BaseSmsSendEo;
import com.landaojia.washclothes.userappserver.dao.exporder.ExpOrderDao;
import com.landaojia.washclothes.userappserver.dao.exporder.ExpOrderEo;
import com.landaojia.washclothes.userappserver.dao.exporderoperatortemp.ExpOrderOperatorTempDao;
import com.landaojia.washclothes.userappserver.dao.exporderoperatortemp.ExpOrderOperatorTempEo;
import com.landaojia.washclothes.userappserver.dao.orderconsigneeaddress.OrderConsigneeAddressDao;
import com.landaojia.washclothes.userappserver.dao.orderconsigneeaddress.OrderConsigneeAddressEo;
import com.landaojia.washclothes.userappserver.dao.orderexpress.OrderExpressDao;
import com.landaojia.washclothes.userappserver.dao.orderexpress.OrderExpressEo;
import com.landaojia.washclothes.userappserver.dao.orderorder.OrderOrderCst;
import com.landaojia.washclothes.userappserver.dao.orderorder.OrderOrderDao;
import com.landaojia.washclothes.userappserver.dao.orderorder.OrderOrderEo;
import com.landaojia.washclothes.userappserver.dao.orderorder.OrderSimpleVo;
import com.landaojia.washclothes.userappserver.dao.orderorder.PagingQueryOrderCo;
import com.landaojia.washclothes.userappserver.dao.orderproduct.OrderProductDao;
import com.landaojia.washclothes.userappserver.dao.orderproduct.OrderProductEo;
import com.landaojia.washclothes.userappserver.dao.orderstep.OrderStepDao;
import com.landaojia.washclothes.userappserver.dao.orderstep.OrderStepEo;
import com.landaojia.washclothes.userappserver.service.orderseq.OrderSeqService;

/**
 * 订单
 * @author liuxi
 */
@Service
public class OrderService {
	
	@Autowired
	private OrderSeqService orderSeqService;
	
	@Autowired
	private OrderOrderDao orderOrderDao;
	
	@Autowired
	private OrderConsigneeAddressDao orderConsigneeAddressDao;
	
	@Autowired
	private ExpOrderDao expOrderDao;
	
	@Autowired
	private OrderExpressDao orderExpressDao;
	
	@Autowired
	private OrderStepDao orderStepDao;
	
	@Autowired
	private OrderProductDao orderProductDao;
	
	@Autowired
	private BasePushDao basePushDao;
	
	@Autowired
	private BaseSmsSendDao baseSmsSendDao;
	
	@Autowired
	private ExpOrderOperatorTempDao expOrderOperatorTempDao;
	
	/**
	 * 一键下单
	 */
	@Transactional(propagation=Propagation.REQUIRED )
	public OrderSimpleVo oneKeyOrder( OneKeyOrderPo po ){
		//----检验------
		//TODO...
		
		//----根据地址匹配站点----
		Long districtId = 0L;
		Long nodeId = 1L;
		String nodeCode = "31_999";
		
		Date now = new Date();
		
		//----生成订单号----
		String orderNumber = orderSeqService.generateOrderNumber();
		
		//----订单信息----
		OrderOrderEo ooEo = new OrderOrderEo();
		ooEo.setOrderNumber( orderNumber );//订单号
		ooEo.setOrderClass( OrderOrderCst.ORDER_CLASS_ONE_KEY );//订单类别
		ooEo.setOrderType( OrderOrderCst.ORDER_TYPE_NORMAL );//分类
		ooEo.setUserId( po.getUserId() );
		ooEo.setTitle( "一键下单" );
		ooEo.setSourceAmount( BigDecimal.ZERO );
		ooEo.setTotalAmount( BigDecimal.ZERO );
		ooEo.setPayAmount( BigDecimal.ZERO );
		ooEo.setPayStatus( 0 );
		ooEo.setPayType( 1 );
		ooEo.setUserRemark( "测试单，不要处理，不要退单" );
		ooEo.setExpectTime( DateUtil.strToDate( po.getExpectTime() ) );
		ooEo.setCsRemark( "测试单，不要处理，不要退单" );
		ooEo.setSystemRemark( null );
		ooEo.setPrintRemark( null );
		ooEo.setRelationId( null );
		ooEo.setRelationNo( null );
		ooEo.setOrderStatus( OrderOrderCst.ORDER_STATUS_FINISH );
		ooEo.setSite( 1 );
		ooEo.setCompleteTime( now );
		ooEo.setAllFinishTime( null );
		ooEo.setChannel( po.getChannel() );
		ooEo.setGetExpressNumber( null );
		ooEo.setGetExpressType( 0 );
		ooEo.setSendExpressNumber( null );
		ooEo.setStep( OrderOrderCst.STEP_2_PICKUP );
		ooEo.setStep1Time( now );
		ooEo.setStep2Time( now );
		ooEo.setStep3Time( null );
		ooEo.setStep4Time( null );
		ooEo.setStep5Time( null );
		ooEo.setStep6Time( null );
		ooEo.setStep7Time( null );
		ooEo.setExpressStatus( 1 );
		ooEo.setPushExpressTime( null );
		ooEo.setObjStatus( 1 );
		ooEo.setObjRemark( null );
		ooEo.setObjCdate( now );
		ooEo.setObjCuser( 0L );
		ooEo.setObjMdate( now );
		ooEo.setObjMuser( 0L );
		ooEo.setSendType( 1 );
		ooEo.setInviteCode( null );
		ooEo.setCommissionStatus( 0 );
		ooEo.setCommissionTime( null );
		ooEo.setTmpPayAmount( BigDecimal.ZERO );
		orderOrderDao.save( ooEo );
		
		//----收发货信息----
		OrderConsigneeAddressEo ocaEo = new OrderConsigneeAddressEo();
		ocaEo.setOId( ooEo.getId() );
		ocaEo.setType( 1 );
		ocaEo.setConsignee( po.getUserName() );
		ocaEo.setDistrictId( districtId );
		ocaEo.setAddress( po.getAddress() );
		ocaEo.setMpNo( po.getMpNo() );
		ocaEo.setPhone( null );
		ocaEo.setZipcode( null );
		ocaEo.setEmail( null );
		ocaEo.setObjStatus( 1 );
		ocaEo.setObjRemark( null );
		ocaEo.setObjCdate( now );
		ocaEo.setObjCuser( 0L );
		ocaEo.setObjMdate( now );
		ocaEo.setObjMuser( 0L );
		orderConsigneeAddressDao.save( ocaEo );
		
		OrderConsigneeAddressEo ocaEo2 = new OrderConsigneeAddressEo();
		ocaEo2.setOId( ooEo.getId() );
		ocaEo2.setType( 2 );
		ocaEo2.setConsignee( po.getUserName() );
		ocaEo2.setDistrictId( districtId );
		ocaEo2.setAddress( po.getAddress() );
		ocaEo2.setMpNo( po.getMpNo() );
		ocaEo2.setPhone( null );
		ocaEo2.setZipcode( null );
		ocaEo2.setEmail( null );
		ocaEo2.setObjStatus( 1 );
		ocaEo2.setObjRemark( null );
		ocaEo2.setObjCdate( now );
		ocaEo2.setObjCuser( 0L );
		ocaEo2.setObjMdate( now );
		ocaEo2.setObjMuser( 0L );
		orderConsigneeAddressDao.save( ocaEo2 );
		
		//----物流订单信息----
		ExpOrderEo eoEo = new ExpOrderEo();
		eoEo.setExpNumber( null );
		eoEo.setOutNumber( orderNumber );
		eoEo.setTransportType( 1 );
		eoEo.setQuickType( 0 );
		eoEo.setDistrictId( districtId );
		eoEo.setAddress( po.getAddress() );
		eoEo.setContacts( po.getUserName() );
		eoEo.setMpNo( po.getMpNo() );
		eoEo.setExpTime( now );
		eoEo.setOperatorId( null );
		eoEo.setNodeId( nodeId );
		eoEo.setOperatorTime( null );
		eoEo.setCompleteTime( null );
		eoEo.setAllotStatus( 1 );
		eoEo.setPackageInfo( "一键下单" );
		eoEo.setPackageCount( 0 );
		eoEo.setChargeFee( BigDecimal.ZERO );
		eoEo.setStep( 0 );
		eoEo.setStepRemark( null );
		eoEo.setUserRemark( null );
		eoEo.setCsRemark( null );
		eoEo.setLatitude( null );
		eoEo.setLongitude( null );
		eoEo.setSystemRemark( null );
		eoEo.setObjStatus( 1 );
		eoEo.setObjRemark( null );
		eoEo.setObjCdate( now );
		eoEo.setObjCuser( 0L );
		eoEo.setObjMdate( now );
		eoEo.setObjMuser( 0L );
		eoEo.setTakeTime( null );
		eoEo.setAllotTime( now );
		eoEo.setCallUserStatus( 0 );
		eoEo.setCallUserTime( null );
		eoEo.setCallUserSecond( 0 );
		eoEo.setInviteCode( "" );
		eoEo.setAlarm( 0 );
		eoEo.setWaitProcess( 0 );
		expOrderDao.save( eoEo );
		
		ExpOrderEo eoEo2 = new ExpOrderEo();
		eoEo2.setExpNumber( null );
		eoEo2.setOutNumber( orderNumber );
		eoEo2.setTransportType( 2 );
		eoEo2.setQuickType( 0 );
		eoEo2.setDistrictId( districtId );
		eoEo2.setAddress( po.getAddress() );
		eoEo2.setContacts( po.getUserName() );
		eoEo2.setMpNo( po.getMpNo() );
		eoEo2.setExpTime( now );
		eoEo2.setOperatorId( null );
		eoEo2.setNodeId( nodeId );
		eoEo2.setOperatorTime( null );
		eoEo2.setCompleteTime( null );
		eoEo2.setAllotStatus( 0 );
		eoEo2.setPackageInfo( "一键下单" );
		eoEo2.setPackageCount( 0 );
		eoEo2.setChargeFee( BigDecimal.ZERO );
		eoEo2.setStep( 0 );
		eoEo2.setStepRemark( null );
		eoEo2.setUserRemark( null );
		eoEo2.setCsRemark( null );
		eoEo2.setLatitude( null );
		eoEo2.setLongitude( null );
		eoEo2.setSystemRemark( null );
		eoEo2.setObjStatus( 1 );
		eoEo2.setObjRemark( null );
		eoEo2.setObjCdate( now );
		eoEo2.setObjCuser( 0L );
		eoEo2.setObjMdate( now );
		eoEo2.setObjMuser( 0L );
		eoEo2.setTakeTime( null );
		eoEo2.setAllotTime( null );
		eoEo2.setCallUserStatus( 0 );
		eoEo2.setCallUserTime( null );
		eoEo2.setCallUserSecond( 0 );
		eoEo2.setInviteCode( null );
		eoEo2.setAlarm( 0 );
		eoEo2.setWaitProcess( 0 );
		expOrderDao.save( eoEo2 );
		
		//----物流信息----
		OrderExpressEo oeEo = new OrderExpressEo();
		oeEo.setOId( ooEo.getId() );
		oeEo.setType( 1 );
		oeEo.setCode( "" );
		oeEo.setContent( "取件" );
		oeEo.setRelationId( null ); 
		oeEo.setAcceptTime( null );
		oeEo.setObjStatus( 1 );
		oeEo.setObjRemark( null );
		oeEo.setObjCdate( now );
		oeEo.setObjCuser( 0L );
		oeEo.setObjMdate( now );
		oeEo.setObjMuser( 0L );
		orderExpressDao.save( oeEo );
		
		//----进程----
		OrderStepEo osEo = new OrderStepEo();
		osEo.setOId( ooEo.getId() );;
		osEo.setType( 1 );
		osEo.setContent( "下单" );
		osEo.setObjStatus( 1 );
		osEo.setObjRemark( null ); 
		osEo.setObjCdate( now );
		osEo.setObjCuser( -1L );
		osEo.setObjMdate( now );
		osEo.setObjMuser( -1L );
		orderStepDao.save( osEo );
		
		OrderStepEo osEo2 = new OrderStepEo();
		osEo2.setOId( ooEo.getId() );;
		osEo2.setType( 2 );
		osEo2.setContent( "取件中" );
		osEo2.setObjStatus( 1 );
		osEo2.setObjRemark( null ); 
		osEo2.setObjCdate( now );
		osEo2.setObjCuser( -1L );
		osEo2.setObjMdate( now );
		osEo2.setObjMuser( -1L );
		orderStepDao.save( osEo2 );
		
		//----推送-----
		BasePushEo bpEo = new BasePushEo();
		bpEo.setTitle( "抢单" );
		bpEo.setContent( po.getAddress() + "产生一笔订单" );
		bpEo.setSendStatus( 2 );
		bpEo.setSendTime( now );
		bpEo.setRunTime( now );
		bpEo.setTag( nodeCode );//站点编号
		bpEo.setType( 2 );
		bpEo.setObjStatus( 1 );
		bpEo.setObjRemark( null );
		bpEo.setObjCdate( now );
		bpEo.setObjCuser( -1L );
		bpEo.setObjMdate( now );
		bpEo.setObjMuser( -1L );
		basePushDao.save( bpEo );
		
		//----短信发送----
		BaseSmsSendEo bssEo = new BaseSmsSendEo();
		bssEo.setMobile( po.getMpNo() );
		bssEo.setContent( "您已成功下单，订单号为1" + orderNumber + "，我们会尽快安排人员上门取件，请您耐心等待，谢谢。" );
		bssEo.setSendStatus( 2 );
		bssEo.setSendTime( now );
		bssEo.setRunTime( now );
		bssEo.setPriority( 3 );
		bssEo.setType( 2 );
		bssEo.setChannel( po.getChannel() ); //渠道...
		bssEo.setSource( 0 );
		bssEo.setSourceValue( null );
		bssEo.setReTry( 0 );
		bssEo.setObjStatus( 1 );
		bssEo.setObjRemark( null );
		bssEo.setObjCdate( now );
		bssEo.setObjCuser( 1L );
		bssEo.setObjMdate( now );
		bssEo.setObjMuser( 1L );
		baseSmsSendDao.save( bssEo );
		
		//----兼职待操作临时记录----
		ExpOrderOperatorTempEo eootEo = new ExpOrderOperatorTempEo();
		eootEo.setOrderId( eoEo.getId() );
		eootEo.setOperatorId( 554L );//操作员id
		eootEo.setObjStatus( 1 );
		eootEo.setObjRemark( null );
		eootEo.setObjCdate( now );
		eootEo.setObjCuser( -1L );
		eootEo.setObjMdate( now );
		eootEo.setObjMuser( -1L );
		expOrderOperatorTempDao.save( eootEo );
		
		
		//----返回值----------------
		OrderSimpleVo ro = new OrderSimpleVo();
		ro.setOrderNumber( orderNumber );
		ro.setUserId( po.getUserId() );
		ro.setUserName( po.getUserName() );
		ro.setMpNo( po.getMpNo() );
		ro.setAddress( po.getAddress() );
		ro.setOrderTime( DateUtil.dateToStr( now, "yyyy-MM-dd HH:mm:ss" ) );
		ro.setOrderStatus( 2 );
		ro.setOrderStatusDesc( "完成订单" );
		ro.setOrderStep( 2 );
		ro.setOrderStepDesc( "取件中" );
		return ro;
	}
	
	/**
	 * 取消订单
	 */
	public OrderSimpleVo cancelOrder( CancelOrderPo po ){
		OrderOrderEo ooEo = orderOrderDao.getByProperties( new String[]{ "orderNumber" }, new Object[]{ po.getOrderNumber() } , "id desc" );
		if( ooEo == null ){
			throw new CommonException( CommonExceptionCode.S000005, po.getOrderNumber() );
		}
		
		if( !po.getUserId().equals( ooEo.getUserId() ) ){
			throw new CommonException( CommonExceptionCode.S000006, po.getOrderNumber() );
		}
		
		if( 2 != ooEo.getOrderStatus() || ooEo.getStep() > 2 ){
			throw new CommonException( CommonExceptionCode.S000007, po.getOrderNumber() );
		}
		
		ooEo.setOrderStatus( 6 );
		ooEo.setStep( 7 );
		ooEo.setUserRemark( ooEo.getUserRemark() == null? po.getReasonDesc() : ooEo.getUserRemark() + " " + po.getReasonDesc() );
		orderOrderDao.update( ooEo );
		
		//----获取物流信息---
		ExpOrderEo eoEo = expOrderDao.getByProperties( new String[]{ "outNumber", "transportType"  },  new Object[]{ ooEo.getOrderNumber(), 1 }, "id desc" );
		
		OrderSimpleVo ro = new OrderSimpleVo();
		ro.setAddress( eoEo.getAddress() );
		ro.setMpNo( eoEo.getMpNo() );
		ro.setOrderNumber( ooEo.getOrderNumber() );
		ro.setOrderTime( DateUtil.dateToStr( ooEo.getStep1Time(), "yyyy-MM-dd HH:mm:ss") );
		ro.setUserId( ooEo.getUserId() );
		ro.setUserName( eoEo.getContacts() );
		ro.setOrderStatus( 6 );
		ro.setOrderStatusDesc( "已退单" );
		ro.setOrderStep( 7 );
		ro.setOrderStepDesc( "完成" );
		return ro;
	}
	
	/**
	 * 分页查询
	 */
	public PagingQueryResult<OrderSimpleVo> pagingQueryOrder( PagingQueryOrderCo co ){
		int c = orderOrderDao.countUserOrder( co.getUserId() );
		List<OrderSimpleVo> list = orderOrderDao.queryUserOrder( co.getUserId(), co.getPageSize(), co.getPageNo() );
		PagingQueryResult<OrderSimpleVo> pqr = new PagingQueryResult<OrderSimpleVo>( list, co.getPageSize(), co.getPageNo(), c );
		return pqr;
	}
	
	/**
	 * 查询订单详情
	 */
	public OrderDetailRo queryOrderDetail( QueryOrderDetailPo po ){
		OrderOrderEo ooEo = orderOrderDao.getByProperties( new String[]{ "orderNumber", "objStatus" }, new Object[]{ po.getOrderNumber(), 1 }, "id desc" );
		if( ooEo == null || !ooEo.getUserId().equals( po.getUserId() ) ){
			throw new CommonException( CommonExceptionCode.S000014 );
		}
		
		ExpOrderEo eoEo = expOrderDao.getByProperties( new String[]{ "outNumber", "transportType"  },  new Object[]{ ooEo.getOrderNumber(), 1 }, "id desc" );
		if( eoEo == null ){
			throw new CommonException( CommonExceptionCode.S000014 );
		}
		
		OrderDetailRo ro = new OrderDetailRo();
		
		//----订单-------
		OrderSimpleVo osVo = new OrderSimpleVo(); 
		osVo.setAddress( eoEo.getAddress() );
		osVo.setMpNo( eoEo.getMpNo() );
		osVo.setOrderNumber( ooEo.getOrderNumber() );
		osVo.setOrderTime( DateUtil.dateToStr( ooEo.getStep1Time(), "yyyy-MM-dd HH:mm:ss") );
		osVo.setUserId( ooEo.getUserId() );
		osVo.setUserName( eoEo.getContacts() );
		osVo.setOrderStatus( ooEo.getOrderStatus() );
		osVo.setOrderStatusDesc( "" );//TODO...
		osVo.setOrderStep( ooEo.getStep() );
		osVo.setOrderStepDesc( "完成" );//TODO...
		ro.setOrderSimpleVo( osVo );
		
		//----洗衣产品-------
		List<OrderProductEo> opList = orderProductDao.findByProperties( new String[]{ "oId", "objStatus" }, new Object[]{ ooEo.getId(), 1 }, "productId asc" );
		if( opList.size() > 0 ){
			List<WashItemSubtVo> wisList = new ArrayList<WashItemSubtVo>();
			
			Long tempOpId = -1L;
			int tempNum = 0;
			BigDecimal tempPrice = BigDecimal.ZERO;
			WashItemSubtVo wisVo = null;
			BigDecimal totalPrice = BigDecimal.ZERO;
			for( int i = 0; i < opList.size(); i ++ ){
				OrderProductEo opEo = opList.get( i );
				
				if( tempOpId != opEo.getProductId() ){
					if( tempOpId != -1 ){
						wisVo.setWashItemName( opEo.getName() );
						wisVo.setPrice( opEo.getPrice() );
						wisVo.setSubtNum( tempNum );
						wisVo.setSubtPrice( new BigDecimal( tempPrice.toString() ) );
						wisList.add( wisVo );
					}
					wisVo = new WashItemSubtVo();			
					tempNum = 0;
					tempPrice = BigDecimal.ZERO;
				}
				tempNum = tempNum + 1;
				tempPrice = tempPrice.add( opEo.getPrice() );
				tempOpId = opEo.getProductId();
				totalPrice = totalPrice.add( opEo.getPrice() );
			}
			
			if( wisVo != null ){
				wisVo.setWashItemName( opList.get( opList.size() - 1 ).getName() );
				wisVo.setPrice( opList.get( opList.size() - 1 ).getPrice() );
				wisVo.setSubtNum( tempNum );
				wisVo.setSubtPrice( new BigDecimal( tempPrice.toString() ) );
				wisList.add( wisVo );
			}
			
			ro.setWashItemSubtList( wisList );
			ro.setTotalPrice( totalPrice );
		}		
		return ro;
	}
}
