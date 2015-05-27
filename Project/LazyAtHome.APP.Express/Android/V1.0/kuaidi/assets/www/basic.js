//全局常量值
//var host="http://api.landaojia.com/webapi/ExpressMobile";
var host="http://ts99.vicp.net:85/ExpressMobile";
var actid="expapp";
var version="1.0.0";
var Key="lz123456789012345678901234567890";

function setItem(key,val){
	window.localStorage.setItem(key,val);
};
function getItem(key){
	return window.localStorage.getItem(key);
}
function setTs(str){
	var str=str.toString();
	if(str.length<2){
		str="0"+str;
	}
	return str;
}
//获取系统当前时间
function getTs(){
	var CurrDate = new Date();
	var year=CurrDate.getFullYear(); //获取完整的年份(4位,1970-????)
	var month=setTs(CurrDate.getMonth()+1); //获取当前月份(0-11,0代表1月)
	var day=setTs(CurrDate.getDate()); //获取当前日(1-31)
	var h=setTs(CurrDate.getHours()); //获取当前小时数(0-23)
	var m=setTs(CurrDate.getMinutes()); //获取当前分钟数(0-59)
	var s=setTs(CurrDate.getSeconds()); //获取当前秒数(0-59)
	var dateString=""+year+month+day+h+m+s;
	return dateString;
}
//提示弹框
function showPopup(str) {
	$("#afui").popup(str);
}




