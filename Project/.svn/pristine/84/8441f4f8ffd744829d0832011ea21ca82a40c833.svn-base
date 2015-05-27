$(document).ready(function () {

    $("#exportorderstep").click(function () {

        var begindate = $("#OrderStepFrom").val();
        var enddate = $("#OrderStepTo").val();

        if (enddate == "" || begindate == "") {
            alert("请选择时间段");
            return;
        }

        $.ajax({
            type: "post",
            url: $("#hdExportOrderStepUrl").val(),
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

    //exportstockout
    $("#exportstockout").click(function () {

        var begindate = $("#StockOutFrom").val();
        var enddate = $("#StockOutTo").val();

        if (enddate == "" || begindate == "") {
            alert("请选择时间段");
            return;
        }

        $.ajax({
            type: "post",
            url: $("#hdExportStockOutUrl").val(),
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


    //exportstockin
    $("#exportstockin").click(function () {

        var begindate = $("#StockInFrom").val();
        var enddate = $("#StockInTo").val();

        if (enddate == "" || begindate == "") {
            alert("请选择时间段");
            return;
        }

        $.ajax({
            type: "post",
            url: $("#hdExportStockInUrl").val(),
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


    //exportexpressstockin
    $("#exportexpressstockin").click(function () {

        var begindate = $("#StockInFrom").val();
        var enddate = $("#StockInTo").val();

        if (enddate == "" || begindate == "") {
            alert("请选择时间段");
            return;
        }

        $.ajax({
            type: "post",
            url: $("#hdExportExpressStockInUrl").val(),
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
});
