package com.landaojia.washclothes.userappserver.web.controller.washprice;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import com.landaojia.washclothes.userappserver.common.vo.JsonResult;
import com.landaojia.washclothes.userappserver.dao.washproduct.WashClassProductVo;
import com.landaojia.washclothes.userappserver.service.washproduct.WashProductService;

/**
 * 洗涤产品
 * @author liuxi
 */
@Controller
@RequestMapping("/washPrice")
public class WashPriceController {
	
	@Autowired
	private WashProductService washProductService;
	
	/**
	 * 准备添加(衣物)条目，开启小窗口
	 */
	@ResponseBody
	@RequestMapping(value="/getAllWashPriceList")
	public JsonResult getAllWashPriceList(){
		List<WashClassProductVo> wcpList = washProductService.getAllWashClassProductListFromCache();
		return JsonResult.success( wcpList );
	}

}
