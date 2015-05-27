package com.landaojia.washclothes.laundry.service.washcode;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Propagation;
import org.springframework.transaction.annotation.Transactional;

import com.landaojia.washclothes.laundry.common.util.TextUtil;
import com.landaojia.washclothes.laundry.dao.washcode.WashCodeDao;
import com.landaojia.washclothes.laundry.dao.washcode.WashCodeEo;

/**
 * 洗涤条码
 * @author liuxi
 */
@Service
public class WashCodeService {
	
	@Autowired
	private WashCodeDao washCodeDao;
	
	/**
	 * 生成水洗条码
	 */
	@Transactional(propagation=Propagation.NOT_SUPPORTED )
	public String generateWashCode(){
		WashCodeEo eo = new WashCodeEo();
		washCodeDao.save( eo );
		String code = TextUtil.lPadForLen(eo.getId().toString(), '0', 10);
		//washCodeDao.delete( eo );
		return code;
	}
}
