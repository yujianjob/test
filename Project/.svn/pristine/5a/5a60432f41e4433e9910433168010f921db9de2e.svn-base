package com.landaojia.washclothes.laundry.common.exception;

/**
 * 异常代码枚举
 * @author liuxi
 */
public enum CommonExceptionCode {
	S000001( "系统异常" ),
	S000002("参数错误"),
	
	@Deprecated
	E999999( null );//保留异常代码，不建议使用

	/**
	 * 消息
	 */
	private String message;

	private CommonExceptionCode( String message ){
		this.message = message;
	}

	public String getCode(){
		return this.name();
	}

	public String getMessage(){
		return message;
	}
}
