package com.landaojia.washclothes.userappserver.service.register;

import java.math.BigDecimal;
import java.util.Date;
import java.util.UUID;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Propagation;
import org.springframework.transaction.annotation.Transactional;

import com.landaojia.washclothes.userappserver.common.exception.CommonException;
import com.landaojia.washclothes.userappserver.common.exception.CommonExceptionCode;
import com.landaojia.washclothes.userappserver.common.logger.Logger;
import com.landaojia.washclothes.userappserver.common.logger.LoggerManager;
import com.landaojia.washclothes.userappserver.dao.userinfo.UserInfoDao;
import com.landaojia.washclothes.userappserver.dao.userinfo.UserInfoEo;

/**
 * 注册
 * @author liuxi
 */
@Service
public class RegisterService {
	
	private Logger logger = LoggerManager.getLogger( this.getClass() );
	
	@Autowired
	private UserInfoDao userInfoDao;
	
	/**
	 * 注册
	 */
	@Transactional(propagation=Propagation.REQUIRED )
	public RegisterRo registerUser( RegisterPo po ){
		logger.info( "registerUser,注册用户,mpNo={},checkCode={},channel={}", po.getMpNo(), po.getCheckCode(), po.getChannel());
		
		//----检验验证码------
		
		
		RegisterRo ro = new RegisterRo();
		
		//检验手机号是否存在...
		UserInfoEo oldUiEo = userInfoDao.getByProperties( new String[]{ "mpNo" }, new Object[]{ po.getMpNo() }, null );
		if( oldUiEo != null ){
			ro.setUserId( oldUiEo.getId() );
			return ro;
			//throw new CommonException( CommonExceptionCode.S000004, po.getMpNo() );
		}
		
		UserInfoEo uiEo = new UserInfoEo();
		UUID uuid = UUID.randomUUID();
		Date now = new Date();
		
		uiEo.setId( uuid.toString().toUpperCase() );
		uiEo.setSeedId( 999999L );
		uiEo.setLoginName( po.getMpNo() );
		uiEo.setMpNo( po.getMpNo() );
		uiEo.setEmail( null );
		uiEo.setLoginPassword( po.getMpNo() );
		uiEo.setPayPassword( null );
		uiEo.setType( 1 );
		uiEo.setLevel( 1 );
		uiEo.setSite( null );
		uiEo.setLastLoginTime( now );
		uiEo.setGrowth( 0 );
		uiEo.setMoney( BigDecimal.ZERO );
		uiEo.setScore( 0L );
		uiEo.setAccountStatus( 1 );
		uiEo.setUserStatus( 1 );
		uiEo.setRegistSource( po.getChannel() );
		uiEo.setRecommendedCode( Long.toHexString( new Long( po.getMpNo() ) ) ); //todo...
		uiEo.setObjStatus( 1 );
		uiEo.setObjRemark( null );
		uiEo.setObjCdate( now );
		uiEo.setObjCuser( 0L );
		uiEo.setObjMdate( now );
		uiEo.setObjMuser( 0L );
		uiEo.setOrderCount( 0 );
		uiEo.setLatitude( null );
		uiEo.setLongitude( null );
		uiEo.setNodeId( null );
		userInfoDao.save( uiEo );
		
		//----TODO其他表...
		
		
		ro.setUserId( uiEo.getId() );
		return ro;
	}
}
