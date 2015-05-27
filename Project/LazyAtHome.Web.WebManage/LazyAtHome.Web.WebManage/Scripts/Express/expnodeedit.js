$(document).ready(function () {

    //选择areaid
    $("#areaidSelectByNode").click(function () {
        var url = $("#hdAreaSelectByNodeUrl").val();
        $(this).attr("href", url + "?source=1");

    })




    //选择上级nodeid
    $("#nodeidSelectByNode").click(function () {
        var nodetype = $("#ExpNodeInfo_Type").val();
        var url = $("#hdNodeSelectByNodeUrl").val();
        $(this).attr("href", url + "?nodetype=" + nodetype + "&source=2");

    })


    //选择node负责人
    $("#opidSelectByNode").click(function () {
        //var nodename = $("#ExpOrderInfo_NodeName").val();
        var url = $("#hdOPIDSelectByNodeUrl").val();
        $(this).attr("href", url + "?source=2");
    })

    //选择node开发者
    $("#createidSelectByNode").click(function () {
        //var nodename = $("#ExpOrderInfo_NodeName").val();
        var url = $("#hdCreateIDSelectByNodeUrl").val();
        $(this).attr("href", url + "?source=5");
    })


    //选择node保安队长
    $("#captainidSelectByNode").click(function () {
        //var nodename = $("#ExpOrderInfo_NodeName").val();
        var url = $("#hdCaptainIDSelectByNodeUrl").val();
        $(this).attr("href", url + "?source=4");
    })

});