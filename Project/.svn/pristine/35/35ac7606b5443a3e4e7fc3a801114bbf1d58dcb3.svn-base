package com.landaojia.washclothes.laundry.common.exception;

/**
 * 通用异常
 * @author liuxi
 */
public class CommonException extends RuntimeException{
	
	private static final long serialVersionUID = -2409606069171261066L;

	/**
	 * 异常代码
	 */
	private String code;
	
	/**
	 * 异常消息
	 */
	private String message;
	
	public CommonException( CommonExceptionCode c ){
		this.code = c.getCode();
		this.message = c.getMessage();
	}
	
	public CommonException( String message ){
		this.code = CommonExceptionCode.E999999.getCode();
		this.message = message;
	}
	
	public String getCode() {
		return code;
	}

	public String getMessage() {
		return message;
	}
}
