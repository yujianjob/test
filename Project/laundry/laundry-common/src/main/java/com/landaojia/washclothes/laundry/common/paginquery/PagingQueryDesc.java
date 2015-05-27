package com.landaojia.washclothes.laundry.common.paginquery;

import javax.sql.DataSource;

/**
 * 分页查询的描述
 * @author liuxi
 */
public interface PagingQueryDesc {
	
	DataSource getDataSource();
	
	String getSqlCount();
	
	String getSqlDataList();
	
	Object[] getArgs();
}
