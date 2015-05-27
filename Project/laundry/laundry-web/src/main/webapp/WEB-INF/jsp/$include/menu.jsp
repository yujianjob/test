<%@ page language="java" contentType="text/html; charset=utf-8" pageEncoding="utf-8"%>
<script type="text/javascript" src="${pageContext.request.contextPath}/statics/dev/js/menu.js" ></script>

<table style="width:100%;border:0px;border-bottom:2px solid #666;margin:0px auto 1px auto;text-align:center;font-size:12px;">
	<tr>
		<td width="100">&nbsp;</td>
		<%--<td class="logo01"></td>--%>
		<td style="width:49px;">&nbsp;</td>
		<td style="font-weight:bold;font-size:18px;text-align:center;letter-spacing:3px;" nowrap>懒到家洗衣工厂站点管理系统</td>
		<td width="200" style="text-align:right;line-height:14px;padding-right:15px;">
			您好，用户 | <a href="#" style="text-decoration:none;color:blue;" onclick="window.location=window.location;">刷新</a> |退出<br/>
			<span style="color:#999;">version:<%=com.landaojia.washclothes.laundry.common.jsptag.StaticFileConfig.t0%></span>
		</td>
	</tr>
</table>

<div style="width:100%;height:31px;position:absolute;border-bottom:0px solid #666;">
	<div style="width:98%;margin:0px auto;border-top:0px solid green;">
		<ul class="mnMain" id="$mnMain">
			<li class="pg" style="display:none;">
				<div>
					<a href="#" id="$mnMainPgUp" class="upY" onclick="window['$mnMain'].flipPg(-1);return false;" title="上一页菜单"></a>
				</div>
				<div>
					<a href="#" id="$mnMainPgDown" class="downN" onclick="window['$mnMain'].flipPg(1);return false;"  title="下一页菜单"></a>
				</div>
			</li>
			
			<li >工厂收件
				<ul class="mnSub mnSubHidden">
					<li><a href="${pageContext.request.contextPath}/a001.do" style="text-decoration:line-through" onclick="$.alert('功能暂未开放');return false;">工厂收件</a></li>
				</ul>
			</li>
			
			<li >拆包洗涤
				<ul class="mnSub mnSubHidden">
					<li><a href="${pageContext.request.contextPath}/splitWash/toSplitWash.do">拆包关联条码</a></li>
				</ul>
			</li>
			
			<li >出库操作
				<ul class="mnSub mnSubHidden">
					<li><a href="${pageContext.request.contextPath}/outBound/toOutBound.do">出库单</a></li>
				</ul>
			</li>
		</ul>
	</div>
</div>

<div style="height:31px;background-color:#F0F0F9;"></div>