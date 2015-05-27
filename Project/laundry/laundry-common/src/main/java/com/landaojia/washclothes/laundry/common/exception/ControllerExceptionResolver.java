package com.landaojia.washclothes.laundry.common.exception;

import java.util.Date;
import java.util.Set;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.web.servlet.HandlerExceptionResolver;
import org.springframework.web.servlet.ModelAndView;

import com.landaojia.washclothes.laundry.common.ajax.AjaxResult;
import com.landaojia.washclothes.laundry.common.logger.Logger;
import com.landaojia.washclothes.laundry.common.logger.LoggerManager;
import com.landaojia.washclothes.laundry.common.util.DateUtil;
import com.landaojia.washclothes.laundry.common.util.HttpRequestUtil;

/**
 * 异常拦截器
 * (1)当发生异常时打印出异常信息
 * (2)根据请求类型(普通/ajax)返回给相应的信息
 * @author liuxi
 */
public class ControllerExceptionResolver implements HandlerExceptionResolver {

	private String errorPage = "/WEB-INF/jsp/error.jsp";
	
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
		String idf = DateUtil.dateToStr( new Date(), "yyyy-MM-dd HH:mm:ss" ) + "(" + (int)( Math.random() * 99999999 ) + ")";
		String errMsg = "";
		CommonException myException = null;
		if (ex instanceof CommonException) {
			myException = ( CommonException )ex;
			errMsg = idf + ":" + myException.getCode() + "-" + myException.getMessage();
			logger.error( errMsg, myException );
		} else {
			myException = new CommonException( "系统异常," + ex.getClass().getName() + ":" + ex.getMessage() );
			errMsg = idf + ":" + myException.getCode() + "-" + myException.getMessage();
			logger.error( errMsg, ex );
		}
		
		//----如果是ajax请求，当发生异常时，一律返回AjaxResult对象，包含简单报错信息-------------
		String isAjax = request.getHeader( "is-ajax" );
		if( "true".equals( isAjax ) ){
			response.setHeader( "is-ajax-result", "true" );
			AjaxResult ar = AjaxResult.failure( errMsg );
			response.setContentType("application/json;charset=UTF-8");
			try {
				response.getWriter().println( ar.toJson() );
			} catch (Exception e) {
				logger.error( "====ERROR(ajax)====", e );
			}
		}
		//----如果是普通请求，当发生异常时，显示错误页面，包含简单报错信息----
		else{
			response.setContentType("text/html;charset=utf-8");
			try {
				request.getRequestDispatcher( errorPage ).forward( request, response );
			} catch (Exception e) {
				logger.error( "====ERROR(form)====", e );
			} 
		}
		
		return new ModelAndView();
	}

	public void setErrorPage(String errorPage) {
		this.errorPage = errorPage;
	}
}
