package com.landaojia.washclothes.laundry.client.test.print;

import com.landaojia.washclothes.laundry.client.printer.CommonPrinter;
import com.landaojia.washclothes.laundry.client.printer.ExpressBill;

/**
 * 
 * @author gray
 *
 */
public class CommonPrinterTest {
	public static void main(String[] args) {
		ExpressBill b = new ExpressBill();
		b.setAmount("9.9");
		b.setBillNo("bill_no123");
		b.setCourier("派件员A");
		b.setLineNo("G_001");
		b.setMemo1("备注1");
		b.setMemo2("备注2");
		b.setOrderNo("1234567890");
		b.setPhoneNo("13500010001");
		b.setRecipients("张三");
		b.setRecipientsAddr1("张三地址1");
		b.setRecipientsAddr2("张三地址2");
		b.setStationAddr1("站点地址1");
		b.setStationAddr2("站点地址2");
		b.setStationNo("31_024");
		CommonPrinter.print(b, false);
	}
}
