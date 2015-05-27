package com.landaojia.washclothes.laundry.dao.washproduct;

import java.math.BigDecimal;
import java.util.ArrayList;
import java.util.List;

import org.springframework.stereotype.Repository;

import com.landaojia.washclothes.laundry.dao.LazyHomeDbHibernateBaseDao;

/**
 * 洗衣产品
 * @author liuxi
 */
@Repository
public class WashProductDao extends LazyHomeDbHibernateBaseDao<WashProductEo, Long>{
	
	/**
	 * 查询出所有备选的洗衣产品对象
	 */
	public List<WashProductPriceVo> queryAllWashProductPriceVOList(){
		String hql = 
		"SELECT cl.id AS clId, cl.name AS clName, ca.id AS caId, ca.name AS caName,  " +
		"pr.id AS prId, pr.name AS prName, pr.type AS prType, pr.marketPrice as prMarketPrice, pr.salesPrice AS prSalesPrice " +
		"FROM WashClassEo cl, WashCategoryEo ca, WashProductEo pr " +
		"WHERE cl.id=ca.classId " +
		"AND ca.id=pr.categoryId " +
		"AND cl.parentId <> 0 " +
		"AND pr.objStatus=1 " +
		"ORDER BY cl.id, ca.id, pr.id ";
		List<?> list = this.find( hql );

		List<WashProductPriceVo> voList = new ArrayList<WashProductPriceVo>( list.size() );
		for( Object o : list ){
			Object[] objs = ( Object[] ) o;
			WashProductPriceVo vo = new WashProductPriceVo();
			vo.setClId( objs[0] == null? null : (Long)objs[0] );
			vo.setClName( objs[1] == null? null : (String)objs[1] );
			vo.setCaId( objs[2] == null? null : (Long)objs[2] );
			vo.setCaName( objs[3] == null? null : (String)objs[3] );
			vo.setPrId( objs[4] == null? null : (Long)objs[4] );
			vo.setPrName( objs[5] == null? null : (String)objs[5] );
			vo.setPrType( objs[6] == null? null : (Integer)objs[6] );
			vo.setPrMarketPrice( objs[7] == null? null : (BigDecimal)objs[7] );
			vo.setPrSalesPrice( objs[8] == null? null : (BigDecimal)objs[8] );
			voList.add( vo );
		}
		
		return voList;
	}
}
