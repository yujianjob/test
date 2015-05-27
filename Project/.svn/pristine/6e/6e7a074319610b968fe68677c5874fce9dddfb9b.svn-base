package com.landaojia.washclothes.userappserver.web.controller.order;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import com.landaojia.washclothes.userappserver.common.paginquery.PagingQueryResult;
import com.landaojia.washclothes.userappserver.common.vo.JsonResult;
import com.landaojia.washclothes.userappserver.dao.orderorder.OrderSimpleVo;
import com.landaojia.washclothes.userappserver.dao.orderorder.PagingQueryOrderCo;
import com.landaojia.washclothes.userappserver.service.order.CancelOrderPo;
import com.landaojia.washclothes.userappserver.service.order.OneKeyOrderPo;
import com.landaojia.washclothes.userappserver.service.order.OrderDetailRo;
import com.landaojia.washclothes.userappserver.service.order.OrderService;
import com.landaojia.washclothes.userappserver.service.order.QueryOrderDetailPo;

/**
 * 订单
 * @author liuxi
 */
@Controller
@RequestMapping("/order")
public class OrderController {
	
	@Autowired
	private OrderService orderService;
	
	/**
	 * 一键下单
	 */
	@ResponseBody
	@RequestMapping(value="/oneKeyOrder")
	public JsonResult oneKeyOrder( OneKeyOrderPo po ){
		OrderSimpleVo vo = orderService.oneKeyOrder( po );
		return JsonResult.success( vo );
	}
	
	/**
	 * 取消订单
	 */
	@ResponseBody
	@RequestMapping(value="/cancelOrder")
	public JsonResult cancelOrder( CancelOrderPo po ){
		OrderSimpleVo vo = orderService.cancelOrder( po );
		return JsonResult.success( vo );
	}
	
	/**
	 * 用户订单列表
	 */
	@ResponseBody
	@RequestMapping(value="/pagingQueryOrder")
	public JsonResult pagingQueryOrder( PagingQueryOrderCo co ){
		PagingQueryResult<OrderSimpleVo> pqr = orderService.pagingQueryOrder( co );
		return JsonResult.success( pqr );
	}
	
	/**
	 * 查询订单详情
	 */
	@ResponseBody
	@RequestMapping(value="/queryOrderDetail")
	public JsonResult queryOrderDetail( QueryOrderDetailPo po ){
		OrderDetailRo ro = orderService.queryOrderDetail( po );
		return JsonResult.success( ro );
	}
	
}
