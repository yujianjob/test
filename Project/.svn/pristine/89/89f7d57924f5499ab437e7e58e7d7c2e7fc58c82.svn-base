package com.landaojia.washclothes.laundry.client.printer;

import static com.landaojia.washclothes.laundry.client.printer.NativeExecutor.executeFunction;

import java.io.UnsupportedEncodingException;

import org.xvolks.jnative.exceptions.NativeException;
import org.xvolks.jnative.pointers.Pointer;

/**
 * 
 * @author gray
 *
 */
public class PPLAPrinter {

	private static final String PRINTER_DLL = "Winppla.dll";

	/**
	 * 加载dll文件
	 */
	static {
		//System.load(PPLAPrinter.class.getResource("/ext_bin/win64/WinPort.dll").getPath());
		//System.load(PPLAPrinter.class.getResource("/ext_bin/win64/Winppla.dll").getPath());
	}

	public static void init() {
		executeFunction(PRINTER_DLL, "A_GetUSBBufferLen", null);
		executeFunction(PRINTER_DLL, "A_EnumUSB", new Object[] { new String(new byte[128]) });// 取得USB埠资料
		/**
		 * 0 -> print to file.<br>
		 * 1 -> lpt1, <br>
		 * 2 -> lpt2, <br>
		 * 3 -> lpt3<br>
		 * 4 -> com1, <br>
		 * 5 -> com2, <br>
		 * 6 -> com3<br>
		 * 10 -> pipe, <br>
		 * 11 -> USBXXX, <br>
		 * 12 -> USB<br>
		 * 13 -> LAN Client(TCP/IP)
		 */
		executeFunction(PRINTER_DLL, "A_CreateUSBPort", new Object[] { 1 });// 开启PPLA库

		executeFunction(PRINTER_DLL, "A_Clear_Memory", null);
		executeFunction(PRINTER_DLL, "A_Set_DebugDialog", new Object[] { 0 });
		executeFunction(PRINTER_DLL, "A_Set_Unit", new Object[] { 'n' });
		executeFunction(PRINTER_DLL, "A_Set_Syssetting", new Object[] { 2, 1, 100, 1, 2 });
		executeFunction(PRINTER_DLL, "A_Set_Darkness", new Object[] { 20 });
		executeFunction(PRINTER_DLL, "A_Del_Graphic", new Object[] { 1, "*" });
		executeFunction(PRINTER_DLL, "A_Set_Sensor_Mode", new Object[] { 'c', 100 });
		executeFunction(PRINTER_DLL, "A_Set_Cutting", new Object[] { 1 });
		executeFunction(PRINTER_DLL, "A_Set_LabelVer", new Object[] { 400 });
	}

	public static void close() {
		executeFunction(PRINTER_DLL, "A_Print_Out", new Object[] { 1, 1, 1, 1 });
		executeFunction(PRINTER_DLL, "A_ClosePrn", null);
	}

	public static void printText(int x, int y, int size, String msg) throws UnsupportedEncodingException, NativeException {
		byte[] mb = msg.getBytes("gb2312");
		Pointer p = Pointer.createPointer(mb.length);
		p.setMemory(mb);
		executeFunction(PRINTER_DLL, "A_Prn_Text_TrueType", new Object[] { x, y, size, "宋体", 1, 400, 0, 0, 0, "AA", p, 1 });
	}

	public static void printBar(int x, int y, String barCode) {
		executeFunction(PRINTER_DLL, "A_Prn_Barcode", new Object[] { x, y, 1, 'E', 3, 20, 50, 'B', 1, barCode });
	}
}