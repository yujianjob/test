package com.landaojia.washclothes.laundry.common.paginquery;

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
	private int totalRowCount;

	/**
	 * 总页数
	 */
	private int totalPageCount;

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

	/**
	 * 设置分页信息
	 */
	public void setPagination(int totalRowCount, int pageSize, int pageNo) {
		this.totalRowCount = totalRowCount;
		this.pageSize = pageSize;
		this.pageNo = pageNo;
		if (totalRowCount == 0) {
			totalPageCount = 1;
		} else {
			totalPageCount = (totalRowCount / pageSize) + (totalRowCount % pageSize > 0 ? 1 : 0);
		}
	}

	/**
	 * 计算指定页在特定页行数下的第一行行号是多少
	 */
	public static int startRowNum(int pageNum, int pageSize) {
		return (pageNum - 1) * pageSize;
	}

	/**
	 * 是否存在下一页
	 */
	public boolean hasNextPage() {
		return pageNo < totalPageCount;
	}

	/**
	 * 是否存在上一页
	 */
	public boolean hasPrePage() {
		return pageNo > 1;
	}

	/**
	 * 取得下一页页码
	 */
	public int getNextPageNo() {
		return hasNextPage() ? pageNo + 1 : pageNo;
	}

	/**
	 * 取得上一页页码
	 */
	public int getPrePageNo() {
		return hasPrePage() ? pageNo - 1 : pageNo;
	}

	public int getTotalRowCount() {
		return totalRowCount;
	}

	public int getTotalPageCount() {
		return totalPageCount;
	}

	public int getPageSize() {
		return pageSize;
	}

	public int getPageNo() {
		return pageNo;
	}

	public void setPageNo( int pageNo ) {
		this.pageNo = pageNo;
	}
	
	public List<DataType> getDataList() {
		return dataList;
	}

	public DataType getFirstData() {
		if (dataList == null || dataList.size() == 0) {
			return null;
		}

		return dataList.get(0);
	}

	public void setDataList(List<DataType> dataList) {
		this.dataList = dataList;
	}

	public void addData(DataType data) {
		dataList.add(data);
	}
}
