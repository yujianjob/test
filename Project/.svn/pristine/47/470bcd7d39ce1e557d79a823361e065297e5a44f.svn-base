package com.landaojia.washclothes.userappserver.common.vo;

import com.alibaba.fastjson.JSON;

/**
 * json结果
 * 
 * @author liuxi
 */
public class JsonResult {
	/**
	 * 是否成功(表示服务端业务方法是否被正确调用到)
	 * 如果是系统异常，一律返回false: (1)没有权限，(2)发生系统异常被拦截器捕获
	 * 如果是业务异常，一律返回true: 在data中设置其他的值来处理业务分支
	 */
	private boolean succFlag = true;
	
	/**
	 * 消息
	 * 当isSucc为false时，包含该消息
	 */
	private String msg;
	
	/**
	 * 结果对象，用于业务
	 */
	private Object data;

	private JsonResult(){
		
	}
	
	/**
	 * 成功的调用
	 */
	public static JsonResult success( Object data ){
		JsonResult ar = new JsonResult();
		ar.succFlag = true;
		ar.data = data;
		return ar;
	}
	
	/**
	 * 转成json格式
	 */
	public String toJson(){
		return JSON.toJSONString( this );
	}
	
	/**
	 * 失败的调用
	 */
	public static JsonResult failure( String msg ){
		JsonResult ar = new JsonResult();
		ar.succFlag = false;
		ar.msg = msg;
		return ar;
	}

	public boolean isSuccFlag() {
		return succFlag;
	}

	public void setSuccFlag(boolean succFlag) {
		this.succFlag = succFlag;
	}

	public String getMsg() {
		return msg;
	}

	public void setMsg(String msg) {
		this.msg = msg;
	}

	public Object getData() {
		return data;
	}

	public void setData(Object data) {
		this.data = data;
	}
}
