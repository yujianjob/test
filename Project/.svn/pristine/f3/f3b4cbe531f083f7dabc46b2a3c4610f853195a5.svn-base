<%--<jsp:forward page="${request.contextPath}/main/toMain.do" />--%>
<%@ page language="java" contentType="text/html; charset=utf-8" pageEncoding="utf-8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>index</title>
    <meta http-equiv=Content-Type content="text/html;charset=utf-8"/>
</head>

<style>
body{font-size:12px;}
.tb0{background:#AAA;font-size:12px;margin-bottom:10px;}
.tb0 tr.ttl td{background:#EEE;text-align:center;font-weight:bold;height:26px;}
.tb0 td{background:#FFF;text-align:left;line-height:18px;padding:2px;}
.tb0 td.lbl{text-align:right;font-weight:bold;color:#1155AA;}
.a0{color:blue;}
.note0{color:green;}
.red{color:red;}
</style>

<script>
function $(id){
	return document.getElementById( id );
}
</script>

<body>
<div style="line-height:28px;text-align:center;font-weight:bold;color:#113366;font-size:14px;">
用户App服务端系统接口说明(v1.2)
</div>

<%
String url = request.getRequestURL().toString();
int port = request.getLocalPort();
String prefix = url.substring( 0, url.indexOf( ":" ) ) + "://" + request.getLocalAddr() + ( port == 80? "" : ":" + request.getLocalPort() ) + request.getContextPath();
String contextPath = request.getContextPath();
%>

<table class="tb0" cellspacing="1" cellpadding="1">
	<tr>
		<td class="lbl">调用地址前缀：</td>
		<td><%=prefix%></td>		
	</tr>
	<tr>
		<td class="lbl">调用地址后缀：</td>
		<td>.do</td>		
	</tr>
	<tr>
		<td class="lbl">请求方式：</td>
		<td>http post</td>		
	</tr>
	<tr>
		<td class="lbl">公共请求参数：</td>
		<td>
			<%--REQSEQ:共32位，手机端生成16位的str1，然后用摘要算法(SHA1)对其算出16位的str2，返回str2+str1--%>
			REQSEQ: <span class="note0">//TODO...</span>
		</td>
	</tr>
	<tr>
		<td class="lbl">统一返回值格式：</td>
		<td>json字符串</td>	
	</tr>
	<tr>
		<td class="lbl">统一返回值对象：</td>
		<td>
			<pre>{
"succFlag":true,  <span class="note0">//成功标志，无系统异常时一律返回true，发生系统异常时一律返回false</span>
"msg":"系统异常xxx", <span class="note0">//当succFlag为true时，为null；当succFlag为false时，表示异常消息</span>
"data": {},       <span class="note0">//当succFlag为true时，表示业务返回值对象(可以是任意类型：单值、对象或数组，也可能为null)；当succFlag为false时，为null</span>
}			</pre>
		</td>	
	</tr>
</table>

<table class="tb0" cellspacing="1" cellpadding="1">
	<tr class="ttl">
		<td>接口描述</td>
		<td>URI</td>
		<td>请求参数</td>
		<td>业务返回值对象(data)</td>
		<td>调用例子</td>		
	</tr>
	
	<tr>
		<td>获取所有洗衣产品(分类、id、名称、价格等)</td>
		<td>/washPrice/getAllWashPriceList</td>
		<td>无</td>
		<td>
			<pre>
数组：
[{"classId":6,  <span class="note0">//大类id</span>
"className":"上装",  <span class="note0">//大类名称</span>
"washProductPriceVOList":[  <span class="note0">//该大类下的洗涤产品列表,数组</span>
	{"caId":1,  <span class="note0">//小类id</span>
	"caName":"西装",  <span class="note0">//小类名称</span>
	"clId":6,  <span class="note0">//大类id</span>
	"clName":"上装",  <span class="note0">//大类名称</span>
	"prId":1,  <span class="note0">//洗涤产品id</span>
	"prMarketPrice":25.00,  <span class="note0">//洗涤产品市场价格</span>
	"prName":"西装",  <span class="note0">//洗涤产品名称</span>
	"prSalesPrice":9.90,  <span class="note0">//洗涤产品销售价格</span>
	"prType":1}  <span class="note0">//洗涤产品类型(1:普通产品 2:活动产品)</span>
	,...]
}
,...]</pre>
		</td>
		<td><a class="a0" href="<%=contextPath%>/washPrice/getAllWashPriceList.do" target="_blank">例子</a></td>
	</tr>
	<tr>
		<td>发送短信-(下单后)验证手机的验证码</td>
		<td>/sms/sendSmForVerifyMobliePhone</td>
		<td>mpNo <span class="note0">//手机号</span></td>
		<td>
			<pre>
字符串：345321 <span class="note0">//返回一个6位的字符串，即发送的验证码</span>
</pre>
		</td>
		<td><a class="a0" href="<%=contextPath%>/sms/sendSmForVerifyMobliePhone.do?mpNo=13524769626" target="_blank">例子</a></td>
	</tr>
	
	
	<tr>
		<td>注册</td>
		<td>/register/registerUser</td>
		<td>
			<pre>
mpNo:13524769626 <span class="note0">//手机号</span>
checkCode:745653 <span class="note0">//(短信)验证码</span>
channel:21 <span class="note0">//app渠道(ios填21，android填22)</span>
			</pre>
		</td>
		<td>
			<pre>
对象：
{
"userId":"06F95B39-448A-402B-B918-6A52766530A4" <span class="note0">//用户id</span>
} 
</pre>
		</td>
		<td><a class="a0" href="<%=contextPath%>/register/registerUser.do?mpNo=13524769626&checkCode=745653&channel=21" target="_blank">例子</a></td>
	</tr>
	
	<tr>
		<td>一键下单</td>
		<td>/order/oneKeyOrder</td>
		<td>
			<pre>
userId:06F95B39-448A-402B-B918-6A52766530A4 <span class="note0">//用户id</span>
userName:刘熹  <span class="note0">//用户姓名或联系人</span>
mpNo:13524769626 <span class="note0">//手机号</span>
address:泰和路2038号D203 <span class="note0">//地址</span>
expectTime:2015-01-29 10:00:00 <span class="note0">//期望收衣时间</span>
userRemark:请尽快收衣 <span class="note0">//用户备注</span>
channel:21 <span class="note0">//下单渠道(ios填21，android填22)</span>
			</pre>
		</td>
		<td>
			<pre>
对象：
{
"orderNumber":"23523523124"<span class="note0">//订单号</span>
"userId":"06F95B39-448A-402B-B918-6A52766530A4" <span class="note0">//用户id</span>
"userName":"刘熹" <span class="note0">//用户姓名或联系人</span>
"mpNo":"13524769626" <span class="note0">//手机号</span>
"address:"泰和路2038号D203" <span class="note0">//地址</span>
"orderTime:"2015-02-03 09:42:33" <span class="note0">//下单收件</span>
"orderStatus:2 <span class="note0">//订单状态</span>
"orderStatusDesc:"订单完成" <span class="note0">//订单状态描述</span>
"orderStep:2 <span class="note0">//订单进程</span>
"orderStepDesc:"已下单" <span class="note0">//订单进程描述</span>
} 
</pre>
		</td>
		<td>
			<a href="#" class="a0" onclick="$('formOneKeyOrder').submit();return false;" >例子</a>
			<form action="<%=contextPath%>/order/oneKeyOrder.do" id="formOneKeyOrder" style="display:none;" target="_blank">
				<input type="hidden" name="userId" value="06F95B39-448A-402B-B918-6A52766530A4" />
				<input type="hidden" name="userName" value="刘熹" />
				<input type="hidden" name="mpNo" value="13524769626" />
				<input type="hidden" name="address" value="泰和路2038号D203" />
				<input type="hidden" name="expectTime" value="2015-01-29 10:00:00" />
				<input type="hidden" name="userRemark" value="请尽快收衣 " />
				<input type="hidden" name="channel" value="21" />
			</form>
		</td>
	</tr>
	<tr>
		<td>取消订单</td>
		<td>/order/cancelOrder</td>
		<td>
			<pre>
userId:06F95B39-448A-402B-B918-6A52766530A4 <span class="note0">//用户id</span>
mpNo:13524769626 <span class="note0">//手机号</span>
orderNumber:23523523124 <span class="note0">//订单号</span>
reasonCode:0001 <span class="note0">//取消原因代码(目前可不传)</span>
reasonDesc:长时间没来收件 <span class="note0">//取消原因</span>
			</pre>
		</td>
		<td>
			<pre>
对象：
{
"orderNumber":"23523523124"<span class="note0">//订单号</span>
"userId":"06F95B39-448A-402B-B918-6A52766530A4" <span class="note0">//用户id</span>
"userName":"刘熹" <span class="note0">//用户姓名或联系人</span>
"mpNo":"13524769626" <span class="note0">//手机号</span>
"address:"泰和路2038号D203" <span class="note0">//地址</span>
"orderTime:"2015-02-03 09:42:33" <span class="note0">//下单收件</span>
"orderStatus:6 <span class="note0">//订单状态</span>
"orderStatusDesc:"已退单" <span class="note0">//订单状态描述</span>
"orderStep:7 <span class="note0">//订单进程</span>
"orderStepDesc:"完成" <span class="note0">//订单进程描述</span>
}
</pre>
		</td>
		<td>
			<a href="#" class="a0" onclick="$('formCancelOrder').submit();return false;" >例子</a>
			<form action="<%=contextPath%>/order/cancelOrder.do" id="formCancelOrder" style="display:none;" target="_blank">
				<input type="hidden" name="userId" value="06F95B39-448A-402B-B918-6A52766530A4" />
				<input type="hidden" name="mpNo" value="13524769626" />
				<input type="hidden" name="orderNumber" value="215031900000007" />
				<input type="hidden" name="reasonCode" value="0001" />
				<input type="hidden" name="reasonDesc" value="长时间没来收件" />
			</form>
		</td>
	</tr>
	
	<tr>
		<td>(分页)查询订单结果集</td>
		<td>/order/pagingQueryOrder</td>
		<td>
			<pre>
userId:06F95B39-448A-402B-B918-6A52766530A4 <span class="note0">//用户id</span>
pageSize:20 <span class="note0">//每页条数</span>
pageNo:1 <span class="note0">//页号</span>
			</pre>
		</td>
		<td>
			<pre>
对象：
{"dataList":[ <span class="note0">//订单数据结果集，数组</span>
	{"orderNumber":"23523523124"<span class="note0">//订单号</span>
	"userId":"06F95B39-448A-402B-B918-6A52766530A4" <span class="note0">//用户id</span>
	"userName":"刘熹" <span class="note0">//用户姓名或联系人</span>
	"mpNo":"13524769626" <span class="note0">//手机号</span>
	"address:"泰和路2038号D203" <span class="note0">//地址</span>
	"orderTime:"2015-02-03 09:42:33" <span class="note0">//下单收件</span>
	"orderStatus:6 <span class="note0">//订单状态</span>
	"orderStatusDesc:"已退单" <span class="note0">//订单状态描述</span>
	"orderStep:7 <span class="note0">//订单进程</span>
	"orderStepDesc:"完成" <span class="note0">//订单进程描述</span>
	},...],
"pageCount":5, <span class="note0">//总页数</span>
"rowCount":47 <span class="note0">//总条数</span>
"pageNo":2, <span class="note0">//第几页</span>
"pageSize":10, <span class="note0">//每页多少条</span>
}
</pre>
		</td>
		<td>
			<a href="#" class="a0" onclick="$('formPagingQueryOrder').submit();return false;" >例子</a>
			<form action="<%=contextPath%>/order/pagingQueryOrder.do" id="formPagingQueryOrder" style="display:none;" target="_blank">
				<input type="hidden" name="userId" value="06F95B39-448A-402B-B918-6A52766530A4" />
				<input type="hidden" name="pageSize" value="3" />
				<input type="hidden" name="pageNo" value="1" />
			</form>
		</td>
	</tr>
	
	<tr>
		<td>查询单笔订单详情</td>
		<td>/order/queryOrderDetail</td>
		<td>
			<pre>
userId:06F95B39-448A-402B-B918-6A52766530A4 <span class="note0">//用户id</span>
orderNumber:23523523124<span class="note0">//订单号</span>
			</pre>
		</td>
		<td>
			<pre>
对象：
{"orderSimpleVo": <span class="note0">//订单信息</span>
	{"orderNumber":"23523523124"<span class="note0">//订单号</span>
	"userId":"06F95B39-448A-402B-B918-6A52766530A4" <span class="note0">//用户id</span>
	"userName":"刘熹" <span class="note0">//用户姓名或联系人</span>
	"mpNo":"13524769626" <span class="note0">//手机号</span>
	"address:"泰和路2038号D203" <span class="note0">//地址</span>
	"orderTime:"2015-02-03 09:42:33" <span class="note0">//下单收件</span>
	"orderStatus:6 <span class="note0">//订单状态</span>
	"orderStatusDesc:"已退单" <span class="note0">//订单状态描述</span>
	"orderStep:7 <span class="note0">//订单进程</span>
	"orderStepDesc:"完成" <span class="note0">//订单进程描述</span>
	},
"washItemSubtList": <span class="note0">//洗涤条目(明细)，数组</span>
	[{"washItemName":"T恤", <span class="note0">//名称</span>
	"price":9.90, <span class="note0">//单价</span>
	"subtNum":2, <span class="note0">//数量</span>
	"subtPrice":19.80 <span class="note0">//小计</span>
	},...],
"totalPrice":19.80 //应付总金额
}
</pre>
		</td>
		<td>
			<a href="#" class="a0" onclick="$('formQueryOrderDetail').submit();return false;" >例子</a>
			<form action="<%=contextPath%>/order/queryOrderDetail.do" id="formQueryOrderDetail" style="display:none;" target="_blank">
				<input type="hidden" name="userId" value="CF149EFA-B9E1-45D9-A27A-CDD96F2F6016" />
				<input type="hidden" name="orderNumber" value="114364390000288" />
			</form>
		</td>
	</tr>
	
	<tr>
		<td>获取用户地址(全部)</td>
		<td>/userAddress/getUserAddressAll</td>
		<td>
			<pre>
userId:5EEA7B06-E333-415A-9302-6A887C6C7C78 <span class="note0">//用户id</span>
			</pre>
		</td>
		<td>
			<pre>
数组：
[{
"id":9 <span class="note0">//id(主键)</span>
"userId":"5EEA7B06-E333-415A-9302-6A887C6C7C78" <span class="note0">//用户id</span>
"consigneeName":"郭敏杰" <span class="note0">//用户名(收、发货人或联系人)</span>
"consigneeMpNo":"18616940990" <span class="note0">//手机号</span>
"address":"泰和路2038号D栋203室" <span class="note0">//地址</span>
"defaultFlag":true <span class="note0">//是否默认地址</span>
}
,...]
</pre>
		</td>
		<td>
			<a href="#" class="a0" onclick="$('formGetUserAddressAll').submit();return false;" >例子</a>
			<form action="<%=contextPath%>/userAddress/getUserAddressAll.do" id="formGetUserAddressAll" style="display:none;" target="_blank">
				<input type="hidden" name="userId" value="5EEA7B06-E333-415A-9302-6A887C6C7C78" />
			</form>
		</td>
	</tr>

	<tr>
		<td>添加用户地址</td>
		<td>/userAddress/addUserAddress</td>
		<td>
			<pre>
userId:5EEA7B06-E333-415A-9302-6A887C6C7C78 <span class="note0">//用户id</span>
consigneeName:郭敏杰 <span class="note0">//收、发货人或联系人</span>
consigneeMpNo:18616940990 <span class="note0">//手机号</span>
address:泰和路2038号D栋203室" <span class="note0">//地址</span>
defaultFlag:true <span class="note0">//是否默认地址</span>
			</pre>
		</td>
		<td>
			<pre>
对象：
{
"id":9 <span class="note0">//id(主键)</span>
"userId":"5EEA7B06-E333-415A-9302-6A887C6C7C78" <span class="note0">//用户id</span>
"consigneeName":"郭敏杰" <span class="note0">//收、发货人或联系人</span>
"consigneeMpNo":"18616940990" <span class="note0">//手机号</span>
"address":"泰和路2038号D栋203室" <span class="note0">//地址</span>
"defaultFlag":true <span class="note0">//是否默认地址</span>
}
</pre>
		</td>
		<td>
			<a href="#" class="a0" onclick="$('formAddUserAddress').submit();return false;" >例子</a>
			<form action="<%=contextPath%>/userAddress/addUserAddress.do" id="formAddUserAddress" style="display:none;" target="_blank">
				<input type="hidden" name="userId" value="5EEA7B06-E333-415A-9302-6A887C6C7C78" />
				<input type="hidden" name="consigneeName" value="刘熹" />
				<input type="hidden" name="consigneeMpNo" value="13524769626" />
				<input type="hidden" name="address" value="福建中路995号" />
				<input type="hidden" name="defaultFlag" value="true" />
			</form>
		</td>
	</tr>
	
	<tr>
		<td>修改用户地址</td>
		<td>/userAddress/updateUserAddress</td>
		<td>
			<pre>
id:9 <span class="note0">//id(主键)</span>
userId:5EEA7B06-E333-415A-9302-6A887C6C7C78 <span class="note0">//用户id</span>
consigneeName:郭敏杰 <span class="note0">//收、发货人或联系人</span>
consigneeMpNo:18616940990 <span class="note0">//手机号</span>
address:泰和路2038号D栋203室" <span class="note0">//地址</span>
defaultFlag:true <span class="note0">//是否默认地址</span>
			</pre>
		</td>
		<td>
			<pre>
对象：
{
"id":9 <span class="note0">//id(主键)</span>
"userId":"5EEA7B06-E333-415A-9302-6A887C6C7C78" <span class="note0">//用户id</span>
"consigneeName":"郭敏杰" <span class="note0">//收、发货人或联系人</span>
"consigneeMpNo":"18616940990" <span class="note0">//手机号</span>
"address":"泰和路2038号D栋203室" <span class="note0">//地址</span>
"defaultFlag":true <span class="note0">//是否默认地址</span>
}
</pre>
		</td>
		<td>
			<a href="#" class="a0" onclick="$('formUpdateUserAddress').submit();return false;" >例子</a>
			<form action="<%=contextPath%>/userAddress/updateUserAddress.do" id="formUpdateUserAddress" style="display:none;" target="_blank">
				<input type="hidden" name="id" value="19418" />
				<input type="hidden" name="userId" value="5EEA7B06-E333-415A-9302-6A887C6C7C78" />
				<input type="hidden" name="consigneeName" value="刘熹" />
				<input type="hidden" name="consigneeMpNo" value="13524769626" />
				<input type="hidden" name="address" value="福建中路9977号" />
				<input type="hidden" name="defaultFlag" value="true" />
			</form>
		</td>
	</tr>
	
	<tr>
		<td>删除用户地址</td>
		<td>/userAddress/deleteUserAddress</td>
		<td>
			<pre>
id:9 <span class="note0">//id(主键)</span>
userId:5EEA7B06-E333-415A-9302-6A887C6C7C78 <span class="note0">//用户id</span>
			</pre>
		</td>
		<td>
			<pre>
对象：
{
"id":9 <span class="note0">//id(主键)</span>
"userId":"5EEA7B06-E333-415A-9302-6A887C6C7C78" <span class="note0">//用户id</span>
"consigneeName":"郭敏杰" <span class="note0">//收、发货人或联系人</span>
"consigneeMpNo":"18616940990" <span class="note0">//手机号</span>
"address":"泰和路2038号D栋203室" <span class="note0">//地址</span>
"defaultFlag":false <span class="note0">//是否默认地址</span>
}
</pre>
		</td>
		<td>
			<a href="#" class="a0" onclick="$('formDeleteUserAddress').submit();return false;" >例子</a>
			<form action="<%=contextPath%>/userAddress/deleteUserAddress.do" id="formDeleteUserAddress" style="display:none;" target="_blank">
				<input type="hidden" name="id" value="19419" />
				<input type="hidden" name="userId" value="5EEA7B06-E333-415A-9302-6A887C6C7C78" />
			</form>
		</td>
	</tr>
	
	<tr>
		<td>获取用户的默认地址</td>
		<td>/userAddress/getDefaultUserAddress</td>
		<td>
			<pre>
userId:5EEA7B06-E333-415A-9302-6A887C6C7C78 <span class="note0">//用户id</span>
			</pre>
		</td>
		<td>
			<pre>
对象：
{
"id":9 <span class="note0">//id(主键)</span>
"userId":"5EEA7B06-E333-415A-9302-6A887C6C7C78" <span class="note0">//用户id</span>
"consigneeName":"郭敏杰" <span class="note0">//收、发货人或联系人</span>
"consigneeMpNo":"18616940990" <span class="note0">//手机号</span>
"address":"泰和路2038号D栋203室" <span class="note0">//地址</span>
"defaultFlag":true <span class="note0">//是否默认地址</span>
}
</pre>
		</td>
		<td>
			<a href="#" class="a0" onclick="$('formGetDefaultUserAddress').submit();return false;" >例子</a>
			<form action="<%=contextPath%>/userAddress/getDefaultUserAddress.do" id="formGetDefaultUserAddress" style="display:none;" target="_blank">
				<input type="hidden" name="userId" value="5EEA7B06-E333-415A-9302-6A887C6C7C78" />
			</form>
		</td>
	</tr>
	
	<tr>
		<td>设置用户地址为默认地址</td>
		<td>/userAddress/setUserAddressDefault</td>
		<td>
			<pre>
id:9 <span class="note0">//id(主键)</span>
userId:5EEA7B06-E333-415A-9302-6A887C6C7C78 <span class="note0">//用户id</span>
			</pre>
		</td>
		<td>
			<pre>
对象：
{
"id":9 <span class="note0">//id(主键)</span>
"userId":"5EEA7B06-E333-415A-9302-6A887C6C7C78" <span class="note0">//用户id</span>
"consigneeName":"郭敏杰" <span class="note0">//收、发货人或联系人</span>
"consigneeMpNo":"18616940990" <span class="note0">//手机号</span>
"address":"泰和路2038号D栋203室" <span class="note0">//地址</span>
"defaultFlag":true <span class="note0">//是否默认地址</span>
}
</pre>
		</td>
		<td>
			<a href="#" class="a0" onclick="$('formSetUserAddressDefault').submit();return false;" >例子</a>
			<form action="<%=contextPath%>/userAddress/setUserAddressDefault.do" id="formSetUserAddressDefault" style="display:none;" target="_blank">
				<input type="hidden" name="id" value="19419" />
				<input type="hidden" name="userId" value="5EEA7B06-E333-415A-9302-6A887C6C7C78" />
			</form>
		</td>
	</tr>
</table>



</body>
</html>