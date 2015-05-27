package com.landaojia.washclothes.laundry.client.camera;


import java.awt.BorderLayout;
import java.awt.Component;
import java.awt.Graphics2D;
import java.awt.Image;
import java.awt.Toolkit;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.Date;

import javax.media.Buffer;
import javax.media.CaptureDeviceInfo;
import javax.media.Manager;
import javax.media.MediaLocator;
import javax.media.Player;
import javax.media.cdm.CaptureDeviceManager;
import javax.media.control.FrameGrabbingControl;
//import java.awt.*;
import javax.media.format.VideoFormat;
import javax.media.util.BufferToImage;
import javax.swing.JFrame;

import com.sun.image.codec.jpeg.ImageFormatException;
import com.sun.image.codec.jpeg.JPEGCodec;
import com.sun.image.codec.jpeg.JPEGEncodeParam;
import com.sun.image.codec.jpeg.JPEGImageEncoder;

/**
 * 摄像头处理器
 * @author codingwoo <long1795@gmail.com>
 */
@SuppressWarnings("restriction")
public class CameraHandler {
    public Player player;
    private CaptureDeviceInfo di;
    private MediaLocator ml;
    private JFrame jf;

    public static  String PIC_BASE_DIR = "D:/laundry/pics/";
    
    public boolean initedFlag = false;
    
    public static CameraHandler cameraHandler;
    
    
    
    public static CameraHandler getInstance(){
    	if( cameraHandler == null ){
    		cameraHandler = new CameraHandler();
    	}
    	
    	return cameraHandler;
    }
    
    /**
     * 开启摄像头
     * (当出现窗口时，点击确定(不要点应用)，大约需要2秒能启动摄像头)
     */
    public void openCamera() {
    	if( initedFlag ){
    		jf.setVisible(true);
    		return;
    	}
    	
        int X1 = Toolkit.getDefaultToolkit().getScreenSize().width;
        int Y1 = Toolkit.getDefaultToolkit().getScreenSize().height;
        
        jf = new JFrame();
        jf.setTitle("窗口");
        jf.setBounds((X1 - 648) / 2, (Y1 - 550) / 2, 648, 550);
        jf.setDefaultCloseOperation(JFrame.HIDE_ON_CLOSE);
        
        //String str1 = "vfw:Logitech USB Video Camera:0";
        String str1 = "vfw:Microsoft WDM Image Capture (Win32):0";
        di = CaptureDeviceManager.getDevice(str1);
        ml = di.getLocator();
        try {
            player = Manager.createRealizedPlayer(ml);
            player.start();
            Component comp = null;
            if ((comp = player.getVisualComponent()) != null) {
                jf.add(comp, BorderLayout.NORTH);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

        jf.setVisible(true);
        initedFlag = true;
        //initedFlag = testCanCapture();
    }

    /**
     * 获取当前图像
     */
    private Image getCurrentImage(){
        FrameGrabbingControl fgc = (FrameGrabbingControl) player.getControl("javax.media.control.FrameGrabbingControl");
        Buffer buffer = fgc.grabFrame();
        BufferToImage bufferToImage = new BufferToImage((VideoFormat) buffer.getFormat());
        Image image = bufferToImage.createImage(buffer);
        return image;
    }
    
    /**
     * 测试是否能正常拍照
     * @deprecated
     */
    public boolean testCanCapture(){
    	Image image = getCurrentImage();
    	try{
    		BufferedImage bi = new BufferedImage(image.getWidth(null), image.getHeight(null), BufferedImage.TYPE_INT_RGB);
    		return true;
    	}catch( Exception e ){
    		return false;
    	}
    }
    
    /**
     * 拍照
     * @param dirPath : 存放图片的基础目录，可为空
     * @params picName : 图片名称
     */
    public void capture( String picBaseDir, String picName ){
        Image image = getCurrentImage();
        
        Date now = new Date();
        String dr1 = new SimpleDateFormat("yyyy/MM/dd").format( now );
        String fullPicPath = ( ( picBaseDir == null || "".equals( picBaseDir ) )? PIC_BASE_DIR : picBaseDir ) +  dr1;//图片所在目录的完整路径，包括基础目录和年月日分级目录
        new File( fullPicPath ).mkdirs();
        //picName = new SimpleDateFormat("yyyyMMddHHmmssSSS").format( now );
        String picPath = fullPicPath + "/" + picName + ".jpg";
        
        BufferedImage bi = new BufferedImage(image.getWidth(null), image.getHeight(null), BufferedImage.TYPE_INT_RGB);
        Graphics2D g2 = bi.createGraphics();
        g2.drawImage(image, null, null);
        FileOutputStream fos = null;
        try {
            fos = new FileOutputStream( picPath );

        } catch (FileNotFoundException e) {
            e.printStackTrace();
        }
        JPEGImageEncoder je = JPEGCodec.createJPEGEncoder(fos);
        JPEGEncodeParam jp = je.getDefaultJPEGEncodeParam(bi);
        jp.setQuality(0.5f, false);
        je.setJPEGEncodeParam(jp);
        try {
            je.encode(bi);
            fos.close();
        } catch (ImageFormatException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
    
    public static void main(String[] args) {
    	CameraHandler cameraHandler = CameraHandler.getInstance();
    	cameraHandler.openCamera();
    }
}
