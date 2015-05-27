<%@ page language="java" contentType="text/html; charset=utf-8" pageEncoding="utf-8"%>
<%@ taglib uri="http://java.sun.com/jstl/core_rt" prefix="c"%>
<c:if test="${pageRO != null}">
	<c:set var="pageNo" value="${pageRO.pageNO==null?1:pageRO.pageNO}"></c:set>
	<c:set var="pageCount" value="${pageRO.totalPageCount==null?0:pageRO.totalPageCount}"></c:set>
	<div id="pagingDivDone" style="display:none;">
		<span class="mgR30" style="color:#CCC;" id="ajaxQueryCostTime"></span>
		第<input type="text" class="pgTxt" value="${pageNo}" id="pageNo"/>页
		<span class="btn" onclick="pagingAjax( document.getElementById( 'pageNo' ).value, ${pageNo}, ${pageCount});">跳 转</span>
		<span class="spliter">|</span>
		每页<input type="text" class="pgTxt" value="${pageRO.pageSize==null?10:pageRO.pageSize}" id="pageSize"/>条
		<span class="btn" onclick="pagingAjax( 1, ${pageNo}, ${pageCount}, true );">设 置</span>
		<span class="spliter">|</span>
		共<span>${pageCount}</span>页/<span>${pageRO.totalRowCount==null?0:pageRO.totalRowCount}</span>条
		<span class="spliter">|</span>
		<span class="btn<c:if test="${pageNo<=1}" >Null</c:if>" onclick="pagingAjax( 1, ${pageNo}, ${pageCount} );">第一页</span>
		<span class="btn<c:if test="${pageNo<=1}" >Null</c:if>" onclick="pagingAjax( ${pageNo - 1}, ${pageNo}, ${pageCount} );">上一页</span>
		<span class="btn<c:if test="${pageNo>=pageCount}" >Null</c:if>" onclick="pagingAjax( ${pageNo + 1}, ${pageNo}, ${pageCount} );">下一页</span>
		<span class="btn<c:if test="${pageNo>=pageCount}" >Null</c:if>" onclick="pagingAjax( ${pageCount}, ${pageNo}, ${pageCount} );">最末页</span>
	</div>
</c:if>