<%@ page language="java" contentType="text/html; charset=utf-8" pageEncoding="utf-8" %>
<%@ taglib uri="http://laundry.washclothes.landaojia.com/jsp/my" prefix="my"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>扫描入库</title>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<my:css file="/css/base.css" />
	<my:js file="/js/base.js" />
	<my:js file="/js/splitwash/splitWash.js" />
</head>
<body>
<jsp:include page="../$include/menu.jsp" />

<div class="caption">扫描入库</div>

<table class="edDtl" cellspacing="0" cellpadding="0">
	<tr>
		<td class="lbl" width="80">物流单<span onclick="$('expNumber').value='002101003480';">号</span>：</td>
		<td width="180">
			<input type="text" id="expNumber" class="txtV01 w150" onkeyup="keyUpOfExpNumber(event);"/>
			<a href="#" class="btn0" style="color:green;" onclick="$('expNumber').value='';return false;" title="清除物流单号的值">清</a>
			<%--<a href="#" class="btn0" style="color:green;" onclick="keyUpOfExpNumber(null, true);return false;">查</a>--%>
			<input type="hidden" id="orderId" />	
		</td>
		<td class="lbl" width="80">订单号：</td>
		<td width="180">
			<input type="text" id="orderNumber" class="txtV01 w150" />
		</td>
		<td width="80">&nbsp;</td>
		<td width="440" id="orderInfoShow" style="color:red;">&nbsp;</td>
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

		<td class="lbl">取件地址：</td>
		<td>
			<input type="text" id="address" class="txtV01 w400" />
		</td>
	</tr>
	<tr>
		<td class="lbl">客服备注：</td>
		<td colspan="3">
			<input type="text" id="csRemark" class="txtV01 w400" />
		</td>
		<td class="lbl">用户备注：</td>
		<td>
			<input type="text" id="userRemark" class="txtV01 w400" />
		</td>
	</tr>
</table>

<div style="margin:10px 0px 2px 0px;padding-right:20px;text-align:center;">
	<a href="#" id="btnAddWashProduct" class="btn1" onclick="toAddOrderProduct();return false;" style="display:none;">添加衣物</a>
	<a href="#" id="btnAddWashProductPuppet" class="btn1Disabled" onclick="return false;">添加衣物</a>
	<a href="#" id="BtnFinishOrder" class="btn1" onclick="finishOrder();return false;" >完成订单</a>
	<%--<a href="#" id="BtnFinishOrderPuppet" class="btn1Disabled" onclick="return false;">清空信息</a> --%>
	<a href="#" id="BtnUnmReport" class="btn1" onclick="toAbnormalSubmit();return false;" style="display:none;">异件上报</a>
	<a href="#" id="BtnUnmReportPuppet" class="btn1Disabled" onclick="return false;">异件上报</a>
	<a href="#" class="btn1" onclick="openCamera();return false;">开启摄像头</a>
</div>

<div style="width:65px;height:20px;line-height:20px;margin-top:10px;padding-left:15px;background-color:#666;color:#FFF;">衣物明细</div>

<table class="listTb" cellspacing="0" width="100%">
	<thead>
		<tr class="ttl">
			<td>id</td>
			<td>衣物编号</td>
			<td>衣物名称</td>
			<td>销售价</td>
			<td>衣物描述</td>
			<td>洗涤条码</td>
			<td>工厂条码</td>
			<td>操作</td>
		</tr>
	</thead>
		
	<tbody id="wpTbody">
		<%--<tr>
			<td>14001</td>
			<td>5</td>
			<td>羽绒服</td>
			<td>9.9</td>
			<td>红 蓝 毛条</td>
			<td>312400344</td>
			<td>97850417</td>
			<td>删除</td>
		</tr>--%>
	</tbody>
</table>

</body>
</html>