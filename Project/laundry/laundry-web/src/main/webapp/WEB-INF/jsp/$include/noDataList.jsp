<%@ page language="java" contentType="text/html; charset=utf-8" pageEncoding="utf-8"%>
<%@ taglib uri="http://java.sun.com/jstl/core_rt" prefix="c"%>
<c:if test="${pageRO!=null && pageRO.totalRowCount==0}">
	<tr>
		<td colspan="100" class="gray" style="border:0px;">
			查无记录
		</td>
	</tr>
</c:if>