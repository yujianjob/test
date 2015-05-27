/**
 * SelectXx1.js，下拉框控件,源码
 * Version: 1.0
 * Date: 2014-09-30
 * Author: dsine
 * 操作要点：(1)支持源触发框，输入可筛选。(2)键盘上下键，选取选项(高亮)。(3)回车键选取
 * 剩余问题，高亮定位滚动条，值不准
 */
(function(){

var SelectXx1 = {
	/**
	 * css样式
	 */
	'cssTxt' : 
		".SelectXx1FocusElmStyle{background:#EEEFF8;color:#0033FF;}" +
		".SelectXx1WidthHelper{position:absolute;top:0px;left:0px;padding:0px 0px 0px 2px;visibility:hidden;background:red;}" +
		".SelectXx1Panel{position:absolute;background:#E5E5F5;padding:1px;border-top:1px solid #FFF;border-left:1px solid #99AABB;border-right:1px solid #667788;border-bottom:1px solid #445560;font-family:宋体;font-size:12px;overflow-x:hidden;overflow-y:auto;filter:alpha(Opacity=90);-moz-opacity:0.90;opacity:0.90;}" +
		".SelectXx1Panel .opt{height:20px;}" +
		".SelectXx1Panel .optCkb{float:left;width:20px;line-height:10px;height:10px;cursor:pointer;margin-top:0px;}" +
		".SelectXx1Panel .optTxt{float:left;line-height:20px;height:20px;cursor:pointer;color:#220011;padding:0px 0px 0px 2px;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;}" +
		".SelectXx1Panel .chsd{background:#D5D5E5;color:green;}" +
		".SelectXx1Panel .msor{background:#1C70F9;color:#FFF;}",
	
	/**
	 * 加载过css标志
	 */
	'appendCssFlag' : false,
	
	/**
	 * 加载css样式
	 */
	'appendCss' : function(){
		var st = document.createElement( "style" );
		st.type="text/css";
		if( st.styleSheet ){
			st.styleSheet.cssText = SelectXx1.cssTxt;
		} else {
			st.innerHTML = SelectXx1.cssTxt;
		}
		document.body.appendChild( st );
	},
	
	/**
	 * 创建元素
	 * @param tagName : 元素名，DIV，【必填】
	 * @param parentNode : 父节点对象，【可选】
	 * @param className : css样式名称，【可选】
	 * @param attributes : 属性对象，【可选】
	 * @param  styleProperties : 样式属性对象，【可选】
	 */
	'makeElm' : function( tagName, parentNode, className, attributes, styleProperties ){
		var e = document.createElement( tagName );
		
		if( attributes ){
			for( var j in attributes ){
				e[j] = attributes[j];
			}
		}
		
		if( className ){
			e.className = className;
		}
		
		if( styleProperties ){
			for( var i in styleProperties ){
				e.style[i] = styleProperties[i];
			}
		}
		
		if( parentNode ){
			parentNode.appendChild( e );
		}
		
		return e;
	},
	
	/**
	 * (给对象)添加事件
	 */
	'addEvent' : function( o, eventName, func ){
		if( o.attachEvent ){
			o.attachEvent( 'on' + eventName, func );
		}else{
			o.addEventListener( eventName, func, false);
		}
	},
	
	/**
	 * 取得元素相对于页面的位置，绝对值，返回left，right, top，bottom
	 * @param o : 目标元素对象
	 */
	'getElmOffset' : function( o ){
	    var l = o.offsetLeft;
	    var t = o.offsetTop;
	    if( o.offsetParent ){
	    	var p = SelectXx1.getElmOffset( o.offsetParent );
	        l = l + p.left;
	        t = t + p.top;
	    }
	    return { "left" : l, "right" : ( l + o.offsetWidth ), "top" : t, "bottom" : ( t + o.offsetHeight )};
	},
	
	/**
	 * 设置元素的样式
	 * @param o : 目标元素对象
	 * @param m : 样式属性，对象，例如：{'display':'none','left':'20px'}
	 */
	'setElmStyle' : function( o, m ){
		for( var i in m ){
			o.style[i] = m[i];
		}
	},
	
	/**
	 * 转义html关键字符
	 */
	'escapeHtml' : function( str ){
		var txt = str.replace( /&/g, "&amp;" )
				 .replace( /[<]/g, "&lt;" )
				 .replace( /[>]/g, "&gt;" )
				 .replace( /\n/g, "<br>" )
				 .replace( /\s/g, "&nbsp;" );
		return txt;
	},
	
	/**
	 * ajax请求函数，统一POST方式，异步方式
	 * @param url : 请求地址
	 * @param params : 参数，对象，value支持字符串和数组
	 * @param callback : 回调函数
	 */
	'ajax' : function( url, params, callback ){
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
		
		x.onreadystatechange = function(){
		    if( x.readyState == 4  ){
		    	if( x.status == 200 ){
		    		callback( x.responseText );
		    	}
		    }
		};
		
		x.send( paramAry.join( '&' ) );
	},
	
	/**
	 * 合并对象属性，将参数中的所有对象的属性合并到一个新对象上，排在后面的对象的属性会覆盖前面对象的相同属性
	 */
	'mergeObjectAttrs' : function( os ){
		var g = {};
		for( var i = 0; i < os.length; i ++ ){
			if( os[i] ){
				for( var j in os[i] ){
					g[j] = os[i][j];
				}
			}
		}
		return g;
	},
	
	/**
	 * 默认的定制参数值
	 */
	'defaultCustomParams' : {
		/**
		 * 是否支持多选
		 */
		'multiSelectFlag' : true,
		
		/**
		 * 是否有第一项，默认：true
		 */
		'hasFirstOpt' : true,
		
		/**
		 * 第一项的文本(通常是全部、-----、空白之类)，默认：--------
		 */
		'firstOptText' : '--------',
		
		/**
		 * 第一项的值，默认：空字符串
		 */
		'firstOptValue' : '',
		
		/**
		 * 第一项的文本，当选择这项时，填入到源文本框中的值
		 */
		'firstOptTextInSrcElm' : '',
		
		/**
		 * 选项模式，取值：textValue(文本+值), text(文本), valueText(值+文本)，默认：text
		 */
		'optMode' : 'text',
		
		/**
		 * 选项中文本和值的分隔符，默认：-
		 */
		'optDelimiter' : '-',
		
		/**
		 * (目标元素上)显示的文本模式，取值：textValue(文本+值), text(文本), valueText(值+文本), value(值)，默认：text
		 */
		'showTextMode' : 'text',
		
		/**
		 * 多选时，(目标元素上)显示文本值之间的分隔符
		 */
		'showTextDelimiter' : ',',
		
		/**
		 * (源文本框上)输入监听处理，取值：【filt】:过滤，仅单选时生效；【match】:匹配(仅多选时生效，建议适用于匹配模式为textValue时)；【none】:什么都不做
		 */
		'inputListen' : 'none',
		
		/**
		 * 输入时匹配选项的模式，取值：textValue(文本+值), text(文本), valueText(值+文本), value(值)，默认：value
		 */
		'inputMatchMode' : 'text',
		
		/**
		 * 宽度限制
		 */
		'widthMax' : 400,
		
		/**
		 * 高度限制
		 */
		'heightMax' : 300,
		
		/**
		 * 当选中某项时，回调函数。【重写时实现具体的内容】
		 * @param params : 参数对象，里包含的值如下：
		 * srcElm : 源文本框，里面有用的值: { value : 文本框显示的值, values ：真实的值数组, texts : 文本内容数组 }
		 * panel : 面板对象，里面有用的值：{ checkedCkbMap : 选中的勾选框Map, checkboxes : 所有勾选框数组 }
		 * checked : 当前选项是否被选中(单选时，始终为true，多选时，值同checkbox的选中状态)
		 * value : 当前选项(optTxt)的值
		 * text : 当前选项的文本
		 */
		'onchoose' : function( params ){
			//var html = 'srcElm:' + params.srcElm.tagName + ', panel:' + params.panel.tagName + ', checked:' + params.checked + ', value:' + params.value + ", text:" + params.text + '<br/>';
			//html = html + 'srcElm.value:' + params.srcElm.value + '<br/>';
			//html = html + 'srcElm.values:' + params.srcElm.values.join( ';' ) + '<br/>';
			//html = html + 'srcElm.texts:' + params.srcElm.texts.join( ';' ) + '<br/>';
			//html = html + 'checkedCkbMap:' + params.panel.checkedCkbMap + '<br/>';
			//html = html + 'checkboxes:' + params.panel.checkboxes.length + '<br/>';
			//alert( html, 1 );
		}
	},
	
	/**
	 * 序号的当前值
	 */
	'seqCurrval' : 0,
	
	/**
	 * 生成序号(为每个下拉框编号)
	 */
	'generatSeq' : function(){
		return SelectXx1.seqCurrval++;
	},
	
	/**
	 * 所有面板的序号引用map，key:seq，value:panel对象
	 */
	'seqPanelMap' : {},
	
	/**
	 * 高亮某个选项。按上下键或鼠标移上时触发
	 */
	'obviousOpt' : function( panel, optTxt ){
		if( optTxt == null ){
			return;
		}
		
		if( panel.nowObviousOpt ){
			panel.nowObviousOpt.className = ( panel.nowObviousOpt.ckb && panel.nowObviousOpt.ckb.checked )? 'optTxt chsd': 'optTxt';
		}
		
		if( optTxt ){
			optTxt.className = 'optTxt msor';
			panel.nowObviousOpt = optTxt;
		}
	},
	
	/**
	 * 控制panel的滚动条到当前高亮的位置
	 */
	'scrollToObvious' : function( panel, optTxt ){
		if( optTxt == null ){
			return;
		}
		panel.scrollTop = optTxt.filtNm * 20 - parseInt( panel.offsetHeight/2, 10 ) + 20;
	},
	
	/**
	 * 确定选中一项
	 */
	'chooseOpt' : function( optTxt ){
		var panel = optTxt.panel;
		
		//----处理选中的值----
		if( optTxt.ckb && !optTxt.ckb.checked ){
			optTxt.ckb.checked = true;
		}
		
		for( var i in optTxt.panel.checkedCkbMap ){
			var b = optTxt.panel.checkedCkbMap[i];
			if( b != null && b.optTxt != optTxt ){
				b.checked = false;
				b.optTxt.className = 'optTxt';
			}
		}
		optTxt.panel.checkedCkbMap = {};
		if( optTxt.ckb ){
			optTxt.panel.checkedCkbMap[''+optTxt.ckb.indNo] = optTxt.ckb;
		}
		
		var showTxt = optTxt.value0;
		if( optTxt.firstOptFlag ){
			showTxt = optTxt.panel.customParams.firstOptTextInSrcElm;
		}else{
			if( panel.customParams.showTextMode == 'textValue' ){
				showTxt = optTxt.value1 + panel.customParams.optDelimiter + optTxt.value0;
			}else if( panel.customParams.showTextMode == 'valueText' ){
				showTxt = optTxt.value0 + panel.customParams.optDelimiter + optTxt.value1;
			}else if( panel.customParams.showTextMode == 'text' ){
				showTxt = optTxt.value1;
			}else{
				showTxt = optTxt.value0;
			}
		}
		
		optTxt.panel.srcElm.value = showTxt;
		optTxt.panel.srcElm.values = [optTxt.value0];
		optTxt.panel.srcElm.texts = [showTxt];
		
		//----执行回调函数-------
		optTxt.panel.customParams.onchoose( {
			'srcElm' : optTxt.panel.srcElm, 
			'panel' : optTxt.panel, 
			'checked' : true, 
			'value' : optTxt.value0, 
			'text' : showTxt 
		});
	},
	
	/**
	 * (向panel中)装载数据
	 */
	'fillOpts' : function( panel, dataList ){
		panel.checkedCkbMap = {};	//panel中选中的checkbox，用于多选时快速赋值
		panel.checkboxes = [];	//panel中所有的checkbox
		
		//====循环所有的项，找出最大长度，用于计算面板的总宽度、高度、选项的宽度===============================
		var txtMaxWidth = 0;
		if( !panel.widthHelper ){
			panel.widthHelper = SelectXx1.makeElm( 'span', document.body, 'SelectXx1WidthHelper', null, { 'display' : '' } );
		}
		
		if( panel.customParams.hasFirstOpt ){
			var txtVl = panel.customParams.optMode == 'textValue'? panel.customParams.firstOptText + panel.customParams.optDelimiter + panel.customParams.firstOptValue :
					( panel.customParams.optMode == 'valueText'? panel.customParams.firstOptValue + panel.customParams.optDelimiter + panel.customParams.firstOptText : panel.customParams.firstOptText );
			txtVl = SelectXx1.escapeHtml( txtVl );
			panel.widthHelper.innerHTML = txtVl;
			txtMaxWidth = panel.widthHelper.offsetWidth;
		}
		
		for( var i = 0; i < dataList.length; i ++ ){
			var d = dataList[i];//选项，支持3种情况：k,v; key,value; 二维数组,0:key,1:value
			var key = d.length == 2? d[0] : ( d.k || d.key );
			var value = d.length == 2? d[1] : ( d.v || d.value );
			
			var txtVl = panel.customParams.optMode == 'textValue'? value + panel.customParams.optDelimiter + key :
					( panel.customParams.optMode == 'valueText'? key + panel.customParams.optDelimiter + value : value );
			txtVl = SelectXx1.escapeHtml( txtVl );
			panel.widthHelper.innerHTML = txtVl;
			txtMaxWidth = panel.widthHelper.offsetWidth > txtMaxWidth? panel.widthHelper.offsetWidth : txtMaxWidth;
		}
		
		panel.widthHelper.innerHTML = '';
		panel.innerHTML = '';
		
		//----计算面板div的总宽度、总高度------
		var isScrollY = false;//是否出现竖向滚动条
		var panelHeight = ( panel.customParams.hasFirstOpt? 1 + dataList.length : dataList.length ) * 20;	//【20是opt div的高度】
		if( panelHeight > panel.customParams.heightMax ){
			panelHeight = panel.customParams.heightMax;
			isScrollY = true;
		}
		
		var panelWidth = panel.customParams.multiSelectFlag? ( 20 + txtMaxWidth ) : txtMaxWidth;	//【20是checkbox的宽度】
		panelWidth = isScrollY? panelWidth + 17 : panelWidth;
		panelWidth = panelWidth < panel.srcElm.offsetWidth - 4? panel.srcElm.offsetWidth - 4 : panelWidth;	//【-4是padding:1及边框1】
		panelWidth = panelWidth > panel.customParams.widthMax? panel.customParams.widthMax : panelWidth;
		
		SelectXx1.setElmStyle( panel, { 'width' : panelWidth + 'px', 'height' : panelHeight + 'px'  } );
		panel.myWidth = panelWidth;
		panel.myHeight = panelHeight;
		
		//----计算选项div的总宽度(包括checkbox和文本项)
		var optWidth = ( panel.customParams.multiSelectFlag? ( panelWidth - 20 ) : panelWidth ) - 2;	//【20是checkbox的宽度，2是padding-left】
		optWidth = optWidth > panel.customParams.widthMax? panel.customParams.widthMax : optWidth;
		optWidth = isScrollY? optWidth - 17 : optWidth;//如果出现滚动条，减掉滚动条的宽度，【17是滚动条的宽度】
		
		//====填充选项====================================================================
		var optTemp = null;	//选项临时对象，用于建立双向链表
		panel.firstOpt = null;	//第一个选项
		panel.filtFirstOpt = null;
		var filtNm = 0;	//过滤(显示的排序，用于计算定位滚动条)
		
		var ckbIndex = 0;
		//----第一项----
		if( panel.customParams.hasFirstOpt ){
			var opt = SelectXx1.makeElm( 'DIV', panel, 'opt' );
			var ckb = null;
			if( panel.customParams.multiSelectFlag ){
				var optCkb = SelectXx1.makeElm( 'DIV', opt, 'optCkb' );
				//----第一项的checkbox是没有的-------
				//ckb = SelectXx1.makeElm( 'INPUT', optCkb, null, { 'type' : 'checkbox' } );
				//ckb.panel = panel;
				//ckb.indNo = ckbIndex;
				//panel.checkboxes[ckb.indNo] = ckb;
				//ckbIndex = ckbIndex + 1;
			}
			var optTxt = SelectXx1.makeElm( 'DIV', opt, 'optTxt', { 'value0' : '', 'value1' : panel.customParams.firstOptTextInSrcElm, 'opt' : opt }, { 'width' : optWidth + 'px' } );
			var txtVl = panel.customParams.optMode == 'textValue'? panel.customParams.firstOptText + panel.customParams.optDelimiter + panel.customParams.firstOptValue :
					( panel.customParams.optMode == 'valueText'? panel.customParams.firstOptValue + panel.customParams.optDelimiter + panel.customParams.firstOptText : panel.customParams.firstOptText );
			optTxt.value2 = txtVl;
			optTxt.title = txtVl;
			optTxt.innerHTML = SelectXx1.escapeHtml( txtVl );
			optTxt.firstOptFlag = true;
			optTxt.panel = panel;
			if( panel.firstOpt == null ){
				panel.firstOpt = optTxt;
				panel.filtFirstOpt = optTxt;
			}
			
			if( ckb ){
				optTxt.ckb = ckb;
				ckb.optTxt = optTxt;
			}
			
			//----链表-------------
			if( optTemp ){
				optTemp.next = optTxt;
				optTemp.filtNext = optTxt;
			}
			optTxt.last = optTemp;
			optTxt.filtLast = optTemp;
			optTxt.filtNm = filtNm++;
			optTemp = optTxt;
			
			//====事件函数==============================
			optTxt.onmouseover = function(){
				var ehi = this.panel.eventHappenInfo;
				//如果最后一次事件是按键事件，并且是上或下键，短时间内不执行obviousOpt，避免选项跳跃
				if( ehi.lastEventTime ){
					if( ( ehi.lastEventKeydownKeyCode == 38 || ehi.lastEventKeydownKeyCode == 40 ) && 
						( new Date().getTime() - ehi.lastEventTime < 200 ) ){
						return;
					}
				}
				SelectXx1.obviousOpt( this.panel, this );
			};
			
			optTxt.onclick = function(){
				SelectXx1.chooseOpt( this );
				SelectXx1.closeSelect( this.panel.seq, true );
			};
		}
		
		//----所有数据项----
		for( var i = 0; i < dataList.length; i ++ ){
			var d = dataList[i];//选项，支持3种情况：k,v; key,value; 二维数组,0:key,1:value
			var key = d.length == 2? d[0] : ( d.k || d.key );
			var value = d.length == 2? d[1] : ( d.v || d.value );
			
			var ckb = null;
			var opt = SelectXx1.makeElm( 'DIV', panel, 'opt' );
			if( panel.customParams.multiSelectFlag ){
				var optCkb = SelectXx1.makeElm( 'DIV', opt, 'optCkb' );
				ckb = SelectXx1.makeElm( 'INPUT', optCkb, null, { 'type' : 'checkbox' } );
				ckb.panel = panel;
				
				ckb.indNo = ckbIndex;
				panel.checkboxes[ckb.indNo] = ckb;
				ckbIndex = ckbIndex + 1;
			}
			var optTxt = SelectXx1.makeElm( 'DIV', opt, 'optTxt', { 'value0' : key, 'value1' : value, 'opt' : opt }, { 'width' : optWidth + 'px' } );
			var txtVl = panel.customParams.optMode == 'textValue'? value + panel.customParams.optDelimiter + key :
					( panel.customParams.optMode == 'valueText'? key + panel.customParams.optDelimiter + value : value );
			optTxt.value2 = txtVl;
			optTxt.title = txtVl;
			optTxt.innerHTML = SelectXx1.escapeHtml( txtVl );
			optTxt.panel = panel;
			if( panel.firstOpt == null ){
				panel.firstOpt = optTxt;
				panel.filtFirstOpt = optTxt;
			}
			
			if( ckb ){
				optTxt.ckb = ckb;
				ckb.optTxt = optTxt;
				
				ckb.onclick = function(){
					var panel = this.panel;
					
					if( this.checked ){
						this.optTxt.className = 'optTxt chsd';
						this.panel.checkedCkbMap[this.indNo] = this;
					}else{
						this.optTxt.className = 'optTxt';
						this.panel.checkedCkbMap[this.indNo] = null;
					}
					
					//----处理选中的值，数组----------
					var showTxts = [];
					var values = [];
					for( var z in this.panel.checkedCkbMap ){
						var ckdCkb = this.panel.checkedCkbMap[z];
						if( ckdCkb ){
							var ot = ckdCkb.optTxt;
							var vv = ot.value0;
							if( panel.customParams.showTextMode == 'textValue' ){
								vv = ot.value1 + panel.customParams.optDelimiter + ot.value0;
							}else if( panel.customParams.showTextMode == 'valueText' ){
								vv = ot.value0 + panel.customParams.optDelimiter + ot.value1;
							}else if( panel.customParams.showTextMode == 'text' ){
								vv = ot.value1;
							}

							showTxts[showTxts.length] = vv;
							values[values.length] = ckdCkb.optTxt.value0;
						}
					}
					
					this.panel.srcElm.value = showTxts.join( panel.customParams.showTextDelimiter );
					this.panel.srcElm.texts = showTxts;
					this.panel.srcElm.values = values;
					
					//----执行回调函数-------
					this.panel.customParams.onchoose( {
						'srcElm' : this.panel.srcElm, 
						'panel' : this.panel, 
						'checked' : this.checked, 
						'value' : this.optTxt.value0, 
						'text' : this.optTxt.title 
					});
				};
			}
			
			//----链表-------------
			if( optTemp ){
				optTemp.next = optTxt;
				optTemp.filtNext = optTxt;
			}
			optTxt.last = optTemp;
			optTxt.filtLast = optTemp;
			optTxt.filtNm = filtNm++;
			optTemp = optTxt;
			
			//====事件函数==============================
			optTxt.onmouseover = function(){
				var ehi = this.panel.eventHappenInfo;
				if( ehi.lastEventTime ){
					//如果最后一次事件是按键事件，并且是上或下键，短时间内不执行obviousOpt，避免选项跳跃
					if( ( ehi.lastEventKeydownKeyCode == 38 || ehi.lastEventKeydownKeyCode == 40 ) && 
						( new Date().getTime() - ehi.lastEventTime < 200 ) ){
						return;
					}
				}
				SelectXx1.obviousOpt( this.panel, this );
			};
			
			optTxt.onclick = function(){
				SelectXx1.chooseOpt( this );
				SelectXx1.closeSelect( this.panel.seq, true );
				if( !this.panel.srcElm.readOnly && this.panel.customParams.inputListen == 'filt' && !this.panel.customParams.multiSelectFlag ){
					SelectXx1.filtOpts( this.panel );
				}
			};
		}
	},
	
	/**
	 * 过滤选项
	 */
	'filtOpts' : function( panel ){
		if( !panel.customParams.inputListen == 'filt' ){
			return;
		}
		
		var currValue = panel.srcElm.value;
		if( ( panel.lastFiltValue == null && currValue == '' ) || panel.lastFiltValue == currValue ){
			return;
		}
		
		var currOptTxt = panel.firstOpt;
		var tempOptTxt = null;
		panel.filtFirstOpt = null;
		var filtNm = 0;
		
		while( true ){
			if( currOptTxt == null ){
				break;
			}
			
			if( currValue == '' || currOptTxt.value2.indexOf( currValue ) != -1 ){
				if( panel.filtFirstOpt == null ){
					panel.filtFirstOpt = currOptTxt;
					currOptTxt.filtLast = null;
				}else{
					currOptTxt.filtLast = tempOptTxt;
					tempOptTxt.filtNext = currOptTxt;
				}
				currOptTxt.filtNext = null;
				currOptTxt.filtNm = filtNm++;
				tempOptTxt = currOptTxt;
				SelectXx1.setElmStyle( currOptTxt.opt, {'display' : ''} );
			}else{
				currOptTxt.filtNext = null;
				currOptTxt.filtLast = null;
				SelectXx1.setElmStyle( currOptTxt.opt, {'display' : 'none'} );
			}
			
			currOptTxt = currOptTxt.next;
		}
		
		panel.lastFiltValue = currValue;
		SelectXx1.obviousOpt( panel, panel.filtFirstOpt );
	},
	
	/**
	 * 定位(及显示)面板
	 */
	'positionSelect' : function( o ){
		var panel = o.selectXx1$Panel;
		var ofs = SelectXx1.getElmOffset( o );
		var l = ofs.left + panel.myWidth > document.body.clientWidth? document.body.clientWidth - panel.myWidth - 5 : ofs.left;
		SelectXx1.setElmStyle( panel, {'visibility' : 'visible', 'display' : '', 'left': l + 'px', 'top' : ofs.bottom + 'px'} );
		panel.statusShow = true;
		o.className = panel.srcElmOrigClassName + ' SelectXx1FocusElmStyle';
		window.setTimeout( function(){
			ofs = SelectXx1.getElmOffset( o );
			l = ofs.left + panel.myWidth > document.body.clientWidth? document.body.clientWidth - panel.myWidth - 5 : ofs.left;
			SelectXx1.setElmStyle( panel, {'visibility' : 'visible', 'display' : '', 'left': l + 'px', 'top' : ofs.bottom + 'px'} );
		},1 );
	},
	
	/**
	 * 关闭面板
	 * @params seq : 面板的序号
	 * @params force : 强制关闭标志
	 */
	'closeSelect' : function( seq, force ){
		var panel = SelectXx1.seqPanelMap[seq];
		if( !panel.cursorOn || force ){
			panel.style.display = 'none';
			panel.srcElm.className = panel.srcElmOrigClassName;
			panel.statusShow = false;
		}
	},
	
	/**
	 * 创建面板
	 */
	'makeSelect' : function( o, optListOrUrl, customParamsEnum, otherCustomParams ){
		if( o.selectXx1$Panel != null ){
			SelectXx1.seqPanelMap[o.selectXx1$Panel.seq] = null;
			document.body.removeChild( o.selectXx1$Panel );
		}
		
		var panel = SelectXx1.makeElm( 'DIV', document.body, 'SelectXx1Panel', {'srcElm' : o, 'srcElmOrigClassName' : o.className, 'cursorOn' : true, 'optListOrUrl' : optListOrUrl, 'seq' : SelectXx1.generatSeq(), 'innerHTML' : '<div class=\"opt\"><div class=\"optTxt\">加载数据中...</div></div>'}, { 'display' : 'none', 'width' : ( o.offsetWidth - 4 ) + 'px' } );
		SelectXx1.seqPanelMap[panel.seq] = panel;
		o.selectXx1$Panel = panel;
		panel.customParams = SelectXx1.mergeObjectAttrs( [SelectXx1.defaultCustomParams, customParamsEnum == null? null : SelectXx1.allCustomParamsEnum[customParamsEnum], otherCustomParams == null? null : otherCustomParams] );
		
		panel.onmouseover = function(){
			this.cursorOn = true;
		};
		
		panel.onmouseout = function( event ){
			this.cursorOn = false;
			window.setTimeout( "SelectXx1.closeSelect( '" + this.seq + "' )", 10 );
		};
		
		panel.oncontextmenu = function( event ){
			var e = event || window.event;
			if( e.preventDefault ){
				e.preventDefault();
			}
			return false;
		};
		
		panel.onmouseup = function(){
			this.srcElm.focus();//焦点回到目标元素，以便按键时上下选择
		};
		
		//----记录已经添加过的监听事件，避免重复添加------
		o.selectXx1$AddedEvent = o.selectXx1$AddedEvent || {};
		
		//----记录事件发生点-----------
		panel.eventHappenInfo = {};
		
		if( !o.onclick ){
			o.onclick = function( event ){
				SelectXx1.activate( event );
			};
		}
		
		if( !o.selectXx1$AddedEvent['mouseover'] ){
			SelectXx1.addEvent( o, 'mouseover', function( event ){
				if( o.selectXx1$Panel && o.selectXx1$Panel.statusShow ){
					o.selectXx1$Panel.cursorOn = true;
				}
			} );
			o.selectXx1$AddedEvent['mouseover'] = true;
		}
		
		if( !o.selectXx1$AddedEvent['mouseout'] ){
			SelectXx1.addEvent( o, 'mouseout', function( event ){
				if( o.selectXx1$Panel ){
					o.selectXx1$Panel.cursorOn = false;
					window.setTimeout( "SelectXx1.closeSelect( '" + o.selectXx1$Panel.seq + "' )", 10 );
				}
			} );
			o.selectXx1$AddedEvent['mouseout'] = true;
		}
		
		if( !o.selectXx1$AddedEvent['blur'] ){
			SelectXx1.addEvent( o, 'blur', function( event ){
				if( o.selectXx1$Panel ){
					window.setTimeout( "SelectXx1.closeSelect( '" + o.selectXx1$Panel.seq + "' )", 10 );
				}
			} );
			o.selectXx1$AddedEvent['blur'] = true;
		}
		
		if( !o.selectXx1$AddedEvent['keyup'] ){
			SelectXx1.addEvent( o, 'keyup', function( event ){
				var panel = o.selectXx1$Panel;
				if( !panel ){
					return;
				}
				
				var evt = event || window.event;
				var kc = evt.keyCode;
				
				if( kc != 38 && kc != 40 && kc != 13 && kc != 27 ){
					if( !o.readOnly ){
						//----筛选-----------
						if( panel.customParams.inputListen == 'filt' && !panel.customParams.multiSelectFlag ){
							if( !panel.willFilt ){
								panel.willFilt = true;
								window.setTimeout( function(){
									panel.willFilt = false;
									SelectXx1.filtOpts( panel );
								}, 360 );
							}
						}
						//----匹配模式---------
						else if( panel.customParams.inputListen == 'match' && panel.customParams.multiSelectFlag ){
							var vs = o.value.split( panel.customParams.showTextDelimiter );
							
							var imm = panel.customParams.inputMatchMode;
							var xOpt = panel.filtFirstOpt;
							
							var showTxts = [];
							var values = [];
							var checkedCkbMap = {};
							while( true ){
								if( xOpt == null ){
									break;
								}
								
								var cf = false;//是否匹配上
								for( var h = 0; h < vs.length; h ++ ){
									if( ( imm == 'value' && vs[h] == xOpt.value0 ) ||
										( imm == 'text' && vs[h] == xOpt.value1 ) ||
										( ( imm == 'textValue' || imm == 'valueText' ) && vs[h] == xOpt.value2 )){
										cf = true;
										break;
									}
								}
								
								if( xOpt.ckb ){
									xOpt.ckb.checked = cf;
									checkedCkbMap[xOpt.ckb.indNo] = cf? xOpt.ckb : null;
								}
								
								if( cf ){
									xOpt.className = 'optTxt chsd';
									var showTxt = xOpt.value0;
									if( xOpt.firstOptFlag ){
										showTxt = panel.customParams.firstOptTextInSrcElm;
									}else{
										if( panel.customParams.showTextMode == 'textValue' ){
											showTxt = xOpt.value1 + panel.customParams.optDelimiter + xOpt.value0;
										}else if( panel.customParams.showTextMode == 'valueText' ){
											showTxt = xOpt.value0 + panel.customParams.optDelimiter + xOpt.value1;
										}else if( panel.customParams.showTextMode == 'text' ){
											showTxt = xOpt.value1;
										}else{
											showTxt = xOpt.value0;
										}
									}
									
									showTxts[showTxts.length] = showTxt;
									values[values.length] = xOpt.value0;
								}else{
									xOpt.className = 'optTxt';
								}
								xOpt = xOpt.filtNext;
							}
							
							o.texts = showTxts;
							o.values = values;
							panel.checkedCkbMap = checkedCkbMap;
						}
					}
				}
				
				if( o.value == '' ){
					o.values = [];
				}
			} );
			o.selectXx1$AddedEvent['keyup'] = true;
		}
		
		if( !o.selectXx1$AddedEvent['keydown'] ){
			SelectXx1.addEvent( o, 'keydown', function( event ){
				var panel = o.selectXx1$Panel;
				if( !panel ){
					return;
				}
				
				var evt = event || window.event;
				var stop = false;
				
				panel.eventHappenInfo.lastEventKeydownKeyCode = evt.keyCode;//上次事件发生时间
				panel.eventHappenInfo.lastEventTime = new Date().getTime();//上次事件发生时间
				
				//----38:up键----
				if( evt.keyCode == 38 ){
					SelectXx1.obviousOpt( panel, panel.nowObviousOpt? panel.nowObviousOpt.filtLast : null );
					SelectXx1.scrollToObvious( panel, panel.nowObviousOpt );
					stop = true;
				}
				//----40:down键----
				else if( evt.keyCode == 40 ){
					if( !panel.statusShow ){
						SelectXx1.positionSelect( o );
					}else{
						SelectXx1.obviousOpt( panel, panel.nowObviousOpt? panel.nowObviousOpt.filtNext : panel.filtFirstOpt );
						SelectXx1.scrollToObvious( panel, panel.nowObviousOpt );
					}
					stop = true;
				}
				//----13:enter键----
				else if( evt.keyCode == 13 ){
					if( !panel.statusShow ){
						SelectXx1.positionSelect( o );
					}else{
						if( panel.nowObviousOpt ){
							SelectXx1.chooseOpt( panel.nowObviousOpt );
							SelectXx1.closeSelect( panel.seq, true );
							if( !o.readOnly && panel.customParams.inputListen == 'filt' && !panel.customParams.multiSelectFlag ){
								SelectXx1.filtOpts( panel );
							}
						}
					}
					stop = true;
				}
				//----27:Esc键----
				else if( evt.keyCode == 27 ){
					SelectXx1.closeSelect( panel.seq, true );
				}
				
				if( stop ){
					if( evt.preventDefault ){
						evt.preventDefault();
					}
					return false;
				}
			} );
			o.selectXx1$AddedEvent['keydown'] = true;
		}
		
		//====加载数据========================
		if( typeof panel.optListOrUrl == 'string' ){
			SelectXx1.ajax( panel.optListOrUrl, {}, function( data ){
				var optDataList = eval( data );
				SelectXx1.fillOpts( panel, optDataList );
				if( !o.readOnly && panel.customParams.inputListen == 'filt' && !panel.customParams.multiSelectFlag ){
					SelectXx1.filtOpts( panel );
				}
			} );
		}else{
			SelectXx1.fillOpts( panel, panel.optListOrUrl );
			if( !o.readOnly && panel.customParams.inputListen == 'filt' && !panel.customParams.multiSelectFlag ){
				SelectXx1.filtOpts( panel );
			}
		}
	},
	
	/**
	 * 重置
	 * @param setValues : 设置的值，数组
	 */
	'reset' : function( o, optListOrUrl, customParamsEnum, otherCustomParams, setValues ){
		//TODO...setValues，给予的默认值处理
		o.value = '';
		o.texts = [];
		o.values = [];
		SelectXx1.makeSelect( o, optListOrUrl, customParamsEnum, otherCustomParams );
	},
	
	/**
	 * 清理，将值和选项清空
	 */
	'clean' : function( o ){
		if( o.selectXx1$Panel != null ){
			o.selectXx1$Panel.cursorOn = false;
			SelectXx1.seqPanelMap[o.selectXx1$Panel.seq] = null;
			document.body.removeChild( o.selectXx1$Panel );
			o.selectXx1$Panel = null;
		}
		o.onclick = null;
		o.value = '';
		o.texts = [];
		o.values = [];
	},
	
	/**
	 * 焦点(元素)显示控件
	 * @params event : 事件对象。【必填】
	 * @params optListOrUrl : 下拉项列表或url。如果是对象类型，表示是下拉项列表(数组);如果是字符串类型，表示是ajax访问的地址，用于取得下拉项列表的数据。【必填】
	 * @params customParamsEnum : 定制参数枚举，字符串型，已经预定义好的参数枚举值。【非必填】
	 * @params otherCustomParams : 其他定制参数，用于控制某些选项。【非必填】
	 */
	'activate' : function( event, optListOrUrl, customParamsEnum, otherCustomParams ){
		//----加载css--------
		if( !SelectXx1.appendCssFlag ){
			SelectXx1.appendCss();
			SelectXx1.appendCssFlag = true;
		}
		
		var evt = event || window.event;
		var o = evt.target || evt.srcElement;
		var panel = o.selectXx1$Panel;
		if( panel ){
			panel.cursorOn = true;
			SelectXx1.positionSelect( o );
		}else{
			SelectXx1.makeSelect( o, optListOrUrl, customParamsEnum, otherCustomParams );
			SelectXx1.positionSelect( o );
		}
		o.focus();
	}
};

/**
 * 所有常用定制参数枚举
 */
SelectXx1.allCustomParamsEnum = {
	/**
	 * 101:单选；有第一项；选项:文本；回填:文本；不可筛选
	 */
	'101' : SelectXx1.mergeObjectAttrs( [SelectXx1.defaultCustomParams, {
		'multiSelectFlag' : false
	}] ),
	
	/**
	 * 102:单选；无第一项；选项:文本；回填:文本；可筛选
	 */
	'102' : SelectXx1.mergeObjectAttrs( [SelectXx1.defaultCustomParams, {
		'multiSelectFlag' : false,
		'hasFirstOpt' : false,
		'inputListen' : 'filt'
	}] ),
	
	/**
	 * 103:单选；有第一项；选项:值-文本；回填:值-文本；不可筛选
	 */
	'103' : SelectXx1.mergeObjectAttrs( [SelectXx1.defaultCustomParams, {
		'multiSelectFlag' : false,
		'optMode' : 'valueText',
		'showTextMode' : 'valueText'
	}] ),
	
	/**
	 * 104:单选；无第一项；选项:值-文本；回填:值-文本；可筛选
	 */
	'104' : SelectXx1.mergeObjectAttrs( [SelectXx1.defaultCustomParams, {
		'multiSelectFlag' : false,
		'hasFirstOpt' : false,
		'optMode' : 'valueText',
		'showTextMode' : 'valueText',
		'inputListen' : 'filt'
	}] ),
			
	/**
	 * 105:单选；有第一项；选项:文本；回填:值-文本；不可筛选
	 */
	'105' : SelectXx1.mergeObjectAttrs( [SelectXx1.defaultCustomParams, {
		'multiSelectFlag' : false,
		'showTextMode' : 'valueText'
	}] ),
	
	/**
	 * 201:多选；有第一项；选项:文本；回填:文本；不可输入匹配
	 */
	'201' : SelectXx1.mergeObjectAttrs( [SelectXx1.defaultCustomParams, {
		
	}] ),
	
	/**
	 * 202:多选；有第一项；选项:文本；回填:文本；输入(文本)匹配
	 */
	'202' : SelectXx1.mergeObjectAttrs( [SelectXx1.defaultCustomParams, {
		'inputListen' : 'match'
	}] ),
	
	/**
	 * 203:多选；有第一项；选项:值-文本；回填:值；输入(值)匹配
	 */
	'203' : SelectXx1.mergeObjectAttrs( [SelectXx1.defaultCustomParams, {
		'optMode' : 'valueText',
		'showTextMode' : 'value',
		'inputListen' : 'match',
		'inputMatchMode' : 'value'
	}] ),
	
	/**
	 * 204:多选；无第一项；选项:文本；回填:文本；不可输入匹配
	 */
	'204' : SelectXx1.mergeObjectAttrs( [SelectXx1.defaultCustomParams, {
		'hasFirstOpt' : false,
		'optMode' : 'text',
		'showTextMode' : 'text'
	}] ),
	
	/**
	 * 205:多选；无第一项；选项:值-文本；回填:值；不可输入匹配
	 */
	'205' : SelectXx1.mergeObjectAttrs( [SelectXx1.defaultCustomParams, {
		'hasFirstOpt' : false,
		'optMode' : 'valueText',
		'showTextMode' : 'value'
	}] ),
	
	/**
	 * 206:多选；无第一项；选项:值-文本；回填:值-文本；不可输入匹配
	 */
	'206' : SelectXx1.mergeObjectAttrs( [SelectXx1.defaultCustomParams, {
		'hasFirstOpt' : false,
		'optMode' : 'valueText',
		'showTextMode' : 'valueText'
	}] )
};

window.SelectXx1 = SelectXx1;

})();