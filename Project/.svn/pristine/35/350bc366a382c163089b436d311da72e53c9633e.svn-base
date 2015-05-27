package com.landaojia.washclothes.userappserver.common.paginquery;

import java.util.ArrayList;
import java.util.List;

/**
 * 分页查询结果
 * @author liuxi
 */
public class PagingQueryResult<DataType> {
	/**
	 * 总行数
	 */
	private int rowCount;

	/**
	 * 总页数
	 */
	private int pageCount;

	/**
	 * 每页的行数
	 */
	private int pageSize;

	/**
	 * 当前页数
	 */
	private int pageNo;

	/**
	 * 数据列表
	 */
	private List<DataType> dataList = new ArrayList<DataType>();

	
	public PagingQueryResult( List<DataType> dataList, int pageSize, int pageNo, int rowCount ){
		this.dataList = dataList;
		this.rowCount = rowCount;
		this.pageSize = pageSize;
		this.pageNo = pageNo;
		if (rowCount == 0) {
			pageCount = 1;
		} else {
			pageCount = (rowCount / pageSize) + (rowCount % pageSize > 0 ? 1 : 0);
		}
	}

	public int getRowCount() {
		return rowCount;
	}

	public int getPageCount() {
		return pageCount;
	}

	public int getPageSize() {
		return pageSize;
	}

	public int getPageNo() {
		return pageNo;
	}

	public List<DataType> getDataList() {
		return dataList;
	}
}
