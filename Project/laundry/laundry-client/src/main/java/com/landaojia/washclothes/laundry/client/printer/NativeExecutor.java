package com.landaojia.washclothes.laundry.client.printer;

import org.xvolks.jnative.JNative;
import org.xvolks.jnative.Type;
import org.xvolks.jnative.exceptions.NativeException;
import org.xvolks.jnative.pointers.Pointer;

/**
 * 
 * @author gray
 *
 */
public class NativeExecutor {
	private NativeExecutor() {

	}

	/**
	 * 获取dll的指令
	 * 
	 * @param functionName
	 *            指令名称
	 * @return
	 * @throws NativeException
	 */
	public static JNative getJNativeByFunction(String dll, String functionName) throws NativeException {
		return new JNative(dll, functionName);
	}

	/**
	 * 执行指令
	 * 
	 * @param functionName
	 * @param params
	 *            参数集合
	 * @return
	 */
	public static int executeFunction(String dll, String functionName, Object[] params) {
		JNative func = null;
		JNative.setLoggingEnabled(true);
		try {
			func = getJNativeByFunction(dll, functionName);
			func.setRetVal(Type.INT);
			if (params != null && params.length > 0) {
				for (int i = 0; i < params.length; i++) {
					if (params[i] instanceof String)
						func.setParameter(i, (String) params[i]);
					else if (params[i] instanceof Integer)
						func.setParameter(i, ((Integer) params[i]).intValue());
					else if (params[i] instanceof Pointer)
						func.setParameter(i, (Pointer) params[i]);
					else if (params[i] instanceof Character)
						func.setParameter(i, Type.PSTRUCT, (String) params[i].toString());
					else if (params[i] instanceof Boolean)
						func.setParameter(i, Type.INT, params[i].toString());
					else if (params[i] instanceof byte[]) {
						System.out.println("bte..");
						func.setParameter(i, Type.PSTRUCT, (byte[]) params[i]);
					}
				}
			}
			func.invoke();
			System.out.println(functionName + "  ************--> " + func.getRetValAsInt());
			return func.getRetValAsInt();
		} catch (Exception e) {
			throw new RuntimeException(e);
		} finally {
			if (func != null) {
				// func.dispose();
			}
		}
	}

}
