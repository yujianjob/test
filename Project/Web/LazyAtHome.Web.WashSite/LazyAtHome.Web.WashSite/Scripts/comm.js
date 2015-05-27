$(function () {
    //搜索
    $('.searchCity').hover(function () {
        $(this).find('ul').show();
    }, function () {
        $(this).find('ul').hide();
    })
    $('.searchCity li').click(function () {
        var html = $(this).find('a').html();
        $('#city').attr('value', html);
        $('.searchCity ul').hide();
    })

    if (!String.prototype.trim) {
        String.prototype.trim = function () {
            return this.replace('/^/s+|/s+$/g', '');
        }
    }

    setUpPos();
});

function WashSearch() {
    if ($("#keyword").val() == "") {
        $("#keyword").focus();
        return false;
    }
    return true;
}

function CartDelete(id, obj, price, pCount) {
    $.ajax({
        type: "post",
        url: "/Cart/jsCartDelete",
        data: { 'id': id },
        dataType: "json",
        success: function (resultJson) {
            if (resultJson.Code == 1) {
                $(obj).parent().parent().remove();
                price.html(resultJson.Price + "元");
                pCount.html(resultJson.Count + "件商品");
            }
        }
    });
}

function setUpPos() {
    var str = "<div id='up'><a href='javascript:void(0);' onclick='backToTop();'><img src='/Content/images/up-icn.png' /></a></div>";
    $("body").prepend(str);
    $(window).scroll(function () {
        getPos();
    });
    $(window).resize(function () {
        getPos();
    });
    function getPos() {
        var scrollT = $(document).scrollTop();
        var clientW = $(window).width();
        var clientH = $(window).height();
        clientH = 120;
        var offsetw = parseInt((clientW - 1000) / 2);
        if (scrollT > clientH && offsetw >= 30) {
            $("#up").css({ right: offsetw - 30, bottom: 50 });
            $("#up").fadeIn();
        } else if (scrollT < clientH && offsetw >= 30) {
            $("#up").css({ right: offsetw - 30, bottom: 50 });
            $("#up").fadeOut();
        } else if ((scrollT > clientH && offsetw <= 0) || (scrollT > clientH && offsetw > 0 && offsetw < 40)) {
            $("#up").css({ right: 0, bottom: 50 });
            $("#up").fadeIn();
        } else if ((scrollT < clientH && offsetw <= 0) || (scrollT < clientH && offsetw > 0 && offsetw < 40)) {
            $("#up").css({ right: 0, bottom: 50 });
            $("#up").fadeOut();
        }
    }
}

function backToTop() {
    $(document).scrollTop(0);
}

function addFavorite() {
    if (document.all) {
        window.external.addFavorite("http://www.landaojia.com", "懒到家生活服务");
    } else if (window.sidebar) {
        window.sidebar.addPanel("懒到家生活服务", "http://www.landaojia.com", "");
    }
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
