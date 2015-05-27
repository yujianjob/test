
$(function () {
    $("#IbtnEnter").click(function () {
        if (!checkData())
            return false;
        $("#TxtErrorMsg").html("正在提交登录信息......");
        $.ajax({
            type: "POST",
            url: $("#LoginUrl").val(),
            data: {iStoreCode : $("#storecode").val(), iLoginName: $("#username").val(), iPassWord: $("#password").val(), iValidateCode: $("#userauthcode").val() },
            dataType: "json",
            success: function (data) {
                $("#TxtErrorMsg").html("提交成功，正在验证信息......");
                if (data.success) {
                    $("#TxtErrorMsg").html("登录成功，正在跳转......");
                    //* 登录成功，跳转主页面
                    window.location.href = $("#SystemMainUrl").val();
                }
                else { 
                    $("#TxtErrorMsg").html(data.message);
                }
            }
        });
        //return false;
    });

    $('#storecode').bind("change", function () {
        $("#TxtErrorMsg").html("");
    });
    $('#username').bind("change", function () {
        $("#TxtErrorMsg").html("");
    });

    $('#password').bind("change", function () {
        $("#TxtErrorMsg").html("");
    });

    $('#userauthcode').bind("change", function () {
        $("#TxtErrorMsg").html("");
    });

    $("#valiCode").bind("click", function () {     
        this.src = $("#ValidateCodeUrl").val() + "?time=" + (new Date()).getTime();
    });

});


function doEnterKey() {
    if (event.keyCode == 13) {
        $('#IbtnEnter').click();
    }
}
//function checkLoginType() {
//    var acct = document.getElementById("account").value;
//    var emailRe = /\w+([-+.]\w+)*\w+([-.]\w+)*\.\w+([-.]\w+)*/;
//    if (emailRe.test(acct)) {
//        document.getElementById("logintype").value = "email";
//    } else {
//        document.getElementById("logintype").value = "username";
//    }
//}
function checkData() {
    var acct = $("#username").val();
    var pwd = $("#password").val();
    var auth = $("#userauthcode").val();

    if (acct.length < 1) {
        $("#TxtErrorMsg").html("请输入用户名!");
        document.getElementById("account").focus();
        return false;
    }
    else if (pwd.length < 1) {
        $("#TxtErrorMsg").html("请输入密码!");
        document.getElementById("password").focus();
        return false;
    }
    else if (auth.length < 1) {
        $("#TxtErrorMsg").html("请输入验证码!");
        document.getElementById("userauthcode").focus();
        return false;
    }
    return true;
}

