<%@ page language="java" contentType="text/html; charset=utf-8" pageEncoding="utf-8" %>
<%@ taglib uri="http://laundry.washclothes.landaojia.com/jsp/my" prefix="my"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>出库主界面</title>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<my:css file="/css/base.css" />
	<my:js file="/js/base.js" />
	<my:js file="/js/outbound/outBound.js" />
</head>
<body>
<jsp:include page="../$include/menu.jsp" />

<div class="caption">出库单</div>

<table class="edDtl" cellspacing="0" cellpadding="0">
	<tr>
		<td class="lbl" width="80">洗涤条<span onclick="$('code').value='160155178';">码</span>：</td>
		<td width="180">
			<input type="text" id="code" class="txtV01 w150" onkeyup="keyUpOfCode(event);"/>
			<a href="#" class="btn0" style="color:green;" onclick="$('code').value='';return false;" title="清除洗涤条码的值">清</a>
			<%--<a href="#" class="btn0" style="color:green;" onclick="keyUpOfCode(event, true);return false;">查</a>--%>
			<input type="hidden" id="orderId" />
			<input type="hidden" id="orderProductId" />
		</td>
		<td class="lbl" width="80">订单号：</td>
		<td width="180"><input type="text" id="orderNumber" class="txtV01 w150"/></td>
		<td width="80">&nbsp;</td>
		<td width="440">&nbsp;</td>
	</tr>
	<tr>
		<td class="lbl">用户姓名：</td>
		<td>
			<input type="text" id="contacts" class="txtV01 w150" />
		</td>
		<td class="lbl" >用户手机：</td>
		<td>
			<input type="text" id="mpNo" class="txtV01 w150" />
		</td>

		<td class="lbl">送件地址：</td>
		<td>
			<input type="text" id="address" class="txtV01 w400" />
		</td>
	</tr>
</table>

<div style="margin:10px 0px 2px 0px;padding-right:20px;text-align:center;">
	<a href="#" id="btnPrintOutBoundInvoices" class="btn1" onclick="printOutBoundInvoices();return false;" style="display:none;">打印出库单</a>
	<a href="#" id="btnPrintOutBoundInvoicesPuppet" class="btn1Disabled" onclick="return false;">打印出库单</a>
	<a href="#" id="BtnAbnormalSubmit" class="btn1" onclick="toAbnormalSubmit();return false;" style="display:none;">异件上报</a>
	<a href="#" id="BtnAbnormalSubmitPuppet" class="btn1Disabled" onclick="return false;">异件上报</a>
</div>

<div style="width:65px;height:20px;line-height:20px;margin-top:10px;padding-left:15px;background-color:#666;color:#FFF;">衣物明细</div>

<table class="listTb" cellspacing="0" width="100%">
	<thead>
		<tr class="ttl">
			<td>id</td>
			<td>衣物名称</td>
			<td>销售价</td>
			<td>衣物描述</td>
			<td>洗涤条码</td>
			<td>状态</td>
		</tr>
	</thead>
		
	<tbody id="wpTbody">
		<%--<tr>
			<td>14001</td>
			<td>羽绒服</td>
			<td>9.9</td>
			<td>红 蓝 毛条</td>
			<td>312400344</td>
			<td>已出库</td>
		</tr>--%>
	</tbody>
</table>

</body>
</html>