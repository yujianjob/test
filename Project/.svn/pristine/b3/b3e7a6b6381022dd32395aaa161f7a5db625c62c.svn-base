/*
       Licensed to the Apache Software Foundation (ASF) under one
       or more contributor license agreements.  See the NOTICE file
       distributed with this work for additional information
       regarding copyright ownership.  The ASF licenses this file
       to you under the Apache License, Version 2.0 (the
       "License"); you may not use this file except in compliance
       with the License.  You may obtain a copy of the License at

         http://www.apache.org/licenses/LICENSE-2.0

       Unless required by applicable law or agreed to in writing,
       software distributed under the License is distributed on an
       "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
       KIND, either express or implied.  See the License for the
       specific language governing permissions and limitations
       under the License.
 */

package com.example.kuaidi;

import android.os.Bundle;

import org.apache.cordova.*;

import android.view.KeyEvent;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View.OnClickListener;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileOutputStream;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.LinkedHashSet;
import java.util.Set;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.DefaultHttpClient;
import org.json.JSONArray;
import org.json.JSONObject;

//import com.example.jpushdemo.ExampleUtil;
//import com.example.jpushdemo.MainActivity.MessageReceiver;







import cn.jpush.android.api.JPushInterface;
import android.app.Activity;
import android.app.AlertDialog;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.pm.PackageManager.NameNotFoundException;
import android.net.Uri;
import android.os.Environment;
import android.os.Handler;
import android.os.Message;
import android.util.Log;

public class MainActivity extends DroidGap {
	public static int vcode;
	String newVerName = "";// 新版本名称
	int newVerCode = 3;// 新版本号
	String downUrl = "";
	ProgressDialog pd = null;
	final String UPDATE_SERVERAPK = "ApkUpdateAndroid.apk";
	
	private Handler mHandler = new Handler();

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		// Set by <content src="index.html" /> in config.xml
		// super.loadUrl(Config.getStartUrl());
		super.setIntegerProperty("splashscreen", R.drawable.splash_screen);
		super.loadUrl("file:///android_asset/www/index.html", 2000);

		Intent intent = new Intent(this, MainActivity.class);
        intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_SINGLE_TOP);
        
		//updateVersion();
        
		// JSNotify js = new JSNotify();
		// // this.appView.addJavascriptInterface(js, "aa");
		// this.appView.addJavascriptInterface(new Object(){
		// public void android_exec()
		// {
		// System.out.println("aaaaa");
		// }
		// }, "test");
		// this.loadUrl("file:///android_asset/www/kd/index.html");

		//初始化 JPush
		JPushinit();
		
	}

	// 初始化 JPush。如果已经初始化，但没有登录成功，则执行重新登录。
	private void JPushinit(){
		try {
	        JPushInterface.setDebugMode(false);
	        JPushInterface.init(this);
	        Set<String> tagSet = new LinkedHashSet<String>();
	        JPushInterface.setTags(this.getApplicationContext(),tagSet, null);
        } catch (Exception e) {
			// TODO Auto-generated catch block
			Log.e("JPush异常", e.getMessage());
		}
	}
		
	public void updateVersion() {
		final int verCode = this.getVerCode(this);
		vcode = verCode;
		getServerVer();
	}
	
	/* 获得版本号 */
	public int getVerCode(Context context) {
		int verCode = -1;
		try {
			String packName = context.getPackageName();
			verCode = context.getPackageManager().getPackageInfo(packName, 0).versionCode;
		} catch (NameNotFoundException e) {
			// TODO Auto-generated catch block
			Log.e("检查更新","版本名称获取异常" + e.getMessage());
		}
		return verCode;
	}

	/* 获得版本名称 */
	public String getVerName(Context context) {
		String verName = "";
		try {
			String packName = context.getPackageName();
			verName = context.getPackageManager().getPackageInfo(packName, 0).versionName;
		} catch (NameNotFoundException e) {
			Log.e("检查更新","版本名称获取异常" + e.getMessage());
		}
		return verName;
	}

	/* 从服务器端获得版本号 */
	public void getServerVer() {
		new Thread(new Runnable() {
			public void run() {
				try {
					System.out.println("start");
					URL url = new URL("http://api.landaojia.com/webapi/ExpressMobile/Version");
					HttpURLConnection httpConnection = (HttpURLConnection) url
							.openConnection();

					httpConnection.setDoInput(true);
					httpConnection.setDoOutput(true);
					httpConnection.setRequestMethod("GET");
					httpConnection.setUseCaches(false);
					System.out.println("connect");

					httpConnection.connect();

					System.out.println("connect over");
					// InputStreamReader reader = new
					// InputStreamReader(httpConnection.getInputStream());
					// BufferedReader bReader = new BufferedReader(reader);

					BufferedReader reader = new BufferedReader(new InputStreamReader(
							httpConnection.getInputStream()));
					String json = reader.readLine();
					reader.close();

					httpConnection.disconnect();

//					alert(json);
					// JSONArray array = new JSONArray(json);
					// JSONObject jsonObj = array.getJSONObject(0);
					JSONObject jsonObject = new JSONObject(json);
					newVerCode = jsonObject.getInt("versioncode");
					newVerName = jsonObject.getString("version");
					downUrl = jsonObject.getString("url");
					//System.out.println("value:" + newVerCode);
					//System.out.println("value:" + downUrl);
					mHandler.post(new Runnable() {
						public void run() {
							if (newVerCode != vcode) {
								doNewVersionUpdate();// 更新版本
							} else {
								//notNewVersionUpdate();
							}
						}
					});
					// newVerName = jsonObj.getString("verName");
				} catch (Exception e) {
					System.out.println("error:" + e.toString());
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
			}
		}).start();
	}

	public static boolean hasSDCard() {
		return Environment.MEDIA_MOUNTED.equals(Environment
				.getExternalStorageState());
	}
	
	private void alert(final String msg) {
    	 Dialog dialog = new AlertDialog.Builder(this).setTitle("软件更新").setMessage(msg)
 				.setPositiveButton("确定", null).create();
 		dialog.show();
	}

	/* 不更新版本 */
	public void notNewVersionUpdate() {
		if(hasSDCard() == false)
		{
			alert("SD卡不存在,无法更新程序");
			return;
		}
		
		int verCode = this.getVerCode(this);
		String verName = this.getVerName(this);
		StringBuffer sb = new StringBuffer();
		sb.append("当前版本：");
		sb.append(verName);
		sb.append(" Code:");
		sb.append(verCode);
		sb.append("\n已是最新版本，无需更新");
		Dialog dialog = new AlertDialog.Builder(this).setTitle("软件更新")
				.setMessage(sb.toString())
				.setPositiveButton("确定", new DialogInterface.OnClickListener() {

					@Override
					public void onClick(DialogInterface dialog, int which) {
						// TODO Auto-generated method stub
						finish();
					}
				}).create();
		dialog.show();
	}

	/* 更新版本 */
	public void doNewVersionUpdate() {
		//int verCode = this.getVerCode(this);
		String verName = this.getVerName(this);
		StringBuffer sb = new StringBuffer();
		sb.append("当前版本：");
		sb.append(verName);
		// sb.append(" Code:");
		// sb.append(verCode);
		sb.append(",发现版本：");
		sb.append(newVerName);
		// sb.append(" Code:");
		// sb.append(newVerCode);
		sb.append(",是否更新");
		Dialog dialog = new AlertDialog.Builder(MainActivity.this)
				.setTitle("软件更新")
				.setMessage(sb.toString())
				.setPositiveButton("更新", new DialogInterface.OnClickListener() {

					@Override
					public void onClick(DialogInterface dialog, int which) {
						// TODO Auto-generated method stub
						pd = new ProgressDialog(MainActivity.this);
						pd.setTitle("正在下载");
						pd.setMessage("请稍后");
						pd.setProgressStyle(ProgressDialog.STYLE_SPINNER);
						downFile(downUrl);
					}
				})
				.setNegativeButton("暂不更新",
						new DialogInterface.OnClickListener() {

							@Override
							public void onClick(DialogInterface dialog,
									int which) {
								// TODO Auto-generated method stub
								// finish();
							}
						}).create();
		// 显示更新框
		dialog.show();
	}

	/* 下载apk */
	public void downFile(final String url) {
		
		final Handler mHandler = new Handler();
		
		pd.show();
		new Thread() {
			public void run() {
				HttpClient client = new DefaultHttpClient();
				HttpGet get = new HttpGet(url);
				HttpResponse response;
				try {
					response = client.execute(get);
					HttpEntity entity = response.getEntity();
					long length = entity.getContentLength();
					InputStream is = entity.getContent();
					FileOutputStream fileOutputStream = null;
					if (is != null) {
						File file = new File(
								Environment.getExternalStorageDirectory(),
								UPDATE_SERVERAPK);
						fileOutputStream = new FileOutputStream(file);
						byte[] b = new byte[1024];
						int charb = -1;
						int count = 0;
						while ((charb = is.read(b)) != -1) {
							fileOutputStream.write(b, 0, charb);
							count += charb;
						}
					}
					fileOutputStream.flush();
					if (fileOutputStream != null) {
						fileOutputStream.close();
					}
					down();
				} catch (final Exception e) {
					 mHandler.post(new Runnable(){
			             public void run(){
			            	 Log.e("检查更新", "下载失败" + e.getMessage());
			             }
			             });
					 
					 pd.cancel();
					 
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
			}
		}.start();
	}

	Handler handler = new Handler() {
		@Override
		public void handleMessage(Message msg) {
			super.handleMessage(msg);
			pd.cancel();
			update();
		}
	};

	/* 下载完成，通过handler将下载对话框取消 */
	public void down() {
		new Thread() {
			public void run() {
				Message message = handler.obtainMessage();
				handler.sendMessage(message);
			}
		}.start();
	}

	/* 安装应用 */
	public void update() {
		Intent intent = new Intent(Intent.ACTION_VIEW);
		intent.setDataAndType(Uri.fromFile(new File(Environment
				.getExternalStorageDirectory(), UPDATE_SERVERAPK)),
				"application/vnd.android.package-archive");
		startActivity(intent);
	}
	
	/* 防止按返回键关闭程序 */
	@Override  
	 public boolean onKeyDown(int keyCode, KeyEvent event) {  
	  if (keyCode == KeyEvent.KEYCODE_BACK) {  
	   moveTaskToBack(false);
	   return true;  
	  }  
	  return super.onKeyDown(keyCode, event);  
	 }
	
	@Override
	protected void onResume() {
		try{

			updateVersion();
			
		}catch(Exception e){

			Log.e("检查更新", e.getMessage());
		}
        super.onResume();
        JPushInterface.onResume(this);
    }
	
	@Override
    protected void onPause() {
        super.onPause();
        JPushInterface.onPause(this);
    }
	
}
