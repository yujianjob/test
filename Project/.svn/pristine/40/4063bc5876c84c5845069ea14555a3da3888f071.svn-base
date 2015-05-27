package com.landaojia.washclothes.userappserver.service.useraddress;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Propagation;
import org.springframework.transaction.annotation.Transactional;

import com.landaojia.washclothes.userappserver.common.exception.CommonException;
import com.landaojia.washclothes.userappserver.common.exception.CommonExceptionCode;
import com.landaojia.washclothes.userappserver.dao.userconsigneeaddress.UserConsigneeAddressDao;
import com.landaojia.washclothes.userappserver.dao.userconsigneeaddress.UserConsigneeAddressEo;

/**
 * 用户地址
 * @author liuxi
 */
@Service
public class UserAddressService {
	
	@Autowired
	private UserConsigneeAddressDao userConsigneeAddressDao;
	
	/**
	 * 获取用户地址全部
	 */
	public List<UserAddressRo> getUserAddressAll( String userId ){
		List<UserConsigneeAddressEo> list = userConsigneeAddressDao.findByProperties( new String[]{ "userId", "objStatus" }, new Object[]{ userId, 1 }, "id asc" );
		List<UserAddressRo> roList = new ArrayList<UserAddressRo>( list.size() );
		int dftIndex = -1;
		for( int i = 0; i < list.size(); i ++ ){
			UserConsigneeAddressEo eo = list.get( i );
			UserAddressRo ro = fillUserAddressRo( eo );
			ro.setDefaultFlag( false );
			if( eo.getIsDefault() ){
				if( dftIndex == -1 ){
					dftIndex = i;
				}
			}
			roList.add( ro );
		}
		
		if( dftIndex > -1 ){
			roList.get( dftIndex ).setDefaultFlag( true );
		}else{
			if( roList.size() > 0 ){
				roList.get( roList.size() - 1 ).setDefaultFlag( true );
			}
		}
		return roList;
	} 
	
	/**
	 * 添加地址
	 */
	@Transactional(propagation=Propagation.REQUIRED )
	public UserAddressRo addUserAddress( EditUserAddressPo po ){
		Date now = new Date();
		UserConsigneeAddressEo eo = new UserConsigneeAddressEo();
		eo.setUserId( po.getUserId() );
		eo.setConsignee( po.getConsigneeName() );
		eo.setDistrictId( 0L );
		eo.setAddress( po.getAddress() );
		eo.setMpNo( po.getConsigneeMpNo() );
		eo.setPhone( null );
		eo.setIsDefault( po.getDefaultFlag() );
		eo.setZipCode( null );
		eo.setEmail( null ); 
		eo.setObjStatus( 1 );
		eo.setObjRemark( null );
		eo.setObjCdate( now );
		eo.setObjCuser( 0L );
		eo.setObjMdate( now );
		eo.setObjMuser( 0L );
		userConsigneeAddressDao.save( eo );
		
		//----如果是默认地址，修改其他默认地址为非默认----
		//TODO...
		
		UserAddressRo ro = fillUserAddressRo( eo );
		return ro;
	}
	
	/**
	 * 修改地址
	 */
	@Transactional(propagation=Propagation.REQUIRED )
	public UserAddressRo updateUserAddress( EditUserAddressPo po ){
		UserConsigneeAddressEo eo = userConsigneeAddressDao.get( po.getId() );
		if( eo == null ){
			throw new CommonException( CommonExceptionCode.S000008 );			
		}
		
		if( !eo.getUserId().equals( po.getUserId() ) ){
			throw new CommonException( CommonExceptionCode.S000009 );
		}
		eo.setConsignee( po.getConsigneeName() );
		eo.setAddress( po.getAddress() );
		eo.setMpNo( po.getConsigneeMpNo() );
		eo.setIsDefault( po.getDefaultFlag() );
		userConsigneeAddressDao.update( eo );
		
		//----如果是默认地址，修改其他默认地址为非默认----
		//TODO...
		
		UserAddressRo ro = fillUserAddressRo( eo );
		return ro;
	}
	
	/**
	 * 删除地址
	 */
	@Transactional(propagation=Propagation.REQUIRED )
	public UserAddressRo deleteUserAddress( EditUserAddressPo po ){
		UserConsigneeAddressEo eo = userConsigneeAddressDao.getByProperties( new String[]{ "id", "objStatus" }, new Object[]{ po.getId(), 1 }, null);
		if( eo == null ){
			throw new CommonException( CommonExceptionCode.S000010 );			
		}
		
		if( !eo.getUserId().equals( po.getUserId() ) ){
			throw new CommonException( CommonExceptionCode.S000011 );
		}
		eo.setObjStatus( 6 );
		userConsigneeAddressDao.update( eo );
		
		//----如果是默认地址，修改其他默认地址为非默认----
		//TODO...
		
		UserAddressRo ro = fillUserAddressRo( eo );
		ro.setDefaultFlag( false );
		return ro;
	}
	
	/**
	 * 设置地址为默认地址
	 */
	@Transactional(propagation=Propagation.REQUIRED )
	public UserAddressRo setUserAddressDefault( EditUserAddressPo po ){
		UserConsigneeAddressEo eo = userConsigneeAddressDao.getByProperties( new String[]{ "id", "objStatus" }, new Object[]{ po.getId(), 1 }, null);
		if( eo == null ){
			throw new CommonException( CommonExceptionCode.S000012 );
		}
		
		if( !eo.getUserId().equals( po.getUserId() ) ){
			throw new CommonException( CommonExceptionCode.S000013 );
		}
		eo.setIsDefault( true );
		userConsigneeAddressDao.update( eo );
		
		//----如果是默认地址，修改其他默认地址为非默认----
		//TODO...
		
		UserAddressRo ro = fillUserAddressRo( eo );
		return ro;
	}
	
	/**
	 * 获取用户的默认地址
	 */
	@Transactional(propagation=Propagation.REQUIRED )
	public UserAddressRo getDefaultUserAddress( EditUserAddressPo po ){
		UserConsigneeAddressEo eo = userConsigneeAddressDao.getByProperties( new String[]{ "userId", "isDefault", "objStatus" }, new Object[]{ po.getUserId(), true, 1 }, "id asc");
		if( eo == null ){
			eo = userConsigneeAddressDao.getByProperties( new String[]{ "userId", "objStatus" }, new Object[]{ po.getUserId(), 1 }, "id asc" );
		}
		
		if( eo == null ){
			return null;
		}
		
		UserAddressRo ro = fillUserAddressRo( eo );
		return ro;
	}
	
	/**
	 * 填充UserAddressRo
	 */
	private UserAddressRo fillUserAddressRo( UserConsigneeAddressEo eo ){
		UserAddressRo ro = new UserAddressRo();
		ro.setId( eo.getId() );
		ro.setUserId( eo.getUserId() );
		ro.setConsigneeName( eo.getConsignee() );
		ro.setConsigneeMpNo( eo.getMpNo() );
		ro.setAddress( eo.getAddress() );
		ro.setDefaultFlag( eo.getIsDefault() );
		return ro;
	}
}
