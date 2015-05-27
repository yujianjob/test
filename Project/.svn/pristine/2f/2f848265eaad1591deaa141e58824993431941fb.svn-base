function gxOperatorOnLineListPanel(){
	$("#afui .header").css("height",70);
	$("#userNameLabel_02").html(getItem("userName"));
	$.ui.toggleNavMenu();
}
function gxloadSelectZd(){
    var networkState = navigator.network.connection.type;
    if( networkState == Connection.NONE ) {
    	$("#afui .header").css("height",70);
    	$("#zdList").html("");
    	showPopup("网络连接失败");
    	$("#navbar").hide();
        return false;
     }else{
		var siteString =getItem('siteList');
		var sitelistObj=JSON.parse(siteString);
		var sitelist ="";
		for(var i =0;i<sitelistObj.length;i++){
			sitelist+="<a  href='javascript:void(0)' style='background:#4cc7c8;color:white;' class='block button' onclick='selSitedian("+sitelistObj[i].id+");'>"+sitelistObj[i].name+"</a>";
		}
		$("#zdList").html(sitelist);
		$.ui.toggleNavMenu();
	}
}
var gxQj_curpage = 1;//当前页数
var gxQj_totalpage = 0;//当前总页码
function gxhistoryQj_load(){
	$("#afui .header").css("height",120);
	var str1 = "";
	var dateA = getPrevDate(29);
	for(var i = 0;i<dateA.length;i++){
		 keyarr = dateA[i].split("-");
		 var vstr = keyarr[1]+"月"+keyarr[2]+"日";
		 str1+="<option value='"+dateA[i]+"'>"+vstr+"</option>";
	}
	$("#gxhistoryQj_monthdaySelect").html(str1);
	gxgetQjInfo();
}
function gxgetPrevInfo(){
	gxQj_curpage--;
	if(gxQj_curpage <= 0){
		showPopup("已经是第一页了");gxQj_curpage = 1;
		return;
	}
	gxgetQjInfo();
}
function gxgetNextInfo(){
	gxQj_curpage++;
	if( (gxQj_curpage) > gxQj_totalpage){
		showPopup("已经是最后一页了");gxQj_curpage = gxQj_totalpage;
		return;
	}
	gxgetQjInfo();
}
function gxhistoryQjSearch(){
	gxQj_curpage = 1;
	gxgetQjInfo();
}
function gxgetQjInfo(){
	process();
	var page = gxQj_curpage;
	var operatorid=getItem("userId");//获取操作员id
	var monthdaySelect = document.getElementById("gxhistoryQj_monthdaySelect");
	var monthdayindex = monthdaySelect.selectedIndex; // 选中索引
	var monthdayvalue = monthdaySelect.options[monthdayindex].value; // 选中值
	monthdayvalue = monthdayvalue.replace(/[-]+/g,'');//获取当前年月日
	var historyQj_status = document.getElementById("gxhistoryQj_status");
	var statusIndex = historyQj_status.selectedIndex;
	var type = historyQj_status.options[statusIndex].value; // 获取当前状态取值
	var startdate = monthdayvalue+"000000";
	var enddate = monthdayvalue+"000000";
	var ts=getTs();
	var str=actid+operatorid+type+startdate+enddate+page+version+ts;
	str=str.toUpperCase();
	var hash=hex_md5(str);
	hash=hash.toUpperCase();
	hash=hash+Key;
	hash=hex_md5(hash);
	hash=hash.toUpperCase();
	var vkey=hash;
	var postData = {"actid":actid,"operatorid":operatorid,"type":type,"startdate":startdate,"enddate":enddate,"page":page,"version":version,"ts":ts,"vkey": vkey };
	$.ajax({
		type: "post",
		url: host+"/OrderLineTakeSiteLog",
		data: postData,
		dataType: "json",
		success: function(data){
			removeProcess();
			if(data.code==0){
				var str="";
				var jsonData=data.list;
				if(jsonData.length > 0){
					for(var i=0;i<jsonData.length;i++){
						var linkstatus = jsonData[i].linkstatus;
						var wait = jsonData[i].wait;
						if(linkstatus && wait){
							str+='<li style="font-size:1.2em;line-height:25px;"><div class="liLeft">姓名:'+jsonData[i].name+'<br>手机:'+jsonData[i].mpno+'<span class="lxTags">已联系</span><span class="lxTags">退单中</span><br>地址:'+jsonData[i].address+'</div><div class="status">'+jsonData[i].status+'</div></li>';
						}
						if(linkstatus && !wait){
							str+='<li style="font-size:1.2em;line-height:25px;"><div class="liLeft">姓名:'+jsonData[i].name+'<br>手机:'+jsonData[i].mpno+'<span class="lxTags">已联系</span><br>地址:'+jsonData[i].address+'</div><div class="status">'+jsonData[i].status+'</div></li>';
						}
						if(!linkstatus && wait){
							str+='<li style="font-size:1.2em;line-height:25px;"><div class="liLeft">姓名:'+jsonData[i].name+'<br>手机:'+jsonData[i].mpno+'<span class="lxTags">退单中</span><br>地址:'+jsonData[i].address+'</div><div class="status">'+jsonData[i].status+'</div></li>';
						}
						if(!linkstatus && !wait){
							str+='<li style="font-size:1.2em;line-height:25px;"><div class="liLeft">姓名:'+jsonData[i].name+'<br>手机:'+jsonData[i].mpno+'<br>地址:'+jsonData[i].address+'</div><div class="status">'+jsonData[i].status+'</div></li>';
						}
					}
					$("#gxhistoryQjUl").html(str);
				}else{
					$("#gxhistoryQjUl").html("<div align='center' style='padding-top:30px;font-size:14px;font-weight:bold;'><font color='red'>暂无信息</font></div>");
				}
				
					
				$("#gxhistoryQj_footer .totalcount").html(data.totalcount);
				$("#gxhistoryQj_footer .page").html(data.page);
				$("#gxhistoryQj_footer .totalpage").html(data.totalpage);
				gxQj_curpage = data.page;
				gxQj_totalpage = data.totalpage;
			}else{
				showPopup(data.message);
			}
		},
		error: function (msg) {
			removeProcess();
			showPopup("服务异常");
		}
	 });		
}


function gxhistoryQj_unload(){
	$("#afui .header").css("height",70);
	//$("#gxhistoryQj_header select").attr("disabled","disabled"); 
}





var gxSj_curpage = 1;//当前页数
var gxSj_totalpage = 0;//当前总页码
function gxhistorySj_load(){
	process();
	$("#afui .header").css("height",120);
	var str1 = "";
	var dateA = getPrevDate(29);
	for(var i = 0;i<dateA.length;i++){
		 keyarr = dateA[i].split("-");
		 var vstr = keyarr[1]+"月"+keyarr[2]+"日";
		 str1+="<option value='"+dateA[i]+"'>"+vstr+"</option>";
	}
	$("#gxhistorySj_monthdaySelect").html(str1);
	gxgetSjInfo();
}
function gxgetSjPrevInfo(){
	gxSj_curpage--;
	if(gxSj_curpage <= 0){
		showPopup("已经是第一页了");gxSj_curpage = 1;
		return;
	}
	gxgetSjInfo();
}
function gxgetSjNextInfo(){
	gxSj_curpage++;
	if( (gxSj_curpage) > gxSj_totalpage){
		showPopup("已经是最后一页了");gxSj_curpage = gxSj_totalpage;
		return;
	}
	gxgetSjInfo();
}
function gxhistorySjSearch(){
	gxSj_curpage = 1;
	gxgetSjInfo();
}
function gxgetSjInfo(){
	process();
	var page = gxSj_curpage;
	var operatorid=getItem("userId");//获取操作员id
	var monthdaySelect = document.getElementById("gxhistorySj_monthdaySelect");
	var monthdayindex = monthdaySelect.selectedIndex; // 选中索引
	var monthdayvalue = monthdaySelect.options[monthdayindex].value; // 选中值
	monthdayvalue = monthdayvalue.replace(/[-]+/g,'');//获取当前年月日
	var historyQj_status = document.getElementById("gxhistorySj_status");
	var statusIndex = historyQj_status.selectedIndex;
	var type = historyQj_status.options[statusIndex].value; // 获取当前状态取值
	var startdate = monthdayvalue+"000000";
	var enddate = monthdayvalue+"000000";
	var ts=getTs();
	var str=actid+operatorid+type+startdate+enddate+page+version+ts;
	str=str.toUpperCase();
	var hash=hex_md5(str);
	hash=hash.toUpperCase();
	hash=hash+Key;
	hash=hex_md5(hash);
	hash=hash.toUpperCase();
	var vkey=hash;
	var postData = {"actid":actid,"operatorid":operatorid,"type":type,"startdate":startdate,"enddate":enddate,"page":page,"version":version,"ts":ts,"vkey": vkey };
	$.ajax({
		type: "post",
		url: host+"/OrderLineFactoryLog",
		data: postData,
		dataType: "json",
		success: function(data){
			removeProcess();
			if(data.code==0){
				var str1="";
				var jsonData=data.list;
				if(jsonData.length > 0 ){
					for(var i=0;i<jsonData.length;i++){
						var linkstatus = jsonData[i].linkstatus;
						var wait = jsonData[i].wait;
						if(linkstatus && wait){
							str1+='<li onclick="" style="font-size:1.2em;line-height:25px;"><div class="liLeft">姓名:'+jsonData[i].name+'<br>手机:'+jsonData[i].mpno+'<span class="lxTags">已联系</span><span class="lxTags">退单中</span><br>地址:'+jsonData[i].address+'</div><div class="status">'+jsonData[i].status+'</div></li>';
						}
						if(linkstatus && !wait){
							str1+='<li onclick="" style="font-size:1.2em;line-height:25px;"><div class="liLeft">姓名:'+jsonData[i].name+'<br>手机:'+jsonData[i].mpno+'<span class="lxTags">已联系</span><br>地址:'+jsonData[i].address+'</div><div class="status">'+jsonData[i].status+'</div></li>';
						}
						if(!linkstatus && wait){
							str1+='<li onclick="" style="font-size:1.2em;line-height:25px;"><div class="liLeft">姓名:'+jsonData[i].name+'<br>手机:'+jsonData[i].mpno+'<span class="lxTags">退单中</span><br>地址:'+jsonData[i].address+'</div><div class="status">'+jsonData[i].status+'</div></li>';
						}
						if(!linkstatus && !wait){
							str1+='<li onclick="" style="font-size:1.2em;line-height:25px;"><div class="liLeft">姓名:'+jsonData[i].name+'<br>手机:'+jsonData[i].mpno+'<br>地址:'+jsonData[i].address+'</div><div class="status">'+jsonData[i].status+'</div></li>';
						}
					}
				}else{
					str1 = "<div align='center' style='padding-top:30px;font-size:14px;font-weight:bold;'><font color='red'>暂无信息</font></div>";
				}
				$("#gxhistorySjUl").html(str1);
				
				$("#gxhistorySj_footer .totalcount").html(data.totalcount);
				$("#gxhistorySj_footer .page").html(data.page);
				$("#gxhistorySj_footer .totalpage").html(data.totalpage);
				gxSj_curpage = data.page;
				gxSj_totalpage = data.totalpage;
			}else{
				showPopup(data.message);
			}
		},
		error: function (msg) {
			removeProcess();
			showPopup("服务异常");
		}
	 });		
}


function gxhistorySj_unload(){
	$("#afui .header").css("height",70);
	//$("#gxhistorySj_header select").attr("disabled","disabled"); 
}
function HgxSongYiDetail(SongYiid){
	setItem("SongYiid",SongYiid);
	setItem("hRecord",1);
	$.ui.loadContent("SongYiDetail_panel",false,false,"slide");
}

//点击操作员按钮执行的函数
function gxLoadOperator(){
 	var networkState = navigator.network.connection.type;
    if( networkState == Connection.NONE ) {
    	showPopup("网络连接失败");
    	$("#afui .header").css("height",70);
		$("#gxOperatorInfoList").html("");
		$("#fzzd,#gxsiteList").hide();
		$("#navbar").hide();
		$("#gxsiteList").html("");
        return false;
    }else{
		var str="";
		str+="<li>姓名:"+getItem('userName')+"</li>";
		str+="<li>手机:"+getItem('userMpno')+"</li>";
		str+="<li>干线编号:"+getItem('nodeNo')+"</li>";
		str+="<li>干线名称:"+getItem('nodeName')+"</li>";
		$("#gxOperatorInfoList").html(str);
		var siteString =getItem('siteList');
		var sitelistObj=JSON.parse(siteString);
		var str1 ="";
		for(var i =0;i<sitelistObj.length;i++){
			str1+="<li>"+sitelistObj[i].name+"</li>";
		}
		$("#gxsiteList").html(str1);
		$("#fzzd,#gxsiteList").show();
	}
	$.ui.toggleNavMenu();
}
function gxhistoryRecord_load(){
	$.ui.toggleNavMenu();
}
//选择站点
function selzd(){
	var siteString =getItem('siteList');
	var sitelistObj=JSON.parse(siteString);
	var sitelist ="";
	for(var i =0;i<sitelistObj.length;i++){
		sitelist+="<a  href='javascript:void(0)' style='background:#4cc7c8;color:white;' class='block button' onclick='selSitedian("+sitelistObj[i].id+");'>"+sitelistObj[i].name+"</a>";
	}
	$("#zdList").html(sitelist);
}
function selSitedian(id){
	var networkState = navigator.network.connection.type;
    if( networkState == Connection.NONE ) {
    	$("#afui .header").css("height",70);
    	showPopup("网络连接失败");
    	$("#navbar").hide();
        return false;
     }else{
     	setItem("siteid",id);//站点名称
		$.ui.loadContent("gxSiteStorage_panel",false,false,"slide");
	}
}

function GetFactoryPackageInfo(){
	$.ui.loadContent("gxSiteStorage_panel",false,false,"slide");
}
function gxuloadSiteStorage(){
	$("#gxmdh").val("");
}

function gxloadSiteStorage(){
    var networkState = navigator.network.connection.type;
    if( networkState == Connection.NONE ) {
    	$("#afui .header").css("height",70);
    	$(".site1StorageUl").html("");
    	$(".danhao,.btngroups").hide();
    	showPopup("网络连接失败");
    	$("#navbar").hide();
        return false;
    }else{
		process();
		var siteid=getItem("siteid");
		var operatorid=getItem("userId");
		var ts=getTs();
		var str=actid+siteid+operatorid+version+ts;
		str=str.toUpperCase();
		var hash=hex_md5(str);
		hash=hash.toUpperCase();
		hash=hash+Key;
		hash=hex_md5(hash);
		hash=hash.toUpperCase();
		var vkey=hash;
		var postData = {"actid":actid,"siteid":siteid,"operatorid":operatorid,"version":version,"ts":ts,"vkey": vkey };
		$.ajax({
			type: "post",
			url: host+"/Line_GetSitePackageInfo",
			data: postData,
			dataType: "json",
			success: function(data){
				removeProcess();
				if(data.code==0){
					var str2 = "<li style='border-bottom:1px solid #333'>干线名称："+getItem('nodeName')+"</li>";
					str2+="<li>工厂名称："+data.name+"</li>";
					str2+="<li>今日应收：<span id='gxCount'>"+data.count+"</span></li>";
					$(".site1StorageUl").html(str2);
					$(".danhao,.btngroups").show();
					if($("#gxmdh").length){
						$("#gxmdh").focus();
					}
				}else{
					showPopup(data.message);
				}
			},
			error:function(){
				removeProcess();
				showpopup("服务异常");	
			}
		 });
	$("#navbar").hide();
	}
}
function gxQzdOnIpt(obj){
	if(obj.value.length == 9){
		gxtjMdh();
	}
}
function gxQFacOnIpt(obj){
	if(obj.value.length == 9){
		gxFactjMdh();
	}
}
function gxtjMdh(){
    var networkState = navigator.network.connection.type;
    if( networkState == Connection.NONE ) {
    	$("#afui .header").css("height",70);
    	showPopup("网络连接失败");
    	$("#navbar").hide();
        return false;
     }else{
			process();
			var expnumber = $("#gxmdh").val();
			if(expnumber.length == 6){
				expnumber = "002101"+expnumber;
				$("#gxmdh").val(expnumber);
			}
			var siteid = getItem("siteid");
			var operatorid=getItem("userId");
			var ts=getTs();
			var str=actid+expnumber+siteid+operatorid+version+ts;
			str=str.toUpperCase();
			var hash=hex_md5(str);
			hash=hash.toUpperCase();
			hash=hash+Key;
			hash=hex_md5(hash);
			hash=hash.toUpperCase();
			var vkey=hash;
			var postData = {"actid":actid,"expnumber":expnumber,"siteid":siteid,"operatorid":operatorid,"version":version,"ts":ts,"vkey": vkey };
			$.ajax({
				type: "post",
				url: host+"/Line_GetSitePackage",
				data: postData,
				dataType: "json",
				success: function(data){
					removeProcess();
					if(data.code==0){
						var str="<div class='infoTips'>提交成功</div>";
						$("body").prepend(str);
						$("#gxmdh").val("").focus();
						setTimeout(function(){
							gxloadSiteStorage();
							$(".infoTips").remove();
						},2000);
						
					}else{
						showPopup(data.message);
						$("#gxmdh").val("");
					}
				},
				error:function(){
					removeProcess();
					showpopup("服务异常");
				}
			 });
	 }
	$("#navbar").hide();	
}

function gxFactjMdh(){
    var networkState = navigator.network.connection.type;
    if( networkState == Connection.NONE ) {
    	$("#afui .header").css("height",70);
    	showPopup("网络连接失败");
    	$("#navbar").hide();
        return false;
     }else{
	process();
	var expnumber = $("#gxFacmdh").val();
	var operatorid=getItem("userId");
	var ts=getTs();
	var str=actid+expnumber+operatorid+version+ts;
	str=str.toUpperCase();
	var hash=hex_md5(str);
	hash=hash.toUpperCase();
	hash=hash+Key;
	hash=hex_md5(hash);
	hash=hash.toUpperCase();
	var vkey=hash;
	var postData = {"actid":actid,"expnumber":expnumber,"operatorid":operatorid,"version":version,"ts":ts,"vkey": vkey };
	$.ajax({
		type: "post",
		url: host+"/Line_GetFactoryPackage",
		data: postData,
		dataType: "json",
		success: function(data){
			removeProcess();
			if(data.code==0){
				var str="<div class='infoTips'>提交成功</div>";
				$("body").prepend(str);
				$("#gxFacmdh").val("").focus();
				setTimeout(function(){
					gxloadgetFactory();
				    $(".infoTips").remove();
				},2000);	
			}else{
				showPopup(data.message);
				//$.ui.loadContent("gxgetFactory_panel",false,false,"slide");
				$("#gxFacmdh").val("");
				//gxloadgetFactory();
			}
		},
		error:function(){
			removeProcess();
			showPopup("服务异常");
		}
	 });
	 }
}
function gxuloadWarehouse(){
	var str3="";
	str3+="<li style='border-bottom:1px solid #333'>干线名称:0</li><li>工厂名称：0</li><li>工厂收件：0</li>";
	$(".site2StorageUl").html(str3);
}
function gxloadWarehouse(){
    var networkState = navigator.network.connection.type;
    if( networkState == Connection.NONE ) {
    	$("#afui .header").css("height",70);
    	showPopup("网络连接失败");
    	$("#navbar").hide();
        return false;
     }else{
	process();
	var operatorid=getItem("userId");
	var ts=getTs();
	var str=actid+operatorid+version+ts;
	str=str.toUpperCase();
	var hash=hex_md5(str);
	hash=hash.toUpperCase();
	hash=hash+Key;
	hash=hex_md5(hash);
	hash=hash.toUpperCase();
	var vkey=hash;
	var postData = {"actid":actid,"operatorid":operatorid,"version":version,"ts":ts,"vkey": vkey };
	$.ajax({
		type: "post",
		url: host+"/Line_SendFactoryPackageInfo",
		data: postData,
		dataType: "json",
		success: function(data){
			removeProcess();
			if(data.code==0){
				var str3="";
				str3+="<li style='border-bottom:1px solid #333'>干线名称:"+getItem('nodeName')+"</li><li>工厂名称："+data.name+"</li><li>工厂收件："+data.count+"</li>";
				$(".site2StorageUl").html(str3);
			}else{
				showPopup(data.message);
			}
		},
		error:function(){
			removeProcess();
			showpopup("服务异常");	
		}
	 });
	 $.ui.toggleNavMenu();
	 }
	 
}
function rkFinish(){
    var networkState = navigator.network.connection.type;
    if( networkState == Connection.NONE ) {
    	$("#afui .header").css("height",70);
    	showPopup("网络连接失败");
    	$("#navbar").hide();
        return false;
     }else{
	process();
	var operatorid=getItem("userId");
	var ts=getTs();
	var str=actid+operatorid+version+ts;
	str=str.toUpperCase();
	var hash=hex_md5(str);
	hash=hash.toUpperCase();
	hash=hash+Key;
	hash=hex_md5(hash);
	hash=hash.toUpperCase();
	var vkey=hash;
	var postData = {"actid":actid,"operatorid":operatorid,"version":version,"ts":ts,"vkey": vkey };
	$.ajax({
		type: "post",
		url: host+"/Line_SendFactoryPackageComplete",
		data: postData,
		dataType: "json",
		success: function(data){
			removeProcess();
			if(data.code==0){
			   $("#afui").popup({
					title: "Alert",
					message: "入库成功",
					cancelText: "确定",
					cancelCallback: function () {
						$.ui.loadContent("gxOperatorOnLineList_panel",false,false,"slide");
					},
					doneText: "",
					doneCallback: function () {},
					cancelOnly: true
				});
			}else{
				showPopup(data.message);
			}
		},
		error:function(){
			removeProcess();
			showpopup("服务异常");	
		}
	 });
	 }	
}
//干线取站点件异常处理
function gxexceptionHandle(){
	$("#afui").popup({
		title: "",
		message: "上报异常",
		cancelText: "取消",
		cancelCallback: function () {
			
		},
		doneText: "提交",
		doneCallback: function () {
    var networkState = navigator.network.connection.type;
    if( networkState == Connection.NONE ) {
    	$("#afui .header").css("height",70);
    	showPopup("网络连接失败");
    	$("#navbar").hide();
        return false;
    }else{
				process();
				var siteid = getItem("siteid");
				var operatorid=getItem("userId");
				var ts=getTs();
				var str=actid+siteid+operatorid+version+ts;
				str=str.toUpperCase();
				var hash=hex_md5(str);
				hash=hash.toUpperCase();
				hash=hash+Key;
				hash=hex_md5(hash);
				hash=hash.toUpperCase();
				var vkey=hash;
				var postData = {"actid":actid,"siteid":siteid,"operatorid":operatorid,"version":version,"ts":ts,"vkey": vkey };
				$.ajax({
					type: "post",
					url: host+"/Line_GetSitePackage_FailNotify",
					data: postData,
					dataType: "json",
					success: function(data){
						removeProcess();
						if(data.code==0){
							showPopup("提交成功");
						}else{
							showPopup(data.message);
						}
					},
					error:function(){
						removeProcess();
						showpopup("服务异常");	
					}
				 });
			}	
		},
		cancelOnly: false
	});	
}
function gxFacexceptionHandle(){
	$("#afui").popup({
		title: "",
		message: "上报异常",
		cancelText: "取消",
		cancelCallback: function () {
			
		},
		doneText: "提交",
		doneCallback: function () {
		    var networkState = navigator.network.connection.type;
		    if( networkState == Connection.NONE ) {
		    	$("#afui .header").css("height",70);
		    	showPopup("网络连接失败");
		    	$("#navbar").hide();
		        return false;
		    }else{
			process();
			var operatorid=getItem("userId");
			var ts=getTs();
			var str=actid+operatorid+version+ts;
			str=str.toUpperCase();
			var hash=hex_md5(str);
			hash=hash.toUpperCase();
			hash=hash+Key;
			hash=hex_md5(hash);
			hash=hash.toUpperCase();
			var vkey=hash;
			var postData = {"actid":actid,"operatorid":operatorid,"version":version,"ts":ts,"vkey": vkey };
			$.ajax({
				type: "post",
				url: host+"/Line_GetFactoryPackage_FailNotify",
				data: postData,
				dataType: "json",
				success: function(data){
					removeProcess();
					if(data.code==0){
						showPopup("提交成功");
					}else{
						showPopup(data.message);
					}
				},
				error:function(){
					removeProcess();
					showpopup("服务异常");	
				}
			 });
			}			
		},
		cancelOnly: false
	});	
}

//收件完成
function gxshoujianComplete(){
		var gxCount = $("#gxCount").text();
		if(gxCount > 0){
			$("#afui").popup({
				title: "",
				message: "本次入库异常还未上报",
				cancelText: "关闭",
				cancelCallback: function () {
				},
				doneText: "上报",
				doneCallback: function () {
				    var networkState = navigator.network.connection.type;
				    if( networkState == Connection.NONE ) {
				    	$("#afui .header").css("height",70);
				    	showPopup("网络连接失败");
				    	$("#navbar").hide();
				        return false;
				     }else{
					process();
					var siteid = getItem("siteid");
					var operatorid=getItem("userId");
					var ts=getTs();
					var str=actid+siteid+operatorid+version+ts;
					str=str.toUpperCase();
					var hash=hex_md5(str);
					hash=hash.toUpperCase();
					hash=hash+Key;
					hash=hex_md5(hash);
					hash=hash.toUpperCase();
					var vkey=hash;
					var postData = {"actid":actid,"siteid":siteid,"operatorid":operatorid,"version":version,"ts":ts,"vkey": vkey };
					$.ajax({
						type: "post",
						url: host+"/Line_GetSitePackage_FailNotify",
						data: postData,
						dataType: "json",
						success: function(data){
							removeProcess();
							if(data.code==0){
								showPopup("提交成功");
							}else{
								showPopup(data.message);
								$.ui.loadContent("gxSelectZd_panel",false,false,"slide");
							}
						},
						error:function(){
							removeProcess();
							showpopup("服务异常");	
						}
					 });
					 }
				},
				cancelOnly: false
			});	
		}else{
			$.ui.loadContent("gxOperatorOnLineList_panel",false,false,"slide");
		}
}
function gxFACshoujianComplete(){
		var faccount = $("#facCount").val();
		if(faccount>0){
			$("#afui").popup({
				title: "",
				message: "本次入库异常还未上报",
				cancelText: "关闭",
				cancelCallback: function () {
					
				},
				doneText: "上报",
				doneCallback: function () {
				    var networkState = navigator.network.connection.type;
				    if( networkState == Connection.NONE ) {
				    	$("#afui .header").css("height",70);
				    	showPopup("网络连接失败");
				    	$("#navbar").hide();
				        return false;
				    }else{
					process();
					var operatorid=getItem("userId");
					var ts=getTs();
					var str=actid+operatorid+version+ts;
					str=str.toUpperCase();
					var hash=hex_md5(str);
					hash=hash.toUpperCase();
					hash=hash+Key;
					hash=hex_md5(hash);
					hash=hash.toUpperCase();
					var vkey=hash;
					var postData = {"actid":actid,"operatorid":operatorid,"version":version,"ts":ts,"vkey": vkey };
					$.ajax({
						type: "post",
						url: host+"/Line_GetFactoryPackage_FailNotify",
						data: postData,
						dataType: "json",
						success: function(data){
							removeProcess();
							if(data.code==0){
								showPopup("提交成功");
							}else{
								showPopup(data.message);
							}
						},
						error:function(){
							removeProcess();
							showpopup("服务异常");	
						}
					 });
					 }	
				},
				cancelOnly: false
			});	
		}else{
			$.ui.loadContent("gxOperatorOnLineList_panel",false,false,"slide");
		}
}
//站点入库扫描
function gxsiteStorageScan(){
    window.plugins.barcodeScanner.scan(
    function(result) {
    	var str="";
    	if(result.text.length==6){
    		str+="002101"+result.text;
    	}else{
    		str+=result.text;
    	}
        $("#gxmdh").val(str);
        //$("#expnumber").attr("readonly","readonly");
    }, function(error) {
        alert("Scan failed: " + error);
    });
}
function gxuloadgetFactory(){
	$("#gxFacmdh").val("");
}
function gxloadgetFactory(){
    var networkState = navigator.network.connection.type;
    if( networkState == Connection.NONE ) {
    	$("#afui .header").css("height",70);
    	$(".FACsiteStorageUl").html("");
    	$(".danhao,.btngroups").hide();
    	showPopup("网络连接失败");
    	$("#navbar").hide();
        return false;
     }else{
	process();
	var operatorid=getItem("userId");
	var ts=getTs();
	var str=actid+operatorid+version+ts;
	str=str.toUpperCase();
	var hash=hex_md5(str);
	hash=hash.toUpperCase();
	hash=hash+Key;
	hash=hex_md5(hash);
	hash=hash.toUpperCase();
	var vkey=hash;
	var postData = {"actid":actid,"operatorid":operatorid,"version":version,"ts":ts,"vkey": vkey };
	$.ajax({
		type: "post",
		url: host+"/Line_GetFactoryPackageInfo",
		data: postData,
		dataType: "json",
		success: function(data){
			removeProcess();
			if(data.code==0){
				var str2 = "<li style='border-bottom:1px solid #333'>干线名称："+getItem('nodeName')+"</li>";
				str2+="<li>工厂名称："+data.name+"</li>";
				str2+="<li>今日应收：<span id='facCount'>"+data.count+"</span></li>";
				$(".FACsiteStorageUl").html(str2);
				$(".danhao,.btngroups").show();
				if($("#gxFacmdh").length){
					$("#gxFacmdh").focus();
				}
			}else{
				showPopup(data.message);
			}
		},
		error:function(){
			removeProcess();
			showpopup("服务异常");	
		}
	 });
	 }
		
	//$.ui.toggleNavMenu();
	$("#navbar").hide();
}
//站点入库扫描
function gxFACsiteStorageScan(){
    window.plugins.barcodeScanner.scan(
    function(result) {
    	var str="";
    	if(result.text.length==6){
    		str+="002101"+result.text;
    	}else{
    		str+=result.text;
    	}
        $("#gxFacmdh").val(str);
        //$("#expnumber").attr("readonly","readonly");
    }, function(error) {
        alert("Scan failed: " + error);
    });
}




