(function($){
	$.alerts={
		okButton:"确认",
		cancelButton:"取消",
		confirm:function(title,msg,callback){
			
			$.alerts._show(title,msg,'confirm',function(result){
				if(callback){
					callback(result);
				}
			});
		},
		_show:function(title,msg,type,callback){
			$("body").append(
			  '<div class="processmask"></div>'+
			  '<div id="popup_container" class="popdiv1">' +
				'<p id="popupTxt"></p>'+
			    '<div id="popup_content" style="padding:0 20px">' +  
				'</div>' +
			  '</div>');
			  //var pos = ($.browser.msie && parseInt($.browser.version) <= 6 ) ? 'absolute' : 'fixed'; 

			  $("#popup_title").html(title);
			  $("#popupTxt").html(msg);
			  //$("#popup_message").text(msg);
			  switch(type){
				  case "confirm":
				  $("#popup_content").html('<div id="popup_panel"><input type="button" value="' + $.alerts.cancelButton + '" id="popup_cancel" class="cancel-btn"/> <input type="button" value="' + $.alerts.okButton + '" id="popup_ok" class="use-btn"/> </div>');
				  break;
			  }
			  $("#popup_ok").click(function(event){
				  if(callback){
					  event.stopPropagation();
					  callback(true);
				  }
				  $.alerts._hide();
		      });
			  $("#popup_cancel").click(function(){
				  if(callback){
					  callback(false);
				  }
				   $.alerts._hide();
		      });
		},
		_hide:function(){
			$("#popup_container").remove();
			$(".processmask").remove();
	    }
		
	},

	jConfirm=function(title,msg,callback){
		$.alerts.confirm(title,msg,callback);
	}
})(jQuery);
