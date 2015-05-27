$(document).ready(function () {
    
    $("#exportcode").click(function () {

        var begindate = $("#dateStart").val();
        var enddate = $("#dateEnd").val();

        if (enddate == "" || begindate == "") {
            alert("请选择时间段");
            return;
        }

        $.ajax({
            type: "post",
            url: $("#hdExportCodeUrl").val(),
            data: { StartDate: begindate, EndDate: enddate },
            dataType: "json",
            success: function (data) {
                if (data.success) {
                    //* 下载
                    window.location.href = data.message;
                }
                else {
                    $.messager.alert("错误信息", data.message, "error");
                }
            }
        });

    });

    $("#exportorder").click(function () {
        //订单编号
        var ordernum = $("#ordernumber").val();
        //手机号
        var mpno = $("#mpno").val();
        //订单分类
        var orderclass = $("#OrderClass").val();
        //订单类型
        var ordertype = $("#OrderType").val();
        //订单状态
        var orderstatus = $("#OrderStatus").val();
        //金额
        var totalamountmin = $("#totalamountmin").val();
        var totalamountmax = $("#totalamountmax").val();

        //下单日期
        var begindate = $("#txtFrom").val();
        var enddate = $("#txtTo").val();
        //下单渠道
        var orderchannel = $("#Channel").val();
        //订单进程
        var orderstep = $("#Step").val();
        //搜索排序
        var sorttype = $("#SortType").val();
        //客户
        var consignee = $("#consignee").val();

        //取件日期
        var getbegindate = $("#txtGetFrom").val();
        var getenddate = $("#txtGetTo").val();

        var getexpresstype = $("#GetExpressType").val();

        $.ajax({
            type: "post",
            url: $("#hdExportOrderUrl").val(),
            data: { ordernum: ordernum, mpno: mpno, orderclass: orderclass, ordertype: ordertype, orderstatus: orderstatus, totalamountmin: totalamountmin, totalamountmax: totalamountmax, begindate: begindate, enddate: enddate, orderchannel: orderchannel, orderstep: orderstep, sorttype: sorttype, consignee: consignee, getbegindate: getbegindate, getenddate: getenddate, getexpresstype: getexpresstype },
            dataType: "json",
            success: function (data) {
                if (data.success) {
                    //* 下载
                    window.location.href = data.message;
                }
                else {
                    $.messager.alert("错误信息", data.message, "error");
                }
            }
        });

    });




    $("#export").click(function () {
        
        //alert("123");

        //手机号
        //var MPNO = $("#MPNO").val();

        $.ajax({
            type: "post",
            url: $("#hdExportUrl").val(),
            dataType: "json",
            success: function (data) {
                if (data.success) {
                    //* 下载
                    window.location.href = data.message;
                }
                else {
                    $.messager.alert("错误信息", data.message, "error");
                }
            }
        });

    });



});
