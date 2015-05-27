package com.landaojia.washclothes.userappserver.common.util;

/**
 * TextUtil
 * @author liuxi
 */
public class TextUtil {
	
	/**
	 * 左边添加某字符，凑齐长度
	 */
	public static String lPadForLen( String str, char c, int len ){
		if( len < str.length() ){
			return str;
		}
		
		StringBuilder sb = new StringBuilder();
		for( int i = 0; i < len - str.length(); i ++ ){
			sb.append( c );
		}
		
		return sb.toString() + str;
	}
}
