package com.landaojia.washclothes.laundry.client.test.print;

import java.io.UnsupportedEncodingException;

import org.xvolks.jnative.exceptions.NativeException;

import com.landaojia.washclothes.laundry.client.printer.PPLAPrinter;

/**
 * @author codingwoo <long1795@gmail.com>
 */
public class PPLAPrinterTest {
	public static void main(String[] args) throws UnsupportedEncodingException, NativeException {
		PPLAPrinter.init();
		PPLAPrinter.printBar(20, 10, "33333333");
		PPLAPrinter.printText(50, 10, 40, "中文msg");
		PPLAPrinter.close();
	}
}
