package com.landaojia.washclothes.userappserver.dao.orderseq;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;

/**
 * 订单流水
 * @author liuxi
 */
@Entity
@Table(name = "order_seq")
public class OrderSeqEo {
	
	/**
	 * id
	 */
	private Long id;

	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	@Column(name = "id")
	public Long getId() {
		return id;
	}

	public void setId(Long id) {
		this.id = id;
	}
}
