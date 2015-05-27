package com.landaojia.washclothes.userappserver.common.paginquery;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;

import org.springframework.jdbc.core.JdbcTemplate;

/**
 * 简单分页查询执行器
 * @author liuxi
 */
public class PagingQueryExcutor {
	
	public static PagingQueryResult<Map<String, Object>> pagingQuery( HttpServletRequest request, PagingQueryDesc desc ){
//		PagingQueryResult<Map<String, Object>> pqr = new PagingQueryResult<Map<String, Object>>();
//		
//		String pageNoStr = request.getParameter( "pageNo" );
//		int pageNo = 1;
//		if( pageNoStr != null && !"".equals( pageNoStr ) ){
//			pageNo = Integer.parseInt( pageNoStr );
//		}
//		
//		String pageSizeStr = request.getParameter( "pageSize" );
//		int pageSize = 1;
//		if( pageSizeStr != null && !"".equals( pageSizeStr ) ){
//			pageSize = Integer.parseInt( pageSizeStr );
//		}
//		
//		//todo...翻页的2参数没法统一起来
//		
//		JdbcTemplate jt = new JdbcTemplate( desc.getDataSource() );
//		int count = jt.queryForInt( desc.getSqlCount(), desc.getArgs() );
//		if( count == 0 ){
//			pqr.setDataList( new ArrayList<Map<String, Object>>( 0 ) );
//		}else{
//			Integer maxPageNo =  (count / pqr.getPageSize()) + (count % pqr.getPageSize() > 0 ? 1 : 0);
//			if( pqr.getPageNo() > maxPageNo ){
//				pqr.setPageNo( maxPageNo );
//			}
//			List<Map<String, Object>> list = jt.queryForList( desc.getSqlDataList(), desc.getArgs() );
//			pqr.setDataList( list );
//		}
//		pqr.setPagination( count, pageSize, pageNo );
		
		return null;
		
		
		
//		Map<String, String[]> paramsMap = request.getParameterMap();
//		
//		StringBuilder sb = new StringBuilder();
//		sb.append( " select * from user_info where 1=1 " );
//		
//		if(  )
	}
	
	/**
	 * 将所有的请求参数转换
	 * @deprecated
	 */
	private static Map<String, String[]> makeParams( HttpServletRequest request ){
		Map<String, String[]> map = request.getParameterMap();

		return map;
	}
	
}
