<%@ page language="java" contentType="text/html; charset=utf-8" pageEncoding="utf-8" %>
<%@ include file="../$include/taglibs.jsp"%>
<table cellspacing="1" cellpadding="0" style="width:99%;background:#554433;">
	<c:forEach items="${wcpList}" var="wcp">
	<tr>
		<td style="color:green;background:#BBB;" width="16">${wcp.className}</td>
		<td style="background:#FFF;padding:3px 2px;line-height:20px;">
			<c:forEach items="${wcp.washProductPriceVOList}" var="wpp">
				<a href="#" class="btn4" onclick="chooseClothesType( '${wpp.prId}', '${wpp.prName}', '${wpp.prSalesPrice}' );return false;" title="${wpp.prSalesPrice}">${wpp.prName}</a>
			</c:forEach>
		</td>
	</tr>
	</c:forEach>
</table>

<table cellspacing="1" cellpadding="0" style="width:99%;background:#554433;margin-top:4px;">
	<tr>
		<td style="color:green;background:#BBB;" width="16">颜色</td>
		<td style="background:#FFF;padding:3px 2px;line-height:20px;">
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '黑' );return false;" style="background:#000;color:#FFF;">黑</a>
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '白' );return false;" style="background:#FFF;color:#000;">白</a>
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '灰' );return false;" style="background:#777;color:#000;">灰</a>
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '红' );return false;" style="background:#FF0000;color:#000;">红</a>
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '粉' );return false;" style="background:#AA4444;color:#000;">粉</a>
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '橙' );return false;" style="background:#FFB010;color:#000;">橙</a>
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '黄' );return false;" style="background:#FFFF30;color:#000;">黄</a>
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '米黄' );return false;" style="background:#EEEE99;color:#000;">米黄</a>
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '金' );return false;" style="background:#FFAF05;color:#000;">金</a>
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '深绿' );return false;" style="background:#006400;color:#000;">深绿</a>
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '浅绿' );return false;" style="background:#72F072;color:#000;">浅绿</a>
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '银' );return false;" style="background:#EEEEEE;color:#000;">银</a>
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '深蓝' );return false;" style="background:#00008F;color:#000;">深蓝</a>
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '浅蓝' );return false;" style="background:#E0F2FF;color:#000;">浅蓝</a>
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '深紫' );return false;" style="background:#490082;color:#000;">深紫</a>
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '浅紫' );return false;" style="background:#800080;color:#000;">浅紫</a>
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '深咖啡' );return false;" style="background:#800000;color:#000;">深咖啡</a>
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '浅咖啡' );return false;" style="background:#8F4513;color:#000;">浅咖啡</a>
		</td>
	</tr>
</table>

<table cellspacing="1" cellpadding="0" style="width:99%;background:#554433;margin-top:4px;">
	<tr>
		<td style="color:green;background:#BBB;" width="16">条纹</td>
		<td style="background:#FFF;padding:3px 2px;line-height:20px;">
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '条' );return false;" >条</a>
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '花' );return false;" >花</a>
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '点' );return false;" >点</a>
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '格' );return false;" >格</a>

		</td>
	</tr>
</table>

<table cellspacing="1" cellpadding="0" style="width:99%;background:#554433;margin-top:4px;">
	<tr>
		<td style="color:green;background:#BBB;" width="16">配件</td>
		<td style="background:#FFF;padding:3px 2px;line-height:20px;">
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '里子' );return false;" >里子</a>
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '毛' );return false;" >毛</a>
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '装饰扣' );return false;" >装饰扣</a>
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '饰物' );return false;" >饰物</a>
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '帽子' );return false;" >帽子</a>
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '内胆' );return false;" >内胆</a>
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '腰带' );return false;" >腰带</a>
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '袖带' );return false;" >袖带</a>
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '颈带' );return false;" >颈带</a>
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '毛领' );return false;" >毛领</a>
			<a href="#" class="btn4" onclick="chooseClothesAttrs( '毛条' );return false;" >毛条</a>

		</td>
	</tr>
</table>

<table cellspacing="0" cellpadding="0" class="edDtl" style="width:auto;margin-top:4px;">
	<tr>
		<td class="lbl" width="80">衣物名称：</td>
		<td>
			<input type="hidden" id="edtProductId" value="" />
			<input type="hidden" id="edtPrSalesPrice" value="" />
			<input type="text" id="edtProductName" class="txt01" style="width:150px;" value="" readonly/>
		</td>
		<td class="lbl" width="80">衣物描述：</td>
		<td>
			<input type="text" id="edtProductAttribute" class="txt01" style="width:250px;" value="" />
			<a href="#" onclick="$('edtProductAttribute').value='';return false;" style="text-decoration:none;color:blue;">清除</a>
		</td>
	</tr>
	<tr>
		<td class="lbl">洗涤条码：</td>
		<td>
			<input type="text" id="edtCode" class="txt01" style="width:150px;background:#EEE;" value="${code}" />
			<a href="#" class="btn0" style="color:green;" onclick="$('edtCode').value='';return false;" title="清除洗涤条码的值">清</a>
		</td><%--onkeyup="keyUpOfWashCode(event);"--%>
		<td class="lbl">工厂条码：</td>
		<td><input type="text" id="edtOtherCode" class="txt01" style="width:150px;" value="" /></td>
	</tr>
</table>

<div style="margin-top:5px;text-align:center;color:#664444;">
	(1)添加时，请将衣物放到摄像头下，系统会拍照；&nbsp;(2)添加后，会打印出洗涤条码，请绑订到衣物上
</div>

<div style="margin-top:5px;text-align:center;">
	<a href="#" class="btn1" onclick="addOrderProduct();return false;">添加</a>
	<%--<a href="#" class="btn1" onclick="capture();return false;">拍照</a>--%>
	<a href="#" class="btn1" onclick="$.windowxWind.style.display='none';return false;">关闭</a>
</div>
