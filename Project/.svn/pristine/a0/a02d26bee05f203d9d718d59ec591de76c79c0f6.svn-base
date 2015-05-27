package com.landaojia.washclothes.laundry.common.ajax;

import com.alibaba.fastjson.JSON;

/**
 * ajax调用后返回的结果
 * @author liuxi
 */
public final class AjaxResult {
	/**
	 * 是否成功(表示服务端业务方法是否被正确调用到)
	 * 返回false的情况 ： (1)没有权限，(2)发生系统异常被拦截器捕获
	 * 如果是业务异常，一律返回true，在data中设置其他的值来处理业务分支
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

	private AjaxResult(){
		
	}
	
	/**
	 * 成功的调用
	 */
	public static AjaxResult success( Object data ){
		AjaxResult ar = new AjaxResult();
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
	public static AjaxResult failure( String msg ){
		AjaxResult ar = new AjaxResult();
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
