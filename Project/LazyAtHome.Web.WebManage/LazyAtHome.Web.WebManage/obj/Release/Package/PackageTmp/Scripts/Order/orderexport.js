$(document).ready(function () {
    $("#OrderDetailExport").click(function () {

        var createbegindate = $("#OrderDetailExportSearchInfo_CreateStartDate").val();
        var createenddate = $("#OrderDetailExportSearchInfo_CreateEndDate").val();

        var finishbegindate = $("#OrderDetailExportSearchInfo_FinishStartDate").val();
        var finishenddate = $("#OrderDetailExportSearchInfo_FinishEndDate").val();

        if (!(createbegindate != "" && createenddate != "") && !(finishbegindate != "" && finishenddate != "")) {
            alert("请选择时间段");
            return;
        }

        $.ajax({
            type: "post",
            url: $("#hdExportOrderDetailUrl").val(),
            data: { CreateStartDate: createbegindate, CreateEndDate: createenddate, FinishStartDate: finishbegindate, FinishEndDate: finishenddate },
            dataType: "json",
            success: function (data) {
                if (data.success) {
                    //* 下载
                    window.location.href = data.message;
                }
                else {
                    //$.messager.alert("错误信息", data.message, "error");
                    alertMsg.warn(data.message)
                }
            }
        });

    });





    //佣金导出
    $("#CommissionBillExport").click(function () {

        var perioddate = $("#CommissionBillExportSearchInfo_PeriodDate").val();


        if (perioddate == "") {
            alert("请选择时间");
            return;
        }

        $.ajax({
            type: "post",
            url: $("#hdExportCommissionBillUrl").val(),
            data: { PeriodDate: perioddate },
            dataType: "json",
            success: function (data) {
                if (data.success) {
                    //* 下载
                    window.location.href = data.message;
                }
                else {
                    //$.messager.alert("错误信息", data.message, "error");
                    alertMsg.warn(data.message)
                }
            }
        });

    });





    //应收账款导出
    $("#PaymentBillExport").click(function () {

        var perioddate = $("#PaymentBillExportSearchInfo_PeriodDate").val();


        if (perioddate == "") {
            alert("请选择时间");
            return;
        }

        $.ajax({
            type: "post",
            url: $("#hdExportPaymentBillUrl").val(),
            data: { PeriodDate: perioddate },
            dataType: "json",
            success: function (data) {
                if (data.success) {
                    //* 下载
                    window.location.href = data.message;
                }
                else {
                    //$.messager.alert("错误信息", data.message, "error");
                    alertMsg.warn(data.message)
                }
            }
        });

    });
});