package com.landaojia.washclothes.userappserver.common.exception;

/**
 * 异常代码枚举
 * @author liuxi
 */
public enum CommonExceptionCode {
	S000001( "系统异常" ),
	S000002( "参数错误" ),
	S000003( "发送短信失败" ),
	S000004( "手机号{}已被注册" ),
	S000005( "订单号{}不存在" ),
	S000006( "订单号{}不是您的订单" ),
	S000007( "订单号{}当前的状态无法取消" ),
	S000008( "地址不存在，无法修改" ),
	S000009( "改地址不属于您，无法修改" ),
	S000010( "地址不存在，无法删除" ),
	S000011( "改地址不属于您，无法删除" ),
	S000012( "地址不存在，无法设置" ),
	S000013( "改地址不属于您，无法设置" ),
	S000014( "订单不存在" ),
	
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
