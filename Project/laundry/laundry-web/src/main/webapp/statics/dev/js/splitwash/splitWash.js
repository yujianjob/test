var WINDOW0_LOAD_PAGE = "";//小窗口加载过的页面地址
var ORDER_CLASS_MAP = {'1':'个人普通', '2':'个人一键下单', '3':'门店订单', '4':'奢侈品', '5':'商城订单'};
var ORDER_CHANNEL = {'-1':'系统', '0':'通用', '1':'WEB', '2':'APP', '3':'新浪微博', '4':'微信', '6':'短信', '91':'快递继续下单'};

/**
 * 按键事件，获取物流订单及洗衣产品信息
 */
function keyUpOfExpNumber(event, isForce){
	if( $('expNumber').value == '' ){
		return;
	}
	
	if( isForce || event.keyCode == '13' ){
		$.ajax( 
			$.contextPath + '/splitWash/getInfoByExpNumber.do', 
			{ 'expNumber' : $('expNumber').value }, 
			function( data ){
				//$.debugObjectAttrs( data );
				var succFlag = data[0];
				if( !succFlag ){
					$.alert( data[1] );
					return;
				}
				
				var expOrderEo = data[1];
				if( expOrderEo != null ){
					$( 'orderNumber' ).value = expOrderEo.outNumber == null? '' : expOrderEo.outNumber;
					$( 'contacts' ).value = expOrderEo.contacts == null? '' : expOrderEo.contacts;
					$( 'mpNo' ).value = expOrderEo.mpno == null? '' : expOrderEo.mpno;
					$( 'address' ).value = expOrderEo.address == null? '' : expOrderEo.address;
				}
				
				var orderOrderEo = data[2];
				if( orderOrderEo != null ){
					$( 'csRemark' ).value = orderOrderEo.csRemark == null? '' : orderOrderEo.csRemark;
					$( 'userRemark' ).value = orderOrderEo.userRemark == null? '' : orderOrderEo.userRemark;
					$( 'orderId' ).value = orderOrderEo.id == null? '' : orderOrderEo.id;
					$( 'orderNumber' ).value = orderOrderEo.orderNumber == null? '' : orderOrderEo.orderNumber;
				}
				
				var orderProductEoList = data[3];
				var wpTbody = $( 'wpTbody' );
				$.removeAllChildNodes( wpTbody );
				if( orderProductEoList != null && orderProductEoList.length > 0 ){
					for( var k = 0; k < orderProductEoList.length; k ++ ){
						var ope = orderProductEoList[k];
						var tr = document.createElement( 'TR' );
						tr.innerHTML = 
							'<td>' + ope.id + '</td><td>' + 
							ope.productId + '</td><td>' + 
							ope.name + '</td><td>' + 
							ope.price + '</td><td>' + 
							ope.attribute + '</td><td>' + 
							( ope.code != null? ope.code + '<a href="#" class="btn0" style="margin-left:5px;" onclick="printWashCode( \''+ ope.code +'\' );return false;">打印</a>' : '&nbsp;' ) + '</td><td>' + 
							( ope.otherCode != null? ope.otherCode : '&nbsp;' ) + '</td><td>' + 
							(orderOrderEo.orderClass == 2? '&nbsp;' : ( ope.code != null? '&nbsp;' : '<a href="#" class="btn0" style="margin-left:5px;" onclick="deleteOrderProduct( \''+ ope.id +'\', this.parentNode.parentNode );return false;">删除</a>' ) ) + '</td>';
						wpTbody.appendChild( tr );
					}
					
					$( 'orderInfoShow' ).innerHTML = '该订单为' + ORDER_CHANNEL[orderOrderEo.channel] + ORDER_CLASS_MAP[orderOrderEo.orderClass] + '，已经添加过衣物';
					if( orderOrderEo.orderClass == 2 ){
						$.showAndHidden( ['btnAddWashProductPuppet','BtnUnmReportPuppet'], ['btnAddWashProduct', 'BtnUnmReport'] );						
					}else{
						$.showAndHidden( ['btnAddWashProduct', 'BtnUnmReport'], ['btnAddWashProductPuppet','BtnUnmReportPuppet'] );
					}
				}else{
					$( 'orderInfoShow' ).innerHTML = '该订单为' + ORDER_CHANNEL[orderOrderEo.channel] + ORDER_CLASS_MAP[orderOrderEo.orderClass] + '，还没添加过衣物';
					toAddOrderProduct();
					$.showAndHidden( ['btnAddWashProduct', 'BtnUnmReport'], ['btnAddWashProductPuppet', 'BtnUnmReportPuppet'] );
				}
			}
		);
	}
}

/**
 * (打开窗口准备)添加衣物
 */
function toAddOrderProduct(){
	if( WINDOW0_LOAD_PAGE != 'toAddOrderProduct' ){
		$.windowx( '添加衣物', '载入中...', 800, 600 );
		$.ajax( 
			$.contextPath + '/splitWash/toAddOrderProduct.do', 
			{'a':'你好001'}, 
			function( data ){
				$.windowx( '添加衣物', data, 800, 600 );
				WINDOW0_LOAD_PAGE = 'toAddOrderProduct';
			}
		);
	}else{
		$.windowx( '添加衣物', null, 800, 600 );
	}
}

/**
 * 选择衣物类型
 */
function chooseClothesType( prId, prName, prSalesPrice ){
	$( 'edtProductId' ).value = prId;
	$( 'edtProductName' ).value = prName;
	$( 'edtPrSalesPrice' ).value = prSalesPrice;
	
}

/**
 * 选择属性
 */
function chooseClothesAttrs( attrName ){
	var ea = $( 'edtProductAttribute' );
	if( ea.value.indexOf( attrName ) <= -1 ){
		ea.value = ea.value == ''? attrName : ea.value + ' ' + attrName;
	}
}

/**
 * 确定添加
 */
function addOrderProduct(){
	if( $( 'edtProductName' ).value == '' ){
		$.alert( '请填写衣物名称' );
		return;
	}

	if( $( 'edtProductAttribute' ).value == '' ){
		$.alert( '请填写衣物描述' );
		return;
	}

	if( $( 'edtOtherCode' ).value == '' ){
		$.alert( '请扫描或填写工厂条码' );
		return;
	}

	var wpTbody = $( 'wpTbody' );
	
	$.ajax( 
		$.contextPath + '/splitWash/addOrderProduct.do', 
		{
			'orderId' : $( 'orderId' ).value,
			'productId' : $( 'edtProductId' ).value,
			'productName' : $( 'edtProductName' ).value,
			'price' : $( 'edtPrSalesPrice' ).value,
			'attr' : $( 'edtProductAttribute' ).value,
			'code' : $( 'edtCode' ).value,
			'otherCode' : $( 'edtOtherCode' ).value,
		}, 
		function( data ){
			var eo = data[1];
			var tr = document.createElement( 'TR' );
			var edtCodeVle = $( 'edtCode' ).value;
			tr.innerHTML = 
				'<td>' + eo.id + '</td><td>' + 
				$( 'edtProductId' ).value + '</td><td>' + 
				$( 'edtProductName' ).value + '</td><td>' + 
				$( 'edtPrSalesPrice' ).value + '</td><td>' + 
				$( 'edtProductAttribute' ).value + '</td><td>' + 
				edtCodeVle + '<a href="#" class="btn0" style="margin-left:5px;" onclick="printWashCode( \''+ edtCodeVle +'\' );return false;">打印</a></td><td>' + 
				$( 'edtOtherCode' ).value + '</td><td>&nbsp;</td>';
			wpTbody.appendChild( tr );
			
			//----拍照-----
			capture();
			
			//----打印水洗条码------
			printWashCode( $( 'edtCode' ).value );
			
			$( 'edtProductId' ).value = '';
			$( 'edtPrSalesPrice' ).value = '';
			$( 'edtProductName' ).value = '';
			$( 'edtProductAttribute' ).value = '';
			$( 'edtCode' ).value = '';
			$( 'edtOtherCode' ).value = '';
			
			$.ajax(
				$.contextPath + '/splitWash/generateWashCode.do', 
				{},
				function( data ){
					$( 'edtCode' ).value = data;
				}
			);
		}
	);
}

/**
 * 删除订单产品
 */
function deleteOrderProduct( opeId, tr ){
	$.ajax(
		$.contextPath + '/splitWash/deleteOrderProduct.do', 
		{ 'id' : opeId },
		function( data ){
			tr.parentNode.removeChild( tr );
		}
	);
}

/**
 * (打开窗口准备)异件上报
 */
function toAbnormalSubmit(){
	if( WINDOW0_LOAD_PAGE != 'toAbnormalSubmit' ){
		$.windowx( '异件上报', '载入中...', 400, 300 );
		$.ajax( 
			$.contextPath + '/splitWash/toAbnormalSubmit.do', 
			{}, 
			function( data ){
				$.windowx( '异件上报', data, 400, 300 );
				WINDOW0_LOAD_PAGE = 'toAbnormalSubmit';
				$( 'arOrderNumber' ).value = $( 'orderNumber' ).value;
				$( 'arRemark' ).value = '';
				$( 'arMsgDiv' ).innerHTML = '';
			}
		);
	}else{
		$.windowx( '异件上报', null, 400, 300 );
		$( 'arRemark' ).value = '';
		$( 'arMsgDiv' ).innerHTML = '';
	}
}

/**
 * 异件上报
 */
function abnormalSubmit(){
	$.ajax( 
		$.contextPath + '/splitWash/abnormalSubmit.do', 
		{ 'orderNumber':$( 'arOrderNumber' ).value, 'type':$.radioValue( 'arType' ), 'remark':$( 'arRemark' ).value }, 
		function( r ){
			if( r[0] == '00' ){
				$( 'arMsgDiv' ).innerHTML = r[1] + '上报成功';
			}
		}
	);
}

/**
 * 开启摄像头
 */
function openCamera(){
	if( window.clientJsApi ){
		window.clientJsApi.openCamera();
	}else{
		alert( '(web方式)无法开启摄像头' );
	}
}

/**
 * 拍照
 */
function capture(){
	var picName = $( 'edtCode' ).value + '_' + $( 'orderNumber' ).value;//水洗条码号-订单号.jgp
	if( window.clientJsApi ){
		window.clientJsApi.capture( "", picName );
	}else{
		alert( '(web方式)无法拍照:' + picName );
	}
}

/**
 * 打印洗涤条码
 * (包含洗涤条码和工厂洗涤条码)
 */
function printWashCode( code ){
	if( window.clientJsApi ){
		window.clientJsApi.printWashCode( code );
	}else{
		alert( '(web方式)无法打印洗涤条码:' + code );
	}
}

/**
 * 完成订单
 */
function finishOrder(){
	$( 'expNumber' ).value = '';
	$( 'orderId' ).value = '';
	$( 'orderNumber' ).value = '';
	$( 'orderInfoShow' ).innerHTML = '';
	$( 'contacts' ).value = '';
	$( 'mpNo' ).value = '';
	$( 'address' ).value = '';
	$( 'csRemark' ).value = '';
	$( 'userRemark' ).value = '';
	$.removeAllChildNodes( wpTbody );
	
	$.showAndHidden( ['btnAddWashProductPuppet','BtnUnmReportPuppet'], ['btnAddWashProduct', 'BtnUnmReport'] );
}

$.addOnloadEvent(
	function(){
		if( window.navigator.userAgent.indexOf( 'JavaFX' ) >= 0 ){
			var it0 = setInterval( function(){
				if( window.clientJsApi ){
					openCamera();
					clearInterval( it0 );
				}
			}, 100 );
		}else{
			//alert( '(web方式)无法开启摄像头' );
		}
	}
);