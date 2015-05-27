( function(){
	
var $ = function(id){
	return document.getElementById(id);
};

$.contextPath = '/laundry-web';

$.elms = function( name ){
	return document.getElementsByName( name );
};

$.trim = function( s ) {
    return s.replace(/(^\s*)(\s*$)/g, '');
};

$.startsWith = function( str, x ){
	if( str != null && x != null && str.length >= x.length ){
		if( str.substring( 0, x.length ) == x ){
			return true;
		}
	}
	return false;
};

$.isArray = function( o ){
	return o instanceof Array;
};

/**
 * 取得控件的类型
 */
$.elmType = function( elm ){
	if( elm == null ){
		return null;
	}
	
	if( elm.tagName == 'INPUT' ){
		if( elm.type == 'text' ){
			if( elm.selectXx1$Panel || $.startsWith( elm.className, 'selectXx1Select' ) || $.startsWith( elm.className, 'selectXx1Input' ) ){
				return "selectXx1";
			}else {
				return "text";
			}			
		}else if( elm.type == 'checkbox' ){
			return "checkbox";
		}else if( elm.type == 'radio' ){
			return "radio";
		}
	}else if( elm.tagName == 'TEXTAREA' ){
		return "textarea";
	}else if( elm.tagName == 'SELECT' ){
		return "select";
	}else{
		return null;
	}
};

/**
 * 取得元素相对于页面的位置，绝对值，返回left，right, top，bottom
 * @param o : 目标元素对象
 */
$.getElmOffset = function( o ){
    var l = o.offsetLeft;
    var t = o.offsetTop;
    if( o.offsetParent ){
    	var p = $.getElmOffset( o.offsetParent );
        l = l + p.left;
        t = t + p.top;
    }
    return { "left" : l, "right" : ( l + o.offsetWidth ), "top" : t, "bottom" : ( t + o.offsetHeight )};
};

/**
 * 移除所有子元素
 */
$.removeAllChildNodes = function( p ){
	var cs = p.childNodes;
	for( var i = cs.length - 1; i >= 0; i -- ){
		p.removeChild( cs[i] );
	}
};

/**
 * 显示或隐藏
 * @params showIds : 显示的元素id，数组
 * @params hiddenIds : 隐藏的元素id，数组
 */
$.showAndHidden = function( showIds, hiddenIds ){
	if( showIds != null && showIds.length > 0 ){
		for( var i = 0; i < showIds.length; i ++ ){
			var elm = $( showIds[i] );
			if( elm ){
				elm.style.display = '';
			}
		}
	}
	
	if( hiddenIds != null && hiddenIds.length > 0 ){
		for( var i = 0; i < hiddenIds.length; i ++ ){
			var elm = $( hiddenIds[i] );
			if( elm ){
				elm.style.display = 'none';
			}
		}
	}
};

/**
 * 指向元素的警告
 */
$.pointToWarn = function( elm, lbl, msg ){
	
	
	//TODO......指向该元素并报警
	$.alert( '【' + lbl + '】' + msg );
};

/**
 * 取得选中的值用于radio
 */
$.radioValue = function( rdoName ){
	var es = $.elms( rdoName );
	for( var i = 0; i < es.length; i ++ ){
		if( es[i].checked ){
			return es[i].value;
		}
	}
	return null;
};

/**
 * ajax请求函数，统一POST方式，异步方式
 * @param url : 请求地址
 * @param params : 参数，对象，value支持字符串和数组
 * @param callback : 回调函数(不发生系统异常都会执行)
 * @param complete : 完成后执行的函数
 */
$.ajax = function( url, params, callback, complete ){
	var x = null;
	if( window.XMLHttpRequest ){
		x = new XMLHttpRequest();
	}else{
		try{
			x = new ActiveXObject("Microsoft.XMLHTTP");
		}catch(e){
			x = new ActiveXObject("Msxml2.XMLHTTP");
		}
	}
	
	x.open( "post", url );
	x.setRequestHeader( "Content-Type", "application/x-www-form-urlencoded" );
	x.setRequestHeader( 'If-Modified-Since', 'Thu, 1 Jan 1970 00:00:00 GMT' );
	x.setRequestHeader( 'Cache-Control', 'no-cache' );
	x.setRequestHeader( "is-ajax", "true" );
	
	var paramAry = [];
	if( params != null ){
		for(var i in params){
			if( params[i] != null ){
				if( typeof params[i] == 'object' && params[i].length ){
					for( var k = 0; k < params[i].length; k ++ ){
						paramAry[paramAry.length] = i + "=" + encodeURIComponent(params[i][k]);
					}
				}else{
					paramAry[paramAry.length] = i + "=" + encodeURIComponent(params[i]);
				}
			}
		}
	}
	
	//----超时..TODO...
	
	x.onreadystatechange = function(){
	    if( x.readyState == 4  ){
    		if( ( x.status >= 200 && x.status < 300 ) ||
    			x.status === 304 || x.status === 1223 || x.status === 0 ){
	    		var isAjaxResult = x.getResponseHeader( "is-ajax-result" );
    			if( 'true' == isAjaxResult ){
    				//alert( x.responseText );
    				var ajaxResult = eval( "(" + x.responseText + ")" );
    				if( ajaxResult.succFlag ){
    					if( callback ){
    						callback( ajaxResult.data );
    					}
    				}else{
    					$.alert( ajaxResult.msg );
    				}
    				
    				if( complete ){
    					complete( ajaxResult.isSucc, ajaxResult.data );
    				}
    			}else{
    				if( callback ){
    					callback( x.responseText );
    				}
    				
	    			if( complete ){
	    				complete( x.responseText );
	    			}
    			}
	    	}
	    }
	};
	x.send( paramAry.join( '&' ) );
};

/**
 * 调用日期控件
 */
$.callDateXx1 = function( format ){
	var evt = window.event || arguments.callee.caller.arguments[0];
	DateXx1.activate( evt, format );
};

/**
 * 自定义alert框
 */
$.alert = function( msg ){
	if( $.alertWind == null ){
		var aw = document.createElement( "DIV" );
		document.body.appendChild( aw );
		aw.className = 'alert';
		aw.style.left = ( document.body.clientWidth - 360 )/2 + 'px';
		//aw.style.top = ( document.body.clientHeight - 140 )/2 + 'px';
		$.alertWind = aw;
	}
	
	$.alertWind.innerHTML = '<div style="height:100px;padding:4px;text-align:center;"><table style="width:99%;height:99%;"><tr><td>' + msg + '</td></tr></table></div><div style="height:24px;text-align:center;"><a href="#" class="btn3" onclick="$.alertWind.style.display=\'none\';return false;">确定</a></div>';
	var h2 = document.body?document.body.scrollTop : document.documentElement.scrollTop;
	$.alertWind.style.top = ( h2 + 30 ) + 'px';
	$.alertWind.style.display = '';
};

/**
 * 小窗口(只能单个)
 */
$.windowx = function( title, html, w, h ){
	if( $.windowxWind == null ){
		var aw = document.createElement( "DIV" );
		aw.className = 'windowx';
		document.body.appendChild( aw );
		$.windowxWind = aw;
		aw.innerHTML = 
		'<div class="top">' + 
			'<table class="titleTb" cellspacing="0" cellpadding="0" >' + 
				'<tr>' + 
					'<td class="title">&nbsp;</td>' + 
					'<td class="close" onclick="">&nbsp;</td>' + 
				'</tr>' + 
			'</table>' + 
		'</div>' + 
		'<div class="content" style="height:' + ( h - 30 ) + 'px;"></div>';
		
		aw.titleTd = ( aw.getElementsByTagName( 'TD' ))[0];
		aw.closeTd = ( aw.getElementsByTagName( 'TD' ))[1];
		aw.closeTd.onclick = function(){
			$.windowxWind.style.display = 'none';
		};
		aw.contentDiv = ( aw.getElementsByTagName( 'DIV' ))[1];
	}
	
	$.windowxWind.titleTd.innerHTML = title;
	if( html != null ){
		$.windowxWind.contentDiv.innerHTML = html;		
	}
	$.windowxWind.style.left = ( document.body.clientWidth - w )/2 + 'px';
	$.windowxWind.style.top = ( ( document.body?document.body.scrollTop : document.documentElement.scrollTop ) + 20 ) + 'px';
	$.windowxWind.style.width = w + 'px';
	$.windowxWind.style.height = h + 'px';
	$.windowxWind.style.display = '';
};

$.addOnloadEvent = function( f ){
	var obt = document.attachEvent? 1 : ( document.addEventListener? 2 : 3 );
	if( obt == 1 ){
		window.attachEvent( 'onload', function( event ){
			f();
		} );
		
	}else if( obt == 2 ){
		window.addEventListener( 'load', function( event ){
			f();
		}, false);
	}
};

/**
 * 调试对象的属性
 * @deprecated
 */
$.debugObjectAttrs = function( obj ){
	var ary = [];
	ary[ary.length] = "obj : " + obj;
	for( var i in obj ){
		ary[ary.length] = i + " : " + obj[i];
	}
	
	var h = document.createElement( 'DIV' );
	h.style.border = '1px solid #999';
	h.style.width = '99%';
	h.style.margin = '5px auto';
	h.innerHTML = '<div><div style="text-align:right;color:red;background:#AAA;" onclick="this.parentNode.parentNode.parentNode.removeChild( this.parentNode.parentNode )">关闭</div><div>' + ary.join( '<br/>' ) + '</div></div>';
	document.body.appendChild( h );
};

window.$ = $;
} )();

//====【通用查询】===========================================================================================================
/**
 * 查询出发按钮的转换
 * 用于：(1)点击查询后，灰掉...(2)查询成功后，恢复
 * @param btn : 按钮对象
 * @param querying : true:查询中， false:查询结束
 */
function btnQueryTrans( btn, querying ){
	if( querying ){
		if( !btn.oldClassName ){
			btn.oldClassName = btn.className;
			btn.oldInnerHTML = btn.innerHTML;
			btn.oldOnclick = btn.onclick;
		}
		btn.className = 'btn1Disabled';
		btn.innerHTML = btn.innerHTML + '中';
		btn.onclick = function(){alert( '操作中，请忽重复点击' );};//操作中，请忽重复点击
	}else{
		btn.className = btn.oldClassName;
		btn.innerHTML = btn.oldInnerHTML;
		btn.onclick = btn.oldOnclick;
	}
}
