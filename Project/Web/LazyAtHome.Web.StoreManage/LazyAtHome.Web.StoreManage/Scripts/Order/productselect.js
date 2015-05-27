$(document).ready(function () {

    //$("#btnadd").click(function () {

    //    var list = "";
    //    $("#tbproductselect input:checked").each(function (i, a) {
    //        if (a.name == "ids") {
    //            list += a.id + ",";

    //        }
    //    });

    //    if (list == "") {
    //        alert("请选择要物品类型");
    //        return false;
    //    }
    //    else {
    //        list = list.substring(0, list.length - 1);


    //        alert(list);

    //    }
    //})

    function cleardata()
    {
        alert("123");
    }


    $("#btnreturn").click(function () {
        var statusCode = "200";

        var message = "";

        var navTabId = "createorder";

        var forwardUrl = "";

        var callbackType = "closeCurrent"



        var response = {
            statusCode: statusCode,

            message: message,

            navTabId: navTabId,

            forwardUrl: forwardUrl,

            callbackType: callbackType

        };

        navTabAjaxDone(response);

    })


})