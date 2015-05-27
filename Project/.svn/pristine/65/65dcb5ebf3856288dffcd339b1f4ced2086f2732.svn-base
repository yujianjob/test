package com.landaojia.washclothes.userappserver.web.controller.useraddress;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import com.landaojia.washclothes.userappserver.common.vo.JsonResult;
import com.landaojia.washclothes.userappserver.service.useraddress.EditUserAddressPo;
import com.landaojia.washclothes.userappserver.service.useraddress.UserAddressRo;
import com.landaojia.washclothes.userappserver.service.useraddress.UserAddressService;

/**
 * 订单地址
 * @author liuxi
 */
@Controller
@RequestMapping("/userAddress")
public class UserAddressController {
	
	@Autowired
	private UserAddressService userAddressService;
	
	/**
	 * 获取用户地址全部
	 */
	@ResponseBody
	@RequestMapping(value="/getUserAddressAll")
	public JsonResult getUserAddressAll( String userId ){
		List<UserAddressRo> list = userAddressService.getUserAddressAll( userId );
		return JsonResult.success( list );
	}
	
	/**
	 * 添加地址
	 */
	@ResponseBody
	@RequestMapping(value="/addUserAddress")
	public JsonResult addUserAddress( EditUserAddressPo po ){
		UserAddressRo ro = userAddressService.addUserAddress( po );
		return JsonResult.success( ro );
	}
	
	/**
	 * 修改地址
	 */
	@ResponseBody
	@RequestMapping(value="/updateUserAddress")
	public JsonResult updateUserAddress( EditUserAddressPo po ){
		UserAddressRo ro = userAddressService.updateUserAddress( po );
		return JsonResult.success( ro );
	}
	
	/**
	 * 删除地址
	 */
	@ResponseBody
	@RequestMapping(value="/deleteUserAddress")
	public JsonResult deleteUserAddress( EditUserAddressPo po ){
		UserAddressRo ro = userAddressService.deleteUserAddress( po );
		return JsonResult.success( ro );
	}
	
	/**
	 * 获取用户的默认地址
	 */
	@ResponseBody
	@RequestMapping(value="/getDefaultUserAddress")
	public JsonResult getDefaultUserAddress( EditUserAddressPo po ){
		UserAddressRo ro = userAddressService.getDefaultUserAddress( po );
		return JsonResult.success( ro );
	}
	
	/**
	 * 设置为默认地址
	 */
	@ResponseBody
	@RequestMapping(value="/setUserAddressDefault")
	public JsonResult setUserAddressDefault( EditUserAddressPo po ){
		UserAddressRo ro = userAddressService.setUserAddressDefault( po );
		return JsonResult.success( ro );
	}
}
