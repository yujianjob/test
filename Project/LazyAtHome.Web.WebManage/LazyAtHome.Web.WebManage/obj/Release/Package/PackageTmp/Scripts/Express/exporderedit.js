$(document).ready(function () {


    //分配
    $("#btnAllocationExpOrder").click(function () {

        var nodeid = $("#ExpOrderInfo_NodeID").val();
        if (nodeid == "") {
            alert("请选择存衣点");
            return;
        }

        var transporttype = $("#ExpOrderInfo_TransportType").val();
        var mess = "";
        var operatorid;
        if (transporttype == 1) {
            mess = "取件";
            operatorid = $("#ExpOrderInfo_OperatorID").val();
            if (operatorid == "") {
                alert("请选择快递员");
                return;
            }     
        }
        else if (transporttype == 2) {
            mess = "送件";
            operatorid = 0;
        }
        

        var orderid = $("#ExpOrderInfo_ID").val();


        //请求地址
        var url = $("#hdAllocationExpOrderUrl").val();

        //$.post(url, { oid: orderid, operatorid: operatorid, nodeid: nodeid }, navTabAjaxDone);


        alertMsg.confirm("您确定要分配" + mess + "信息吗？", {
            okCall: function () {
                $.post(url, { oid: orderid, transporttype: transporttype, operatorid: operatorid, nodeid: nodeid }, navTabAjaxDone, "json");
            }
        });

    });

    //强制分配
    $("#btnAllocationForcedExpOrder").click(function () {

        var nodeid = $("#ExpOrderInfo_NodeID").val();
        if (nodeid == "") {
            alert("请选择存衣点");
            return;
        }

        var transporttype = $("#ExpOrderInfo_TransportType").val();
        var mess = "";
        var operatorid;
        if (transporttype == 1) {
            mess = "取件";
            operatorid = $("#ExpOrderInfo_OperatorID").val();
            if (operatorid == "") {
                operatorid = 0;
                //alert("请选择快递员");
                //return;
            }

        }
        else if (transporttype == 2) {
            mess = "送件";
            operatorid = 0;
        }

        var csremark = $("#ExpOrderInfo_CSRemark").val();

        var orderid = $("#ExpOrderInfo_ID").val();


        //请求地址
        var url = $("#hdAllocationForcedExpOrderUrl").val();

        //$.post(url, { oid: orderid, operatorid: operatorid, nodeid: nodeid }, navTabAjaxDone);

        var operatortype = 1
        if (operatorid != 0) {
            operatortype = $("#ExpOrderInfo_OperatorType").val();
        }
        

        alertMsg.confirm("您确定要分配" + mess + "信息吗？", {
            okCall: function () {

                if (operatortype == 0) {
                    alertMsg.confirm("该名快递员现在未上岗，还需要继续分配吗？", {
                        okCall: function () {
                            $.post(url, { oid: orderid, transporttype: transporttype, operatorid: operatorid, nodeid: nodeid, csremark: csremark }, navTabAjaxDone, "json");
                        }
                    });
                }
                else
                {
                    $.post(url, { oid: orderid, transporttype: transporttype, operatorid: operatorid, nodeid: nodeid, csremark: csremark }, navTabAjaxDone, "json");
                }      
            }
        });

    });


    //取消分配
    $("#btnAllocationCancelExpOrder").click(function () {

        var orderid = $("#ExpOrderInfo_ID").val();

        var transporttype = $("#ExpOrderInfo_TransportType").val();
        var mess = "";
        if (transporttype == 1) {
            mess = "取件";
        }
        else if (transporttype == 2) {
            mess = "送件";
        }

        var csremark = $("#ExpOrderInfo_CSRemark").val();

        //请求地址
        var url = $("#hdAllocationCancelExpOrderUrl").val();

        alertMsg.confirm("您确定要重新自动分配" + mess + "信息吗？", {
            okCall: function () {
                $.post(url, { oid: orderid, transporttype: transporttype, csremark: csremark }, navTabAjaxDone, "json");
            }
        });
    })


    //修改客户信息
    $("#btnEditExpOrder").click(function () {
        var orderid = $("#ExpOrderInfo_ID").val();

        var contacts = $("#ExpOrderInfo_Contacts").val();

        var mpno = $("#ExpOrderInfo_Mpno").val();
        if (mpno == "") {
            alert("请填写手机号");
            return;
        }

        var districtid = $("#ddlDivisionL3").val();
        if (districtid == -1) {
            alert("请选择行政区划");
            return;
        }

        var address = $("#ExpOrderInfo_Address").val();
        if (address == "") {
            alert("请填写地址");
            return;
        }

        var packageinfo = $("#ExpOrderInfo_PackageInfo").val();
        if (packageinfo == "") {
            alert("请填写物品信息");
            return;
        }

        var packagecount = $("#ExpOrderInfo_PackageCount").val();
        if (packagecount == "") {
            alert("请填写物品数量");
            return;
        }

        var chargefee = $("#ExpOrderInfo_ChargeFee").val();

        var exptime = $("#ExpOrderInfo_ExpTime").val();

        //var csremark = $("#ExpOrderInfo_CSRemark").val();

        //var userremark = $("#ExpOrderInfo_UserRemark").val();

        //请求地址
        var url = $("#hdEditExpOrderUrl").val();

        alertMsg.confirm("您确定要修改客户信息吗？", {
            okCall: function () {
                $.post(url, { oid: orderid, contacts: contacts, mpno: mpno, districtid: districtid, address: address, packageinfo: packageinfo, packagecount: packagecount, chargefee: chargefee, exptime: exptime }, navTabAjaxDone, "json");
            }
        });
    })


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

    //$("#ExpOrderInfo_Step").change(function () {
    //    alert("123");
    //    var step = $("#Step").val();
    //    alert(step);
    //})


    //选择快递员时 把存衣点名称作为参数
    $("#opidselect").click(function () {

        var nodeid = $("#ExpOrderInfo_NodeID").val();
        if (nodeid == "") {
            alert("请先选择站点");
            //return;
            nodeid = 0;
        }
        //var nodename = $("#ExpOrderInfo_NodeName").val();
        var url = $("#hdOPIDSelectUrl").val();
        $(this).attr("href", url + "?source=1&nodeid=" + nodeid);


    })

});
