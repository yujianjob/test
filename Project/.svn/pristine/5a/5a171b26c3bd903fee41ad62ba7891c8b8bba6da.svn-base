package com.landaojia.washclothes.userappserver.web.controller.sms;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import com.landaojia.washclothes.userappserver.common.util.TextUtil;
import com.landaojia.washclothes.userappserver.common.vo.JsonResult;
import com.landaojia.washclothes.userappserver.service.sms.yimei.SmsYiMeiService;

/**
 * 短信
 * @author liuxi
 */
@Controller
@RequestMapping("/sms")
public class SmsController {
	
	@Autowired
	private SmsYiMeiService smsYiMeiService;
	
	/**
	 * 发送短信-(下单后)验证手机的验证码
	 * @param len 数字的长度
	 */
	@ResponseBody
	@RequestMapping(value="/sendSmForVerifyMobliePhone")
	public JsonResult sendSmForVerifyMobliePhone( String mpNo ){
		String number = TextUtil.lPadForLen( ( int )( Math.random() * 999999 ) + "", '0', 6 );
		smsYiMeiService.sendSms( mpNo, "感谢您使用懒到家手机客户端，您的手机验证码为" + number );
		return JsonResult.success( number );
	}
	
}
