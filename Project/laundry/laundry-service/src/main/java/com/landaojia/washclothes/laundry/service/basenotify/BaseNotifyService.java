package com.landaojia.washclothes.laundry.service.basenotify;

import java.util.Date;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Propagation;
import org.springframework.transaction.annotation.Transactional;

import com.landaojia.washclothes.laundry.common.util.TextUtil;
import com.landaojia.washclothes.laundry.dao.basenotify.BaseNotifyDao;
import com.landaojia.washclothes.laundry.dao.basenotify.BaseNotifyEo;

/**
 * 基本通知
 * @author liuxi
 */
@Service
public class BaseNotifyService {
	
	@Autowired
	private BaseNotifyDao baseNotifyDao;
	
	/**
	 * 保存一条异件上报
	 */
	@Transactional(propagation=Propagation.REQUIRED )
	public void saveBaseNotifyForAbnormalSubmit( AbnormalSubmitPo po ){
		Date now = new Date();
		BaseNotifyEo eo = new BaseNotifyEo();
		//eo.setId();
		eo.setEventNumber( "YZ" + System.currentTimeMillis() );
		eo.setOrderNumber( po.getOrderNumber() );
		eo.setRoleId( 9L );
		eo.setPersonnelId( 3L );
		eo.setClazz( 0 );
		eo.setTitle( po.getType() );
		eo.setContent( po.getRemark() );
		eo.setLevel( 3 );
		eo.setStatus( 0 );
		eo.setResult( null );
		eo.setNotifyUserList( null );
		eo.setIsSms( false );
		eo.setIsEmail( false );
		eo.setObjStatus( 1 );
		eo.setObjRemark( null );
		eo.setObjCdate( now );
		eo.setObjCuser( 0L );
		eo.setObjMdate( now );
		eo.setObjMuser( 20L );
		baseNotifyDao.save( eo );
		
		eo.setEventNumber( "YL" + TextUtil.lPadForLen( "" + eo.getId(), '0', 12 ) );
		baseNotifyDao.update( eo );
	}
}
