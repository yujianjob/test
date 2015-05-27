package com.landaojia.washclothes.laundry.dao.washproduct;

import java.math.BigDecimal;

/**
 * 洗衣产品价格对象
 * @author liuxi
 */
public class WashProductPriceVo {
	/**
	 * 大类id
	 */
	private Long clId;

	/**
	 * 大类名称
	 */
	private String clName;

	/**
	 * 小类id
	 */
	private Long caId;
	
	/**
	 * 小类名称
	 */
	private String caName;
	
	/**
	 * 产品id
	 */
	private Long prId;
	
	/**
	 * 产品名称
	 */
	private String prName;
	
	/**
	 * 产品里类型
	 */
	private Integer prType;

	/**
	 * 市场价
	 */
	private BigDecimal prMarketPrice;
	
	/**
	 * 售价
	 */
	private BigDecimal prSalesPrice;

	public Long getClId() {
		return clId;
	}

	public void setClId(Long clId) {
		this.clId = clId;
	}

	public String getClName() {
		return clName;
	}

	public void setClName(String clName) {
		this.clName = clName;
	}

	public Long getCaId() {
		return caId;
	}

	public void setCaId(Long caId) {
		this.caId = caId;
	}

	public String getCaName() {
		return caName;
	}

	public void setCaName(String caName) {
		this.caName = caName;
	}

	public Long getPrId() {
		return prId;
	}

	public void setPrId(Long prId) {
		this.prId = prId;
	}

	public String getPrName() {
		return prName;
	}

	public void setPrName(String prName) {
		this.prName = prName;
	}

	public Integer getPrType() {
		return prType;
	}

	public void setPrType(Integer prType) {
		this.prType = prType;
	}

	public BigDecimal getPrSalesPrice() {
		return prSalesPrice;
	}

	public void setPrSalesPrice(BigDecimal prSalesPrice) {
		this.prSalesPrice = prSalesPrice;
	}

	public BigDecimal getPrMarketPrice() {
		return prMarketPrice;
	}

	public void setPrMarketPrice(BigDecimal prMarketPrice) {
		this.prMarketPrice = prMarketPrice;
	}
}
