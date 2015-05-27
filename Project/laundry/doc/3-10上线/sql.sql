创建洗涤条码表

CREATE TABLE wash_code (
  id int(18) NOT NULL AUTO_INCREMENT COMMENT 'id',
  PRIMARY KEY (id)
) ENGINE=InnoDB AUTO_INCREMENT=100000001 DEFAULT CHARSET=utf8 COMMENT='水洗条码'

id:调整为1000000001

System.load(PPLAPrinter.class.getResource("/ext_bin/win32/WinPort.dll").getPath());
System.load(PPLAPrinter.class.getResource("/ext_bin/win32/Winppla.dll").getPath());