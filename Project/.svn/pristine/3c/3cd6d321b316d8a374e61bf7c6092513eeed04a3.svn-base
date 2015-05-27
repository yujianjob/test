/*
 * Barebones implementation of displaying camera preview.
 * 
 * Created by lisah0 on 2012-02-24
 */
package com.dtr.zbar.scan;

import java.io.IOException;

import android.content.Context;
import android.hardware.Camera;
import android.hardware.Camera.AutoFocusCallback;
import android.hardware.Camera.PreviewCallback;
import android.util.Log;
import android.view.SurfaceHolder;
import android.view.SurfaceView;

/** A basic Camera preview class */
public class CameraPreview extends SurfaceView implements SurfaceHolder.Callback {
	private SurfaceHolder mHolder;
	//这个不用多说，我们要调用相机，所以必需有一个相机的实例  
	private Camera mCamera; 
	//这个方法是用来让相机自动对焦的，具体的<strong>实现</strong>过程在CameraTestActivity中，稍后做介绍 
	private PreviewCallback previewCallback; 
	//这个回调方法就是<strong>解码</strong>程序与相机捕捉到的画面之间的接口，也是在CameraTestActivity中具体<strong>实现</strong> 
	private AutoFocusCallback autoFocusCallback;

	//这是CameraPreview这个类的构造方法，初始化所有需要参数
	@SuppressWarnings("deprecation")
	public CameraPreview(Context context, Camera camera, PreviewCallback previewCb, AutoFocusCallback autoFocusCb) {
		super(context);
		mCamera = camera;
		previewCallback = previewCb;
		autoFocusCallback = autoFocusCb;
		//相机，PreviewCallback和AutoFocusCallback这三个实例都是在CameraTestActivity中建立的，然后传给CameraPreview
		
		
		//这段代码是在API level 9以后<strong>实现</strong>的持续对焦功能，在这里我们不需要，直接使用AutoFocusCallback就行了
		
		/*
		 * Set camera to continuous focus if supported, otherwise use software
		 * auto-focus. Only works for API level >=9.
		 */
		/*
		 * Camera.Parameters parameters = camera.getParameters(); for (String f
		 * : parameters.getSupportedFocusModes()) { if (f ==
		 * Parameters.FOCUS_MODE_CONTINUOUS_PICTURE) {
		 * mCamera.setFocusMode(Parameters.FOCUS_MODE_CONTINUOUS_PICTURE);
		 * autoFocusCallback = null; break; } }
		 */

		// Install a SurfaceHolder.Callback so we get notified when the
		// underlying surface is created and destroyed.
		mHolder = getHolder();
		mHolder.addCallback(this);

		// deprecated setting, but required on Android versions prior to 3.0
		//这句是必需加的，但在3.0以后已经被淘汰了，不过为了兼容3.0以前的版本，还是得加上，不加会报错
		mHolder.setType(SurfaceHolder.SURFACE_TYPE_PUSH_BUFFERS);
	}

	public void surfaceCreated(SurfaceHolder holder) {
		// The Surface has been created, now tell the camera where to draw the
		// preview.
		try {
			//当相机启动是，把捕捉到的画面显示到SurfaceHolder所控制的SurfaceView上，注意try,catch  
			mCamera.setPreviewDisplay(holder);
		} catch (IOException e) {
			Log.d("DBG", "Error setting camera preview: " + e.getMessage());
		}
	}

	//在相机关闭时，应该释放相机，不过此应用中已经在CameraTestActivity的onPause()方法释放，所以在这里不做任何操作
	public void surfaceDestroyed(SurfaceHolder holder) {
		// Camera preview released in activity
	}

	//当我们<strong>手机</strong>发生旋转等改变时，就会启动surfaceChanged这个方法，在这个方法中，
	//我们要先判断一下用于显示的SurfaceView是否还存在，如果不存在，则直接返回，  
	//如果存在，那么先停止相机捕捉画面，改变mCamera的相应设置，再重新启动画面捕捉  
	public void surfaceChanged(SurfaceHolder holder, int format, int width, int height) {
		/*
		 * If your preview can change or rotate, take care of those events here.
		 * Make sure to stop the preview before resizing or reformatting it.
		 */
		////判断SurfaceView是否为空  
		if (mHolder.getSurface() == null) {
			// preview surface does not exist
			return;
		}

		// stop preview before making changes
		try {
			mCamera.stopPreview();//停止相机捕捉画面  
		} catch (Exception e) {
			// ignore: tried to stop a non-existent preview
		}

		try {
			// Hard code camera surface rotation 90 degs to match Activity view
			// in portrait
			mCamera.setDisplayOrientation(90);//屏幕发生旋转，则我们相应的改变显示的角度  

			mCamera.setPreviewDisplay(mHolder);//重新为Camera添加SurfaceHolder
			mCamera.setPreviewCallback(previewCallback);//添加捕画面的回调方法 
			mCamera.startPreview();//重新启动相机捕捉画面  
			mCamera.autoFocus(autoFocusCallback);//添加自动对焦方法  
		} catch (Exception e) {
			Log.d("DBG", "Error starting camera preview: " + e.getMessage());
		}
	}
}
