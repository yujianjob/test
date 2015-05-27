package com.landaojia.washclothes.laundry.common.util;

import java.lang.reflect.InvocationHandler;
import java.lang.reflect.Method;
import java.lang.reflect.Proxy;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

import org.springframework.aop.framework.AopProxy;
import org.springframework.beans.BeansException;
import org.springframework.context.ApplicationContext;
import org.springframework.context.ApplicationContextAware;
import org.springframework.stereotype.Component;

/**
 * spring帮助类
 * @author liuxi
 */
@Component
public class SpringUtil implements ApplicationContextAware{
	
	private static ApplicationContext applicationContext;
	
	/**
	 * bean的Class缓存
	 */
	private static Map<String, Class<?>> BEAN_CLASS_MAP = new ConcurrentHashMap<String, Class<?>>();

	/**
	 * bean的方法缓存
	 */
	private static Map<String, Method> BEAN_METHOD_MAP = new ConcurrentHashMap<String, Method>();
	
	@Override
	public void setApplicationContext(ApplicationContext ac) throws BeansException {
		applicationContext = ac;
	}
	
	/**
	 * 取得bean
	 */
	@SuppressWarnings("unchecked")
	public static <T> T getBean( String name, Class<T> clazz ){
		return (T)applicationContext.getBean( name );
	}
	
	/**
	 * 取得bean
	 */
	public static <T> T getBean( Class<T> clazz ){
		return applicationContext.getBean( clazz );
	}
	
	/**
	 * 调用bean的方法取得返回值
	 * @param beanName : bean的名称
	 * @param methodName : 方法名称
	 * @param args : 参数值
	 * @param argTypes : 参数类型
	 */
	public static Object invokeBeanMethod( String beanName, String methodName, Object[] args, Class<?>[] argTypes ){
		try {
			Object o = applicationContext.getBean( beanName );
			if( o == null ){
				throw new RuntimeException( "beanName:" + beanName + " is null" );
			}
			InvocationHandler ih = (Proxy.getInvocationHandler( o ));
			AopProxy aopProxy = (AopProxy)ih;

			Class<?> clazz = null;
			if( BEAN_CLASS_MAP.containsKey( beanName ) ){
				clazz = BEAN_CLASS_MAP.get( beanName );
			}else{
				String clzName = String.valueOf( aopProxy.getProxy() );
				if( clzName.contains( "@" ) ){
					clzName = clzName.substring( 0, clzName.indexOf( "@" ) );
				}
				clazz = Class.forName( clzName );
				BEAN_CLASS_MAP.put( beanName, clazz );
			}
			
			Method method = null;
			String argTypesStr = "";
			if( argTypes != null && argTypes.length > 0 ){
				for( int i = 0; i < argTypes.length; i ++ ){
					argTypesStr = argTypes[i].getName() + ( i + 1 == argTypes.length? "" : "," );
				}
			}
			
			if( BEAN_METHOD_MAP.containsKey( beanName + "$$" + methodName + "$$" + argTypesStr ) ){
				method = BEAN_METHOD_MAP.get( beanName + "$$" + methodName + "$$" + argTypesStr );
			}else{
				method = clazz.getMethod(methodName, argTypes );
				BEAN_METHOD_MAP.put( beanName + "$$" + methodName + "$$" + argTypesStr, method );
			}
			
			Object r = ih.invoke(aopProxy.getProxy(), method, args == null? new Object[]{} : args );
			return r;
		} catch (Throwable e) {
			throw new RuntimeException( e );
		} 
	}
}
