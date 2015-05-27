package com.landaojia.washclothes.laundry.client.client;

import com.alibaba.fastjson.JSON;
import com.landaojia.washclothes.laundry.client.camera.CameraHandler;
import com.landaojia.washclothes.laundry.client.printer.CommonPrinter;
import com.landaojia.washclothes.laundry.client.printer.ExpressBill;
import com.landaojia.washclothes.laundry.client.printer.PPLAPrinter;

/**
 * 客户端js接口(用于页面js调用java)
 * @author liuxi
 */
public class ClientJsApi {	
	/**
	 * 获取版本
	 */
	public String getVersion( String label ){
		return "v2.0.1";
	}
	
	/**
	 * 启动摄像头
	 */
	public boolean openCamera(){
		System.out.println( "【openCamera】" );
		CameraHandler.getInstance().openCamera();				
		return true;
	}
	
	/**
	 * 拍照
	 */
	public void capture( String dirPath, String picName ){
		System.out.println( "【capture】, dirPath: " + dirPath + ", picName: " + picName );
		CameraHandler.getInstance().capture( dirPath, picName );
	}
	
	/**
	 * 打印水洗条码
	 */
	public boolean printWashCode( String code ){
		System.out.println( "【printWashCode】, code: " + code );
		
		try {
			PPLAPrinter.init();
			PPLAPrinter.printBar( 20, 10, code );
			//PPLAPrinter.printText(50, 10, 40, code );
			PPLAPrinter.close();
		} catch (Exception e) {
			e.printStackTrace();
		} 
		
		return true;
	}
	
	/**
	 * 打印出库单
	 * @param json : 打印信息json
	 */
	public boolean printOutBoundInvoices( String json ){
		System.out.println( "【printOutBoundInvoices】，json:" + json );
		ExpressBill b = JSON.parseObject( json, ExpressBill.class ); 
		CommonPrinter.print(b, false);
		return true;
	}
}
