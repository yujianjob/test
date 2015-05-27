package com.landaojia.washclothes.userappserver.common.util;

/**
 * 栈踪迹工具
 * @author liuxi
 */
public class StackTraceUtil {
	/**
	 * 获取踪迹信息
	 */
	public static String getInfo( Integer hry ) {
		StackTraceElement[] stes = Thread.currentThread().getStackTrace();
		StackTraceElement tgt = stes[hry];
		return tgt.getClassName() + "[" + tgt.getMethodName() + "():" + tgt.getLineNumber() + "]";
	}
}
