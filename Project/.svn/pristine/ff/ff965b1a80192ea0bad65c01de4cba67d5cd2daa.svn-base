package com.landaojia.washclothes.laundry.web.interceptor;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.web.servlet.HandlerInterceptor;
import org.springframework.web.servlet.ModelAndView;

import com.landaojia.washclothes.laundry.common.logger.Logger;
import com.landaojia.washclothes.laundry.common.logger.LoggerManager;
import com.landaojia.washclothes.laundry.common.util.HttpRequestUtil;

/**
 * 权限拦截器
 * @author liuxi
 */
public class PermissionInterceptor implements HandlerInterceptor{

	private Logger logger = LoggerManager.getLogger( this.getClass() );
	
	@Override
	public boolean preHandle(HttpServletRequest request, HttpServletResponse response, Object obj) throws Exception {
		//----如果是没有权限todo...----------
		String sessionId = request.getSession().getId();
		logger.info( "sessionId:" + sessionId + ", ip:" + HttpRequestUtil.getRequestIp( request ) + ", request:" + HttpRequestUtil.getPageUri( request ) );
		
		return true;
	}
	
	@Override
	public void postHandle(HttpServletRequest request, HttpServletResponse response, Object obj, ModelAndView mav ) throws Exception {
		
	}
	
	@Override
	public void afterCompletion(HttpServletRequest request, HttpServletResponse response, Object obj, Exception exception ) throws Exception {
		
	}
}
