//用户登录
function loadLoginPanel(){
	$.ui.toggleHeaderMenu();
	$.ui.toggleNavMenu();
	$("#username").val(getItem("username"));
	$("#password").val(getItem("password"));
}
//注销用户
function exitUser(type){
	if(type == 2){
		clearTag();
	}
	$.ui.loadContent("login_panel",false,false,"slide");
}
//点击登录按钮执行的函数
function loginEntrance(){
	process();
	var username=$("#username").val();
	var password=$("#password").val();
	var ts=getTs();
	var str=actid+username+password+version+ts;
	str=str.toUpperCase();
	var hash=hex_md5(str);
	hash=hash.toUpperCase();
	hash=hash+Key;
	hash=hex_md5(hash);
	hash=hash.toUpperCase();
	var vkey=hash;
	var postData = {"actid":actid,"username": username, "password": password,"version":version,"ts":ts, "vkey": vkey };
	$.ajax({
		type: "post",
		url: host+"/login",
		data: postData,
		dataType: "json",
		success: function(data){
			//alert(data);
			//return;
			removeProcess();
			if(data.code==0){//用户存在
				var sitelist =JSON.stringify(data.user.sitelist);
				setItem("username",username);//用户名
				setItem("password",password);//密码
				setItem("userId",data.user.id);//用户id
				setItem("nodeId",data.user.nodeid);//站点id
				setItem("nodeName",data.user.nodename);//站点名称
				setItem("reccode",data.user.reccode);//推荐码
				setItem("nodeNo",data.user.nodeno);//站点编号
				setItem("addr",data.user.addr);//地址
				setItem("siteList",sitelist);//站点列表
				setItem("userName",data.user.name);//用户名
				setItem("userType",data.user.type);//用户类型
				setItem("userMpno",data.user.mpno);//用户手机号
				if(data.user.type==1){					
					$.ui.loadContent("OperatorOnLine_panel",false,false,"slide");
				}
				if(data.user.type==2){//跟车
					pushTag(data.user.nodeno,data.user.id);
					$.ui.loadContent("gxOperatorOnLineList_panel",false,false,"slide");
				}
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
function pushTag(nodeCode,userId){
	cordova.exec(function(result){
		if(result == "true"){
			
		}
	}, function(error) {
      	//showPopup("登录失败");
    }, "CommonFun", "setTag", [nodeCode,userId]);
}

function clearTag(){
	cordova.exec(function(result){
		if(result == "true"){
			
		}
	}, function(error) {
      	//showPopup("登录失败");
    }, "CommonFun", "clearTag",[]);
}
function historyRecord_load(){
	$.ui.toggleNavMenu();
}

var curpage = 1;//当前页数
var totalpage = 0;//当前总页码
function historyQj_load(){
	process();
	$("#afui .header").css("height",120);
	var str1 = "";
	var dateA = getPrevDate(29);
	for(var i = 0;i<dateA.length;i++){
		 keyarr = dateA[i].split("-");
		 var vstr = keyarr[1]+"月"+keyarr[2]+"日";
		 str1+="<option value='"+dateA[i]+"'>"+vstr+"</option>";
	}
	$("#historyQj_monthdaySelect").html(str1);
	getQjInfo();
}
function getPrevInfo(){
	curpage--;
	if(curpage <= 0){
		showPopup("已经是第一页了");curpage = 1;
		return;
	}
	getQjInfo();
}
function getNextInfo(){
	curpage++;
	if( (curpage) > totalpage){
		showPopup("已经是最后一页了");curpage = totalpage;
		return;
	}
	getQjInfo();
}
function historyQjSearch(){
	curpage = 1;
	getQjInfo();
}
function getQjInfo(){
	process();
	var page = curpage;
	var operatorid=getItem("userId");//获取操作员id
	var monthdaySelect = document.getElementById("historyQj_monthdaySelect");
	var monthdayindex = monthdaySelect.selectedIndex; // 选中索引
	var monthdayvalue = monthdaySelect.options[monthdayindex].value; // 选中值
	monthdayvalue = monthdayvalue.replace(/[-]+/g,'');//获取当前年月日
	var historyQj_status = document.getElementById("historyQj_status");
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
		url: host+"/OrderSiteTakeUserLog",
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
				}else{
					str = "<div align='center' style='padding-top:30px;font-size:14px;font-weight:bold;'><font color='red'>暂无信息</font></div>";
				}
				$("#historyQjUl").html(str);
					
				$("#historyQj_footer .totalcount").html(data.totalcount);
				$("#historyQj_footer .page").html(data.page);
				$("#historyQj_footer .totalpage").html(data.totalpage);
				curpage = data.page;
				totalpage = data.totalpage;
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


function historySj_unload(){
	$("#afui .header").css("height",70);
	//$("#historySj_header select").attr("disabled","disabled"); 
}
var hsj_curpage = 1;//当前页数
var hsj_totalpage = 0;//当前总页码



function historySj_load(){
	process();
	$("#afui .header").css("height",120);
	var str1 = "";
	var dateA = getPrevDate(29);
	for(var i = 0;i<dateA.length;i++){
		 keyarr = dateA[i].split("-");
		 var vstr = keyarr[1]+"月"+keyarr[2]+"日";
		 str1+="<option value='"+dateA[i]+"'>"+vstr+"</option>";
	}
	$("#historySj_monthdaySelect").html(str1);
	getSjInfo();
}
function getSjPrevInfo(){
	hsj_curpage--;
	if(hsj_curpage <= 0){
		showPopup("已经是第一页了");hsj_curpage = 1;
		return;
	}
	getSjInfo();
}
function getSjNextInfo(){
	hsj_curpage++;
	if( (hsj_curpage) > totalpage){
		showPopup("已经是最后一页了");hsj_curpage = hsj_totalpage;
		return;
	}
	getSjInfo();
}
function historySjSearch(){
	hsj_curpage = 1;
	getSjInfo();
}
function getSjInfo(){
	process();
	var page = hsj_curpage;
	var operatorid=getItem("userId");//获取操作员id
	var monthdaySelect = document.getElementById("historySj_monthdaySelect");
	var monthdayindex = monthdaySelect.selectedIndex; // 选中索引
	var monthdayvalue = monthdaySelect.options[monthdayindex].value; // 选中值
	monthdayvalue = monthdayvalue.replace(/[-]+/g,'');//获取当前年月日
	var historySj_status = document.getElementById("historySj_status");
	var statusIndex = historySj_status.selectedIndex;
	var type = historySj_status.options[statusIndex].value; // 获取当前状态取值
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
		url: host+"/OrderSiteTakeLineLog",
		data: postData,
		dataType: "json",
		success: function(data){
			removeProcess();
			if(data.code==0){
				var str3="";
				var jsonData=data.list;
				if(jsonData.length > 0){
					for(var i=0;i<jsonData.length;i++){
						var linkstatus = jsonData[i].linkstatus;
						var wait = jsonData[i].wait;
						if(linkstatus && wait){
							str3+='<li onclick="HSongYiDetail(\''+jsonData[i]+'\')" style="font-size:1.2em;line-height:25px;"><div class="liLeft">姓名:'+jsonData[i].name+'<br>手机:'+jsonData[i].mpno+'<span class="lxTags">已联系</span><span class="lxTags">退单中</span><br>地址:'+jsonData[i].address+'</div><div class="status">'+jsonData[i].status+'</div></li>';
						}
						if(linkstatus && !wait){
							str3+='<li onclick="HSongYiDetail(\''+jsonData[i]+'\')" style="font-size:1.2em;line-height:25px;"><div class="liLeft">姓名:'+jsonData[i].name+'<br>手机:'+jsonData[i].mpno+'<span class="lxTags">已联系</span><br>地址:'+jsonData[i].address+'</div><div class="status">'+jsonData[i].status+'</div></li>';
						}
						if(!linkstatus && wait){
							str3+='<li onclick="HSongYiDetail(\''+jsonData[i]+'\')" style="font-size:1.2em;line-height:25px;"><div class="liLeft">姓名:'+jsonData[i].name+'<br>手机:'+jsonData[i].mpno+'<span class="lxTags">退单中</span><br>地址:'+jsonData[i].address+'</div><div class="status">'+jsonData[i].status+'</div></li>';
						}
						if(!linkstatus && !wait){
							str3+='<li onclick="HSongYiDetail(\''+jsonData[i]+'\')" style="font-size:1.2em;line-height:25px;"><div class="liLeft">姓名:'+jsonData[i].name+'<br>手机:'+jsonData[i].mpno+'<br>地址:'+jsonData[i].address+'</div><div class="status">'+jsonData[i].status+'</div></li>';
						}
					}
				}else{
					str3 = "<div align='center' style='padding-top:30px;font-size:14px;font-weight:bold;'><font color='red'>暂无信息</font></div>";
				}
				$("#historySjUl").html(str3);
					
				$("#historySj_footer .totalcount").html(data.totalcount);
				$("#historySj_footer .page").html(data.page);
				$("#historySj_footer .totalpage").html(data.totalpage);
				hsj_curpage = data.page;
				hsj_totalpage = data.totalpage;
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





function getPrevDate(AddDayCount){
	var dateArr = [];
	var mydate = new Date();
	var startDate = mydate;
	var startMillisecond = mydate.getTime();
	for(var i=0;i<=AddDayCount;i++){
		startDate.setTime(startMillisecond-i*24*60*60*1000);//获取AddDayCount天后的日期
		var y = startDate.getFullYear(); 
		var m = startDate.getMonth()+1;//获取当前月份的日期 
		var d = startDate.getDate(); 
		if(m.toString().length < 2){
			m = "0"+m;
		}
		if(d.toString().length < 2){
			d = "0"+d;
		}
		var riqi =y+"-"+ m+"-"+d;
		dateArr.push(riqi);	
	}
	return dateArr;
}

function historyQj_unload(){
	$("#afui .header").css("height",70);
	//$("#historyQj_header select").attr("disabled","disabled"); 
}


//上岗
function OperatorOnline(){
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
			url: host+"/OperatorOnLine",
			data: postData,
			dataType: "json",
			success: function(data){
				removeProcess();
				if(data.code==0){
					pushTag(getItem('nodeNo'),getItem('userId'));
					$.ui.loadContent("OperatorOnLineList_panel",false,false,"slide");
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
}
//离岗
function OperatorOffLine(){
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
		//alert(actid+"--"+operatorid);
		var postData = {"actid":actid,"operatorid":operatorid,"version":version,"ts":ts,"vkey": vkey };
		$.ajax({
			type: "post",
			url: host+"/OperatorOffLine",
			data: postData,
			dataType: "json",
			success: function(data){
				clearTag();
				removeProcess();
				if(data.code==0){
					$.ui.loadContent("OperatorOnLine_panel",false,false,"slide");
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
}
function OperatorOnLine_fn(){
	$("#afui .header").css("height",70);
	$("#userNameLabel_02").html(getItem("userName"));
	$.ui.toggleNavMenu();
}
function unOperatorOnLine_fn(){

}
//跳转主页的方法
//加载进度
var timer1 = null;
process1 = function () {
	var str = "";
	str += "<div class='processmask'></div><div class='processBox'><div class='ball'></div><div class='ball1'></div></div>";
	$("body").prepend(str);
	timer1 = setTimeout(function(){
		if($(".processmask").length && $(".processBox").length){
			$(".processmask, .processBox").remove();
			showPopup("加载延时");
			clearTimeout(timer1);
			timer = null;
		}
	},300);	

	document.querySelector(".processmask").addEventListener("touchmove", function (e) {
		e.preventDefault();
	}, false);
}

removeProcess1 = function (status) {
	$(".processmask, .processBox").remove();
	clearTimeout(timer1);	
}
function jumpMainPage(){
	$.ui.loadContent("OperatorOnLineList_panel",false,false,"slide");
}
//操作菜单选项列表函数
function OperatorOnLineListPanel(){
	//jumping=true;
	$("#afui .header").css("height",70);
	$("#userNameLabel_01").html(getItem("userName"));
	$.ui.toggleNavMenu();
}
//离开操作菜单选项列表函数
function uOperatorOnLineListPanel(){
	$("#afui .header").css("height",70);
}
//操作员panel加载时执行函数
function LoadOperator(){
    var networkState = navigator.network.connection.type;
    if( networkState == Connection.NONE ) {
    	$("#afui .header").css("height",70);
    	$("#OperatorInfoList").html("");
    	$("#jyDetail").html(""); 
    	showPopup("网络连接失败");
    	$("#navbar").hide();
        return false;
    }else{
		process();
		var str="";
		str+="<li>姓名:"+getItem('userName')+"</li>";
		str+="<li>手机:"+getItem('userMpno')+"</li>";
		str+="<li>站点号:"+getItem('nodeNo')+"</li>";
		str+="<li>站点名称:"+getItem('nodeName')+"</li>";
		str+="<li>地址:"+getItem('addr')+"</li>";
		str+="<li>推荐码:<b>"+getItem('reccode')+"</b></li>";
		$("#OperatorInfoList").html(str);
		
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
			url: host+"/OperMoney",
			data: postData,
			dataType: "json",
			success: function(data){
				removeProcess();
				if(data.code==0){
					var str1 = "";
					str1+="<h2 style='margin-top:30px;margin-bottom:15px;text-align:center'>佣金金额:<font color='red'>￥"+data.brokerage+"</font></h2><a class='button block' href='#Jydetail_panel' style='background:#4cc7c8;color:white;'>交易详情</a><h2 style='margin-top:15px;text-align:center'>代收货款:<font color='red'>￥"+data.payment+"</font></h2>"
					$("#jyDetail").html(str1);  
				}else{
					showPopup(data.message);
				}
			},
			error:function(msg){
				removeProcess();
				showPopup("服务异常");	
			}
		 });	
		$.ui.toggleNavMenu();
	}
}
//交易详情加载执行函数
function LoadJydetail(){
    var networkState = navigator.network.connection.type;
    if( networkState == Connection.NONE ) {
    	$("#afui .header").css("height",70);
    	$(".jylist").html("");
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
				url: host+"/BrokerageList",
				data: postData,
				dataType: "json",
				success: function(data){
					removeProcess();
					if(data.code==0){
						var s = "";
						for(var j=0;j<data.list.length;j++){
							
							s+="<tr><td>"+data.list[j].time+"</td><td>"+data.list[j].name+"</td><td>"+data.list[j].money+"</td></tr>";
							
						}
						$(".jylist").html(s);
					}else{
						showPopup(data.message);
					}
				},
				error:function(msg){
					removeProcess();
					showPopup("服务异常");	
				}
			 });	
			$.ui.toggleNavMenu();
	}
}
function exceptionHandle(){
	$("#afui").popup({
		title: "",
		message: "上报异常",
		cancelText: "关闭",
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
					url: host+"/GetLinePackage_FailNotify",
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
					error:function(msg){
						removeProcess();
						showPopup("服务异常");	
					}
				 });
			 }	
		},
		cancelOnly: false
	});	
}
//操作员panel离开时执行函数
function unLoadOperator(){
	
}
//点击操作员按钮执行的函数
function OperatorInfo(){
	$.ui.loadContent("OperatorInfo_panel",false,false,"slide");
}
function uloadRk(){
	$("#mdhinput").val("");
}
//站点入库页加载执行函数
function loadRk(){
    var networkState = navigator.network.connection.type;
    if( networkState == Connection.NONE ) {
    	$("#afui .header").css("height",70);
    	$(".siteStorageUl").html("");
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
			url: host+"/GetLinePackageInfo",
			data: postData,
			dataType: "json",
			success: function(data){
				removeProcess();
				if(data.code==0){
					var str =""+"<li style='border-bottom:1px solid #333'>站点名称："+getItem('nodeName')+"</li><li>干线名称："+data.name+"</li><li>今日应收：<span id='ysCount'>"+data.count+"</span></li>";
					$(".siteStorageUl").html(str);
					$(".danhao,.btngroups").show();
					if($("#mdhinput").length){
						$("#mdhinput").focus();
					}
				}else{
					showPopup(data.message);
				}
			},
			error:function(msg){
				removeProcess();
				showPopup("服务异常");	
			}
		 });
		$("#navbar").hide();
	}
}
function shoujianComplete(){
	var count = parseInt($("#ysCount").text());
	if(count > 0){
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
					url: host+"/GetLinePackage_FailNotify",
					data: postData,
					dataType: "json",
					success: function(data){
						removeProcess();
						if(data.code==0){
							showPopup("提交成功");
							$.ui.loadContent("OperatorOnLineList_panel",false,false,"slide");
						}else{
							showPopup(data.message);
						}
					},
					error:function(msg){
						removeProcess();
						showPopup("服务异常");	
					}
				 });
		}	
		},
		cancelOnly: false
	});		
	}else{
		$.ui.loadContent("OperatorOnLineList_panel",false,false,"slide");
	}
}
//站点入库扫描
function siteStorageScan(){
    window.plugins.barcodeScanner.scan(
    function(result) {
    	var str="";
    	if(result.text.length==6){
    		str+="002101"+result.text;
    	}else{
    		str+=result.text;
    	}
        $("#mdhinput").val(str);
        //$("#expnumber").attr("readonly","readonly");
    }, function(error) {
        alert("Scan failed: " + error);
    });
}
//提交面单
function tjMdh(){
    var networkState = navigator.network.connection.type;
    if( networkState == Connection.NONE ) {
    	$("#afui .header").css("height",70);
    	showPopup("网络连接失败");
    	$("#navbar").hide();
        return false;
    }else{
		process();
		var expnumber=$("#mdhinput").val();
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
			url: host+"/GetLinePackage",
			data: postData,
			dataType: "json",
			success: function(data){
				removeProcess();
				if(data.code==0){
					$("#afui").popup({
						title: "",
						message: "提交成功",
						cancelText: "确定",
						cancelCallback: function () {
							loadRk();
							$("#mdhinput").val("").focus();
						},
						doneText: "确认",
						doneCallback: function () {
					
						},
						cancelOnly: true
					});
				}else{
					showPopup(data.message);
				}
			},
			error:function(msg){
				removeProcess();
				showPopup("服务异常");	
			}
		 });
	 }
}
//待收件列表
function OrderWaitTakeList(){
	$.ui.loadContent("OrderWaitTakeList_panel",false,false,"slide");
}
//加载进度
var timer = null;
process = function (status) {
	var str = "";
	str += "<div class='processmask'></div><div class='processBox'><div class='ball'></div><div class='ball1'></div></div>";
	$("body").prepend(str);
	timer = setTimeout(function(){
		if($(".processmask").length && $(".processBox").length){
			$(".processmask, .processBox").remove();
			showPopup("加载延时");
			clearTimeout(timer);
			timer = null;
		}
	},30000);	

	document.querySelector(".processmask").addEventListener("touchmove", function (e) {
		e.preventDefault();
	}, false);
}

removeProcess = function (status) {
	$(".processmask, .processBox").remove();
	clearTimeout(timer);	
}
//待接单panel加载时执行函数
function loadOrderWaitTakeListPanel(s){
    var networkState = navigator.network.connection.type;
    if( networkState == Connection.NONE ) {
    	$("#afui .header").css("height",120);
    	$("#OrderWaitTakeListUl").html("");
    	showPopup("网络连接失败");
    	$("#navbar").hide();
        return false;
    }else{
		process();
		$("#afui .header").css("height",120);
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
			url: host+"/OrderWaitAccept",
			data: postData,
			dataType: "json",
			success: function(data){
				removeProcess();
				if(data.code==0){
					var str="";
					var jsonData=data.list;
					for(var i=0;i<jsonData.length;i++){
						str+="<li onclick=''><span style='font-size:1.2em;line-height:25px'>姓名:"+jsonData[i].name+"<br>手机:"+jsonData[i].mpno+"<br>地址:"+jsonData[i].address+"</span><div class='btnGroup'><a class='button refuse' onclick='refuseOrder(this,"+jsonData[i].id+");'>拒绝</a><a class='button accept' onclick='OrderAccept(this,"+jsonData[i].id+");'>接收</a></div></li>";
					}
					$("#OrderWaitTakeListUl").html(str);
					$("#jdsearchInput").val("");
				}else{
					showPopup(data.message);
				}
			},
			error: function (msg) {
				removeProcess();
				showPopup("服务异常");
			}
		 });
		 if(s != 'true'){
		 	$.ui.toggleNavMenu();
		 }
	 }
	 
}
//接收订单函数
function OrderAccept(obj,orderid){
	showPopup3(obj,orderid);
}
function showPopup3(obj,orderid) {
	$("#afui").popup({
		title: "",
		message: "确定接单?",
		cancelText: "关闭",
		cancelCallback: function () {
			
		},
		doneText: "确认",
		doneCallback: function () {
		    var networkState = navigator.network.connection.type;
		    if( networkState == Connection.NONE ) {
		    	$("#afui .header").css("height",120);
		    	showPopup("网络连接失败");
		    	$("#navbar").hide();
		        return false;
		     }else{
				process();
				var operatorid=getItem('userId');
				var ts=getTs();
				var str=actid+orderid+operatorid+version+ts;
				str=str.toUpperCase();
				var hash=hex_md5(str);
				hash=hash.toUpperCase();
				hash=hash+Key;
				hash=hex_md5(hash);
				hash=hash.toUpperCase();
				var vkey=hash;
				var postData = {"actid":actid,"orderid":orderid,"operatorid":operatorid,"version":version,"ts":ts,"vkey": vkey };
				$.ajax({
					type: "post",
					url: host+"/OrderAccept",
					data: postData,
					dataType: "json",
					success: function(data){
						removeProcess();
						if(data.code==0){
							$(obj).parent().parent().remove();
						}else{
							showPopup(data.message);
						}
						loadOrderWaitTakeListPanel();
					},
					error: function (msg) {
						removeProcess();
						showPopup("服务异常");
					}
				 });
				 
			}
		},
		cancelOnly: false
	});
}
//拒绝接单函数
function refuseOrder(obj,orderid){
	showPopup2(obj,orderid);
}
function showPopup2(obj,orderid) {
	$("#afui").popup({
		title: "",
		message: "确定要拒绝?",
		cancelText: "关闭",
		cancelCallback: function () {
			
		},
		doneText: "确认",
		doneCallback: function () {
			    var networkState = navigator.network.connection.type;
			    if( networkState == Connection.NONE ) {
			    	$("#afui .header").css("height",120);
			    	showPopup("网络连接失败");
			    	$("#navbar").hide();
			        return false;
			     }else{
				process();
				var operatorid=getItem('userId');
				var ts=getTs();
				var str=actid+orderid+operatorid+version+ts;
				str=str.toUpperCase();
				var hash=hex_md5(str);
				hash=hash.toUpperCase();
				hash=hash+Key;
				hash=hex_md5(hash);
				hash=hash.toUpperCase();
				var vkey=hash;
				var postData = {"actid":actid,"orderid":orderid,"operatorid":operatorid,"version":version,"ts":ts,"vkey": vkey };
				$.ajax({
					type: "post",
					url: host+"/OrderUnAccept",
					data: postData,
					dataType: "json",
					success: function(data){
						removeProcess();
						if(data.code==0){
							$(obj).parent().parent().remove();
						}else{
							showPopup(data.message);
						}
						loadOrderWaitTakeListPanel();
					},
					error: function (msg) {
						removeProcess();
						showPopup("服务异常");
					}
				 });
			}
		},
		cancelOnly: false
	});
}
//待接单panel离开时执行函数
function uloadOrderWaitTakeListPanel(){
	$("#afui .header").css("height",70);
}
//收衣函数
function ShouYi(){
	$.ui.loadContent("ShouYiList_panel",false,false,"slide");
}
function uloadShouYiList(){
	$("#afui .header").css("height",70);
}
function loadShouYiList(s){
    var networkState = navigator.network.connection.type;
    if( networkState == Connection.NONE ) {
    	$("#afui .header").css("height",120);
    	showPopup("网络连接失败");
    	$("#ShouYiList_panelUl").html("");
    	$("#navbar").hide();
        return false;
    }else{	
		process();
		$("#afui .header").css("height",120);
		var operatorid=getItem('userId');
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
			url: host+"/OrderWaitTakeList",
			data: postData,
			dataType: "json",
			success: function(data){
				removeProcess();
				if(data.code==0){
					var str="";
					var jsonData=data.list;
					for(var i=0;i<jsonData.length;i++){
						var linkstatus = jsonData[i].linkstatus;
						var wait = jsonData[i].wait;
						//setItem("wait",wait);
						//setItem("linkstatus",linkstatus);
						
						if(linkstatus && wait){
							str+='<li style="font-size:1.2em;line-height:25px;" onclick="ShouYiDetail(\''+linkstatus+'\',\''+wait+'\',\''+jsonData[i].id+'\',\''+jsonData[i].name+'\',\''+jsonData[i].mpno+'\',\''+jsonData[i].address+'\',\''+jsonData[i].count+'\')">姓名:'+jsonData[i].name+'<br>手机:'+jsonData[i].mpno+'<span class="lxTags">已联系</span><span class="lxTags">退单中</span><br>地址:'+jsonData[i].address+'</li>';
						}
						if(linkstatus && !wait){
							str+='<li style="font-size:1.2em;line-height:25px;" onclick="ShouYiDetail(\''+linkstatus+'\',\''+wait+'\',\''+jsonData[i].id+'\',\''+jsonData[i].name+'\',\''+jsonData[i].mpno+'\',\''+jsonData[i].address+'\',\''+jsonData[i].count+'\')">姓名:'+jsonData[i].name+'<br>手机:'+jsonData[i].mpno+'<span class="lxTags">已联系</span><br>地址:'+jsonData[i].address+'</li>';
						}
						if(!linkstatus && wait){
							str+='<li style="font-size:1.2em;line-height:25px;" onclick="ShouYiDetail(\''+linkstatus+'\',\''+wait+'\',\''+jsonData[i].id+'\',\''+jsonData[i].name+'\',\''+jsonData[i].mpno+'\',\''+jsonData[i].address+'\',\''+jsonData[i].count+'\')">姓名:'+jsonData[i].name+'<br>手机:'+jsonData[i].mpno+'<span class="lxTags">退单中</span><br>地址:'+jsonData[i].address+'</li>';
						}
						if(!linkstatus && !wait){
							str+='<li style="font-size:1.2em;line-height:25px;" onclick="ShouYiDetail(\''+linkstatus+'\',\''+wait+'\',\''+jsonData[i].id+'\',\''+jsonData[i].name+'\',\''+jsonData[i].mpno+'\',\''+jsonData[i].address+'\',\''+jsonData[i].count+'\')">姓名:'+jsonData[i].name+'<br>手机:'+jsonData[i].mpno+'<br>地址:'+jsonData[i].address+'</li>';
						}
					}
					$("#ShouYiList_panelUl").html(str);
					$("#searchInput").val("");
				}else{
					showPopup(data.message);
				}
			},
			error: function (msg) {
				removeProcess();
				showPopup("服务异常");
			}
		 });
		 if(s != 'true'){
			$.ui.toggleNavMenu();
		 }
	}
}
//收衣详情
function ShouYiDetail(linkstatus,wait,orderid,name,mpno,address,ordercount){
	setItem("orderid",orderid);
	setItem("ordername",name);
	setItem("ordermpno",mpno);
	setItem("orderaddress",address);
	setItem("ordercount",ordercount);
	setItem("linkstatus",linkstatus);
	setItem("wait",wait);
	$.ui.loadContent("ShouYiDetail_panel",false,false,"slide");
}
function loadShouYiDetail(){
    var networkState = navigator.network.connection.type;
    if( networkState == Connection.NONE ) {
    	showPopup("网络连接失败");
    	$("#ShouYiDetailP").html("");
    	$("#navbar").hide();
        return false;
    }else{	
		var orderid=getItem("orderid");
		var operatorid=getItem('userId');
		var ordername=getItem("ordername");
		var ordermpno=getItem("ordermpno");
		var orderaddress=getItem("orderaddress");
		var ordercount=getItem("ordercount");
		var wait = getItem("wait");
		var linkstatus = getItem("linkstatus");
		
		var str="<p style='line-height:30px;font-size:1.5em'>姓名："+ordername+"<br>电话：<a href='javascript:void(0)' onclick='dadianhua("+orderid+","+ordermpno+","+operatorid+");'>"+ordermpno+"</a><br>地址："+orderaddress+"<br></p>";
		str+="<p>物流单号:<br><input type='text' style='width:60%;float:left' id='expnumber'><a class='button sdsrBtn' onclick='Sdshu();'>手动输入</a></p>";
		str+="<p style='clear:both'>产品总数:"+ordercount+"</p>";
		if(wait == 1){
			str+="<a class='button shoujianBtn' onClick='' >收件完成</a>";
			str+="<a class='button ycclBtn' href='javascript:void(0)' onclick=''>异常处理</a>";
		}else{
			str+="<a class='button shoujianBtn' onClick='OrderTakeComplete();'>收件完成</a>";
			str+="<a class='button ycclBtn' href='javascript:void(0)' onclick='ycclShowPopup3();'>异常处理</a>";
		}
		str+="<a class='button contactCustomer' href='tel:4008762799'>联系客服</a>";
		

		$("#ShouYiDetailP").html(str);
		$.ui.toggleNavMenu();
	}
}
function ycclShowPopup3(){
	var str = "";
	str += "<span class='popupTit'>异常处理</span>";
	//str +="<input type='text' class='ipt1'>";
	str +="<select class='ipt1' style='width:100%' name='reasonSelect' id='reasonSelect' ><option value='用户退单'>用户退单</option><option value='延迟取件'>延迟取件</option><option value='联系不到用户'>联系不到用户</option></select><br>";
	str+="<div class='delayTime'><span class='popupTit'>延迟取件时间:</span>";
	str+="<select class='ipt1' style='width:40%;float:left' name='monthdaySelect' id='monthdaySelect'></select>";
	str+="<select class='ipt1' style='width:27%;float:left;margin-left:4%' name='hourSelect' id='hourSelect'>";
	str+="<option value='00'>00时</option><option value='01'>01时</option><option value='02'>02时</option><option value='03'>03时</option><option value='04'>04时</option><option value='05'>05时</option><option value='06'>06时</option><option value='07'>07时</option><option value='08'>08时</option><option value='09'>09时</option><option value='10'>10时</option><option value='11'>11时</option>";
	str+="<option value='12'>12时</option><option value='13'>13时</option><option value='14'>14时</option><option value='15'>15时</option><option value='16'>16时</option>";
	str+="<option value='17'>17时</option><option value='18'>18时</option><option value='19'>19时</option><option value='20'>20时</option>";
	str+="<option value='21'>21时</option><option value='22'>22时</option><option value='23'>23时</option>";
	str+="</select><select class='ipt1' name='minSelect' id='minSelect' style='width:27%;float:right'><option value='00'>00分</option><option value='15'>15分</option><option value='30'>30分</option><option value='45'>45分</option></select></div>";    $("#afui").popup({
        title: "",
        message: str,
        cancelText: "取消",
        cancelCallback: function () {
        	
        },
        doneText: "提交",
        doneCallback: function () {
	     var networkState = navigator.network.connection.type;
    	if( networkState == Connection.NONE ) {
	    	$("#afui .header").css("height",120);
	    	showPopup("网络连接失败");
	    	$("#navbar").hide();
	        return false;
        	}else{
			process();	
			var time = "";
			var ts=getTs();
			if(getItem("orderid")){
				var orderid = getItem("orderid");
			}
			if(getItem("userId")){
				var operatorid = getItem("userId");
			}
			var reasonSel = document.getElementById("reasonSelect");
			var index = reasonSel.selectedIndex; // 选中索引
			var text = reasonSel.options[index].text; // 选中文本
			var reason = reasonSel.options[index].value; // 选中值
			
			var monthdaySelect = document.getElementById("monthdaySelect");
			var monthdayindex = monthdaySelect.selectedIndex; // 选中索引
			var monthdayvalue = monthdaySelect.options[monthdayindex].value; // 选中值
			monthdayvalue = monthdayvalue.replace(/[-]+/g,'');
			var hourSelect = document.getElementById("hourSelect");
			var hourIndex = hourSelect.selectedIndex; // 选中索引
			var hourvalue = hourSelect.options[hourIndex].value; // 选中值
			
			
			var minSelect = document.getElementById("minSelect");
			var minindex = minSelect.selectedIndex; // 选中索引
			var minvalue = minSelect.options[minindex].value; // 选中值
			var s = hourvalue;
			if(s.toString().length < 2){
				s = "0"+s;
			}
			if(index != 1){
				time+="";
			}else{
				time+=monthdayvalue+s+minvalue+"00";			
			}
			var ts=getTs();
			var str2=actid+orderid+operatorid+time+version+ts;
			str2=str2.toUpperCase();
			var hash=hex_md5(str2);
			hash=hash.toUpperCase();
			hash=hash+Key;
			hash=hex_md5(hash);
			hash=hash.toUpperCase();
			var vkey=hash;
			var postData = {"actid":actid,"orderid":orderid,"operatorid":operatorid,"reason":reason,"time":time,"version":version,"ts":ts,"vkey": vkey };
			$.ajax({
				type: "post",
				url: host+"/OrderTakeFail",
				data: postData,
				dataType: "json",
				success: function(data){
					removeProcess();
					if(data.code==0){
						  $("#afui").popup({
								title: "Alert",
								message: "执行成功",
								cancelText: "确定",
								cancelCallback: function () {
									$.ui.loadContent("ShouYiList_panel",false,false,"slide");
								},
								doneText: "",
								doneCallback: function () {},
								cancelOnly: true
							});
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
        },
        doneOnly: true
    });
	var str1 = "";
	var dateA = getNextDate(6);
	for(var i = 0;i<dateA.length;i++){
		 keyarr = dateA[i].split("-");
		 var vstr = keyarr[1]+"月"+keyarr[2]+"日";
		 str1+="<option value='"+dateA[i]+"'>"+vstr+"</option>";
	}
	$("#monthdaySelect").append(str1);
	var reasonSel = document.getElementById("reasonSelect");
	var indx = reasonSel.selectedIndex;
	if(indx == 1){
		$(".delayTime").show();
	}else{
		$(".delayTime").hide();
	}	
	$("#reasonSelect").bind("change",function(){
		var indx = this.selectedIndex;
		if(indx == 1){
			$(".delayTime").show();
		}else{
			$(".delayTime").hide();
		}
	});
}
function getNextDate(AddDayCount){
	var dateArr = [];
	var mydate = new Date();
	var startDate = mydate;
	var startMillisecond = mydate.getTime();
	for(var i=0;i<=AddDayCount;i++){
		startDate.setTime(startMillisecond+i*24*60*60*1000);//获取AddDayCount天后的日期
		var y = startDate.getFullYear(); 
		var m = startDate.getMonth()+1;//获取当前月份的日期 
		var d = startDate.getDate(); 
		if(m.toString().length < 2){
			m = "0"+m;
		}
		if(d.toString().length < 2){
			d = "0"+d;
		}
		var riqi =y+"-"+ m+"-"+d;
		dateArr.push(riqi);	
	}
	return dateArr;
}
function Sdshu(){
	$("#expnumber").removeAttr("readonly");
}
function OrderTakeComplete(){
	var expnumber=$("#expnumber").val();
	if(expnumber==""){
		showPopup("物流单号不能为空，请输入");
	}else{
		$("#afui").popup({
			title: "",
			message: "确认收衣?",
			cancelText: "关闭",
			cancelCallback: function () {
				
			},
			doneText: "确认",
			doneCallback: function () {
			    var networkState = navigator.network.connection.type;
			    if( networkState == Connection.NONE ) {
			    	$("#afui .header").css("height",70);
			    	showPopup("网络连接失败");
			    	$("#navbar").hide();
			        return false;
			     }else{
				process();
				var orderid=getItem('orderid');
				var operatorid=getItem('userId');
				var ts=getTs();
				var str=actid+orderid+expnumber+operatorid+version+ts;
				str=str.toUpperCase();
				var hash=hex_md5(str);
				hash=hash.toUpperCase();
				hash=hash+Key;
				hash=hex_md5(hash);
				hash=hash.toUpperCase();
				var vkey=hash;
				var postData = {"actid":actid,"orderid":orderid,"expnumber":expnumber,"operatorid":operatorid,"version":version,"ts":ts,"vkey": vkey };
				$.ajax({
					type: "post",
					url: host+"/OrderTakeComplete",
					data: postData,
					dataType: "json",
					success: function(data){
						removeProcess();
						if(data.code==0){
							$.ui.loadContent("ShouYiList_panel",false,false,"slide");
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
			},
			cancelOnly: false
		});	
	}
}
function goBackList(){
	$.ui.loadContent("OperatorOnLineList_panel",false,false,"slide");
}

//收衣扫描
function scanCode() {
    window.plugins.barcodeScanner.scan(
    function(result) {
    	var str="";
    	if(result.text.length==6){
    		str+="002101"+result.text;
    	}else{
    		str+=result.text;
    	}
        $("#expnumber").val(str);
        $("#expnumber").attr("readonly","readonly");
    }, function(error) {
        alert("Scan failed: " + error);
    });
}
//揽件
function LoadOrderTake(){
	$("#afui .header").css("height",120);
}
function unLoadOrderTake(){
	$("#afui .header").css("height",70);
}
function addWldh(){//addWldhInput
	var str ="";
	var wldh = $("#addWldhInput").val();
	if(wldh == ""){
		showPopup("物流单号为空，请重新输入");
	}else{
		for(var i=0;i<$("#OrderTakeList .expnumberUnit div span").length;i++){
			var expnumberNum=$("#OrderTakeList .expnumberUnit div span").eq(i).text();
			if(wldh==expnumberNum){
				showPopup("物流单号重复");
				$("#OrderTakeList .expnumberUnit div span").eq(i).css("color","red");
				return;
			}
		}		
		str+="<li class='expnumberUnit'><div style='float:left;width:70%;'>物流单号:<span>"+wldh+"</span></div><a class='button' style='width:25%;float:right' onclick='delExpnumber(this);'>删除</a></li>";
		$("#OrderTakeList").prepend(str);
		$("#addWldhInput").val("");
	}
}
//揽件扫描
function OrderTakescanCode() {
    window.plugins.barcodeScanner.scan(
    function(result) {
    	if(result.cancelled!=true){
    		for(var i=0;i<$("#OrderTakeList .expnumberUnit div span").length;i++){
    			var expnumberNum=$("#OrderTakeList .expnumberUnit div span").eq(i).text();
    			if(result.text.length==6){
    				var wldh1="002101"+result.text;
    				if(wldh1==expnumberNum){
    					showPopup("物流单号重复");
    					return;
    				}
    			}
    		}
	    	var wldh="";
	    	var str="";
	    	if(result.text.length==6){
	    		wldh+="002101"+result.text;
	    	}else{
	    		wldh+=result.text;
	    	}
	    	str+="<li class='expnumberUnit'><div style='float:left;width:70%;'>物流单号:<span>"+wldh+"</span></div><a class='button' style='width:25%;float:right' onclick='delExpnumber(this);'>删除</a></li>";
	    	$("#OrderTakeList").prepend(str);
    	}
    }, function(error) {
        alert("Scan failed: " + error);
    });
}
function expnumberSub(){
	var len=$("#OrderTakeList .expnumberUnit").length;
	if(len==0){
		showPopup("提交物流单号失败");
	}else{
	    var networkState = navigator.network.connection.type;
	    if( networkState == Connection.NONE ) {
	    	$("#afui .header").css("height",120);
	    	showPopup("网络连接失败");
	    	$("#navbar").show();
	        return false;
	    }else{
			process();
			var operatorid=getItem('userId');
			var ts=getTs();
			var str="";
			var expnumber="";
			var expnumberArr=[];
		    for(var i=0;i<$("#OrderTakeList .expnumberUnit div span").length;i++){
				var expnumberNum=$("#OrderTakeList .expnumberUnit div span").eq(i).text();
				expnumber+=expnumberNum;
				expnumberArr.push(expnumberNum);
			}
			str=actid+expnumber+operatorid+version+ts;
			str=str.toUpperCase();
			var hash=hex_md5(str);
			hash=hash.toUpperCase();
			hash=hash+Key;
			hash=hex_md5(hash);
			hash=hash.toUpperCase();
			var vkey=hash;
			var postData = {"actid":actid,"expnumberlist":expnumberArr,"operatorid":operatorid,"version":version,"ts":ts,"vkey": vkey };
			$.ajax({
				type: "post",
				url: host+"/OrderTakeSend",
				data: postData,
				dataType: "json",
				success: function(data){
					removeProcess();
					if(data.code==0){
						showPopup("揽件成功");
						$("#OrderTakeList .expnumberUnit").remove();
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
	}
}
function delExpnumber(obj){
	$("#afui").popup({
		title: "",
		message: "您确定要删除物流单号吗?",
		cancelText: "取消",
		cancelCallback: function () {
			
		},
		doneText: "确定",
		doneCallback: function () {
			$(obj).parent().remove();
		},
		cancelOnly: false
	});			
}
function showMask(text) {
    $.ui.showMask(text);
    window.setTimeout(function () {
        $.ui.hideMask();
    }, 2000);
}
function unLoadSongYiList(){
	$("#afui .header").css("height",70);
}
//送衣列表函数
function LoadSongYiList(){
    var networkState = navigator.network.connection.type;
    if( networkState == Connection.NONE ) {
    	$("#afui .header").css("height",120);
    	showPopup("网络连接失败");
    	$("#navbar").hide();
        return false;
    }else{
		process();
		$("#afui .header").css("height",120);
		var operatorid=getItem('userId');
		var ts=getTs();
		str=actid+operatorid+version+ts;
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
			url: host+"/OrderWaitSendList",
			data: postData,
			dataType: "json",
			success: function(data){
				removeProcess();
				if(data.code==0){
					var str1="";
					var jsonData=data.list;
					for(var i=0;i<jsonData.length;i++){
						str1+='<li style="font-size:1.2em;line-height:25px" onclick="SongYiDetail(\''+jsonData[i].id+'\')">姓名:'+jsonData[i].name+'<br>手机:'+jsonData[i].mpno+'<br>地址:'+jsonData[i].address+'<br>送件数量:'+jsonData[i].sendcount+'</li>';
					}
					$("#SongYiList").html(str1);
				}else{
					showPopup(data.message);
				}
			},
			error:function(msg){
				removeProcess();
				showPopup("服务异常");
			}
		 });
		 $.ui.toggleNavMenu();
	 }
}

//送衣详情
function SongYiDetail(SongYiid){
	setItem("SongYiid",SongYiid);
	setItem("hRecord",0)
	$.ui.loadContent("SongYiDetail_panel",false,false,"slide");
}
//历史送衣详情
function HSongYiDetail(SongYiInfo){
	setItem("SongYiInfo",SongYiInfo);
	$.ui.loadContent("hSongYiDetail_panel",false,false,"slide");
}

function hLoadSongYiDetail(){
	var songYiInfo = getItem("SongYiInfo");
	//var thjson = eval('(' + songYiInfo + ')');
	//alert(thjson.mpno);
	$("#navbar").hide();
}

function syDetailRefresh(){
	LoadSongYiDetail();	
}



//送衣详情panel加载时执行的函数
function LoadSongYiDetail(){
    var networkState = navigator.network.connection.type;
    if( networkState == Connection.NONE ) {
    	$("#afui .header").css("height",70);
    	$("#SongYiDetailUl").html("");
    	showPopup("网络连接失败");
    	$("#navbar").hide();
        return false;
        }else{
			process();
			var orderid=getItem('SongYiid');
			var operatorid=getItem('userId');
			var ts=getTs();
			str=actid+orderid+operatorid+version+ts;
			str=str.toUpperCase();
			var hash=hex_md5(str);
			hash=hash.toUpperCase();
			hash=hash+Key;
			hash=hex_md5(hash);
			hash=hash.toUpperCase();
			var vkey=hash;
			var postData = {"actid":actid,"orderid":orderid,"operatorid":operatorid,"version":version,"ts":ts,"vkey": vkey };
			$.ajax({
				type: "post",
				url: host+"/OrderWaitSendDetail",
				data: postData,
				dataType: "json",
				success: function(data){
					removeProcess();
					if(data.code==0){
						var jsonData=data.order;
						var str="<p style='line-height:30px;font-size:1.5em'>姓名:"+jsonData.name+"<br>电话:<a href='tel:"+jsonData.mpno+"' >"+jsonData.mpno+"</a><br>地址:"+jsonData.address+"<br>物流单号:"+jsonData.expnumber+"<br>产品总数:"+jsonData.count+"<br>送件数量:"+jsonData.sendcount+" </p>";
						str+="<table border='0' cellspacing='0' cellpadding='0' align='center' class='syxqtabs'>";
						str+="<thead><tr><th>名称</th><th>数量</th></tr></thead>";
						for(var j=0;j<jsonData.itemlist.length;j++){
							str+="<tbody><tr><td>"+jsonData.itemlist[j].name+"</td><td>"+jsonData.itemlist[j].count+"</td></tr></tbody>";
						}
						str+="</table>";
						str+="<p style='text-align:right;font-size:16px'>应收金额：<font color='red'>￥"+jsonData.money+"</font></p>";
						str+="<p>备注:"+jsonData.remark+"</p>";		 
						$("#SongYiDetailUl").html(str);
					}else{
						showPopup(data.message);
					}
				},
				error:function(msg){
					removeProcess();
					showPopup("服务异常");
				}
			 });
	 }
	 if(getItem("hRecord") == 1){
		 $("#navbar").hide();
	 }else{
		  $("#navbar").show();
	  }
 	//
}
function OrderSendComplete(){
	$("#afui").popup({
		title: "",
		message: "确认送达?",
		cancelText: "关闭",
		cancelCallback: function () {
			
		},
		doneText: "确定",
		doneCallback: function () {
    var networkState = navigator.network.connection.type;
    if( networkState == Connection.NONE ) {
    	$("#afui .header").css("height",70);
    	showPopup("网络连接失败");
    	$("#navbar").hide();
        return false;
    }else{
			process();
			var orderid=getItem('SongYiid');
			var operatorid=getItem('userId');
			var ts=getTs();
			str=actid+orderid+operatorid+version+ts;
			str=str.toUpperCase();
			var hash=hex_md5(str);
			hash=hash.toUpperCase();
			hash=hash+Key;
			hash=hex_md5(hash);
			hash=hash.toUpperCase();
			var vkey=hash;
			var postData = {"actid":actid,"orderid":orderid,"operatorid":operatorid,"version":version,"ts":ts,"vkey": vkey };
			$.ajax({
				type: "post",
				url: host+"/OrderSendComplete",
				data: postData,
				dataType: "json",
				success: function(data){
					removeProcess();
					if(data.code==0){
						showPopup("送件成功");
						$("#SongYiDetailUl").html("");
						$("#afui").popup({
							title: "",
							message: "送件完成",
							cancelText: "关闭",
							cancelCallback: function () {
								$.ui.loadContent("SongYiList_panel",false,false,"slide");
							},
							doneText: "继续收衣",
							doneCallback: function () {
								$("#afui").popup({
									title: "",
									id:"pops",
									message: "您确定要收衣吗?",
									cancelText: "取消",
									cancelCallback: function () {
										$.ui.loadContent("SongYiList_panel",false,false,"slide");
									},
									doneText: "确定",
									doneCallback: function () {
										var networkState = navigator.network.connection.type;
										if( networkState == Connection.NONE ) {
									    	$("#afui .header").css("height",70);
									    	showPopup("网络连接失败");
									    	$("#navbar").hide();
									        return false;
									    }else{
											var orderid=getItem('SongYiid');
											var operatorid=getItem('userId');
											var ts=getTs();
											str=actid+orderid+operatorid+version+ts;
											str=str.toUpperCase();
											var hash=hex_md5(str);
											hash=hash.toUpperCase();
											hash=hash+Key;
											hash=hex_md5(hash);
											hash=hash.toUpperCase();
											var vkey=hash;
											var postData1 = {"actid":actid,"orderid":orderid,"operatorid":operatorid,"version":version,"ts":ts,"vkey": vkey };
											$.ajax({
												type: "post",
												url: host+"/ReCreateOrder",
												data: postData1,
												dataType: "json",
												success: function(data){
													if(data.code==0){
														$.ui.loadContent("ShouYiList_panel",false,false,"slide");
													}else{
														showPopup(data.message);
													}
												},
												error:function(msg){
													showPopup("服务异常");
												}
											 });
									    }
									},
									cancelOnly: false
								});		
							},
							cancelOnly: false
						});
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
		},
		cancelOnly: false
	});	
}
//接单列表模糊搜索
function jdsearchResult(){

	$("#OrderWaitTakeListUl li").hide();
	var v = $("#jdsearchInput").val().toString();
	v = v.replace(/\s+/g,',');
	var keyArr = v.split(',');
	$("#OrderWaitTakeListUl li").each(function(i){
		for (var i = 0; i < keyArr.length; i++){
			if($(this).html().indexOf(keyArr[i]) >=0){
				$(this).html().replace(keyArr[i],"<b>"+keyArr[i]+"</b>");
				$(this).show();
			}				
		}
	});
}
function jdOnInput(event){
	var val = event.target.value;
	if(val == ""){
		$("#OrderWaitTakeListUl li").show();
	}
}


//收衣列表模糊查询
function searchResult(){
	$("#ShouYiList_panelUl li").hide();
	var v = $("#searchInput").val().toString();
	v = v.replace(/\s+/g,',');
	var keyArr = v.split(',');
	$("#ShouYiList_panelUl li").each(function(i){
		for (var i = 0; i < keyArr.length; i++){
			if($(this).html().indexOf(keyArr[i]) >=0){
				$(this).html().replace(keyArr[i],"<b>"+keyArr[i]+"</b>");
				$(this).show();
			}				
		}
	});
}
function OnInput(event){
	var val = event.target.value;
	if(val == ""){
		$("#ShouYiList_panelUl li").show();
	}
}
//送衣列表模糊查询
function songYisearchResult(){
	$("#SongYiList li").hide();
	var v = $("#songYisearchInput").val().toString();
	v = v.replace(/\s+/g,',');
	var keyArr = v.split(',');
	$("#SongYiList li").each(function(i){
		for (var i = 0; i < keyArr.length; i++){
			if($(this).html().indexOf(keyArr[i]) >=0){
				$(this).show();
			}				
		}
	});
}
function songYiOnInput(event){
	var val = event.target.value;
	if(val == ""){
		$("#SongYiList li").show();
	}
}

                   

