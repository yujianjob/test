package com.landaojia.washclothes.laundry.client.washfactory.yibeijie;

import java.io.File;
import java.net.MalformedURLException;
import java.net.URL;

import javax.xml.namespace.QName;

import com.landaojia.washclothes.laundry.client.washfactory.WashFactoryInterface;
import com.landaojia.washclothes.laundry.client.washfactory.yibeijie.ws.IOrderService;
import com.landaojia.washclothes.laundry.client.washfactory.yibeijie.ws.OrderService;

/**
 * 洗衣工厂客接口-衣贝洁
 * @author liuxi
 */
public class WashFactoryInterfaceYiBeiJie implements WashFactoryInterface{
	
	public static final String WSDL_LOCATION = "http://180.166.224.253:89/CabinetWcf/OrderService.svc?wsdl";
	
	private static URL URL;
	
	private static QName QNAME;
	
	private native String getPassword();
	
	/**
	 * 初始化方法
	 */
	public WashFactoryInterfaceYiBeiJie(){
		try {
			URL = new URL( WSDL_LOCATION );
		} catch (MalformedURLException e) {
			throw new RuntimeException( e );
		}
		QNAME = new QName( "http://tempuri.org/", "OrderService" );
		
		try {
			String cp = new File("").getCanonicalPath();
			System.out.println( "cp:" + cp );
			
			//System.loadLibrary( cp + "\\aa.dll" );
		} catch (Exception e) {
			throw new RuntimeException( e );
		}
	}
	
	/**
	 * 创建订单
	 */
	public String makeOrder(){
		try {
			OrderService o = new OrderService(URL, QNAME);
			IOrderService iService = o.getBasicHttpBindingIOrderService();
			
			//----组装订单xml-----
			String password = "";
			String xml = "";

			try {
				Integer r = 2;//iService.orderUpLoad( xml, password );
				System.out.println( "call iService.orderUpLoad r:" + r );
				if( r == 1 ){
					//成功
				}else if( r == 0 ){
					//失败
				}else{
					//其他异常
				}
			} catch (Exception e) {
				//其他异常
				e.printStackTrace();
			}
			
			return "00";
		} catch (Exception e) {
			e.printStackTrace();
			return "";
		}
	}
	
	public static void main( String[] ss ){
		WashFactoryInterfaceYiBeiJie x = new WashFactoryInterfaceYiBeiJie();
		x.makeOrder();
	}
}
