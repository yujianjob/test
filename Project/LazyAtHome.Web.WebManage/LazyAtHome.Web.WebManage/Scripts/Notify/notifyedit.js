$(document).ready(function () {

    //领取 通知消息
    $("#btnDealNotify").click(function () {
        var notifyid = $("#NotifyInfo_ID").val();
        var url = $("#hdDealNotifyUrl").val();
        
        alertMsg.confirm("您确定要跟进此通知消息吗？", {
            okCall: function () {
                $.post(url, { notifyid: notifyid}, navTabAjaxDone, "json");
            }
        });
    })


    //关闭 通知消息
    $("#btnCloseNotify").click(function () {
        var notifyid = $("#NotifyInfo_ID").val();
        var result = $("#NotifyInfo_Result").val();
        var url = $("#hdFinishNotifyUrl").val();

        alertMsg.confirm("您确定要关闭此通知消息吗？", {
            okCall: function () {
                $.post(url, { notifyid: notifyid, result: result, status: 6 }, navTabAjaxDone, "json");
            }
        });
    })

    //完成 通知消息
    $("#btnFinishNotify").click(function () {
        var notifyid = $("#NotifyInfo_ID").val();
        var result = $("#NotifyInfo_Result").val();
        var url = $("#hdFinishNotifyUrl").val();

        alertMsg.confirm("您确定要完成此通知消息吗？", {
            okCall: function () {
                $.post(url, { notifyid: notifyid, result: result, status: 2 }, navTabAjaxDone, "json");
            }
        });
    })

    //更新 处理意见
    $("#btnResultNotify").click(function () {
        var notifyid = $("#NotifyInfo_ID").val();
        var result = $("#NotifyInfo_Result").val();
        var url = $("#hdResultNotifyUrl").val();

        alertMsg.confirm("您确定要完成此通知消息吗？", {
            okCall: function () {
                $.post(url, { notifyid: notifyid, result: result }, navTabAjaxDone, "json");
            }
        });
    })
});
