package com.landaojia.washclothes.userappserver.web.controller.register;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import com.landaojia.washclothes.userappserver.common.vo.JsonResult;
import com.landaojia.washclothes.userappserver.service.register.RegisterPo;
import com.landaojia.washclothes.userappserver.service.register.RegisterRo;
import com.landaojia.washclothes.userappserver.service.register.RegisterService;

/**
 * 注册
 * @author liuxi
 */
@Controller
@RequestMapping("/register")
public class RegisterController {
	
	@Autowired
	private RegisterService registerService;
	
	/**
	 * 注册
	 */
	@ResponseBody
	@RequestMapping("/registerUser")
	public JsonResult registerUser( RegisterPo po ){
		RegisterRo ro = registerService.registerUser( po );
		return JsonResult.success( ro );
	}
}
