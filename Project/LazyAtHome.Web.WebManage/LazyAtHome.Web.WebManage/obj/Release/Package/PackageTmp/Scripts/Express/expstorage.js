$(document).ready(function () {




    //强制分配所在地
    $("#btnEditStorageItemIO").click(function () {

        var itemid = $("#StorageItemInfo_ID").val();
        var sourceid = $("#StorageItemInfo_StorageID").val();
        var targetid = $("#StorageInfo_TargetStorageID").val();
        var itemtype = $("#StorageItemInfo_ItemType").val();
        var targetidtype = $("#StorageInfo_TargetStorageType").val();

        if (targetid == "") {
            alert("请选择所在地");
            return;
        }


        //请求地址
        var url = $("#hdAdjustStorageItemUrl").val();

        //$.post(url, { oid: orderid, operatorid: operatorid, nodeid: nodeid }, navTabAjaxDone);


        alertMsg.confirm("您确定要强制调整物品所在地吗？", {
            okCall: function () {
                $.post(url, { itemid: itemid, sourceid: sourceid, targetid: targetid, targetidtype: targetidtype, itemtype: itemtype }, navTabAjaxDone, "json");
            }
        });

    });







    //设定订单状态

    $("#btnSetStepExpOrder").click(function () {
        var orderid = $("#ExpOrderInfo_ID").val();


        var step = $("#ExpOrderInfo_Step").val();
        //alert(step);
        //var step = $("#ExpOrderInfo_Step option:selected").val();
        //alert(step);

        //var step = $("select[name='ExpOrderInfo_Step']").val();
        //alert(step);

        var stepremark = $("#stepremark").val();

        //请求地址
        var url = $("#hdSetStepExpOrderUrl").val();

        alertMsg.confirm("您确定要修改订单状态吗？", {
            okCall: function () {
                $.post(url, { oid: orderid, step: step, stepremark: stepremark }, navTabAjaxDone, "json");
            }
        });
    })



});
