var WINDOW0_LOAD_PAGE = '';

/**
 * 按键事件，获取物流订单及洗衣产品信息
 */
function keyUpOfCode(event, isForce){
	if( $('code').value == '' ){
		return;
	}
	
	if( event.keyCode == '13' || isForce ){
		$.ajax( 
			$.contextPath + '/outBound/getInfoByCode.do', 
			{ 'code' : $('code').value }, 
			function( data ){
				//$.debugObjectAttrs( data );
				var succFlag = data[0];
				if( !succFlag ){
					$.alert( data[1] );
					return;
				}
				
				var orderOrderEo = data[1];
				if( orderOrderEo != null ){
					$( 'orderId' ).value = orderOrderEo.id == null? '' : orderOrderEo.id;
				}
				
				var expOrderEo = data[2];
				if( expOrderEo != null ){
					$( 'orderNumber' ).value = expOrderEo.outNumber == null? '' : expOrderEo.outNumber;
					$( 'contacts' ).value = expOrderEo.contacts == null? '' : expOrderEo.contacts;
					$( 'mpNo' ).value = expOrderEo.mpno == null? '' : expOrderEo.mpno;
					$( 'address' ).value = expOrderEo.address == null? '' : expOrderEo.address;
				}
				
				var orderProductEoList = data[3];
				var wpTbody = $( 'wpTbody' );
				$.removeAllChildNodes( wpTbody );
				if( orderProductEoList != null && orderProductEoList.length > 0 ){
					for( var k = 0; k < orderProductEoList.length; k ++ ){
						var ope = orderProductEoList[k];
						var tr = document.createElement( 'TR' );
						if( ope.code == $('code').value ){
							tr.style.background = '#DDD';
						}
						
						tr.innerHTML = 
							'<td>' + ope.id + '</td><td>' + 
							ope.name + '</td><td>' + 
							ope.price + '</td><td>' + 
							ope.attribute + '</td><td>' + 
							ope.code + '</td><td>' + 
							( ope.step == 5? '已出库' : '' ) + '</td>';
						tr.id = 'trOpe' + ope.id;
						wpTbody.appendChild( tr );
						
						if( ope.code == $('code').value ){
							$('orderProductId').value = ope.id;
						}
					}
					
					$.showAndHidden( ['btnPrintOutBoundInvoices','BtnAbnormalSubmit'], ['btnPrintOutBoundInvoicesPuppet', 'BtnAbnormalSubmitPuppet'] );
				}else{
					$.showAndHidden( ['btnPrintOutBoundInvoicesPuppet', 'BtnAbnormalSubmitPuppet'], ['btnPrintOutBoundInvoices','BtnAbnormalSubmit'] );
				}
			}
		);
	}
}

/**
 * 打印出库单
 */
function printOutBoundInvoices(){
	var orderProductIdVal = $('orderProductId').value;
	if( orderProductIdVal == '' ){
		$.alert( '请输入正确的洗涤条码并查询出信息后再打印' );
		return;
	}
	
	$.ajax( 
		$.contextPath + '/outBound/outBoundOrderProduct.do', 
		{ 'id' : orderProductIdVal }, 
		function( data ){
			$( 'trOpe' + orderProductIdVal ).childNodes[5].innerHTML = '已出库';
			if( window.clientJsApi ){
				window.clientJsApi.printOutBoundInvoices( data[1] );
			}else{
				$.alert( '无法打印出库单' );		
			}
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
			$.contextPath + '/outBound/toAbnormalSubmit.do', 
			{}, 
			function( data ){
				$.windowx( '异件上报', data, 400, 300 );
				WINDOW0_LOAD_PAGE = 'toAbnormalSubmit';
				$( 'arOrderNumber' ).value = $( 'orderNumber' ).value;
				$( 'arRemark' ).value = '水洗条码：' + $( 'code' ).value;
			}
		);
	}else{
		$.windowx( '异件上报', null, 400, 300 );
		$( 'arOrderNumber' ).value = $( 'orderNumber' ).value;
		$( 'arRemark' ).value = '水洗条码：' + $( 'code' ).value;
		$( 'arMsgDiv' ).innerHTML = '';
	}
}

/**
 * 异件上报
 */
function abnormalSubmit(){
	$.ajax( 
		$.contextPath + '/outBound/abnormalSubmit.do', 
		{ 'orderNumber':$( 'arOrderNumber' ).value, 'type':$.radioValue( 'arType' ), 'remark':$( 'arRemark' ).value }, 
		function( r ){
			if( r[0] == '00' ){
				$( 'arMsgDiv' ).innerHTML = r[1] + '上报成功';
			}
		}
	);
}