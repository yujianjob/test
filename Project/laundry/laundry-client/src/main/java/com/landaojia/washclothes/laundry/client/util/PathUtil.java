package com.landaojia.washclothes.laundry.client.util;

/**
 * 路径工具
 * @author liuxi
 */
public class PathUtil {
	
	/**
	 * 获取当前运行所在的目录地址
	 * (1)class或eclipse中运行，该class所在的根目录，例如：D:/work/javaProject/landaojia/washclothes/laundry/laundry-client/target/classes/
	 * (2)jar方式运行，即jar所在的目录，例如：D:/work/javaProject/landaojia/washclothes/laundry/
	 */
	public static String getRunDir(){
		String p = PathUtil.class.getProtectionDomain().getCodeSource().getLocation().getPath();
		String path = p.substring( 0, p.lastIndexOf( "/" ) ) + "/";
		path = path.startsWith( "/" )? p.substring( 1 ) : p;
		System.out.println( path );
		return path;
	}
	
	public static void main( String[] s ){
		getRunDir();
	}
}
