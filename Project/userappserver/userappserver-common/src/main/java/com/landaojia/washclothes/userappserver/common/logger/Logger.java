package com.landaojia.washclothes.userappserver.common.logger;

import com.landaojia.washclothes.userappserver.common.util.StackTraceUtil;

/**
 * 日志
 * @author liuxi
 */
public class Logger {

	//private org.apache.log4j.Logger realLogger;
	
	private org.slf4j.Logger realLogger;

	public Logger(Class<?> clazz) {
		//realLogger = org.apache.log4j.Logger.getLogger(clazz);
		realLogger = org.slf4j.LoggerFactory.getLogger(clazz);
	}

//	public void debug(String message, Object... args) {
//		if (isPrint()) {
//			realLogger.debug(message + args);
//		}
//	}

	public void info(String message, Object... args) {
		if (isPrint()) {
			//realLogger.info( getInvokerInfo() + " " + message );
			realLogger.info( message, args );
		}
	}

//	public void warn(String message, Object... args) {
//		if (isPrint()) {
//			realLogger.warn(message + args);
//		}
//	}

	public void error(String message) {
		if (isPrint()) {
			realLogger.error(getInvokerInfo() + " " + message);
		}
	}

	public void error(String message, Object...objs ) {
		if (isPrint()) {
			realLogger.error(getInvokerInfo() + " " + message );
		}
	}

	public void error(String message, Exception e, Object...objs) {
		if (isPrint()) {
			realLogger.error( StackTraceUtil.getInfo( 1 ) + " " + message, e );
		}
	}

	// public void error(String message, Exception e, Object... args) {
	// if( isPrint() ){
	// realLogger.error(message, e, args);
	// }
	// }

	/**
	 * 记录(当发生错误时)上层调用的类、方法、参数，异常信息
	 */
//	public void errorInvokeInfo(Exception e) {
//		if (isPrint()) {
//			realLogger.error(this.getTargetInvokeClassMethodParams(), e);
//		}
//	}

	/**
	 * 取得源类的栈调用信息
	 */
	private String getInvokerInfo() {
//		StackTraceElement[] stes = Thread.currentThread().getStackTrace();
//		StackTraceElement tgt = stes[1];
//		return tgt.getClassName() + "[" + tgt.getMethodName() + "():" + tgt.getLineNumber() + "]";
		return "";
	}

	/**
	 * 获取目标被调用者的信息(类、方法、参数)
	 *//*
	private String getTargetInvokeClassMethodParams() {
		StackTraceElement[] stack = Thread.currentThread().getStackTrace();// (new Throwable()).getStackTrace();
		for (int i = 0; i < stack.length; i++) {
			StackTraceElement ste = stack[i];
			String className = ste.getClassName();
			boolean f = false;
			if (className.equals(this.getClass().getName())) {
				f = true;
			} else {
				if (f) {
					String methodName = ste.getMethodName();
					// TODO......怎样获取目标被调用者的信息(类、方法、参数)？？
					return className + "." + methodName + "(...)";
				}
			}
			// System.out.println( ste.getClassName()+ "." + ste.getMethodName()
			// + "(...);");
			// System.out.println(i + "-" + ste.getMethodName());
			// System.out.println(i + "-" + ste.getFileName());
			// System.out.println(i + "-" + ste.getLineNumber());
		}
		return "";
	}*/

	/**
	 * 拦截服务，决定是否打印日志
	 */
	private Boolean isPrint() {
		// TODO......怎样实现决定是否打印日志？？黑白名单？？哪个类哪个方法？
		// TODO......需要控制级别吗？
		return true;
	}
	
	/**
	 * 记录上层调用的类、方法、参数
	 */
//	public void infoInvokeInfo() {
//		if (isPrint()) {
//			realLogger.error(this.getTargetInvokeClassMethodParams());
//		}
//	}
}
