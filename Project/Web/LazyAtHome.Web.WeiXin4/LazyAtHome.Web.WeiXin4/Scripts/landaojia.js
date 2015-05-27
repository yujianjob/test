function showPopDiv(msg, success, time, callBack) {
    if (time == null)
        time = 2000;
    if ($(".popBox").length == 0) {
        $("body").prepend("<div class='popBox'><span>" + msg + "</span></div>");
        var clientw = $(window).width();
        var h = $(".popBox").height() * 2;
        $(".popBox").css({ width: parseInt(clientw / 1.2), marginLeft: -parseInt(clientw / 2.4), marginTop: -h / 2 });
        setTimeout(function () {
            $(".popBox").remove();
            if (success) {
                if (callBack != null)
                    window.location.href = callBack;
                else
                    window.location.reload();
            }
        }, time);
    }
}

function process() {
    var str = "<div class='loading'><img src='../img/loading.gif'></div>";
    $("body").prepend(str);
}

function removeProcess() {
    if ($(".loading").length > 0) {
        $(".loading").remove();
    }
}

function GetLocation() {
    process();
    $.ajax({
        type: "post",
        url: "/Location/GetUserLocation",
        dataType: "json",
        success: function (resultJson) {
            removeProcess();
            if (resultJson != null) {
                if (resultJson.Code == 1) {
                    txtAddress.value = resultJson.Address;
                }
                else
                    showPopDiv(resultJson.Msg);
            }
        },
        error: function (msg) {
            showPopDiv(msg.toString());
        }
    });
}

function CheckEmail(obj, msgObj) {
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

function CheckMobile(obj, msgObj) {
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
