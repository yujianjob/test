<%@ page language="java" contentType="text/html; charset=utf-8" pageEncoding="utf-8" %>
<%@ include file="../$include/taglibs.jsp"%>
<table cellspacing="0" cellpadding="0" class="edDtl" style="width:auto;margin-top:4px;">
	<tr>
		<td class="lbl" width="80">订单编号：</td>
		<td>
			<input type="text" id="arOrderNumber" class="txt01" style="width:200px;" value="" />
		</td>
	</tr>
	<tr>
		<td class="lbl">异件类型：</td>
		<td style="padding:5px 0px;">
			<input type="radio" name="arType" value="工厂站点-数量不符" checked>数量不符&nbsp;
			<input type="radio" name="arType" value="工厂站点-品类不符" >品类不符&nbsp;<br/>
			<input type="radio" name="arType" value="工厂站点-衣物破损" >衣物破损&nbsp;
			<input type="radio" name="arType" value="工厂站点-工厂不洗" >工厂不洗&nbsp;<br/>
			<input type="radio" name="arType" value="工厂站点-其他异常" >其他异常&nbsp;
		</td>
	</tr>
	<tr>
		<td class="lbl">异件备注：</td>
		<td>
			<textarea id="arRemark" style="width:240px;height:100px;border:1px solid #888;"></textarea>
		</td>
	</tr>
</table>

<div style="margin-top:5px;text-align:center;">
	<a href="#" class="btn1" onclick="abnormalSubmit();return false;">上报</a>
	<a href="#" class="btn1" onclick="$.windowxWind.style.display='none';return false;">关闭</a>
</div>

<div style="margin-top:15px;text-align:center;color:green;" id="arMsgDiv">

</div>