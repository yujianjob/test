package com.landaojia.washclothes.laundry.common.util;

import com.alibaba.fastjson.JSON;

/**
 * json工具
 * @author liuxi
 */
public class JsonUtil {
	
	/**
	 * 对象转json字符串
	 */
	public static String toJson( Object object ){
		return JSON.toJSONString( object ); 
	}
	
	/**
	 * json字符串转对象
	 */
	public static <T> T toObject( String json, Class<T> clazz ){
		return JSON.parseObject( json, clazz ); 
	}
	
}
