/*!
 * DateXx1.js，日历控件,源码
 * Version: 1.0
 * Date: 2014-09-30
 * Author: dsine
 * 操作要点：(1)滚轮加减值，原触发框，点击对应的项，加减对应的项。(2)滚轮加减值，面板区:年、时、分、秒。(3)双击原触发框，获取当前日期。(4)按下ctrl双击原触发框，清空当前值
 */
(function(){

function DateXx1(){
	/**
	 * css样式
	 */
	this.cssTxt = 
		".dateXx1FocusElmStyle{color:#0033FF;}" +
		".dateXx1Panel{position:absolute;width:242px;height:auto;background:#F8F8FF;border-top:1px solid #8899AA;border-left:1px solid #AABBCC;border-right:1px solid #445566;border-bottom:1px solid #334455;padding:2px;font-family:宋体;font-size:12px;filter:alpha(Opacity=96);-moz-opacity:0.96;opacity:0.96;}" +
		".dateXx1Panel .clVl{float:left;width:auto;height:10px;line-height:10px;overflow:hidden;margin-top:3px;color:#448855;}" +
		".dateXx1Panel .clVc{float:left;height:9px;line-height:9px;overflow:hidden;margin-top:4px;background:#999 url('" + DateXx1.path + "DateXx1.png') repeat 0px -55px;cursor:pointer;}" +
		".dateXx1Panel .clVs6{float:left;width:6px;height:11px;line-height:11px;margin-top:3px;background:#000 url('" + DateXx1.path + "DateXx1.png') no-repeat -44px -100px;}" +
		".dateXx1Panel .clVs3{float:left;width:3px;height:11px;line-height:11px;margin-top:3px;background:#000 url('" + DateXx1.path + "DateXx1.png') no-repeat -51px -100px;}" +
		".dateXx1Panel .clVr{float:left;width:auto;height:10px;line-height:10px;overflow:hidden;margin-top:3px;color:#448855;}" +
		".dateXx1Panel .yearCtn{height:20px;background:#FFF url('" + DateXx1.path + "DateXx1.png') repeat 0px 0px;}" +
		".dateXx1Panel .yearVle{width:36px;height:14px;margin:0px 0px 0px 0px;line-height:14px;font-size:12px;text-align:center;border:0px;background:#FFF url('" + DateXx1.path + "DateXx1.png') repeat 0px -66px;color:#001840;font-weight:bold;}" +
		".dateXx1Panel .yearLamBtnA{float:left;width:16px;height:14px;line-height:14px;background:#AAA url('" + DateXx1.path + "DateXx1.png') no-repeat -17px -83px;color:#000012;margin-top:2px;margin-left:2px;text-align:center;cursor:pointer;}" +
		".dateXx1Panel .yearLamBtnB{float:left;width:16px;height:14px;line-height:14px;background:#888 url('" + DateXx1.path + "DateXx1.png') no-repeat 0px -83px;color:#FFEEFF;margin-top:2px;margin-left:2px;text-align:center;cursor:pointer;}" +
		".dateXx1Panel .monCtn{height:18px;background:#8899AA url('" + DateXx1.path + "DateXx1.png') repeat 0px -31px;cursor:pointer;}" +
		".dateXx1Panel .monTc{float:left;width:20px;height:16px;line-height:16px;text-align:center;color:#FFF;margin-top:1px;}" +
		".dateXx1Panel .monTs{float:left;width:20px;height:16px;line-height:16px;text-align:center;color:#001840;margin-top:1px;background:#FFF url('" + DateXx1.path + "DateXx1.png') no-repeat -38px -66px;font-weight:bold;}" +
		".dateXx1Panel .dayCtn{clear:both;height:112px;}" +
		".dateXx1Panel .dayCtrLeft{float:left;width:50px;height:110px;}" +
		".dateXx1Panel .dayCtrCenter{float:left;width:140px;height:111px;background:#FFFFFF;border:1px dashed #6688DD;border-top:0px;}" +
		".dateXx1Panel .dayCtrBox{background-color:red;}" +
		".dateXx1Panel .wekCtn{background:#99AA99;height:14px;line-height:14px;text-align:center;}" +
		".dateXx1Panel .wekTc{float:left;width:20px;height:14px;line-height:14px;text-align:center;color:#FFEEEE;margin-top:1px;}" +
		".dateXx1Panel .dayCtrRight{float:left;width:50px;height:110px;}" +
		".dateXx1Panel .dayTb{float:left;width:20px;height:16px;line-height:16px;}" +
		".dateXx1Panel .dayTc{float:left;width:20px;height:16px;line-height:16px;background:#F0FAFB;color:#662211;text-align:center;cursor:pointer;}" +
		".dateXx1Panel .dayTs{float:left;width:20px;height:16px;line-height:16px;background:#FFF url('" + DateXx1.path + "DateXx1.png') no-repeat 0px -134px;color:#001840;text-align:center;font-weight:bold;bold;cursor:pointer;}" +
		".dateXx1Panel .dayLamBtnB{clear:both;width:42px;height:12px;line-height:12px;padding:2px 0px;background:#999 url('" + DateXx1.path + "DateXx1.png') no-repeat 0px -117px;color:#FFF;text-decoration:none;text-align:center;margin:2px auto 2px auto;cursor:pointer;}" +
		".dateXx1Panel .dayLamBtnA{clear:both;width:42px;height:12px;line-height:12px;padding:2px 0px;background:#BBB url('" + DateXx1.path + "DateXx1.png') no-repeat 0px -100px;color:#333355;text-decoration:none;text-align:center;margin:2px auto 2px auto;cursor:pointer;}" +
		".dateXx1Panel .daySmtCnt{clear:both;width:42px;height:16px;margin:2px auto 2px auto;}" +
		".dateXx1Panel .daySmtBtnB{float:left;width:20px;height:12px;line-height:12px;padding:2px 0px;background:#999 url('" + DateXx1.path + "DateXx1.png') no-repeat -22px -151px;color:#FFF;text-decoration:none;text-align:center;margin:0px 1px 0px 0px;cursor:pointer;}" +
		".dateXx1Panel .daySmtBtnA{float:left;width:20px;height:12px;line-height:12px;padding:2px 0px;background:#BBB url('" + DateXx1.path + "DateXx1.png') no-repeat 0px -151px;color:#333355;text-decoration:none;text-align:center;margin:0px 1px 0px 0px;cursor:pointer;}" +
		".dateXx1Panel .daySmtCnt2{clear:both;width:50px;height:16px;margin:2px auto 2px auto;text-align:right;}" +
		".dateXx1Panel .daySmtMns{float:left;line-height:10px;margin:3px 0px 0px 2px;color:#333355;font-size:12px;font-family:'宋体';}" +
		".dateXx1Panel .dayAjTxt0{float:left;width:13px;height:12px;line-height:12px;border:1px solid #999;color:333355;text-align:center;font-size:12px;margin:0px 1px 0px 0px;font-family:'宋体';}" +
		".dateXx1Panel .hmsCtn{height:auto;padding-top:1px;}" +
		".dateXx1Panel .hmsVle{width:18px;height:14px;line-height:14px;margin:0px 0px 0px 0px;font-size:12px;text-align:center;border:0px;background:#FFF url('" + DateXx1.path + "DateXx1.png') no-repeat -35px -83px;color:#001840;font-weight:bold;}" +
		".dateXx1Panel .hmsSmtBtnB{float:left;width:20px;height:12px;line-height:12px;padding:2px 0px;background:#333355 url('" + DateXx1.path + "DateXx1.png') no-repeat 0px -202px;color:#DDDDEE;text-align:center;margin:0px 2px 0px 2px;cursor:pointer;}" +
		".dateXx1Panel .hmsSmtBtnA{float:left;width:20px;height:12px;line-height:12px;padding:2px 0px;background:#BBCCDD url('" + DateXx1.path + "DateXx1.png') no-repeat -22px -202px;color:#113355;text-align:center;margin:0px 2px 0px 2px;cursor:pointer;}" +
		".dateXx1Panel .hsmLamCtn{height:20px;padding-left:7px;}" +
		".dateXx1Panel .hmsLamBtnB{float:left;width:52px;height:12px;line-height:12px;padding:2px 0px;background:#333355 url('" + DateXx1.path + "DateXx1.png') no-repeat 0px -168px;color:#DDDDEE;text-align:center;margin:2px 2px 0px 2px;cursor:pointer;}" +
		".dateXx1Panel .hmsLamBtnA{float:left;width:52px;height:12px;line-height:12px;padding:2px 0px;background:#BBCCDD url('" + DateXx1.path + "DateXx1.png') no-repeat 0px -185px;color:#113355;text-align:center;margin:2px 2px 0px 2px;cursor:pointer;}" +
		".dateXx1Panel .tip1Box{position:absolute;width:28px;height:16px;line-height:16px;text-align:center;background:#312F33;z-index:90000;color:#FFF;}" +
		".dateXx1Panel .w3{width:3px;}" +
		".dateXx1Panel .w6{width:6px;}" +
		".dateXx1Panel .w18{width:18px;}" +
		".dateXx1Panel .w32{width:32px;}" +
		".dateXx1Panel .h18{height:18px;}" +
		".dateXx1Panel .lftL{float:left}" +
		".dateXx1Panel .mrgR5{margin-right:5px;}";
	
	/**
	 * 语言
	 */
	this.lang = {
		'SET_LANGUAGE' : 'zh-cn',	//当前设置的语言
		'zh-cn' : { 
			'weekStr' : ["日","一","二","三","四","五","六"], 
			'dayQkStr' : ["上月1","上月末","下月1","下月末"], 
			'nowTimeStr' : '当前时间', 
			'todayStr' : '今日日期',
			'cleanDateStr' : '清除时间' },
		'en' : { 
			'weekStr' : ["Su","Mo","Tu","We","Th","Fr","Sa"], 
			'dayQkStr' : ["last1","last30","next1","next30"], 
			'nowTimeStr' : 'now time',
			'todayStr' : 'today',
			'cleanDateStr' : 'clean time' }
	};
	
	/**
	 * 当前焦点元素
	 */
	this.focusElm = null;
	
	/**
	 * 当前焦点元素原始的样式class名
	 */
	this.focusOrigClassName = null;
	
	/**
     * 点击焦点元素时的时间(整数值)
     */
	this.focusClickTime = 0;
	
	/**
	 * 在焦点元素时，按下的键盘键值集合
	 */
	this.focusDownKeyMap = {};
	
	/**
	 * 焦点元素上次的值，当输入值格式错误时，用于恢复框中的值
	 */
	this.lastFocusElmValue = '';
	
	/**
	 * 当前时间格式，支持：yyyy、MM、dd、HH、mm、ss
	 */
	this.format = 'yyyy-MM-dd HH:mm:ss';
	
	/**
	 * 当前日期值对象
	 */
	this.date = null;
	
	/**
	 * 要加减的时间项，取值：yyyy、MM、dd、HH、mm、ss
	 */
	this.adsbItem = 'dd';
	
	/**
	 * 主面板元素对象
	 */
	this.panel = null;
	
	/**
	 * 鼠标是否在触发元素或显示面板的区域内
	 */
	this.cursorOn = false;
	
	/**
	 * 年份选择条起始年份
	 */
	this.yearClStartYear = null;
	
	/**
	 * (支持的)最小日期
	 */
	this.minDate = new Date( 100, 0, 1, 0, 0, 0, 0, 0 );
	
	/**
	 * (支持的)最大日期
	 */
	this.maxDate = new Date( 9999, 11, 31, 23, 59, 59, 999 );
	
	/**
	 * 根据id获取元素
	 */
	this.getElm = function( id ){
		return document.getElementById( id );
	};
	
	/**
	 * 创建元素
	 * @param tagName : 元素名，DIV，【必填】
	 * @param parentNode : 父节点对象，【可选】
	 * @param className : css样式名称，【可选】
	 * @param attributes : 属性对象，【可选】
	 * @param  styleProperties : 样式属性对象，【可选】
	 */
	this.makeElm = function( tagName, parentNode, className, attributes, styleProperties ){
		var e = document.createElement( tagName );
		if( parentNode ){
			parentNode.appendChild( e );
		}
		
		if( className ){
			e.className = className;
		}
		
		if( attributes ){
			for( var j in attributes ){
				e[j] = attributes[j];
			}
		}
		
		if( styleProperties ){
			for( var i in styleProperties ){
				e.style[i] = styleProperties[i];
			}
		}
		
		return e;
	};
	
	/**
	 * 去除左右空格
	 */
	this.trim = function( s ){
		return s.replace( /(^\s+)|(\s+$)/g, '' );
	}
	
	/**
	 * 判断浏览器
	 * @params bts : 浏览器类型名称，数组。例如：['IE','Firefox']，匹配上任意一个值即返回true。支持的值：MSIE/IE、Firefox、Safari、Opera、Chrome
	 */
	this.isBroswerType = function( bts ){
		for( var i = 0; i < bts.length; i ++ ){
			if( ( '' + window.navigator.userAgent ).indexOf( bts[i] ) >= 0 ){
				return true;
			}
		}
		return false;
	};
	
	/**
	 * 获取光标(在文本框)中的位置
	 */
	this.getCursorPosition = function( o ) {
		if( document.selection ) {
			var sel = document.selection.createRange();
			sel.moveStart( 'character', -o.value.length );
			return sel.text.length;
		}else if( o.selectionStart || o.selectionStart == '0' ){
			return o.selectionStart;
		}
	};
	
	/**
	 * 取得元素相对于页面的位置，绝对值，返回left，right, top，bottom
	 * @param o : 目标元素对象
	 */
	this.getElmOffset = function( o ){
	    var l = o.offsetLeft;
	    var t = o.offsetTop;
	    if( o.offsetParent ){
	    	var p = DateXx1.me.getElmOffset( o.offsetParent );
	        l = l + p.left;
	        t = t + p.top;
	    }
	    return { "left" : l, "right" : ( l + o.offsetWidth ), "top" : t, "bottom" : ( t + o.offsetHeight )};
	};
	
	/**
	 * 设置元素的样式
	 * @param o : 目标元素对象
	 * @param m : 样式属性，对象，例如：{'display':'none','left':'20px'}
	 */
	this.setElmStyle = function( o, m ){
		for( var i in m ){
			o.style[i] = m[i];
		}
	};
	
	/**
	 * 左补0凑齐总长度
	 */
	this.leftPadZero = function( n, len ){
		var s = '' + n;
		for( var i = 0, l = len - ( '' + n ).length; i < l; i ++ ){
			s = '0' + s;
		}
		return s;
	};
	
	/**
	 * 是否有效的项值(年月日时分秒的范围、长度)
	 * @param v : 项的值
	 * @param len : 项的长度
	 * @param min : 项的最小值
	 * @param max : 项的最大值
	 */
	this.isValidItem = function( v, len, min, max ){
		if( v.length != len ){
			return false;
		}
		
		for( var i = 0; i < v.length; i ++ ){
			var c = v.charCodeAt( i );
			if( c < 48 || c > 57 ){
				return false;
			}
		}
		
		var w = parseInt( v, 10 );
		if( 'NaN' == '' + w || w < min || w > max ){
			return false;
		}
		
		return true;
	};
	
	/**
	 * 验证日期格式
	 */
	this.verifyDateFormat = function( s, format ){
		if( s.length != format.length ){
			return null;
		}
		
		var yyyyP = format.indexOf( 'yyyy' );
		var MMP = format.indexOf( 'MM' );
		var ddP = format.indexOf( 'dd' );
		var HHP = format.indexOf( 'HH' );
		var mmP = format.indexOf( 'mm' );
		var ssP = format.indexOf( 'ss' );
		
		if( yyyyP >= 0 ){
			var yyyy = s.substring( yyyyP, yyyyP + 4 );
			if( !this.isValidItem( yyyy, 4, 100, 9999 ) ){
				return false;
			}
		}
		
		if( MMP >= 0 ){
			var MM = s.substring( MMP, MMP + 2 );
			if( !this.isValidItem( MM, 2, 1, 99 ) ){
				return false;
			}
		}
		
		if( ddP >= 0 ){
			var dd = s.substring( ddP, ddP + 2 );
			if( !this.isValidItem( dd, 2, 1, 99 ) ){
				return false;
			}
		}
		
		if( HHP >= 0 ){
			var HH = s.substring( HHP, HHP + 2 );
			if( !this.isValidItem( HH, 2, 0, 99 ) ){
				return false;
			}
		}
		
		if( mmP >= 0 ){
			var mm = s.substring( mmP, mmP + 2 );
			if( !this.isValidItem( mm, 2, 0, 99 ) ){
				return false;
			}
		}
		
		if( ssP >= 0 ){
			var ss = s.substring( ssP, ssP + 2 );
			if( !this.isValidItem( ss, 2, 0, 99 ) ){
				return false;
			}
		}
		
		return true;
	};
	
	/**
     * 解析出日期
     */
	this.parseDate = function( s, format ){
		if( s.length != format.length || !this.verifyDateFormat( s, format ) ){
			return null;
		}
		
		var yyyyP = format.indexOf( 'yyyy' );
		var MMP = format.indexOf( 'MM' );
		var ddP = format.indexOf( 'dd' );
		var HHP = format.indexOf( 'HH' );
		var mmP = format.indexOf( 'mm' );
		var ssP = format.indexOf( 'ss' );
		
		var yyyy = yyyyP != -1? parseInt( s.substring( yyyyP, yyyyP + 4 ), 10 ) : new Date().getFullYear();
		var MM = MMP != -1? parseInt( s.substring( MMP, MMP + 2 ), 10 ) : 1;
		var dd = ddP != -1? parseInt( s.substring( ddP, ddP + 2 ), 10 ) : 1;
		var HH = HHP != -1? parseInt( s.substring( HHP, HHP + 2 ), 10 ) : 0;
		var mm = mmP != -1? parseInt( s.substring( mmP, mmP + 2 ), 10 ) : 0;
		var ss = ssP != -1? parseInt( s.substring( ssP, ssP + 2 ), 10 ) : 0;
		
		return new Date( yyyy, MM -1, dd, HH, mm, ss );
	};
	
	/**
     * 格式化日期
     */
	this.formatDate = function( date, format ){
		var d = date;
		var yyyy = d.getFullYear();
		var MM = d.getMonth() + 1;
		var dd = d.getDate();
		var HH = d.getHours();
		var mm = d.getMinutes();
		var ss = d.getSeconds();
		
		return format.replace( 'yyyy', this.leftPadZero( yyyy, 4 ) )
			.replace( 'MM', this.leftPadZero( MM, 2 ) )
			.replace( 'dd', this.leftPadZero( dd, 2 ) )
			.replace( 'HH', this.leftPadZero( HH, 2 ) )
			.replace( 'mm', this.leftPadZero( mm, 2 ) )
			.replace( 'ss', this.leftPadZero( ss, 2 ) );
	};
	
	/**
     * 操作触发框的值
     * @param r : 要操作的项，取值：yyyy、MM、dd、HH、mm、ss
     * @param t : 操作类型，取值：1:在操作项上加减值，2:操作项的值
     * @param v : 值，任意整数(当t=1时，加或减某值；当t=2时，就是这个值)
     */
	this.fillForValue = function( r, t, v ){
		var d = this.date;
		var yyyy = d.getFullYear();
		var MM = d.getMonth() + 1;
		var dd = d.getDate();
		var HH = d.getHours();
		var mm = d.getMinutes();
		var ss = d.getSeconds();
		
		yyyy = r == 'yyyy'? ( t == 1? yyyy + v : v ) : yyyy;
		if( yyyy <= 99 ){
			yyyy = 100;
		}
		
		MM = r == 'MM'? ( t == 1? MM + v : v ) : MM;
		dd = r == 'dd'? ( t == 1? dd + v : v ) : dd;
		HH = r == 'HH'? ( t == 1? HH + v : v ) : HH;
		mm = r == 'mm'? ( t == 1? mm + v : v ) : mm;
		ss = r == 'ss'? ( t == 1? ss + v : v ) : ss;
		
		this.date = new Date( yyyy, MM - 1, dd, HH, mm, ss );
		this.date = this.date.getTime() < this.minDate.getTime()? this.minDate : ( this.date.getTime() > this.maxDate.getTime()? this.maxDate : this.date );
		this.focusElm.value = this.formatDate( this.date, this.format );
		this.lastFocusElmValue = this.focusElm.value;
	};
	
	/**
	 * 鼠标滚轮事件
	 */
	this.mouseWheel = function( event ){
		var evt = event || window.event;
		var o = evt.target || evt.srcElement;
		var me = DateXx1.me;
		var p = me.panel;
		//上滚还是下滚, d == 120 || d == -3, 上滚，加值; d == -120 || d == 3, 下滚，减值
		var d = evt.wheelDelta || evt.detail;
		var v = ( d == 120 || d == -3 )? 1 : -1;
		
		var goon = false;	//鼠标滚动事件是否继续传播
		//====焦点元素，滚动时增减对应项的值==============
		if( me.focusElm == o ){
			me.fillForValue( me.adsbItem, 1, v );
		}
		//====年份，输入框============
		else if( p.yearVle == o ){
			me.fillForValue( 'yyyy', 1, v );
		}
		//====小时，输入框============
		else if( p.hhVle == o ){
			me.fillForValue( 'HH', 1, v );
		}
		//====分钟，输入框============
		else if( p.mmVle == o ){
			me.fillForValue( 'mm', 1, v );
		}
		//====秒，输入框============
		else if( p.ssVle == o ){
			me.fillForValue( 'ss', 1, v );
		}
		//====自填日期加减框===========
		else if( p.dayAjTxtLft == o || p.dayAjTxtRgt == o ){
			var g = parseInt( o.value, 10 );
			if( 'NaN' != '' + g ){
				o.value = g + v;
			}
		}
		else{
			goon = true;
		}
		me.drawPanel();
		
		if( !goon ){
			if( evt.preventDefault ){
				evt.preventDefault();
			}
			return false;
		}
	};
	
	/**
     * 组装年份选择条
     * @param v : 取值-1/0/1
     */
	this.fitYearCl = function( v ){
		var me = DateXx1.me;
		me.yearClStartYear = me.date.getFullYear() - 5;
		
		if( v == -1 ){
			me.yearClStartYear = me.yearClStartYear - 5;
		}else if( v == 1 ){
			me.yearClStartYear = me.yearClStartYear + 5;
		}
		
		var p = me.panel;
		var yearValue = me.date.getFullYear();
		
		if( !p.yearClCtn.itd ){
			//----左边的年份数字------
			p.yearClCtn.ln = me.makeElm( 'DIV', p.yearClCtn, 'clVl', {'innerHTML':me.yearClStartYear} );
			
			//----10个年份选择块------
			p.yearClCtn.cls = [];
			
			for( var n = 0; n < 10; n ++ ){
				var c = me.makeElm( 'DIV', p.yearClCtn, null, {'val0':n} );
				p.yearClCtn.cls[n] = c;
				if( me.yearClStartYear + n == yearValue ){
					c.className = 'clVs6';
					p.yearClCtn.lastYearClObj = c;
				}else{
					c.className = 'clVc w6';
				}
				
				//----年份选择条每个单元格的点击事件：选取年份---------
				c.onmousedown = function(){
					me.panel.yearClCtn.lastYearClObj.className = 'clVc w6';
					this.className = 'clVs6';
					me.panel.yearClCtn.lastYearClObj = this;
					me.fillForValue( 'yyyy', 2, me.yearClStartYear + this.val0 );
					me.drawPanel( { 'fitYearCl01':'N' } );//重画面板时，不重画进度条
				};
				
				//----鼠标over事件，显示tip---------
				c.onmouseover = function(){
					me.tip1( this, true, me.yearClStartYear + this.val0 );
				};
				
				//----鼠标over事件，关闭tip---------
				c.onmouseout = function(){
					me.tip1( this, false, null );
				};
			}
			
			//----右边的年份数字------
			p.yearClCtn.rn = me.makeElm( 'DIV', p.yearClCtn, 'clVr', {'innerHTML':me.yearClStartYear + 9} );
			
			p.yearClCtn.itd = true;
		}else{
			p.yearClCtn.ln.innerHTML = me.yearClStartYear;
			
			if( me.panel.yearClCtn.lastYearClObj.val0 + me.yearClStartYear != yearValue ){
				me.panel.yearClCtn.lastYearClObj.className = 'clVc w6';
				me.panel.yearClCtn.cls[yearValue-me.yearClStartYear].className = 'clVs6';
				me.panel.yearClCtn.lastYearClObj = me.panel.yearClCtn.cls[yearValue-me.yearClStartYear];
			}
			
			p.yearClCtn.rn.innerHTML = me.yearClStartYear + 9;
		}
	};
	
	/**
	 * 年份快捷按钮，+或-几年
	 * @param v 需要加减年份的值
	 */
	this.qkbYearVal = function( v ){
		var me = DateXx1.me;
		me.fillForValue( 'yyyy', 1, v );
		me.drawPanel();
	};
	
	/**
	 * 组装月份区域
	 */
	this.fitMonthAr = function(){
		var me = DateXx1.me;
		var mth = me.date.getMonth();//月份值，实际值少1
		var mCtn = me.panel.monCtn;
		
		if( !mCtn.itd ){
			mCtn.ms = [];
			for( var i = 0; i < 12; i ++ ){
				var c = me.makeElm( 'DIV', mCtn, null, {'innerHTML':( i ) < 9? '0' + ( i + 1 ) : ( i + 1 ), 'val0' : ( i + 1 )} );
				mCtn.ms[i] = c;
				if(  mth == i ){
					c.className = 'monTs';
					mCtn.lastMonObj = c;
				}else{
					c.className = 'monTc';
				}
				
				c.onmousedown = function(){
					me.fillForValue( 'MM', 2, this.val0 );
					me.drawPanel( { 'fitYearCl01':'N' } );//重画面板时，不重画进度条
				};
			}
			mCtn.itd = true;
		}else{
			if( mCtn.lastMonObj.val0 != mth + 1 ){
				mCtn.lastMonObj.className = 'monTc';
				mCtn.ms[mth].className = 'monTs';
				mCtn.lastMonObj = mCtn.ms[mth];
			}
		}
	};
	
	/**
	 * 月份快捷按钮，上月1、上月末、下月1、下月末
	 * @param v 不通的逻辑处理
	 */
	this.qkbMonthForDay = function(v){
		var me = DateXx1.me;
		if( v == 1 ){	//上月1
			me.fillForValue( 'dd', 2, 1 );
			me.fillForValue( 'MM', 1, -1 );
		}else if( v == 2 ){		//上月末
			me.fillForValue( 'dd', 2, 0 );
		}else if( v == 3 ){		//下月1
			me.fillForValue( 'dd', 2, 1 );
			me.fillForValue( 'MM', 1, 1 );
		}else if( v == 4 ){		//下月末
			me.fillForValue( 'dd', 2, 1 );
			me.fillForValue( 'MM', 1, 2 );
			me.fillForValue( 'dd', 2, 0 );
		}
		me.drawPanel();
	};
	
	/**
	 * 组装日期区域
	 */
	this.fitDayAr = function(){
		var me = DateXx1.me;
		var d = me.date;
		var dayCtrBox = me.panel.dayCtrBox;
		var yyyy = d.getFullYear();
		var MM = d.getMonth() + 1;
		var dd = d.getDate();
		
		var temDate = new Date( yyyy, MM - 1, 1 );	//本月1号的日期
		var firstDayWk = temDate.getDay();//本月1号是星期几
		
		var temDate2 = new Date( yyyy, MM, 0 );	//本月最后1天的日期
		var maxDayNum = temDate2.getDate();//本月最后一天的号数
		
		if( !dayCtrBox.itd ){
			dayCtrBox.ds = [];
			for( var a = 0; a < 37; a ++ ){
				var c = me.makeElm( 'DIV', dayCtrBox, null, null, null );
				dayCtrBox.ds[a] = c;
				var ggd = a - firstDayWk + 1;
				if( a < firstDayWk ){
					c.className = 'dayTb';
				}else if( ggd <= maxDayNum ){
					c.innerHTML = ggd < 10? '0' + ggd : ggd;
					c.className = dd == ggd? 'dayTs' : 'dayTc';
					c.onmousedown = function(){
						me.fillForValue( 'dd', 2, this.innerHTML );
						me.drawPanel( { 'fitYearCl01':'N' } );//重画面板时，不重画进度条
						return false;
					};
					if( dd == ggd ){
						dayCtrBox.lastDayObj = c;
					}
				}else{
					c.className = 'dayTb';
				}
			}
			dayCtrBox.itd = true;
		}else{
			/*
			if( 如果上次的年和月与本次相同，可不重画日期区，简易处理 ){
				dayCtrBox.lastDayObj.className = 'dayTc';
				dayCtrBox.ds[ dd + firstDayWk - 1].className = 'dayTs';
				dayCtrBox.lastDayObj = dayCtrBox.ds[ dd + firstDayWk - 1];
			}else{*/
				for( var a = 0; a < 37; a ++ ){
					var c = dayCtrBox.ds[a];
					var ggd = a - firstDayWk + 1;
					if( a < firstDayWk ){
						c.className = 'dayTb';
						c.innerHTML = '';
						c.onmousedown = null;
					}else if( ggd <= maxDayNum ){
						c.innerHTML = ggd < 10? '0' + ggd : ggd;
						c.className = dd == ( ggd )? 'dayTs' : 'dayTc';
						c.onmousedown = function(){
							me.fillForValue( 'dd', 2, this.innerHTML );
							me.drawPanel( { 'fitYearCl01':'N' } );//重画面板时，不重画进度条
							return false;
						};
						dayCtrBox.lastDayObj = c;
					}else{
						c.className = 'dayTb';
						c.innerHTML = '';
						c.onmousedown = null;
					}
				}
			//}
		}
	};
	
	/**
	 * 日期快捷按钮，+或-几天
	 * @param v 需要加减天数的值
	 */
	this.qkbDayVal = function( v ){
		var w = parseInt( v, 10 );
		if( 'NaN' == '' + w ){
			return;
		}
		var me = DateXx1.me;
		me.fillForValue( 'dd', 1, w );
		me.drawPanel();
	};
	
	/**
	 * 组装小时的选择条
	 */
	this.fitHhCl = function(){
		var me = DateXx1.me;
		var p = me.panel;
		var hhClCtn = p.hhClCtn;
		
		if( !hhClCtn.hasCl ){
			me.makeElm( 'DIV', hhClCtn, 'clVl', { 'innerHTML' : '00' } );//小时选择条，最左边的00
			
			hhClCtn.clCells = [];//进度条单元格数组，24个
			for( var i = 0; i <= 23; i ++ ){
				var hhClCell = me.makeElm( 'DIV', hhClCtn, 'clVc w3', {'val0': i < 10? '0' + i : i } );//小时选择条，每个单元格
				hhClCtn.clCells[i] = hhClCell;
				
				if( me.date.getHours() == i ){
					hhClCell.className = 'clVs3';
					hhClCtn.lastHhClObj = hhClCell;
				}
				
				hhClCell.onmousedown = function(){
					me.fillForValue( 'HH', 2, this.val0 );
					me.drawPanel( { 'fitYearCl01':'N'} );
					return false;
				};
				
				//----鼠标over事件，提示tip---------
				hhClCell.onmouseover = function(){
					me.tip1( this, true, this.val0 );
				};
				
				//----鼠标over事件，提示tip---------
				hhClCell.onmouseout = function(){
					me.tip1( this, false, null );
				};
			}
			
			me.makeElm( 'DIV', hhClCtn, 'clVr', { 'innerHTML' : '23' } );//小时选择条，最右边的23
			
			//----4个小时快捷按钮--------
			var gVe = [-3, -1, 1, 3];
			for( var g = 0; g < 4; g ++ ){
				me.makeElm( 'hmsSmtBtnA', hhClCtn, 'hmsSmtBtnA', { 
					'val0' : gVe[g],
					'innerHTML' : gVe[g]>0? '+' + gVe[g] : gVe[g],
					'onmouseover' : function(){
						this.className = 'hmsSmtBtnB';
					},
					'onmouseout' : function(){
						this.className = 'hmsSmtBtnA';
					},
					'onmousedown' : function(){
						me.fillForValue( 'HH', 1, this.val0 );
						me.drawPanel( { 'fitYearCl01':'N'} );
					}
				} );
			}
			
			hhClCtn.hasCl = true;
		}else{
			if( parseInt( hhClCtn.lastHhClObj.val0, 10 ) != me.date.getHours() ){
				hhClCtn.lastHhClObj.className = 'clVc w3';
				hhClCtn.clCells[me.date.getHours()].className = 'clVs3';
				hhClCtn.lastHhClObj = hhClCtn.clCells[me.date.getHours()];
			}
		}
	};
	
	/**
	 * 组装分钟的选择条
	 */
	this.fitMmCl = function(){
		var me = DateXx1.me;
		var p = me.panel;
		var mmClCtn = p.mmClCtn;
		
		if( !mmClCtn.hasCl ){
			me.makeElm( 'DIV', mmClCtn, 'clVl', { 'innerHTML' : '00' } );//分钟选择条，最左边的00
			
			mmClCtn.clCells = [];//进度条单元格数组，60个
			for( var i = 0; i <= 59; i ++ ){
				var mmClCell = me.makeElm( 'DIV', mmClCtn, 'clVc w3', {'val0': i < 10? '0' + i : i } );//分钟选择条，每个单元格
				mmClCtn.clCells[i] = mmClCell;
				
				if( me.date.getMinutes() == i ){
					mmClCell.className = 'clVs3';
					mmClCtn.lastMmClObj = mmClCell;
				}
				
				mmClCell.onmousedown = function(){
					me.fillForValue( 'mm', 2, this.val0 );
					me.drawPanel( { 'fitYearCl01':'N'} );
				};
				
				//----鼠标over事件，提示tip---------
				mmClCell.onmouseover = function(){
					me.tip1( this, true, this.val0 );
				};
				
				//----鼠标over事件，提示tip---------
				mmClCell.onmouseout = function(){
					me.tip1( this, false, null );
				};
			}
			
			me.makeElm( 'DIV', mmClCtn, 'clVr', { 'innerHTML' : '59' } );//分钟选择条，最右边的59
			
			mmClCtn.hasCl = true;
		}else{
			if( parseInt( mmClCtn.lastMmClObj.val0, 10 ) != me.date.getMinutes() ){
				mmClCtn.lastMmClObj.className = 'clVc w3';
				mmClCtn.clCells[me.date.getMinutes()].className = 'clVs3';
				mmClCtn.lastMmClObj = mmClCtn.clCells[me.date.getMinutes()];
			}
		}
	};
	
	/**
	 * 组装秒的选择条
	 */
	this.fitSsCl = function(){
		var me = DateXx1.me;
		var p = me.panel;
		var ssClCtn = p.ssClCtn;
		
		if( !ssClCtn.hasCl ){
			me.makeElm( 'DIV', ssClCtn, 'clVl', { 'innerHTML' : '00' } );//秒选择条，最左边的00
			
			ssClCtn.clCells = [];//进度条单元格数组，60个
			for( var i = 0; i <= 59; i ++ ){
				var ssClCell = me.makeElm( 'DIV', ssClCtn, 'clVc w3', {'val0': i < 10? '0' + i : i } );//秒选择条，每个单元格
				ssClCtn.clCells[i] = ssClCell;
				
				if( me.date.getSeconds() == i ){
					ssClCell.className = 'clVs3';
					ssClCtn.lastSsClObj = ssClCell;
				}
				
				ssClCell.onmousedown = function(){
					me.fillForValue( 'ss', 2, this.val0 );
					me.drawPanel( { 'fitYearCl01':'N'} );
				};
				
				//----鼠标over事件，提示tip---------
				ssClCell.onmouseover = function(){
					me.tip1( this, true, this.val0 );
				};
				
				//----鼠标over事件，提示tip---------
				ssClCell.onmouseout = function(){
					me.tip1( this, false, null );
				};
			}
			
			me.makeElm( 'DIV', ssClCtn, 'clVr', { 'innerHTML' : '59' } );//秒选择条，最右边的59
			
			ssClCtn.hasCl = true;
		}else{
			if( parseInt( ssClCtn.lastSsClObj.val0, 10 ) != me.date.getSeconds() ){
				ssClCtn.lastSsClObj.className = 'clVc w3';
				ssClCtn.clCells[me.date.getSeconds()].className = 'clVs3';
				ssClCtn.lastSsClObj = ssClCtn.clCells[me.date.getSeconds()];
			}
		}
	};
	
	/**
	 * 时分秒快捷按钮，00:00:00、23:59:59、现在时间、今天日期
	 * @param v 不通的逻辑处理
	 */
	this.qkbHmsVal = function( v ){
		var me = DateXx1.me;
		if( v == 1 ){	//00:00:00
			me.fillForValue( 'HH', 2, 0 );
			me.fillForValue( 'mm', 2, 0 );
			me.fillForValue( 'ss', 2, 0 );
		}else if( v == 2 ){	//23:59:59
			me.fillForValue( 'HH', 2, 23 );
			me.fillForValue( 'mm', 2, 59 );
			me.fillForValue( 'ss', 2, 59 );
		}else if( v == 3 ){	//当前时间
			this.date = new Date();
			me.fillForValue( 'dd', 1, 0 );
		}else if( v == 4 ){	//今天日期
			var d = new Date();
			me.fillForValue( 'yyyy', 2, d.getFullYear() );
			me.fillForValue( 'MM', 2, d.getMonth() + 1 );
			me.fillForValue( 'dd', 2, d.getDate() );
		}else if( v == 5 ){	//清除时间
			me.focusElm.value = '';
		}
		me.drawPanel();
	};
	
	/**
	 * 小提示1，用于年、时、分、秒选择条的提示
	 */
	this.tip1 = function( o, showOrHide, txt ){
		var me = DateXx1.me;
		if( me.tip1Box == null ){
			me.tip1Box = me.makeElm( 'DIV', me.panel, 'tip1Box' );
		}
		if( showOrHide ){
			var pp = me.getElmOffset( me.panel );
			var p = me.getElmOffset( o );
			me.setElmStyle( me.tip1Box, { 'position' : 'absolute', 'display' : '', 'left' : ( p.left - pp.left - 34 ) + 'px', 'top' : ( p.top - pp.top + 9 ) + 'px' } );
			if( txt != null ){
				me.tip1Box.innerHTML = txt;
			}
		}else{
			me.setElmStyle( me.tip1Box, { 'display' : 'none' } );
		}
	};
	
	/**
	 * 取得面板
	 * @param param : 特殊逻辑控制参数
	 */
	this.drawPanel = function( param ){
		param = param == null? {} : param;
		var me = DateXx1.me;
		var p = me.panel;
		
		if( p != null ){
			if( param['fitYearVle01'] != 'N' ){
				p.yearVle.value = me.leftPadZero( me.date.getFullYear(), 4 );
			}
			if( param['fitYearCl01'] != 'N' ){
				me.fitYearCl( 0 );
			}
			me.fitMonthAr();
			me.fitDayAr();
			
			if( param['hhVle01'] != 'N' ){
				p.hhVle.value = me.leftPadZero( me.date.getHours(), 2 );
			}
			me.fitHhCl();
			
			if( param['mmVle01'] != 'N' ){
				p.mmVle.value = me.leftPadZero( me.date.getMinutes(), 2 );
			}
			me.fitMmCl();
			
			if( param['ssVle01'] != 'N' ){
				p.ssVle.value = me.leftPadZero( me.date.getSeconds(), 2 );
			}
			me.fitSsCl();
			
			p.hhCtn.style.display = me.format.indexOf( 'HH' ) > -1? '' : 'none';
			p.mmCtn.style.display = me.format.indexOf( 'mm' ) > -1? '' : 'none';
			p.ssCtn.style.display = me.format.indexOf( 'ss' ) > -1? '' : 'none';
			p.hsmLamCtn.style.display = me.format.indexOf( 'HH' ) > -1? '' : 'none';
			
			return p;
		}
		
		var lang = me.lang[me.lang['SET_LANGUAGE']];
		
		p = me.makeElm( 'DIV', document.body, 'dateXx1Panel', {
			//----邮件的菜单功能去掉---------
			'oncontextmenu': function( event ){
				var e = event || window.event;
				if( e.preventDefault ){
					e.preventDefault();
				}
				return false;
			}
		}, {'display':'none'} );
		me.panel = p;
		
		//====年=========================
		p.yearCtn = me.makeElm( 'DIV', p, 'yearCtn' );//年，总div
		p.yearVleCtn = me.makeElm( 'DIV', p.yearCtn, 'lftL h18 mrgR5' );//年份数字div
		p.yearVle = me.makeElm( 'input', p.yearVleCtn, 'yearVle', {'value':me.date.getFullYear(), 'maxLength':'4', 
			'onkeyup' : function( event ){
				var evt = event || window.event;
				if( 48 <= evt.keyCode && evt.keyCode <= 57 ){
					if( this.value.length > 2 ){
						me.fillForValue( 'yyyy', 2, this.value );
						me.drawPanel( {'fitYearVle01':'N'} );
					}
				}else{
					this.value = '';
					if( evt.preventDefault ){
						evt.preventDefault();
					}
					return false;
				}
			}
		} );//年份数字input框
		
		p.yearClCtn = me.makeElm( 'DIV', p.yearCtn, 'lftL h18' );//年份选择条容器div
		me.fitYearCl( 0 );//设置年选择条
		var onMouseFuncYear = " onmouseover=\"this.className='yearLamBtnB'\" onmouseout=\"this.className='yearLamBtnA'\" ";
		p.yearLamCnt = me.makeElm( 'DIV', p.yearCtn, 'lftL h18', {
			"innerHTML" : 
			"<div class=\"yearLamBtnA\"" + onMouseFuncYear + "onmousedown=\"DateXx1.me.qkbYearVal(-1);\">-1</div>" + 
			"<div class=\"yearLamBtnA\"" + onMouseFuncYear + "onmousedown=\"DateXx1.me.qkbYearVal(1);\">+1</div>" + 
			"<div class=\"yearLamBtnA\"" + onMouseFuncYear + "onmousedown=\"DateXx1.me.qkbYearVal(-10);\">&lt;-</div>" + 
			"<div class=\"yearLamBtnA\"" + onMouseFuncYear + "onmousedown=\"DateXx1.me.qkbYearVal(10);;\">-&gt;</div>"
		}, null );//年份快捷键容器div
		
		//====月========================
		p.monCtn = me.makeElm( 'DIV', p, 'monCtn' );//月分总div
		me.fitMonthAr();
		
		//====日期======================
		p.dayCtn = me.makeElm( 'DIV', p, 'dayCtn' );//日期，总div
		
		//----日期，左快捷操作区----
		var onMouseFuncDayLam = " onmouseover=\"this.className='dayLamBtnB'\" onmouseout=\"this.className='dayLamBtnA'\" ";
		var onMouseFuncDaySmt = " onmouseover=\"this.className='daySmtBtnB'\" onmouseout=\"this.className='daySmtBtnA'\" ";
		p.dayCtrLeft = me.makeElm( 'DIV', p.dayCtn, 'dayCtrLeft', {
			"innerHTML" : 
			"<div class=\"dayLamBtnA\"" + onMouseFuncDayLam + "onmousedown=\"DateXx1.me.qkbMonthForDay(1);\">" + lang.dayQkStr[0] + "</div>" + 
			"<div class=\"daySmtCnt\">" + 
				"<div class=\"daySmtBtnA\"" + onMouseFuncDaySmt + "onmousedown=\"DateXx1.me.qkbDayVal(-1);\">-1</div>" + 
				"<div class=\"daySmtBtnA\"" + onMouseFuncDaySmt + "onmousedown=\"DateXx1.me.qkbDayVal(-2);\">-2</div>" + 
			"</div>" + 
			"<div class=\"daySmtCnt\">" + 
				"<div class=\"daySmtBtnA\"" + onMouseFuncDaySmt + "onmousedown=\"DateXx1.me.qkbDayVal(-3);\">-3</div>" + 
				"<div class=\"daySmtBtnA\"" + onMouseFuncDaySmt + "onmousedown=\"DateXx1.me.qkbDayVal(-7);\">-7</div>" + 
			"</div>" + 
			"<div class=\"daySmtCnt\">" + 
				"<div class=\"daySmtBtnA\"" + onMouseFuncDaySmt + "onmousedown=\"DateXx1.me.qkbDayVal(-15);\">-15</div>" + 
				"<div class=\"daySmtBtnA\"" + onMouseFuncDaySmt + "onmousedown=\"DateXx1.me.qkbDayVal(-30);\">-30</div>" + 
			"</div>" + 
			"<div class=\"daySmtCnt2\">" + 
				"<span class=\"daySmtMns\">-</span><input type=\"text\" class=\"dayAjTxt0\" value=\"31\" maxlength=\"4\"><div class=\"daySmtBtnA\" " + onMouseFuncDaySmt + "onmousedown=\"DateXx1.me.qkbDayVal( -1 * DateXx1.me.panel.dayAjTxtLft.value );\">√</div>" + 
			"</div>" + 
			"<div class=\"dayLamBtnA\"" + onMouseFuncDayLam + "onmousedown=\"DateXx1.me.qkbMonthForDay(2);\">" + lang.dayQkStr[1] + "</div>"
		}, null );//日期，左操作区
		p.dayAjTxtLft = ( p.dayCtrLeft.getElementsByTagName( 'INPUT' ) )[0];
		
		//----日期，中间操作区----
		p.dayCtrCenter = me.makeElm( 'DIV', p.dayCtn, 'dayCtrCenter' );
		
		//----日期，星期抬头----
		p.wekCtn = me.makeElm( 'DIV', p.dayCtrCenter, 'wekCtn', {
			"innerHTML" : 
			"<div class=\"wekTc\">" + lang.weekStr[0] + "</div>" +
			"<div class=\"wekTc\">" + lang.weekStr[1] + "</div>" +
			"<div class=\"wekTc\">" + lang.weekStr[2] + "</div>" +
			"<div class=\"wekTc\">" + lang.weekStr[3] + "</div>" +
			"<div class=\"wekTc\">" + lang.weekStr[4] + "</div>" +
			"<div class=\"wekTc\">" + lang.weekStr[5] + "</div>" +
			"<div class=\"wekTc\">" + lang.weekStr[6] + "</div>"
		} );
		
		//----日期，所有日期单元格容器
		p.dayCtrBox = me.makeElm( 'DIV', p.dayCtrCenter, 'dayCtrBox' );
		me.fitDayAr();
		
		//----日期，右快捷操作区----
		p.dayCtrRight = me.makeElm( 'DIV', p.dayCtn, 'dayCtrRight', {
			"innerHTML" : 
			"<div class=\"dayLamBtnA\"" + onMouseFuncDayLam + "onmousedown=\"DateXx1.me.qkbMonthForDay(3);\">" + lang.dayQkStr[2] + "</div>" + 
			"<div class=\"daySmtCnt\">" + 
				"<div class=\"daySmtBtnA\"" + onMouseFuncDaySmt + "onmousedown=\"DateXx1.me.qkbDayVal(1);\">+1</div>" + 
				"<div class=\"daySmtBtnA\"" + onMouseFuncDaySmt + "onmousedown=\"DateXx1.me.qkbDayVal(2);\">+2</div>" + 
			"</div>" + 
			"<div class=\"daySmtCnt\">" + 
				"<div class=\"daySmtBtnA\"" + onMouseFuncDaySmt + "onmousedown=\"DateXx1.me.qkbDayVal(3);\">+3</div>" + 
				"<div class=\"daySmtBtnA\"" + onMouseFuncDaySmt + "onmousedown=\"DateXx1.me.qkbDayVal(7);\">+7</div>" + 
			"</div>" + 
			"<div class=\"daySmtCnt\">" + 
				"<div class=\"daySmtBtnA\"" + onMouseFuncDaySmt + "onmousedown=\"DateXx1.me.qkbDayVal(15);\">+15</div>" + 
				"<div class=\"daySmtBtnA\"" + onMouseFuncDaySmt + "onmousedown=\"DateXx1.me.qkbDayVal(30);\">+30</div>" + 
			"</div>" + 
			"<div class=\"daySmtCnt2\">" + 
				"<span class=\"daySmtMns\">+</span><input type=\"text\" class=\"dayAjTxt0\" value=\"31\" maxlength=\"4\"><div class=\"daySmtBtnA\"" + onMouseFuncDaySmt + "onmousedown=\"DateXx1.me.qkbDayVal( DateXx1.me.panel.dayAjTxtRgt.value );\">√</div>" + 
			"</div>" + 
			"<div class=\"dayLamBtnA\"" + onMouseFuncDayLam + "onmousedown=\"DateXx1.me.qkbMonthForDay(4);\">" + lang.dayQkStr[3] + "</div>"
		}, null );
		p.dayAjTxtRgt = ( p.dayCtrRight.getElementsByTagName( 'INPUT' ) )[0];
		
		//====时分秒区域====================
		p.hmsCtn = me.makeElm( 'DIV', p, 'hmsCtn' );//时分秒，总div
		
		//----小时区---------------
		p.hhCtn = me.makeElm( 'DIV', p.hmsCtn, 'h18', null, { 'display' : me.format.indexOf( 'HH' ) > -1? '' : 'none' } );//小时，div
		p.hhVleCtn = me.makeElm( 'DIV', p.hhCtn, 'lftL h18 mrgR5' );
		p.hhVle = me.makeElm( 'input', p.hhVleCtn, 'hmsVle', {'value':me.leftPadZero( me.date.getHours(), 2 ), 'maxLength':'2', 
			'onkeyup' : function(){
				var b = parseInt( this.value, 10 );
				if( 'NaN' == '' + b ){
					return;
				}
				if( b < 0 ){
					b = 0;
					p.hhVle.value = 0;
				}
				if( b > 23 ){
					b = 23;
					p.hhVle.value = 23;
				}
				me.fillForValue( 'HH', 2, b );
				me.drawPanel( { 'hhVle01':'N' } );
			}
		} );//小时数字input框
		p.hhClCtn = me.makeElm( 'DIV', p.hhCtn, 'lftL h18', { 'hasCl':false } );//小时，选择条容器div，hasCl：是否有进度条
		me.fitHhCl();//设置小时选择条
		
		//----分钟区---------------
		p.mmCtn = me.makeElm( 'DIV', p.hmsCtn, 'h18', null, { 'display' : me.format.indexOf( 'mm' ) > -1? '' : 'none' }  );//分钟，div
		p.mmVleCtn = me.makeElm( 'DIV', p.mmCtn, 'lftL h18 mrgR5' );
		p.mmVle = me.makeElm( 'input', p.mmVleCtn, 'hmsVle', {'value':me.leftPadZero( me.date.getMinutes(), 2 ), 'maxLength':'2', 
			'onkeyup' : function(){
				var b = parseInt( this.value, 10 );
				if( 'NaN' == '' + b ){
					return;
				}
				if( b < 0 ){
					b = 0;
					p.mmVle.value = 0;
				}
				if( b > 59 ){
					b = 59;
					p.mmVle.value = 59;
				}
				me.fillForValue( 'mm', 2, b );
				me.drawPanel( { 'mmVle01':'N' } );
			}
		} );//分钟数字input框
		p.mmClCtn = me.makeElm( 'DIV', p.mmCtn, 'lftL h18', { 'hasCl':false } );//分钟，选择条容器div，hasCl：是否有进度条
		me.fitMmCl();//设置分钟选择条
		
		//----秒区---------------
		p.ssCtn = me.makeElm( 'DIV', p.hmsCtn, 'h18', null, { 'display' : me.format.indexOf( 'ss' ) > -1? '' : 'none' }  );//秒，div
		p.ssVleCtn = me.makeElm( 'DIV', p.ssCtn, 'lftL h18 mrgR5' );
		p.ssVle = me.makeElm( 'input', p.ssVleCtn, 'hmsVle', {'value':me.leftPadZero( me.date.getSeconds(), 2 ), 'maxLength':'2', 
			'onkeyup' : function(){
				var b = parseInt( this.value, 10 );
				if( 'NaN' == '' + b ){
					return;
				}
				if( b < 0 ){
					b = 0;
					p.ssVle.value = 0;
				}
				if( b > 59 ){
					b = 59;
					p.ssVle.value = 59;
				}
				me.fillForValue( 'ss', 2, b );
				me.drawPanel( { 'ssVle01':'N' } );
			}
		} );//秒数字input框
		p.ssClCtn = me.makeElm( 'DIV', p.ssCtn, 'lftL h18', { 'hasCl':false } );//秒，选择条容器div，hasCl：是否有进度条
		me.fitSsCl();//设置秒选择条
		
		//====时分秒，快捷按钮区============
		var onMouseFuncHms = " onmouseover=\"this.className='hmsLamBtnB'\" onmouseout=\"this.className='hmsLamBtnA'\" ";
		p.hsmLamCtn = me.makeElm( 'DIV', p.hmsCtn, 'hsmLamCtn', {
			"innerHTML" : 
			"<div class=\"hmsLamBtnA\"" + onMouseFuncHms + "onmousedown=\"DateXx1.me.qkbHmsVal(1);\">00:00:00</div>" + 
			"<div class=\"hmsLamBtnA\"" + onMouseFuncHms + "onmousedown=\"DateXx1.me.qkbHmsVal(2);\">23:59:59</div>" + 
			"<div class=\"hmsLamBtnA\"" + onMouseFuncHms + "onmousedown=\"DateXx1.me.qkbHmsVal(3);\">" + lang.nowTimeStr + "</div>" + 
			"<div class=\"hmsLamBtnA\"" + onMouseFuncHms + "onmousedown=\"DateXx1.me.qkbHmsVal(5);\">" + lang.cleanDateStr + "</div>"
		}, {
			'display' : me.format.indexOf( 'HH' ) > -1? '' : 'none'
		} );
		
		return p;
	};
	
	/**
     * (鼠标移上/点击)，激活时间控件
     */
	this.activate = function( event, format ){
		var evt = event || window.event;
		this.focusElm = evt.target || evt.srcElement;
		this.focusOrigClassName = this.focusElm.className;
		this.format = format;
		var ds = this.trim( this.focusElm.value );
		//根据格式解析出日期对象
		if( ds == '' ){
			this.date = new Date();
		}else{
			//----验证值-------
			if( !this.verifyDateFormat( ds, format ) ){
				this.date = new Date();
			}else{
				this.date = this.parseDate( ds, format );
			}
			ds = this.formatDate( this.date, format );
		}
		
		this.focusElm.className = this.focusElm.className + ' dateXx1FocusElmStyle';
		this.lastFocusElmValue = ds;
		this.focusElm.value = ds;
		this.adsbItem = 'dd';
		this.cursorOn = true;
		
		//====赋予当前触发元素的鼠标按下事件执行函数============
		if( this.focusElm.onclick == null ){
			this.focusElm.onclick = function( event ){
				var evt = event || window.event;
				var me = DateXx1.me;
				var cd = new Date();
				
				//根据(鼠标)点击的位置，判断出时间项值(年/月/日/时/分/秒)，用于滚轮时加减该项的值
				var cp = me.getCursorPosition( me.focusElm );
				var im = 'dd';//要加减的项
				var fmt = me.format;
				var yyyyP = fmt.indexOf( 'yyyy' );
				var MMP = fmt.indexOf( 'MM' );
				var ddP = fmt.indexOf( 'dd' );
				var HHP = fmt.indexOf( 'HH' );
				var mmP = fmt.indexOf( 'mm' );
				var ssP = fmt.indexOf( 'ss' );
				
				if( yyyyP <= cp && cp <= yyyyP + 4 ){
					im = 'yyyy';
				}else if( MMP <= cp && cp <= MMP + 2 ){
					im = 'MM';
				}else if( ddP <= cp && cp <= ddP + 2 ){
					im = 'dd';
				}else if( HHP <= cp && cp <= HHP + 2 ){
					im = 'HH';
				}else if( mmP <= cp && cp <= mmP + 2 ){
					im = 'mm';
				}else if( ssP <= cp && cp <= ssP + 2 ){
					im = 'ss';
				}
				me.adsbItem = im;
			};
		}
		
		//====赋予当前触发元素的鼠标弹起事件执行函数============
		if( this.focusElm.onmouseup == null ){
			this.focusElm.onmouseup = function( event ){
				var evt = event || window.event;
				var me = DateXx1.me;
				var cd = new Date();
				
				//与上次的点击时间间隔小于270毫秒，即类似双击，触发一些事件
				if( cd.getTime() - me.focusClickTime < 270 ){
					//----当按住了ctrl(17)键，清空焦点元素的值---
					if( me.focusDownKeyMap['k17'] ){
						me.focusElm.value = '';
					}else{
						me.focusElm.value = me.formatDate( cd, me.format );
						me.date = cd;
						me.drawPanel();
					}
					me.focusClickTime = 0;
				}else{
					me.focusClickTime = cd.getTime();
				}
			};
		}
		
		//====赋予当前触发元素的鼠标移出事件执行函数============
		if( this.focusElm.onmouseout == null ){
			this.focusElm.onmouseout = function( event ){
				DateXx1.me.cursorOn = false;
				var me = DateXx1.me;
				if( me.focusElm.value != '' && !me.verifyDateFormat( me.focusElm.value, me.format ) ){
					me.focusElm.value = me.formatDate( new Date(), me.format );
				}
				DateXx1.me.focusElm.className = DateXx1.me.focusOrigClassName;
				window.setTimeout( function(){
					if( !DateXx1.me.cursorOn ){
						DateXx1.me.panel.style.display = 'none';
					}
				},1 );
			};
		}
		
		//====赋予当前触发元素的按键(弹起)事件执行函数============
		if( this.focusElm.onkeyup == null ){
			this.focusElm.onkeyup = function( event ){
				var evt = event || window.event;
				var me = DateXx1.me;
				if( me.lastFocusElmValue != this.value ){
					if( this.value.length == me.format.length ){
						var d = me.parseDate( this.value, me.format );
						if( d != null && 'NaN' != '' + d && 'Invalid Date' != '' + d ){
							me.date = d;
							me.drawPanel( { 'mmVle01':'N' } );
							var dv = me.formatDate( me.date, me.format );
							if( dv != me.focusElm.value ){
								me.focusElm.value = dv;
								me.lastFocusElmValue = dv;
							}
						}
					}
				}
			};
		}
		
		//====显示控件的主面板============
		this.panel = this.drawPanel();
		if( this.panel.onmouseout == null ){
			this.panel.onmouseout = function( event ){
				DateXx1.me.cursorOn = false;
				var me = DateXx1.me;
				if( me.focusElm.value != '' && !me.verifyDateFormat( me.focusElm.value, me.format ) ){
					me.focusElm.value = me.formatDate( new Date(), me.format );
				}
				DateXx1.me.focusElm.className = DateXx1.me.focusOrigClassName;
				window.setTimeout( function(){
					if( !DateXx1.me.cursorOn ){
						DateXx1.me.panel.style.display = 'none';
					}
				},1 );
			};
			
			this.panel.onmouseover = function( event ){
				DateXx1.me.cursorOn = true;
				DateXx1.me.focusElm.className = DateXx1.me.focusElm.className + ' dateXx1FocusElmStyle';
			};
		}
		var eos = this.getElmOffset( this.focusElm );
		
		this.setElmStyle( this.panel, {'display' : '', 'top' : (eos.bottom ) + 'px'} );
		//----如果面板的右端超过了屏幕的宽度，左移一段距离------
		if( eos.left + this.panel.offsetWidth > document.body.clientWidth ){
			this.panel.style.left = ( document.body.clientWidth - this.panel.offsetWidth ) + 'px';
		}else{
			this.panel.style.left = eos.left + 'px';
		}
	};
	
	/**
	 * 初始化
	 */
	this.init = function(){
		//====设置CSS样式=======================
		DateXx1.me.appendCss();
		
		var obt = document.attachEvent? 1 : ( document.addEventListener? 2 : 3 );
		if( obt == 1 ){
			//====赋予滚轮事件执行函数============
			document.attachEvent( 'onmousewheel', function( event ){
				return DateXx1.me.mouseWheel( event );
			});
			
			//====赋予按键(按下)时间执行函数==========
			document.body.attachEvent( 'onkeydown', function( event ){
				var evt = event || window.event;
				DateXx1.me.focusDownKeyMap['k'+evt.keyCode] = true;
			} );
			
			//====赋予按键(弹起)时间执行函数==========
			document.body.attachEvent( 'onkeyup', function( event ){
				var evt = event || window.event;
				DateXx1.me.focusDownKeyMap['k'+evt.keyCode] = false;
			} );
			
		}else if( obt == 2 ){
			//====赋予滚轮事件执行函数============
			document.addEventListener( this.isBroswerType( ['Firefox'] )?"DOMMouseScroll":"mousewheel", DateXx1.me.mouseWheel, false );
			
			//====赋予按键(按下)时间执行函数==========
			document.body.addEventListener( 'keydown', function( event ){
				var evt = event || window.event;
				DateXx1.me.focusDownKeyMap['k'+evt.keyCode] = true;
			}, false);
			
			//====赋予按键(弹起)时间执行函数==========
			document.body.addEventListener( 'keyup', function( event ){
				var evt = event || window.event;
				DateXx1.me.focusDownKeyMap['k'+evt.keyCode] = false;
			}, false);
		}
		
	};
	
	/**
	 * 用到的样式
	 */
	this.appendCss = function(){
		var st = document.createElement( "style" );
		st.type="text/css";
		if( st.styleSheet ){
			st.styleSheet.cssText = this.cssTxt;
		} else {
			st.innerHTML = this.cssTxt;
		}
		document.body.appendChild( st );
	};
};

/**
 * 用于引用DateXx1实例化后的对象
 */
DateXx1.me = null;

/**
 * 本js文件所在的路径
 */
DateXx1.path = (function(){
	var jsSrc = document.scripts[document.scripts.length - 1].src;
	return jsSrc.substring( 0, jsSrc.lastIndexOf( "/" ) + 1 );
})();

/**
 * 焦点(元素)显示控件
 */
DateXx1.activate = function( event, format ){
	var x = DateXx1.me;
	if( x == null ){
		x = new DateXx1();
		DateXx1.me = x;
		x.init();
	}
	x.activate( event, format );
};

window.DateXx1 = DateXx1;

})();


/*
 * 控件的整体结构
 */
/*
<div class="dateXx1Panel" style="display:none1;">
	<div class="yearCtn"><!--yearCtn-->
		<div class="lftL h18 mrgR5"><!--yearVleCtn-->
			<input type="text" class="yearVle" value="2014" maxlength="4" /><!--yearVle-->
		</div>
		<div class="lftL h18"><!--yearClCtn-->
			<div class="clVl">2008</div>
			<div class="clVc w6"></div>
			<div class="clVc w6"></div>
			<div class="clVc w6"></div>
			<div class="clVc w6"></div>
			<div class="clVc w6"></div>
			<div class="clVs6"></div>
			<div class="clVc w6"></div>
			<div class="clVc w6"></div>
			<div class="clVc w6"></div>
			<div class="clVc w6"></div>
			<div class="clVc w6"></div>
			<div class="clVr">2020</div>
		</div>
		<div class="lftL h18"><!--yearLamCnt-->
			<div class="yearLamBtnA" onmouseover="this.className='yearLamBtnB'" onmouseout="this.className='yearLamBtnA'">-1</div>
			<div class="yearLamBtnA" onmouseover="this.className='yearLamBtnB'" onmouseout="this.className='yearLamBtnA'">+1</div>
			<div class="yearLamBtnA" onmouseover="this.className='yearLamBtnB'" onmouseout="this.className='yearLamBtnA'">&lt;-</div>
			<div class="yearLamBtnA" onmouseover="this.className='yearLamBtnB'" onmouseout="this.className='yearLamBtnA'">-&gt;</div>
		</div>
	</div>
	
	<div class="monCtn"><!--monCtn-->
		<div class="monTc">01</div>
		<div class="monTc">02</div>
		<div class="monTc">03</div>
		<div class="monTc">04</div>
		<div class="monTc">05</div>
		<div class="monTc">06</div>
		<div class="monTc">07</div>
		<div class="monTc">08</div>
		<div class="monTs">09</div>
		<div class="monTc">10</div>
		<div class="monTc">11</div>
		<div class="monTc">12</div>
	</div>
	
	<div class="dayCtn" ><!--dayCtn-->
		<div class="dayCtrLeft"><!--dayCtrLeft-->
			<div class="dayLamBtnA" onmouseover="this.className='dayLamBtnB'" onmouseout="this.className='dayLamBtnA'">上月1</div>
			<div class="daySmtCnt">
				<div class="daySmtBtnA" onmouseover="this.className='daySmtBtnB'" onmouseout="this.className='daySmtBtnA'">-1</div>
				<div class="daySmtBtnA">-2</div>
			</div>
			<div class="daySmtCnt">
				<div class="daySmtBtnA">-3</div>
				<div class="daySmtBtnA">-7</div>
			</div>
			<div class="daySmtCnt">
				<div class="daySmtBtnA">-15</div>
				<div class="daySmtBtnA">-30</div>
			</div>
			<div class="daySmtCnt2">
				<span class="daySmtMns">-</span><input type="text" class="dayAjTxt0" value="30"><div class="daySmtBtnA">√</div>
			</div>
			<div class="dayLamBtnA">上月末</div>
		</div>
		<div class="dayCtrCenter"><!--dayCtrCenter-->
			<div class="wekCtn"><!--wekCtn-->
				<div class="wekTc">日</div>
				<div class="wekTc">一</div>
				<div class="wekTc">二</div>
				<div class="wekTc">三</div>
				<div class="wekTc">四</div>
				<div class="wekTc">五</div>
				<div class="wekTc">六</div>
			</div>
			<div class="dayCtrBox"><!--dayCtrBox-->
				<div class="dayTb"></div>
				<div class="dayTb"></div>
				<div class="dayTb"></div>
				<div class="dayTb"></div>
				<div class="dayTb"></div>
				<div class="dayTb"></div>
				<div class="dayTc">01</div>
				<div class="dayTc">02</div>
				<div class="dayTc">03</div>
				<div class="dayTc">04</div>
				<div class="dayTc">05</div>
				<div class="dayTc">06</div>
				<div class="dayTc">07</div>
				<div class="dayTc">08</div>
				<div class="dayTc">09</div>
				<div class="dayTc">10</div>
				<div class="dayTc">11</div>
				<div class="dayTc">12</div>
				<div class="dayTc">13</div>
				<div class="dayTc">14</div>
				<div class="dayTc">15</div>
				<div class="dayTc">16</div>
				<div class="dayTc">17</div>
				<div class="dayTc">18</div>
				<div class="dayTc">19</div>
				<div class="dayTc">20</div>
				<div class="dayTc">21</div>
				<div class="dayTc">22</div>
				<div class="dayTc">23</div>
				<div class="dayTc">24</div>
				<div class="dayTc">25</div>
				<div class="dayTc">26</div>
				<div class="dayTs">26</div>
				<div class="dayTc">28</div>
				<div class="dayTc">29</div>
				<div class="dayTc">30</div>
				<div class="dayTc">31</div>
			</div>
		</div>
		<div class="dayCtrRight"><!--dayCtrRight-->
			<div class="dayLamBtnA">下月1</div>
			<div class="daySmtCnt">
				<div class="daySmtBtnA">+1</div>
				<div class="daySmtBtnA">+2</div>
			</div>
			<div class="daySmtCnt">
				<div class="daySmtBtnA">+3</div>
				<div class="daySmtBtnA">+7</div>
			</div>
			<div class="daySmtCnt">
				<div class="daySmtBtnA">+15</div>
				<div class="daySmtBtnA">+30</div>
			</div>
			<div class="daySmtCnt2">
				<span class="daySmtMns">+</span><input type="text" class="dayAjTxt0" value="30"><div class="daySmtBtnA">√</div>
			</div>
			<div class="dayLamBtnA">下月末</div>
		</div>
	</div>
	
	<div class="hmsCtn"><!--hmsCtn-->
		<div class="h18"><!--hhCtn-->
			<div class="lftL h18 mrgR5"><!--hhVleCtn-->
				<input type="text" class="hmsVle" value="09" /><!--hhVle-->
			</div>
			<div class="lftL h18"><!--hhClCtn-->
				<div class="clVl">00</div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVs3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVr">23</div>
				
				<div class="hmsSmtBtnA" onmouseover="this.className='hmsSmtBtnB';" onmouseout="this.className='hmsSmtBtnA';">-3</div>
				<div class="hmsSmtBtnA" onmouseover="this.className='hmsSmtBtnB';" onmouseout="this.className='hmsSmtBtnA';">-1</div>
				<div class="hmsSmtBtnA" onmouseover="this.className='hmsSmtBtnB';" onmouseout="this.className='hmsSmtBtnA';">+1</div>
				<div class="hmsSmtBtnA" onmouseover="this.className='hmsSmtBtnB';" onmouseout="this.className='hmsSmtBtnA';">+3</div>
			</div>
		</div>
		
		<div class="h18"><!--mmCtn-->
			<div class="lftL h18 mrgR5"><!--mmVleCtn-->
				<input type="text" class="hmsVle" value="23" /><!--mmVle-->
			</div>
			<div class="lftL h18"><!--mmClCtn-->
				<div class="clVl">00</div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>

				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVs3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>

				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>

				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>

				<div class="clVr">59</div>
			</div>
		</div>
		
		<div class="h18"><!--ssCtn-->
			<div class="lftL h18 mrgR5"><!--ssVleCtn-->
				<input type="text" class="hmsVle" value="35" /><!--ssVle-->
			</div>
			<div class="lftL h18"><!--ssClCtn-->
				<div class="clVl">00</div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3" ></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>

				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>

				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVs3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>

				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>
				<div class="clVc w3"></div>

				<div class="clVr">59</div>
			</div>
		</div>
		
		<div class="hsmLamCtn"><!--hsmLamCtn-->
			<div class="hmsLamBtnA" onmouseover="this.className='hmsLamBtnB';" onmouseout="this.className='hmsLamBtnA';">00:00:00</div>
			<div class="hmsLamBtnA" onmouseover="this.className='hmsLamBtnB';" onmouseout="this.className='hmsLamBtnA';">23:59:59</div>
			<div class="hmsLamBtnA" onmouseover="this.className='hmsLamBtnB';" onmouseout="this.className='hmsLamBtnA';">当前时间</div>
			<div class="hmsLamBtnA" onmouseover="this.className='hmsLamBtnB';" onmouseout="this.className='hmsLamBtnA';">今日日期</div>
		</div>
	</div>
</div>
*/