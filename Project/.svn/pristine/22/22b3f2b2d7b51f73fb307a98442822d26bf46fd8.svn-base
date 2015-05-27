
﻿(function ($) {
    $.popdiv = {
        iconImg: "<img src='/img/icon-120.png'>",
        bottom: 50,
        right: 20,

        clientw: 0,
        clienth: 0,
        bool: "true",
        moveOnce: "true",

        isToggle: "true",
        _init: function () {

            $.popdiv.clientw = $(window).width();
            $.popdiv.clienth = $(window).height();

            $.popdiv._showIconImg();
        },

        _showIconImg: function () {
            var str = "<div id='menuIcon' class='menuIcon'>" + $.popdiv.iconImg + "";
            str += "</div>";
            $("body").prepend(str);
            if (getItem("l") && getItem("t")) {
                $(".menuIcon").css({
                    left: getItem("l") + "px",
                    top: getItem("t") + "px"
                });
            } else {
                $(".menuIcon").css({
                    right: $.popdiv.right,
                    bottom: $.popdiv.bottom
                });
            }
            document.querySelector(".menuIcon").addEventListener("touchmove", $.popdiv._iconMove, false);
            document.querySelector(".menuIcon").addEventListener("touchend", $.popdiv._showPop, false);
        },
        _iconMove: function (event) {
            var obj = document.querySelector(".menuIcon");
            document.body.addEventListener('touchmove', $.popdiv.a, false);
            if (event.targetTouches.length == 1) {
                event.preventDefault();
                event.stopPropagation();
                $(obj).css({
                    opacity: .8
                });
                var touch = event.targetTouches[0];
                if (touch.clientX - 30 <= 0) {
                    obj.style.left = 0
                } else if (touch.clientX >= $.popdiv.clientw - 30) {
                    obj.style.left = $.popdiv.clientw - 60 + 'px';
                } else if (touch.clientY - 30 <= 0) {
                    obj.style.top = 0
                } else if (touch.clientY >= $.popdiv.clienth - 30) {
                    obj.style.top = $.popdiv.clienth - 60 + 'px';
                } else {
                    obj.style.left = touch.clientX - 30 + 'px';
                    obj.style.top = touch.clientY - 30 + 'px';
                }
                $.popdiv.bool = false;
            }
        },
        _showPop: function (event) {
            event.preventDefault();
            event.stopPropagation();
            document.body.removeEventListener("touchmove", $.popdiv.a, false);

            $(".menuIcon").css({
                opacity: .4
            });
            setItem("l", parseInt($(".menuIcon").css("left")));
            setItem("t", parseInt($(".menuIcon").css("top")));

            if ($.popdiv.bool) {
                $(".navIconGroup").show();
                $(".iconGroup1").show();
                $(".iconGroup2").hide();
				$("body").prepend("<div class='blackmask' ontouchend='hidePopdiv(event);'></div>");
				$(".menuIcon").hide();
                $DisSelects = $("select");
                $DisSelects.attr("disabled", true);
                $DisInputs = $("input");
                $DisInputs.attr("disabled", true);
                $DisTextarea = $("textarea");
                $DisTextarea.attr("disabled", true);
            }
            setTimeout(function () {
                $.popdiv.bool = true;
            }, 15);
        },
        a: function (e) { e.preventDefault(); },
    }
    switchPage = function (event, wpage) {
        setTimeout(function () {
            window.location.href = wpage;
        }, 300);
        event.preventDefault();
        event.stopPropagation();

    }
    _docPrevent = function () {
        document.addEventListener("touchmove", $.popdiv.a, false);
    }
    _docRemovePrevent = function () {
        document.removeEventListener("touchmove", $.popdiv.a, false);
    }
    switchIconGroup = function (n, event) {
        switch (n) {
            case 1:
                $(".iconGroup1").hide();
                $(".iconGroup2").show();
                break;
            case 2:
                $(".iconGroup1").show();
                $(".iconGroup2").hide();
                break;
        }
        event.stopPropagation();
    },
	hidePopdiv = function (event) {
	    event.preventDefault();
	    event.stopPropagation();
	    setTimeout(function () {
	        $(".navIconGroup").hide();
	        $DisSelects = $("select");
	        $DisSelects.attr("disabled", false);
	        $DisInputs = $("input");
	        $DisInputs.attr("disabled", false);
	        $DisTextarea = $("textarea");
	        $DisTextarea.attr("disabled", false);
	    }, 300);
	    $(".blackmask").remove();
	    $(".menuIcon").show();

	}
    viewShopcartDetail = function (event) {
        
        event.preventDefault();
        if ($.popdiv.isToggle) {
            $("body").prepend("<div class='whitemask'></div>");
            $(".shopcartList").css({
                'bottom': 50,
                'zIndex': 200
            });
            $.popdiv.isToggle = false;
        } else {
            $(".whitemask").remove();
            $(".shopcartList").css({
                'bottom': -380,
                'zIndex': -1
            });
            $.popdiv.isToggle = true;
        }
    }
    setCss3 = function (obj, attrObj) {
        for (var i in attrObj) {
            var newi = i;
            if (newi.indexOf("-") > 0) {
                var num = newi.indexOf("-");
                newi = newi.replace(newi.substr(num, 2), newi.substr(num + 1, 1).toUpperCase());
            }
            obj.style[newi] = attrObj[i];
            newi = newi.replace(newi.charAt(0), newi.charAt(0).toUpperCase());
            obj.style["webkit" + newi] = attrObj[i];
            obj.style["moz" + newi] = attrObj[i];
            obj.style["o" + newi] = attrObj[i];
            obj.style["ms" + newi] = attrObj[i];
        }
    }
    showPopDiv = function (msg, success, time, callBack) {
        if (time == null)
            time = 2000;
        if ($(".popdiv").length == 0) {
            $("body").prepend("<div class='mask'></div><div class='popdiv'><span>" + msg + "</span></div>");
            setTimeout(function () {
                $(".popdiv,.mask").remove();
                if (success) {
                    if (callBack != null)
                        window.location.href = callBack;
                    else
                        window.location.reload();
                }
            }, time);
        }
    }
    setItem = function (key, val) {
        window.localStorage.setItem(key, val);
    }
    getItem = function (key) {
        return window.localStorage.getItem(key);
    }

    //加载进度
    process = function (status) {
        var str = "";
        str += "<div class='processmask'></div><div class='processBox'><div class='ball'></div><div class='ball1'></div></div>";
        $("body").prepend(str);

        document.querySelector(".processmask").addEventListener("touchmove", function (e) {
            e.preventDefault();
        }, false);
    }

    removeProcess = function (status) {
        $(".processmask, .processBox").remove();
    }

    CheckEmail = function (obj, msgObj) {
        var rtn = new Object();
        rtn.code = 1;
        if ($(obj).val() == "") {
            rtn.code = 0;
            rtn.msg = "邮箱不能为空！";
            if (msgObj != null) {
                $(msgObj).css("color", "red");
                $(msgObj).html(rtn.msg);
            }
            return rtn;
        }


        if (!$(obj).val().match(/^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/)) {
            rtn.code = 0;
            rtn.msg = "邮箱格式不正确！";
            if (msgObj != null) {
                $(msgObj).css("color", "red");
                $(msgObj).html(rtn.msg);
            }
            return rtn;
        }
        return rtn;
    }

    CheckMobile = function (obj, msgObj) {
        var rtn = new Object();
        rtn.code = 1;
        if ($(obj).val() == "") {
            rtn.code = 0;
            rtn.msg = "手机号码不能为空！";
            if (msgObj != null) {
                $(msgObj).css("color", "red");
                $(msgObj).html(rtn.msg);
            }
            return rtn;
        }

        if (!$(obj).val().match(/^0?(13[0-9]|15[012356789]|18[0-9]|14[57]|177)[0-9]{8}$/)) {
            rtn.code = 0;
            rtn.msg = "手机号码格式不正确！";
            if (msgObj != null) {
                $(msgObj).css("color", "red");
                $(msgObj).html(rtn.msg);
            }
            return rtn;
        }
        return rtn;
    }

})(jQuery);


$(function () {
    $.popdiv._init();
});

