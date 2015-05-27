package com.landaojia.washclothes.laundry.dao;

import java.io.Serializable;

import org.hibernate.SessionFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;

import com.landaojia.washclothes.laundry.common.dao.HibernateBaseDao;

/**
 * 懒到家hibernate基础dao
 * @author liuxi
 */
public class LazyHomeDbHibernateBaseDao<T, I extends Serializable> extends HibernateBaseDao<T, I>{
	@Autowired(required = false)
	public void setSessionFactoryBean(@Qualifier("hibernateSessionFactory")SessionFactory sessionFactory) {
		super.setSessionFactory(sessionFactory);
	}
}
