package com.landaojia.washclothes.laundry.web.controller.outbound;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import com.landaojia.washclothes.laundry.common.ajax.AjaxResult;
import com.landaojia.washclothes.laundry.common.util.JsonUtil;
import com.landaojia.washclothes.laundry.dao.exporder.ExpOrderEo;
import com.landaojia.washclothes.laundry.dao.orderorder.OrderOrderEo;
import com.landaojia.washclothes.laundry.dao.orderproduct.OrderProductEo;
import com.landaojia.washclothes.laundry.dao.orderproduct.OrderProductOutBoundVo;
import com.landaojia.washclothes.laundry.service.basenotify.BaseNotifyService;
import com.landaojia.washclothes.laundry.service.exporder.ExpOrderService;
import com.landaojia.washclothes.laundry.service.orderorder.OrderOrderService;
import com.landaojia.washclothes.laundry.service.orderproduct.OrderProductService;


/**
 * 出库管理
 * @author liuxi
 */
@Controller
@RequestMapping("/outBound")
public class OutBoundController {
	
	@Autowired
	private ExpOrderService expOrderService;
	
	@Autowired
	private OrderOrderService orderOrderService;
	
	@Autowired
	private OrderProductService orderProductService;
	
	@Autowired
	private BaseNotifyService baseNotifyService;
	
	/**
	 * 到出库单界面
	 */
	@RequestMapping("/toOutBound")
	public String toOutBound(){
		return "/outbound/outBound";
	}
	
	/**
	 * 根据洗涤条码，查询订单信息，订单产品
	 */
	@ResponseBody
	@RequestMapping(value="/getInfoByCode")
	public AjaxResult getInfoByCode( String code, ModelMap model ){
		
		//----洗涤产品----
		OrderProductEo orderProductEo = orderProductService.getByCode( code );
		if( orderProductEo == null ){
			return AjaxResult.success( new Object[]{ false, "根据洗涤条码：" + code + "未找到洗涤衣物记录" } );
		}
		
		//----取得订单-------
		OrderOrderEo orderOrderEo = orderOrderService.getById( orderProductEo.getOId() );
		if( orderOrderEo == null ){
			return AjaxResult.success( new Object[]{ false, "根据洗涤条码：" + code + "未找到洗涤衣物记录" } );
		}
		
		//----获取物流订单expOrder-------
		ExpOrderEo expOrderEo = expOrderService.getByOutNumberSendBack( orderOrderEo.getOrderNumber() );
		if( expOrderEo == null ){
			return AjaxResult.success( new Object[]{ false, "根据洗涤条码：" + code + "未找到物流订单记录" } );
		}

		//----获取洗涤产品orderProduct------
		List<OrderProductEo> opList = orderProductService.getListByOrderId( orderOrderEo.getId() );
		
		return AjaxResult.success( new Object[]{ true, orderOrderEo, expOrderEo, opList } );
	}
	
	/**
	 * 出库操作
	 */
	@ResponseBody
	@RequestMapping(value="/outBoundOrderProduct")
	public AjaxResult outBoundOrderProduct( Long id, ModelMap model ){
		OrderProductOutBoundVo opobVo = orderProductService.orderProductOutBound( id );
		return AjaxResult.success( new Object[]{ "00", JsonUtil.toJson( opobVo ) } );
	}
	
	/**
	 * 异件上报，开启小窗口
	 */
	@RequestMapping(value="/toAbnormalSubmit")
	public String toAbnormalSubmit( ModelMap model ){
		return "/outbound/abnormalSubmit";
	}
	
	/**
	 * 异件上报
	 */
	@ResponseBody
	@RequestMapping(value="/abnormalSubmit")
	public AjaxResult abnormalSubmit( AbnormalSubmitParams params, ModelMap model ){
		baseNotifyService.saveBaseNotifyForAbnormalSubmit( params );
		return AjaxResult.success( new Object[]{ "00", params.getOrderNumber() } );
	}
}
