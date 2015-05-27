package com.landaojia.washclothes.userappserver.service.sms.yimei;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import javax.xml.namespace.QName;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

import com.landaojia.washclothes.userappserver.common.exception.CommonException;
import com.landaojia.washclothes.userappserver.common.exception.CommonExceptionCode;
import com.landaojia.washclothes.userappserver.common.logger.Logger;
import com.landaojia.washclothes.userappserver.common.logger.LoggerManager;
import com.landaojia.washclothes.userappserver.common.util.DateUtil;
import com.landaojia.washclothes.userappserver.service.sms.yimei.ws.SDKClient;
import com.landaojia.washclothes.userappserver.service.sms.yimei.ws.SDKService;

/**
 * 短信服务(亿美)
 * @author liuxi
 */
@Service
public class SmsYiMeiService {
	
	private Logger logger = LoggerManager.getLogger( this.getClass() );
	
	/**
	 * 发送开关，测试时关闭
	 */
	@Value( "${smsYiMeiService.isSend}" )
	private String isSend;
	
	/**
	 * sn,软件序列号
	 */
	@Value( "${smsYiMeiService.sn}" )
	private String sn;
	
	/**
	 * 密码（6位）
	 */
	@Value( "${smsYiMeiService.pwd}" )
	private String pwd;
	
	/**
	 * key,用户自定义key值， 长度不超过15个字符字，和软件序列号注册时的关键字保持一致
	 */
	@Value( "${smsYiMeiService.key}" )
	private String key;
	
	
	/**
	 * 企业名称
	 */
	@Value( "${smsYiMeiService.enterpriseName}" )
	private String enterpriseName;
	
	/**
	 * 联系人姓名
	 */
	@Value( "${smsYiMeiService.contactMan}" )
	private String contactMan;
	
	/**
	 * 联系人电话
	 */
	@Value( "${smsYiMeiService.telephoneNo}" )
	private String telephoneNo;
	
	/**
	 * 联系人手机
	 */
	@Value( "${smsYiMeiService.mpNo}" )
	private String mpNo;
	
	/**
	 * 电子邮件
	 */
	@Value( "${smsYiMeiService.email}" )
	private String email;
	
	/**
	 * 传真
	 */
	@Value( "${smsYiMeiService.fax}" )
	private String fax;
	
	/**
	 * (公司)地址
	 */
	@Value( "${smsYiMeiService.address}" )
	private String address;
	
	/**
	 * (公司)邮编
	 */
	@Value( "${smsYiMeiService.zipCode}" )
	private String zipCode;
	
	
    private SDKClient port;
	
    public SmsYiMeiService(){
    	QName qName = new QName( "http://sdkhttp.eucp.b2m.cn/", "SDKService" );
    	SDKService s = new SDKService( SDKService.WSDL_LOCATION, qName);
    	port = s.getSDKService(); 
    }
    
    /**
     * 注册
     */
    public void register(){
    	int result = port.registEx( sn, key, pwd);
    	logger.info( "register, result:" + result );
    }
    
    /**
     * 注册
     */
    public void registDetailInfo(){
        int result = port.registDetailInfo(sn, key, enterpriseName, contactMan, telephoneNo, mpNo, email, fax, address, zipCode );
        logger.info( "registDetailInfo, result:" + result );
    }
    
	/**
	 * 发送短信
	 * @param mpNo : 手机号
	 * @param content : 内容
	 * @throws Exception 
	 */
	public void sendSms( String mpNo, String content ) {
        
        String sendTimeStr = DateUtil.dateToStr( new Date(), "yyyyMMddHHmmss" );
        List<String> mpNoList = new ArrayList<String>(1);
        mpNoList.add( mpNo );
        
        //----参数的意义--------------
        //sn,软件序列号
        //key,用户自定义key值， 长度不超过15个字符字，和软件序列号注册时的关键字保持一致
        //定时短信的定时时间，格式为:年年年年月月日日时时分分秒秒
        //手机号码(字符串数组,最多为200个手机号码)
        //短信内容(最多500个汉字或1000个纯英文
        //扩展号码 (长度小于15的字符串) 用户可通过附加码自定义短信类别 扩展号码的功能，需另外申请，当未申请扩展号码功能时，该参数默认为空值即可。
        //GBK
        //短信等级，范围1~5，数值越高优先级越高
        //短信ID，自定义唯一的消息ID，数字位数最大19位，与状态报告ID一一对应，需用户自定义ID规则确保ID的唯一性。如果smsID为0将获取不到相应的状态报告信息。
        Integer result = 0;
        if( "1".equals( isSend ) ){
        	result = port.sendSMS(sn, key, sendTimeStr, mpNoList, content, null, "GBK", 3, 0);
        }
        logger.info( "sendSms, mpNo:" + mpNo + ", content:" + content + ", result:" + result );
        if( result != 0 ){
        	throw new CommonException( CommonExceptionCode.S000003 );
        }
        
//        101:网络故障
//        110:短信内容为空或超长
//        998:超时(长时间没有得到响应消息
//        -1:系统异常
//        -104:请求超过限制
//        -117:发送短信失败
//        -9001:序列号格式错误
//        -9016:发送短信包大小超出范围
//        -9020:发送短信手机号格式错误
//        -9019:发送短信优先级格式错误
//        -9022:发送短信唯一序列值错误
//        -9017:发送短信内容格式错误
	}
	
	
	
}
