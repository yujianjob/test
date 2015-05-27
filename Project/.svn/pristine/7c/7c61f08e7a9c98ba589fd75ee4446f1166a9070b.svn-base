package com.landaojia.washclothes.laundry.common.interceptor;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.web.servlet.HandlerInterceptor;
import org.springframework.web.servlet.ModelAndView;

/**
 * ajax请求拦截器
 * @author liuxi
 */
public class AjaxRequestInterceptor implements HandlerInterceptor{	
	@Override
	public boolean preHandle(HttpServletRequest request, HttpServletResponse response, Object obj) throws Exception {
		//----对于ajax请求的头信息处理------------
		String isAjax = request.getHeader( "is-ajax" );
		if( "true".equals( isAjax ) ){
			response.setHeader( "is-ajax-result", "true" );//表示返回结果是AjaxResult对象
			response.setHeader( "Content-Type", "text/html;charset=utf-8" );
		}
		return true;
	}
	
	@Override
	public void postHandle(HttpServletRequest request, HttpServletResponse response, Object obj, ModelAndView mav ) throws Exception {
		//----是否是ajax请求类型，在响应头中添加isAjax和isAjaxResult标志----
		String isAjax = request.getHeader( "is-ajax" );
		if( "true".equals( isAjax ) && mav != null ){
			response.setHeader( "is-ajax-result", "false" );//表示返回的是页面片段
		}
	}
	
	@Override
	public void afterCompletion(HttpServletRequest request, HttpServletResponse response, Object obj, Exception exception ) throws Exception {
		
	}
}
