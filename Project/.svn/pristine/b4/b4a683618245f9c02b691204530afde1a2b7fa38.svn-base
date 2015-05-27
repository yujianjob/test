package com.landaojia.washclothes.laundry.common.dao;

import java.io.Serializable;
import java.lang.reflect.ParameterizedType;
import java.util.Collection;
import java.util.List;

import org.hibernate.LockMode;
import org.hibernate.Query;
import org.hibernate.SQLQuery;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.springframework.orm.hibernate3.HibernateCallback;
import org.springframework.orm.hibernate3.support.HibernateDaoSupport;

/**
 * hibernate基础dao
 * @author liuxi
 */
@SuppressWarnings("rawtypes")
public abstract class HibernateBaseDao<T, I extends Serializable> extends HibernateDaoSupport{
	protected Class<T> clazz;
	
	/**
	 * @param sessionFactory 需要注入的sessionFactory
	 */
	public abstract void setSessionFactoryBean( SessionFactory sessionFactory);
	
	@SuppressWarnings( "unchecked" )
	public HibernateBaseDao(){
		this.clazz = (Class<T>) ((ParameterizedType)this.getClass().getGenericSuperclass()).getActualTypeArguments()[0];
	}
	
	public Class<T> getClazz() {
		return clazz;
	}

	public void save( T t ){
		this.getHibernateTemplate().save( t );
	}
	
	public void saveAll( Collection<T> c ){
		this.getHibernateTemplate().saveOrUpdateAll( c );
	}
	
	public void delete( T t ){
		this.getHibernateTemplate().delete( t );
	}
	
	public void delete( T t, LockMode lockMode ){
		this.getHibernateTemplate().delete( t, lockMode );
	}
	
	public void deleteAll( Collection<T> c ){
		this.getHibernateTemplate().deleteAll( c );
	}
	
	public void update( T t ){
		this.getHibernateTemplate().update( t );
	}
	
	public void update( T t, LockMode lockMode ){
		this.getHibernateTemplate().update( t, lockMode );
	}
	
	public void updateAll( Collection<T> c ){
		this.getHibernateTemplate().saveOrUpdateAll( c );
	}
	
	public void saveOrUpdate( T t ){
		this.getHibernateTemplate().saveOrUpdate( t );
	}
	
	public void saveOrUpdateAll( Collection<T> c ){
		this.getHibernateTemplate().saveOrUpdateAll( c );
	}
	
	public T get( I id ){
		return this.getHibernateTemplate().get( this.clazz, id );
	}
	
	public T get( I id, LockMode lockMode ){
		return this.getHibernateTemplate().get( this.clazz, id, lockMode );
	}

	public T getByProperties( String[] ps, Object[] vs, String orderBy ){
		List<T> list = findByProperties( ps, vs, orderBy );
		return list.size() == 0 ? null : list.get( 0 );
	}
	
	public Object getObject( String hql, Object...params ){
		List list = find( hql, params );
		return list.size() > 0? list.get( 0 ) : null;
	}
	
	public List find( String hql ){
		List list = this.getHibernateTemplate().find( hql );
		return list;
	}
	
	public List find( String hql, Object...params ){
		List list = this.getHibernateTemplate().find( hql, params );
		return list;
	}

	public List findBySql( String sql, Object...params ){
		Session session = this.getSession();
		SQLQuery sqlQuery = session.createSQLQuery( sql );
		if( params != null && params.length > 0 ){
			for( int i = 0; i < params.length; i ++ ){
				sqlQuery.setParameter( i, params[i] );
			}
		}
		List<?> list = sqlQuery.list();
		session.close();
		return list;
	}
	
	/**
	 * 根据属性查询对象列表
	 */
	public List<T> findByProperties( String[] ps, Object[] vs, String orderBy ){
		StringBuilder str0 = new StringBuilder();
		for( int i = 0; i < ps.length; i ++ ){
			if( i != 0 ){
				str0.append( "and " );
			}
			str0.append( ps[i] ).append( "=? " );
		}
		
		List<T> list = this.getHibernateTemplate().find( "from " + clazz.getName() + ( ps.length > 0? " where " : "" ) + str0 + ( orderBy == null? "" : " order by " + orderBy ) , vs );
		return list;
	}
	
	/**
	 * 根据属性查询条数
	 */
	public Integer countByProperties( String[] ps, Object[] vs ){
		StringBuilder str0 = new StringBuilder();
		for( int i = 0; i < ps.length; i ++ ){
			if( i != 0 ){
				str0.append( "and " );
			}
			str0.append( ps[i] ).append( "=? " );
		}
		
		List<T> list = this.getHibernateTemplate().find( "select count(*) from " + clazz.getName() + ( ps.length > 0? " where " : "" ) + str0, vs );
		return Integer.valueOf( String.valueOf( list.get( 0 ) ) );
	}
	
	public List findOfPage( final String hql, final Integer pageNo, final Integer pageSize, final Object...params ){
		List list = this.getHibernateTemplate().executeFind( new HibernateCallback(){
			public Object doInHibernate (Session session){
				Query query = session.createQuery ( hql );
				query.setFirstResult ( pageNo * pageSize - pageSize);
				query.setMaxResults ( pageSize );
				if( params != null ){
					for( int i = 0; i < params.length; i ++ ){
						query.setParameter( i, params[i] );
					}
				}
				List list = query.list ();
				return list;
			}
		} );
		return list;
	}

	/**
	 * get
	 * load
	 * find
	 * @param t
	 * @param lockMode
	 * @deprecated
	 */
	public void test( T t, LockMode lockMode ){
		Session session = this.getSession();
		
		Query query = session.createQuery( "" );
		query.executeUpdate();
		query.list();
		
		SQLQuery sqlQuery = session.createSQLQuery( "" );
		sqlQuery.executeUpdate();
		sqlQuery.list();
		
		session.close();
	}
}
