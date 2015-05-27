package com.dtr.zbar.scan;

import java.io.IOException;
import java.lang.reflect.Field;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.Service;
import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.graphics.Rect;
import android.hardware.Camera;
import android.hardware.Camera.AutoFocusCallback;
import android.hardware.Camera.PreviewCallback;
import android.hardware.Camera.Size;
import android.media.Ringtone;
import android.media.RingtoneManager;
import android.net.Uri;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.os.Vibrator;
import android.text.TextUtils;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.animation.Animation;
import android.view.animation.TranslateAnimation;
import android.widget.Button;
import android.widget.FrameLayout;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.TextView;

import com.dtr.zbar.build.ZBarDecoder;

public class CaptureActivity extends Activity {

	private Camera mCamera;//创建一个Camera实例  
	private CameraPreview mPreview;//这个类就是我们刚才用来捕捉和显示画面的类
	private Handler autoFocusHandler;//添加一个Handler是为了一会儿我们创建一个线程来单独控制相机的自动对焦  
	private CameraManager mCameraManager;

//	private String SCAN_RESULT;
//	private String SCAN_RESULT_FORMAT;
	
	private Intent intentResult = null; 
	
	private TextView scanResult;
	private Button scanRestart;//布局中的文本视力和按键
	private RelativeLayout scanContainer;
	private FrameLayout scanPreview;
	private RelativeLayout scanCropView;//这个类就是ZBar提供给我们的扫描二维码并<strong>解码</strong>的主要接口  
	//private ImageView scanLine;  

	private Rect mCropRect = null;
	private boolean barcodeScanned = false; //一个标志变量，用来指示是否获取到了二维码  
	private boolean previewing = true; //一个标志变量，指示相机是否在捕捉画面，用来控制自动对焦线程的启动和停止  

	
	
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		setContentView(R.layout.activity_capture);
		//设置<strong>手机</strong>为竖屏显示  
		setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);
		
		findViewById();
		addEvents();
		initViews();
	}

	private void findViewById() {
		scanPreview = (FrameLayout) findViewById(R.id.capture_preview);
		scanResult = (TextView) findViewById(R.id.capture_scan_result);
		scanRestart = (Button) findViewById(R.id.capture_restart_scan);
		scanContainer = (RelativeLayout) findViewById(R.id.capture_container);
		scanCropView = (RelativeLayout) findViewById(R.id.capture_crop_view);
		//scanLine = (ImageView) findViewById(R.id.capture_scan_line);
	}

	private void addEvents() {
		scanRestart.setOnClickListener(new OnClickListener() {
			public void onClick(View v) {
				//判断是否取得了<strong>解码</strong><strong>结果</strong>,若没有,则不出处;若有,则重启扫描程序  
				if (barcodeScanned) {
					barcodeScanned = false;
					scanResult.setText("Scanning...");
					//重新添加PreviewCallback回调方法  
					mCamera.setPreviewCallback(previewCb);
					//启动画面捕捉 
					mCamera.startPreview();
					previewing = true;
					//启动自动对焦  
					mCamera.autoFocus(autoFocusCB);
				}
			}
		});
	}

	private void initViews() {
		autoFocusHandler = new Handler();
		mCameraManager = new CameraManager(this);
		try {
			mCameraManager.openDriver();
		} catch (IOException e) {
			e.printStackTrace();
		}

		mCamera = mCameraManager.getCamera();
		mPreview = new CameraPreview(this, mCamera, previewCb, autoFocusCB);
		scanPreview.addView(mPreview);

//		TranslateAnimation animation = new TranslateAnimation(Animation.RELATIVE_TO_PARENT, 0.0f, Animation.RELATIVE_TO_PARENT, 0.0f, Animation.RELATIVE_TO_PARENT, 0.0f, Animation.RELATIVE_TO_PARENT,
//				0.85f);
//		animation.setDuration(3000);
//		animation.setRepeatCount(-1);
//		animation.setRepeatMode(Animation.REVERSE);
		//scanLine.startAnimation(animation);
	}

	public void onPause() {
		super.onPause();
		releaseCamera();
	}

	private void releaseCamera() {
		if (mCamera != null) {
			previewing = false;
			mCamera.setPreviewCallback(null);
			mCamera.release();
			mCamera = null;
		}
	}

	//Runnable子线程
	private Runnable doAutoFocus = new Runnable() {
		public void run() {
			//判断扫描标志位,如果在扫描中,则启动自动对焦   
			if (previewing)
				mCamera.autoFocus(autoFocusCB);
		}
	};

	//PreviewCallback能够时时的取得相机捕捉到的画面复本,我们也就是利用这个复本来进行<strong>解码</strong>的  
	PreviewCallback previewCb = new PreviewCallback() {
		//这个方法中的byte[] data参数就是相机捕捉到的画面转换成的byte数组   
		public void onPreviewFrame(byte[] data, Camera camera) {
			//获取相机参数实例   //通过这个实例取得相机的尺寸(宽和高)
			Size size = camera.getParameters().getPreviewSize();

			// 这里需要将获取的data翻转一下，因为相机默认拿的的横屏的数据
			byte[] rotatedData = new byte[data.length];
			for (int y = 0; y < size.height; y++) {
				for (int x = 0; x < size.width; x++)
					rotatedData[x * size.height + size.height - y - 1] = data[x + y * size.width];
			}

			// 宽高也要调整
			int tmp = size.width;
			size.width = size.height;
			size.height = tmp;

			initCrop();
			
			ZBarDecoder zBarDecoder = new ZBarDecoder();
			String result = zBarDecoder.decodeCrop(rotatedData, size.width, size.height, mCropRect.left, mCropRect.top, mCropRect.width(), mCropRect.height());

			if (!TextUtils.isEmpty(result)) {
				previewing = false;
				mCamera.setPreviewCallback(null);
				mCamera.stopPreview();
				
				
				scanResult.setText("结果: " + result);
				barcodeScanned = true;
				
				intentResult = new Intent();  
				intentResult.putExtra("SCAN_RESULT", result);
				intentResult.putExtra("SCAN_RESULT_FORMAT", "000000");
				setResult(RESULT_OK, intentResult);
				
//				Uri notification = RingtoneManager.getDefaultUri(RingtoneManager.URI_COLUMN_INDEX);
//				Ringtone r = RingtoneManager.getRingtone(getApplicationContext(), notification);
//				r.play(); 
				try {
					Vibrator vib = (Vibrator)CaptureActivity.this.getSystemService(Service.VIBRATOR_SERVICE);   
		            vib.vibrate(100);  
				} catch (Exception e) {
					
				}
				finish();
				
//				new AlertDialog.Builder(CaptureActivity.this)   
//				.setTitle("确认")  
//				.setMessage(result)
//				.setPositiveButton("是", null)
//				.show();
				
				
			}
		}
	};

	
	// Mimic continuous auto-focusing
	//要<strong>实现</strong>相机自动对焦,必需建立一个AutoFocusCallback实例,并重写onAutoFoucs方法,根据自己的需要设定自动对焦时间 
	AutoFocusCallback autoFocusCB = new AutoFocusCallback() {
		public void onAutoFocus(boolean success, Camera camera) {
			//doAutoFoucs是一个Runnable的对象,利用Handler的postDelayed()方法,每隔一秒启动一次自动对焦子线程  
			autoFocusHandler.postDelayed(doAutoFocus, 1000);
		}
	};

	/**
	 * 初始化截取的矩形区域
	 */
	private void initCrop() {
		int cameraWidth = mCameraManager.getCameraResolution().y;
		int cameraHeight = mCameraManager.getCameraResolution().x;

		/** 获取布局中扫描框的位置信息 */
		int[] location = new int[2];
		scanCropView.getLocationInWindow(location);

		int cropLeft = location[0];
		int cropTop = location[1] - getStatusBarHeight();

		int cropWidth = scanCropView.getWidth();
		int cropHeight = scanCropView.getHeight();

		/** 获取布局容器的宽高 */
		int containerWidth = scanContainer.getWidth();
		int containerHeight = scanContainer.getHeight();

		/** 计算最终截取的矩形的左上角顶点x坐标 */
		int x = cropLeft * cameraWidth / containerWidth;
		/** 计算最终截取的矩形的左上角顶点y坐标 */
		int y = cropTop * cameraHeight / containerHeight;

		/** 计算最终截取的矩形的宽度 */
		int width = cropWidth * cameraWidth / containerWidth;
		/** 计算最终截取的矩形的高度 */
		int height = cropHeight * cameraHeight / containerHeight;

		/** 生成最终的截取的矩形 */
		mCropRect = new Rect(x, y, width + x, height + y);
	}

	private int getStatusBarHeight() {
		try {
			Class<?> c = Class.forName("com.android.internal.R$dimen");
			Object obj = c.newInstance();
			Field field = c.getField("status_bar_height");
			int x = Integer.parseInt(field.get(obj).toString());
			return getResources().getDimensionPixelSize(x);
		} catch (Exception e) {
			e.printStackTrace();
		}
		return 0;
	}
}
