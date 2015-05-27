package com.landaojia.washclothes.userappserver.common.exception;

import java.util.Date;
import java.util.Set;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.web.servlet.HandlerExceptionResolver;
import org.springframework.web.servlet.ModelAndView;


//import com.landaojia.washclothes.userappserver.common.ajax.AjaxResult;
import com.landaojia.washclothes.userappserver.common.logger.Logger;
import com.landaojia.washclothes.userappserver.common.logger.LoggerManager;
import com.landaojia.washclothes.userappserver.common.util.DateUtil;
import com.landaojia.washclothes.userappserver.common.util.HttpRequestUtil;
import com.landaojia.washclothes.userappserver.common.util.StackTraceUtil;
import com.landaojia.washclothes.userappserver.common.vo.JsonResult;

/**
 * 异常拦截器
 * (1)当发生异常时打印出异常信息
 * (2)当发生异常时，返回统一的JsonResult对象
 * @author liuxi
 */
public class ControllerExceptionResolver implements HandlerExceptionResolver {

	private Logger logger = LoggerManager.getLogger(this.getClass());
	
	@Override
	public ModelAndView resolveException(HttpServletRequest request, HttpServletResponse response, Object handler, Exception ex) {
		//----打印出所有请求参数---------------
		Set<String> keySet = request.getParameterMap().keySet();
		StringBuilder sb = new StringBuilder();
		if (keySet != null && keySet.size() > 0) {
			int size = keySet.size();
			int i = 0;
			for (String key : keySet) {
				i += 1;
				
				String value = request.getParameter(key);				
				sb.append(key).append("=").append(value);
				if( i != size ){
					sb.append(",");
				}
			}
		}
		logger.info( "ip:" + HttpRequestUtil.getRequestIp( request ) + " request:" + HttpRequestUtil.getPageUri( request ) + " params:" + sb.toString() );

		//----对于异常类型处理----------------
		String idf = "【" + DateUtil.dateToStr( new Date(), "yyyy-MM-dd HH:mm:ss" ) + "(" + (int)( Math.random() * 99999999 ) + ")】";
		String errMsg = "";
		if (ex instanceof CommonException) {
			CommonException ce = ( CommonException ) ex;
			errMsg = ce.getMessage();
			logger.error( ce.getCode() + "-" + ce.getMessage() + "【" + ce.getThrowAt() + "】" );
		} else {
			errMsg = "系统异常" + idf;
			logger.error( errMsg, ex );
		}
		
		JsonResult ar = JsonResult.failure( errMsg );
		response.setContentType("application/json;charset=UTF-8");
		try {
			response.getWriter().println( ar.toJson() );
		} catch (Exception e) {
			logger.error( "====ERROR(ajax)====", e );
		}

		return new ModelAndView();
	}
}
