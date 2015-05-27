package com.landaojia.washclothes.userappserver.common.cache;

import java.util.concurrent.ExecutionException;
import java.util.concurrent.TimeUnit;

import com.google.common.cache.CacheBuilder;
import com.google.common.cache.CacheLoader;
import com.google.common.cache.LoadingCache;
import com.google.common.cache.RemovalListener;
import com.google.common.cache.RemovalNotification;
import com.landaojia.washclothes.userappserver.common.logger.Logger;
import com.landaojia.washclothes.userappserver.common.logger.LoggerManager;


/**
 * 简单对象内存缓存
 * 用于缓存唯一一个对象
 * @author liuxi
 */
public abstract class SimpleMemoryCache<T> {
	
	private Logger logger = LoggerManager.getLogger( this.getClass() );
	
	private LoadingCache<String, T> cache;
	
	/**
	 * 构造
	 * @param expireSeconds : 过期时间，秒数
	 */
	public SimpleMemoryCache( long expireSeconds ){
		cache = CacheBuilder.newBuilder().initialCapacity( 1 )
		.maximumSize( 1 )
		.concurrencyLevel( 1 )
		.expireAfterWrite( expireSeconds, TimeUnit.SECONDS )
		.removalListener(
			new RemovalListener<Object, Object>() {
	            public void onRemoval(RemovalNotification<Object, Object> notification) {
	            	//logger.info( "MemoryCache<T>" + notification.getKey() + " was removed, cause is " + notification.getCause());
	            }
	        }	
		)
		.build(
			new CacheLoader<String, T>(){
				public T load(String key) throws Exception{
					return loadObject();
				}
			}
		);
	}
	
	/**
	 * 载入对象
	 */
	public abstract T loadObject();
	
	/**
	 * 获取
	 */
	public T get(){
		try {
			return cache.get( "1" );
		} catch (ExecutionException e) {
			logger.error( "MemoryCache<T>, get() error", e ); 
			return null;
		}
	}
}
