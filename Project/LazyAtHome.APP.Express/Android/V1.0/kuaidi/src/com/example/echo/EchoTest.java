package com.example.echo;
import org.apache.cordova.api.CallbackContext;
import org.apache.cordova.api.CordovaPlugin;
import org.apache.cordova.api.PluginResult;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import android.view.Menu; 
import android.view.MenuItem;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileOutputStream;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.DefaultHttpClient;
import org.json.JSONArray;
import org.json.JSONObject;

import com.example.kuaidi.MainActivity;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.pm.PackageManager.NameNotFoundException;
import android.net.Uri;
import android.os.Environment;
import android.os.Handler;
import android.os.Message;
import android.util.Log;

public class EchoTest extends CordovaPlugin {
	String versioncode = "";

    final String UPDATE_SERVERAPK = "ApkUpdateAndroid.apk";

	ProgressDialog pd = null;
    
    @Override
    public boolean execute(String action, JSONArray args, CallbackContext callbackContext) throws JSONException {
        if (action.equals("echo")) {   //action=echo 
        	String str = "";
            String url = args.getString(0);  //  
            versioncode = args.getString(1);
             
            String code= ""+MainActivity.vcode;
            str=str+url+"-----"+versioncode+"---"+code+"-----"+"����ԭ������777777";
            
            //if(versioncode!=code){
            MainActivity ins = new MainActivity();
            //ins.doNewVersionUpdate(MainActivity.vcode,versioncode);
            	//doNewVersionUpdate();
            //}
            
            this.echo(str, callbackContext);
            return true;
        }else{
        	 	callbackContext.error("�ⲻ��һ��echo����");
        	 	return false;
        }
        
    }
    /*��ð汾��*/
    public int getVerCode(Context context){
        int verCode = -1;
        try {
         String packName = context.getPackageName();
            verCode = context.getPackageManager().getPackageInfo(packName, 0).versionCode;
        } catch (NameNotFoundException e) {
            // TODO Auto-generated catch block
            Log.e("�汾�Ż�ȡ�쳣", e.getMessage());
        }
        return verCode;
    }
    /*���°汾*/
    public void doNewVersionUpdate(){
    	//MainActivity.this.doNewVersionUpdate(MainActivity.vcode, versioncode);
        int verCode = MainActivity.vcode;
        StringBuffer sb = new StringBuffer();
        sb.append("��ǰ�汾��");
        sb.append(verCode);
        sb.append(",���ְ汾��");
        sb.append(versioncode);
        sb.append(",�Ƿ����");
        AlertDialog dialog = new AlertDialog.Builder(null)   
		.setTitle("ȷ��")  
		.setMessage(sb.toString())
		.setPositiveButton("��", new DialogInterface.OnClickListener() {
            
            @Override
            public void onClick(DialogInterface dialog, int which) {
                // TODO Auto-generated method stub
                //finish();
            }
        }).create();
		 
		
        //Dialog dialog = new AlertDialog.Builder(this)
        //.setTitle("��������")
        //.setMessage(sb.toString())
        //.setPositiveButton("����", new DialogInterface.OnClickListener() {
            
//            @Override
//            public void onClick(DialogInterface dialog, int which) {
//                // TODO Auto-generated method stub
//                pd = new ProgressDialog(MainActivity.this);
//                pd.setTitle("��������");
//                pd.setMessage("���Ժ�");
//                pd.setProgressStyle(ProgressDialog.STYLE_SPINNER);
//                downFile("http://newsyue.8866.org:85/base/Exp_android_download");
//            }
//        })
//        .setNegativeButton("�ݲ�����", new DialogInterface.OnClickListener() {
//            
//            @Override
//            public void onClick(DialogInterface dialog, int which) {
//                // TODO Auto-generated method stub
//                //finish();
//            }
//        }).create();
        //��ʾ���¿�
        dialog.show();
    }
    
    public void downFile(final String url){
        pd.show();
        new Thread(){
            public void run(){
                HttpClient client = new DefaultHttpClient();
                HttpGet get = new HttpGet(url);
                HttpResponse response;
                try {
                    response = client.execute(get);
                    HttpEntity entity = response.getEntity();
                    long length = entity.getContentLength();
                    InputStream is =  entity.getContent();
                    FileOutputStream fileOutputStream = null;
                    if(is != null){
                        File file = new File(Environment.getExternalStorageDirectory(),UPDATE_SERVERAPK);
                        fileOutputStream = new FileOutputStream(file);
                        byte[] b = new byte[1024];
                        int charb = -1;
                        int count = 0;
                        while((charb = is.read(b))!=-1){
                            fileOutputStream.write(b, 0, charb);
                            count += charb;
                        }
                    }
                    fileOutputStream.flush();
                    if(fileOutputStream!=null){
                        fileOutputStream.close();
                    }
                    down();
                }  catch (Exception e) {
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
    
    /*������ɣ�ͨ��handler�����ضԻ���ȡ��*/
    public void down(){
        new Thread(){
            public void run(){
                Message message = handler.obtainMessage();
                handler.sendMessage(message);
            }
        }.start();
    }
    
    /*��װӦ��*/
    public void update(){
        Intent intent = new Intent(Intent.ACTION_VIEW);
        intent.setDataAndType(Uri.fromFile(new File(Environment.getExternalStorageDirectory(),UPDATE_SERVERAPK))
                , "application/vnd.android.package-archive");
        MainActivity ins = new MainActivity();
        ins.startActivity(intent);
    }

    
    private void echo(String message, CallbackContext callbackContext) {
        if (message != null && message.length() > 0) {
            callbackContext.success(message);
        } else {
            callbackContext.error("Expected one non-empty string argument.");
        }
    }
}