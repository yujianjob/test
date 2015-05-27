$(document).ready(function () {

    //修改会员等级
    $("#btnUserLevel").click(function () {

        var userid = $("#UserInfo_ID").val();
        //alert(userid);
        var userlevel = $("#UserInfo_Level").val();
        //alert(userlevel);
        var user = $("#UserInfo_LoginName").val();
        if (user == "") {
            user = $("#UserInfo_MPNo").val();
        }
        if (user == "") {
            user = $("#UserInfo_Email").val();
        }
        //alert(user);
        //请求地址
        var url = $("#hdUserLevel").val();

        $.post(url, { uid: userid, level: userlevel, user : user }, navTabAjaxDone);

    });

})