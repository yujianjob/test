$(function () {
    setSiderbar();
    setshopcartList();
    setorderList();
    $("a[data-toggle='dropdown']").mouseout(function () {
        $(this).css("background", "#4cc7c8");
    });

    var a = function (e) { e.preventDefault(); }
    if (nvh) {
        var nvh = document.getElementById("navbar-header");
        nvh.addEventListener("touchmove", a, false);
    }
    if (od) {
        var od = document.getElementById("sidebar-wrapper");
        od.addEventListener("touchmove", a, false);
    }

    if (!String.prototype.trim) {
        String.prototype.trim = function () {
            return this.replace('/^/s+|/s+$/g', '');
        }
    }
	
	
});

//点击显示掩藏侧边栏
var bo = true;
var a = function (e) { e.preventDefault(); }
function showHideSiderbar() {
    $('.wrapper').toggleClass('active');
	if (bo) {
		$('body').prepend("<div class='mask' id='mask'></div>");
		var omask = document.getElementById('mask');
		omask.addEventListener("touchstart", a, false);
		omask.addEventListener("touchmove", a, false);
		omask.addEventListener("touchend", a, false);
		
		
		var siderbar = document.getElementById('sidebar-wrapper');
		siderbar.addEventListener("touchstart", a, false);
		siderbar.addEventListener("touchmove", a, false);
		siderbar.addEventListener("touchend", a, false);
		
		
		$("#navbar-header").css({
			"position":"fixed",
			"left":220,
			"top":0,
			"width":"100%",
			"zIndex":100
		});
		bo = false;
		
	}
	else {
		$('.mask').remove();
		$("#navbar-header").css({
			"position":"fixed",
			"left":0,
			"top":0,
			"width":"100%",
			"zIndex":100
		});
		bo = true;
	}
}
$(function(){
	$(window).load(function(){
		var toph=$("#navbar-header").height();
		$("#navbar-header").css({
			"position":"fixed",
			"left":0,
			"top":0,
			"width":"100%",
			"zIndex":100
		});
		$(".page-content").css("paddingTop",toph-30);
	});
});

$(window).resize(function () {
    setSiderbar();
});
function setSiderbar() {
    var clientw = $(window).width();
    if (clientw <= 767) {
        $('.wrapper').addClass('active');
    } else {
        $('.wrapper').removeClass('active');
    }
}
//我的懒人卡显示掩藏
function lrkCollapse(obj) {
    $(obj).mouseout(function () {
        $(this).css("textDecoration", "none");
    });
    $(".mylanren-card .rt a").html('∨');
    if ($(obj).attr('class')) {
        $(obj).html('∧');
    } else {
        $(obj).html('∨');
    }
}
function setshopcartList() {
    $(window).scroll(function () {
        var scrollTop = $(window).scrollTop();
        if (scrollTop >= 30) {
            $(".shopcartList").css({ 'position': 'fixed', 'top': 0, 'right': 15 })
        } else {
            $(".shopcartList").css({ 'position': '', 'top': '' })
        }
        var mt = $(document).height() - 50 - $(".shopcartList").height() - scrollTop;
        if (mt > 0) mt = 0;
        $(".shopcartList").css("marginTop", mt);
    });
}
function showXdfw(obj) {
    $(".xdfw-box .panel-title a").html('∨');
    if ($(obj).attr('class') == "panel-heading collapsed") {
        $(obj).find("a").html('∧');
    } else {
        $(obj).find("a").html('∨');
    }
}

function setorderList() {
    $(window).scroll(function () {
        var scrollTop = $(window).scrollTop();
        if (scrollTop >= 30) {
            $(".myorderList").css({ 'position': 'fixed', 'top': 0, 'right': 15 })
        } else {
            $(".myorderList").css({ 'position': '', 'top': '' })
        }
        var mt = $(document).height() - 50 - $(".myorderList").height() - scrollTop;
        if (mt > 0) mt = 0;
        $(".myorderList").css("marginTop", mt);
    });
}
var isOrderMASK = true, isorderTxt = true;
function OrderCollpase(obj) {
    if (isOrderMASK) {
        //$("body").prepend("<div class='visible-xs visible-sm mask'></div>");
        isOrderMASK = false;
    }
    if (isorderTxt) {
        $(obj).html("关闭订单");
        var clientw = $(window).width();
        var offsetTop = $(obj).offset().top;
        var offsetLeft = $(obj).offset().left;
        var h = $(window).height() - offsetTop;
        $(".shopcart-collapse").show();
        isorderTxt = false;
    } else {
        $(obj).html("查看订单");
        $(".shopcart-collapse").hide();
        //$(".mask").remove();
        isOrderMASK = true;
        isorderTxt = true;
    }
    $(window).resize(function () {
        var clientw1 = $(window).width();
        var offsetTop1 = parseInt($(obj).offset().top);
        var offsetLeft1 = $(obj).offset().left;
        var h1 = $(window).height() - offsetTop1;
        if (!isOrderMASK) {
            $(".shopcart-collapse").show();
        }
    });
}


function CheckEmail(obj, msgObj) {
    var rtn = new Object();
    rtn.code = 1;
    if ($(obj).val() == "") {
        rtn.code = 0;
        rtn.msg = "邮箱不能为空！";
        obj.focus();
        if (msgObj != null) {
            $(msgObj).css("color", "red");
            $(msgObj).html(rtn.msg);
        }
        return rtn;
    }


    if (!$(obj).val().match(/^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/)) {
        rtn.code = 0;
        rtn.msg = "邮箱格式不正确！";
        obj.focus();
        if (msgObj != null) {
            $(msgObj).css("color", "red");
            $(msgObj).html(rtn.msg);
        }
        return rtn;
    }
    return rtn;
}

function CheckMobile(obj, msgObj) {
    var rtn = new Object();
    rtn.code = 1;
    if ($(obj).val() == "") {
        rtn.code = 0;
        rtn.msg = "手机号码不能为空！";
        obj.focus();
        if (msgObj != null) {
            $(msgObj).css("color", "red");
            $(msgObj).html(rtn.msg);
        }
        return rtn;
    }

    if (!$(obj).val().match(/^0?(13[0-9]|15[012356789]|18[0-9]|14[57])[0-9]{8}$/)) {
        rtn.code = 0;
        rtn.msg = "手机号码格式不正确！";
        obj.focus();
        if (msgObj != null) {
            $(msgObj).css("color", "red");
            $(msgObj).html(rtn.msg);
        }
        return rtn;
    }
    return rtn;
}
$(function(){
	$(window).load(function(){
		var h=$("#navbar-header").height();
		$(".viewshopcart").css({
			"position":"fixed",
			'top':h+5,
			'right':20
		});
		$(".shopcartList-collapse").css({
			"position":"fixed",
			'top':h+60,
			'right':20
		});
		$(".orderList-collapse").css({
			"position":"fixed",
			'top':h+60,
			'right':20
		});

	});	
});
function showPopDiv(msg, success, time, callBack) {
    if (time == null)
        time = 2000;
    if ($(".popdiv").length == 0) {
        $("body").prepend("<div class='popdiv'>" + msg + "</div>");
        setTimeout(function () {
            $(".popdiv").remove();
            if (success) {
                if (callBack != null)
                    window.location.href = callBack;
                else
                    window.location.reload();
            }
        }, time);
    }
}