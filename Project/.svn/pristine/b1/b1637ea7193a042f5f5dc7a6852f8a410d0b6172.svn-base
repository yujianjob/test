package com.landaojia.washclothes.laundry.service.washproduct;

import java.util.ArrayList;
import java.util.List;

import javax.annotation.PostConstruct;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.landaojia.washclothes.laundry.common.cache.SimpleMemoryCache;
import com.landaojia.washclothes.laundry.dao.washproduct.WashClassProductVo;
import com.landaojia.washclothes.laundry.dao.washproduct.WashProductDao;
import com.landaojia.washclothes.laundry.dao.washproduct.WashProductPriceVo;

/**
 * 洗衣产品Service
 * @author liuxi
 */
@Service
public class WashProductService {
	/**
	 * 所有洗衣大类与洗衣产品的缓存
	 */
	private static SimpleMemoryCache<List<WashClassProductVo>> allWashClassProductListCache;
	
	@Autowired
	private WashProductDao washProductDao;

	
	public List<WashClassProductVo> getAllWashClassProductListFromCache(){
		return allWashClassProductListCache.get();
	}
	
	@PostConstruct
	public void init(){
		allWashClassProductListCache = new SimpleMemoryCache<List<WashClassProductVo>>( 3600 ){
			public List<WashClassProductVo> loadObject() {
				List<WashClassProductVo> wcpList = new ArrayList<WashClassProductVo>();
				List<WashProductPriceVo> wppList = washProductDao.queryAllWashProductPriceVOList();
				
				Long tempClId = -1L;
				WashClassProductVo wcp = null;
				for( WashProductPriceVo wpp : wppList ){
					if( !wpp.getClId().equals( tempClId ) ){
						wcp = new WashClassProductVo();
						wcp.setClassId( wpp.getClId() );
						wcp.setClassName( wpp.getClName() );
						wcp.setWashProductPriceVOList( new ArrayList<WashProductPriceVo>() );
						wcpList.add( wcp );
						tempClId = wpp.getClId();
					}
					
					wcp.getWashProductPriceVOList().add( wpp );
				}

				return wcpList;
			}
		};
	}
}
