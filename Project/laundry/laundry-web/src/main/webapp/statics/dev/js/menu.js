/**
 * 菜单类
 * @params menuId : 菜单ul的id
 * @params myMenuVarName : 初始化时定义的变量名
 */
function MyMenu( menuUlId, myMenuVarName ){
	this.mnWidth = 160;	//菜单项的宽度
	this.mnSubHeight = 24;//二级以下菜单的高度
	this.mouseoutSeq = 0;
	this.mouseoutMap = {};
	this.menuUl = document.getElementById( menuUlId );
	this.mnList = [];//所有一级菜单对象
	this.mnCount = 0;//一级菜单总数
	this.mnCrtInd = 1;//当前一级菜单页码
	this.mnPgSize = 1;	//每页菜单的数量
	this.mnPgCount = 0;	//总页数
	this.pgLiWidth = 22;	//翻页li的宽度
	this.pgUp = document.getElementById( menuUlId + 'PgUp' );	//向前翻页按钮的id
	this.pgDown = document.getElementById( menuUlId + 'PgDown' );//向后翻页按钮的id
	
	/**
	 * 初始化,(new后被调用)
	 */
	this.init = function(){
		//----所有一级菜单----
		var mn1s = this.menuUl.childNodes;
		for( var f = 0; f < mn1s.length; f ++ ){
			if( mn1s[f] && mn1s[f].tagName == 'LI' ){
				this.mnList[this.mnList.length] = mn1s[f];
			}
		}
		this.mnList.splice( 0,1 );
		this.mnCount = this.mnList.length;
		
		//----所有li绑定鼠标事件----
		var liList = this.menuUl.getElementsByTagName( "li" );
		for( var j = 0; j < liList.length; j ++ ){
			if(j == 0 ){
				continue;
			}
			liList[j].onmouseover = function( event ){
				var subList = this.childNodes;
				var m = window['$mnMain'];
				for( var k = 0; k < subList.length; k ++ ){
					if( subList[k] && subList[k].tagName == 'UL' ){
						subList[k].myDisp = "1";
						subList[k].className = 'mnSub';
						if( window[myMenuVarName].getMenuLevel( subList[k] ) >= 3 ){
							if( document.all ){
								var tgt = subList[k].parentNode;
								subList[k].style.position = 'absolute';
								subList[k].style.left = ( tgt.clientWidth + tgt.offsetLeft ) + "px";
								subList[k].style.top = ( tgt.offsetTop ) + "px";
							}else{
								subList[k].style.position = 'relative';
								subList[k].style.left = m.mnWidth + 'px';
								subList[k].style.top = '-' + m.mnSubHeight + 'px';
							}
						}
						break;
					}
				}
			};
			
			liList[j].onmouseout = function( event ){
				var subList = this.childNodes;
				for( var k = 0; k < subList.length; k ++ ){
					if( subList[k] && subList[k].tagName == 'UL' ){
						subList[k].myDisp = "0";
						if( document.all ){
							var me = window[myMenuVarName];
							me.mouseoutSeq = me.mouseoutSeq + 1;
							me.mouseoutMap['' + me.mouseoutSeq] = subList[k];
							window.setTimeout( myMenuVarName + ".hideSub('" + me.mouseoutSeq + "')", 50 );
						}else{
							subList[k].className = 'mnSub mnSubHidden';
						}
						break;
					}
				}
			};
		}
		
		this.menuUl.style.display = '';
	};
	
	/**
	 * 隐藏子菜单
	 */
	this.hideSub = function( seq ){
		if( this.mouseoutMap[seq].myDisp == "0" ){
			this.mouseoutMap[seq].className = 'mnSub mnSubHidden';
			this.mouseoutMap[seq] = null;
		}
	};
	
	/**
	 * 取得菜单的级数(从1开始)
	 */
	this.getMenuLevel = function( ul ){
		var o = ul;
		var lev = 0;
		while( o ){
			if( o.tagName == 'UL' ){
				lev = lev + 1;
				if( o.id == menuUlId ){
					return lev;
				}
			}
			o = o.parentNode;
		}
	};
	
	/**
	 * 高亮菜单，根据页面的地址会自动找到匹配的链接地址，并顺级高亮
	 */
	this.highLightMenu = function (){
		//【正式的取funcCode的方法，正式使用时，放开下2行注释】
		var funcCode = '' + window.location.href;
		funcCode = funcCode.substring( 0, funcCode.lastIndexOf( '.do' ) );
		//funcCode = 'stock/goodsbaseinfo/toQueryGoodsBaseInfo';
		
		//【模拟的funcCode,正式使用时，注释掉下1行，TODO...】
		//var funcCode = 'www.baidu.com/4.2.4.4.4.3';
		
		var aList = this.menuUl.getElementsByTagName( 'A' );
		var mn1 = null;
		for( var i = 0; i < aList.length; i ++ ){
			var f = aList[i];
			if( f.id == '$mnMainPgUp' || f.id == '$mnMainPgDown' ){
				continue;
			}
			
			var maLk = '' + f.href;
			if( maLk.indexOf( funcCode ) >= 0 || funcCode.indexOf( maLk ) >= 0 ){
				f.className = f.className + ' choosed';
				//alert( '找到了, mnLink:' + maLk + ', funcCode:' + funcCode + ", className:" + f.className);

				while( f ){
					if( f.tagName == 'LI' ){
						f.className = f.className + ' choosed';
					}
					
					if( f == this.menuUl ){
						break;
					}
					
					mn1 = f;
					f = f.parentNode;
				}
				break;
			}
		}
		return mn1;
	};
	
	/**
	 * 自动调整菜单个数，翻页
	 */
	this.autoAdjust = function(){
		var mnCrt = this.highLightMenu();
		
		//----根据宽度和一级菜单总个数计算出/每页的一级菜单的个数，总页数----
		this.mnPgSize = parseInt( "" + ( ( this.menuUl.parentNode.scrollWidth - this.pgLiWidth - 10 ) / this.mnWidth ), 10 );//每页的一级菜单的个数
		this.mnPgCount = parseInt( "" + ( this.mnCount /this.mnPgSize ), 10 ) + ( this.mnCount % this.mnPgSize == 0? 0 : 1 );	//一级菜单的总页数
		
		//----找出当前一级菜单所在的页----
		for( var c = 0; c < this.mnList.length; c ++ ){
			if( this.mnList[c] == mnCrt ){
				this.mnCrtInd = parseInt( '' + ( ( c + 1 )/this.mnPgSize ), 10 ) + ( ( c + 1 ) % this.mnPgSize == 0? 0 : 1 );
				break;
			}
		}
		
		//----定位到该页----
		this.flipPg( 0 );
		
	};
	
	/**
	 * 翻页
	 * @param g : 取值为0或1或-1，0:到自己所在的页，1:往后翻页，-1:往前翻页
	 */
	this.flipPg = function( g ){
		if( ( this.mnCrtInd == 1 && g == -1 ) || ( this.mnCrtInd == this.mnPgCount && g == 1 ) ){
			return;
		}
		
		this.mnCrtInd = this.mnCrtInd + g;
		this.mnCrtInd = this.mnCrtInd < 1? 1 : ( this.mnCrtInd > this.mnPgCount? this.mnPgCount : this.mnCrtInd );
		
		var minInd = ( this.mnCrtInd - 1 ) * this.mnPgSize;
		var maxInd = this.mnCrtInd * this.mnPgSize;
		for( var c = 0; c < this.mnList.length; c ++ ){
			this.mnList[c].style.display = ( c >= minInd && c < maxInd )?'':'none';
		}
		
		this.pgUp.className = this.mnCrtInd<=1? 'upN' : 'upY';
		this.pgDown.className = this.mnCrtInd>=this.mnPgCount? 'downN' : 'downY';
	};
	
};


( function(){
	var a = window.attachEvent || window.addEventListener;
	var p = window.attachEvent? "on" : '';
	
	a( p + 'load', function(){
		window['$mnMain'] = new MyMenu( '$mnMain', '$mnMain' );
		window['$mnMain'].init();
		window['$mnMain'].autoAdjust();
	} );
	
	a( p + 'resize', function(){
		window['$mnMain'].autoAdjust();
	} );
	
} )();