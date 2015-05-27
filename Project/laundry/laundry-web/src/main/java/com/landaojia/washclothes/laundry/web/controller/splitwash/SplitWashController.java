package com.landaojia.washclothes.laundry.web.controller.splitwash;

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
import com.landaojia.washclothes.laundry.dao.washproduct.WashClassProductVo;
import com.landaojia.washclothes.laundry.service.basenotify.BaseNotifyService;
import com.landaojia.washclothes.laundry.service.exporder.ExpOrderService;
import com.landaojia.washclothes.laundry.service.orderorder.OrderOrderService;
import com.landaojia.washclothes.laundry.service.orderproduct.OrderProductService;
import com.landaojia.washclothes.laundry.service.washcode.WashCodeService;
import com.landaojia.washclothes.laundry.service.washproduct.WashProductService;

/**
 * 拆包洗涤
 * @author liuxi
 */
@Controller
@RequestMapping("/splitWash")
public class SplitWashController {
	
	@Autowired
	private WashProductService washProductService;
	
	@Autowired
	private ExpOrderService expOrderService;
	
	@Autowired
	private OrderOrderService orderOrderService;

	@Autowired
	private OrderProductService orderProductService;
	
	@Autowired
	private BaseNotifyService baseNotifyService;
	
	@Autowired
	private WashCodeService washCodeService;
	
	/**
	 * 扫描主页面
	 */
	@RequestMapping(value="/toSplitWash")
	public String toSplitWash(){
		return "/splitwash/splitWash";
	}
	
	/**
	 * 根据填写的物流单号，带出订单及物件条目
	 */
	@ResponseBody
	@RequestMapping(value="/getInfoByExpNumber")
	public AjaxResult getInfoByExpNumber( String expNumber, ModelMap model ){
		//----获取物流订单expOrder-------
		ExpOrderEo expOrderEo = expOrderService.getByExpNumber( expNumber );
		if( expOrderEo == null ){
			return AjaxResult.success( new Object[]{ false, "根据物流单号：" + expNumber + "未找到物流订单信息" } );
		}
		
		//----取得订单-------
		OrderOrderEo orderOrderEo = orderOrderService.getByOrderNumber( expOrderEo.getOutNumber() );
		if( orderOrderEo == null ){
			return AjaxResult.success( new Object[]{ false, "根据物流单号：" + expNumber + "未找到订单信息" } );
		}
		
		//----获取洗涤产品orderProduct---------
		List<OrderProductEo> opList = orderProductService.getListByOrderId( orderOrderEo.getId() );

		return AjaxResult.success( new Object[]{ true, expOrderEo, orderOrderEo, opList } );
	}
	
	/**
	 * 准备添加(衣物)条目，开启小窗口
	 */
	@RequestMapping(value="/toAddOrderProduct")
	public String toAddOrderProduct( ModelMap model ){
		List<WashClassProductVo> wcpList = washProductService.getAllWashClassProductListFromCache();
		model.put( "wcpList", wcpList );
		model.put( "code", washCodeService.generateWashCode() );
		
		String json = JsonUtil.toJson( wcpList );
		System.out.println( json );		
		return "/splitwash/editOrderProduct";
	}
	
	/**
	 * 生成洗涤条码
	 */
	@ResponseBody
	@RequestMapping(value="/generateWashCode")
	public AjaxResult generateWashCode(){
		String code = washCodeService.generateWashCode();
		return AjaxResult.success( code );
	}
	
	/**
	 * 添加衣物
	 */
	@ResponseBody
	@RequestMapping(value="/addOrderProduct")
	public AjaxResult addOrderProduct( EditOrderProductParams params ){
		OrderProductEo eo = orderProductService.addOrderProduct( params );
		return AjaxResult.success( new Object[]{ true, eo } );
	}

	/**
	 * 删除订单产品
	 */
	@ResponseBody
	@RequestMapping(value="/deleteOrderProduct")
	public AjaxResult deleteOrderProduct( Long id ){
		orderProductService.deleteOrderProduct( id );
		return AjaxResult.success( new Object[]{ true } );
	}
	
	/**
	 * 异件上报，开启小窗口
	 */
	@RequestMapping(value="/toAbnormalSubmit")
	public String toAbnormalSubmit( ModelMap model ){
		return "/splitwash/abnormalSubmit";
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
