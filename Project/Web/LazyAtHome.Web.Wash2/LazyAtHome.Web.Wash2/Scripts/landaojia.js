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
    if (browser.versions.android || browser.versions.iPhone || browser.versions.iPad || browser.versions.ios) {
        if (bo) {
            $('body').prepend("<div class='mask' id='mask'></div>");
            var omask = document.getElementById('mask');
            omask.addEventListener("touchstart", a, false);
            omask.addEventListener("touchmove", a, false);
            omask.addEventListener("touchend", a, false);
            bo = false;
        }
        else {
            $('.mask').remove();
            bo = true;
        }
    }
}

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
var isMask = true, isTxt = true;
function shopcartCollpase(obj) {
    if (isMask) {
        //$("body").prepend("<div class='visible-xs visible-sm mask'></div>");
        isMask = false;
    }
    if (isTxt) {
        $(obj).html("关闭洗衣篮");
        var clientw = $(window).width();
        var offsetTop = $(obj).offset().top;
        var offsetLeft = $(obj).offset().left;
        var h = $(window).height() - offsetTop;
        $(".shopcart-collapse").show();
        isTxt = false;
    } else {
        $(obj).html("查看洗衣篮");
        $(".shopcart-collapse").hide();
        //$(".mask").remove();
        isMask = true;
        isTxt = true;
    }
    $(window).resize(function () {
        var clientw1 = $(window).width();
        var offsetTop1 = parseInt($(obj).offset().top);
        var offsetLeft1 = $(obj).offset().left;
        var h1 = $(window).height() - offsetTop1;
        if (!isMask) {
            $(".shopcart-collapse").show();
        }
    });
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
function orderCollpase(obj) {
    if (isOrderMASK) {
        //$("body").prepend("<div class='visible-xs visible-sm mask'></div>");
        isOrderMASK = false;
    }
    if (isorderTxt) {
        $(obj).html("掩藏订单");
        var clientw = $(window).width();
        var offsetTop = $(obj).offset().top;
        var offsetLeft = $(obj).offset().left;
        var h = $(window).height() - offsetTop;
        $(".order-collapse").show();
        isorderTxt = false;
    } else {
        $(obj).html("查看订单");
        $(".order-collapse").hide();
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
            $(".order-collapse").show();
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

function showPopDiv(msg, success, time, callBack) {
    if (time == null)
        time = 2000;
    if ($(".popdiv").length == 0) {
        $("body").prepend("<div class='popdiv'><span>" + msg + "</span></div>");
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



(function ($) {
    $.alerts = {
        okButton: "确定",
        cancelButton: "取消",
        confirm: function (title, msg, callback) {

            $.alerts._show(title, msg, 'confirm', function (result) {
                if (callback) {
                    callback(result);
                }
            });
        },
        _show: function (title, msg, type, callback) {
            $("body").append(
			  '<div id="popup_container" class="popdiv1">' +
			    '<h2 id="popup_title" style="padding-top:15px;padding-bottom:15px"></h2>' +
			    '<div id="popup_content" style="padding:0 20px">' +
				'</div>' +
			  '</div>');
            //var pos = ($.browser.msie && parseInt($.browser.version) <= 6 ) ? 'absolute' : 'fixed'; 

            $("#popup_title").text(title);
            //$("#popup_message").text(msg);
            switch (type) {
                case "confirm":
                    $("#popup_content").html('<div id="popup_panel"><input type="button" value="' + $.alerts.okButton + '" id="popup_ok" /> <input type="button" value="' + $.alerts.cancelButton + '" id="popup_cancel" /></div>');
                    break;
            }
            $("#popup_ok").click(function () {
                if (callback) {
                    callback(true);
                }
                
            });
            $("#popup_cancel").click(function () {
                if (callback) {
                    callback(false);
                }
                $.alerts._hide();
            });
        },
        _hide: function () {
            $("#popup_container").remove();
        }

    },

	jConfirm = function (title, msg, callback) {
	    $.alerts.confirm(title, msg, callback);
	}
})(jQuery);