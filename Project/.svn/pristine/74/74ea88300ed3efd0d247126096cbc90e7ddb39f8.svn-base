$(document).ready(function () {
    
    // 渐变弹出层
    //var speed = 600;//动画速度

    //#pay a
    //支付详情层效果
    //$("#pay").click(function (event) {//绑定事件处理

    //    var payflag = $("#payflag").val();
    //    if (payflag == 0) {
    //        event.stopPropagation();
    //        var offset = $(event.target).offset();//取消事件冒泡
    //        $("#payinfo").css({ top: offset.top + $(event.target).height() + "px", left: offset.left });//设置弹出层位置
    //        $("#payinfo").show(speed);//动画显示

    //        $("#payflag").val("1");
    //    }
    //    else {
    //        $("#payinfo").hide(speed);
    //        $("#payflag").val("0");
    //    }
        
    //});

    //$(document).click(function (event) { $("#payinfo").hide(speed) });//单击空白区域隐藏
    //$("#payinfo").click(function (event) { $("#payinfo").hide(speed) });//单击弹出层则自身隐藏


    //#discount a
    //优惠详情层效果
    //$("#discount").click(function (event) {//绑定事件处理

    //    var discountflag = $("#discountflag").val();
    //    if (discountflag == 0) {

    //        event.stopPropagation();
    //        var offset = $(event.target).offset();//取消事件冒泡
    //        $("#discountinfo").css({ top: offset.top + $(event.target).height() + "px", left: offset.left });//设置弹出层位置
    //        $("#discountinfo").show(speed);//动画显示

    //        $("#discountflag").val("1");
    //    }
    //    else
    //    {
    //        $("#discountinfo").hide(speed);
    //        $("#discountflag").val("0");
    //    }
    //});

    //$(document).click(function (event) { $("#discountinfo").hide(speed) });//单击空白区域隐藏
    //$("#discountinfo").click(function (event) { $("#discountinfo").hide(speed) });//单击弹出层则自身隐藏


    //修改期望收件时间
    $("#btnExpectTime").click(function () {

        var expecttime = $("#OrderInfo_ExpectTime").val();
        //if (expecttime == "") {
        //    alert("请填写期望收件时间");
        //    return;
        //}
        var orderid = $("#OrderInfo_ID").val();

        //请求地址
        var url = $("#hdExpectTimeUrl").val();

        //$.post(url, { oid: orderid, expecttime: expecttime }, navTabAjaxDone, "json");
        var msg = "确认要修改期望收件时间[" + expecttime + "]吗？";
        //alert(msg);
        alertMsg.confirm(msg, {
            okCall: function () {
                $.post(url, { oid: orderid, expecttime: expecttime }, navTabAjaxDone, "json");
            }

        });
    })

    //修改取件物流类型
    $("#btnGetExpressType").click(function () {

        var getexpresstype = $("#OrderInfo_GetExpressType").val();

        //var reasonSel = document.getElementById("OrderInfo_GetExpressType");
        //var index = reasonSel.selectedIndex; // 选中索引
        //var text = reasonSel.options[index].text; // 选中文本
        //alert(text);

        var orderid = $("#OrderInfo_ID").val();

        //请求地址
        var url = $("#hdGetExpressTypeUrl").val();

        var msg = "确认要修改取件物流类型吗？";

        alertMsg.confirm(msg, {
            okCall: function () {
                $.post(url, { oid: orderid, getexpresstype: getexpresstype }, navTabAjaxDone, "json");
            }

        });
    })

    //推送短信
    $("#btnOrderSmsType").click(function () {

        var smstype = $("#orderSmsType").val();

        var mpno = $("#OrderInfo_GetAddress_Mpno").val();

        var orderid = $("#OrderInfo_ID").val();

        var csremark = $("#OrderInfo_CSRemark").val();

        //请求地址
        var url = $("#hdSendOrderSmsUrl").val();

        var msg = "确认要推送短信吗？";

        alertMsg.confirm(msg, {
            okCall: function () {
                $.post(url, { oid: orderid, remark: csremark, smstype: smstype, mpno: mpno }, navTabAjaxDone, "json");
            }

        });
    })

    //修改取件快递单号
    $("#btnGetExpressNumber").click(function () {

        var getexpressnumber = $("#OrderInfo_GetExpressNumber").val();
        if (getexpressnumber == "") {
            alert("请填写取件快递单号");
            return;
        }

        var orderid = $("#OrderInfo_ID").val();


        //请求地址
        var url = $("#hdGetExpressNumberUrl").val();

        var msg = "确认要修改取件快递单号[" + getexpressnumber + "]吗？";

        //$.post(url, { oid: orderid, getexpressnumber: getexpressnumber }, navTabAjaxDone);

        alertMsg.confirm(msg, {
            okCall: function () {
                $.post(url, { oid: orderid, getexpressnumber: getexpressnumber }, navTabAjaxDone, "json");
            }

        });

    });

    //修改客服备注
    $("#btnCSRemark").click(function () {

        var csremark = $("#OrderInfo_CSRemark").val();
        if (csremark == "")
        {
            alert("请填写客服备注");
            return;
        }

        var orderid = $("#OrderInfo_ID").val();
        

        //请求地址
        var url = $("#hdCSRemarkUrl").val();

        var msg = "确认要修改客服备注[" + csremark + "]吗？";

        //$.post(url, { oid: orderid, remark: csremark }, navTabAjaxDone);

        alertMsg.confirm(msg, {
            okCall: function () {
                $.post(url, { oid: orderid, remark: csremark }, navTabAjaxDone, "json");
            }

        });

    });



    //修改取件地址
    $("#btnEditGetAddress").click(function () {
        var id = $("#OrderInfo_GetAddress_ID").val();
        //寄件人
        var consignee = $("#OrderInfo_GetAddress_Consignee").val();
        if (consignee == "") {
            alert("请填写寄件人");
            return;
        }

        //行政区
        var districtid = $("select[name='getaddressBDVIdL3']").val();
        if (districtid == -1) {
            alert("请选择行政区");
            return;
        }
        //地址
        var address = $("#OrderInfo_GetAddress_Address").val();
        if (address == "") {
            alert("请填写地址");
            return;
        }
        //手机号
        var mpno = $("#OrderInfo_GetAddress_Mpno").val();
        if (mpno == "") {
            alert("请填写手机号");
            return;
        }
        //固话
        var phone = $("#OrderInfo_GetAddress_Phone").val();

        var orderid = $("#OrderInfo_ID").val();

        //请求地址
        var url = $("#hdEditAddressUrl").val();


        //检查地址
        var checkurl = $("#hdCheckAddressUrl").val();

        var postData = { "address": address };

        $.ajax({
            type: "post",
            url: checkurl,
            data: postData,
            dataType: "json",
            success: function (resultJson) {
                if (resultJson.code == 1) {
                    //地址在服务范围
                    alertMsg.confirm("确认要修改取送地址吗？", {
                        okCall: function () {
                            $.post(url, { ID: id, OID: orderid, Consignee: consignee, DistrictID: districtid, Address: address, Mpno: mpno, Phone: phone, Flag: 1 }, navTabAjaxDone);
                        }

                    });
                }
                else if (resultJson.code == 0) {
                    //地址不在服务范围
                    alertMsg.confirm("地址不在服务范围内，您还确认要修改取送地址吗？", {
                        okCall: function () {
                            $.post(url, { ID: id, OID: orderid, Consignee: consignee, DistrictID: districtid, Address: address, Mpno: mpno, Phone: phone, Flag: 1 }, navTabAjaxDone);
                        }

                    });
                }
                else {
                    alert("wcf异常，请稍后再试。");
                }
            },
            error: function() {
                alert("系统异常，请稍后再试。");
            }
        });

        
        //var msg = "确认要修改取送地址吗？";

        //alertMsg.confirm(msg, {
        //    okCall: function () {
        //        $.post(url, { ID: id, OID: orderid, Consignee: consignee, DistrictID: districtid, Address: address, Mpno: mpno, Phone: phone, Flag: 1 }, navTabAjaxDone);
        //    }

        //});
    });


    //修改收件地址
    $("#btnEditSendAddress").click(function () {
        var id = $("#OrderInfo_SendAddress_ID").val();

        //收件人
        var consignee = $("#OrderInfo_SendAddress_Consignee").val();
        if (consignee == "") {
            alert("请填写收件人");
            return;
        }
        //行政区
        var districtid = $("select[name='sendaddressBDVIdL3']").val();
        if (districtid == -1) {
            alert("请选择行政区");
            return;
        }
        //地址
        var address = $("#OrderInfo_SendAddress_Address").val();
        if (address == "") {
            alert("请填写地址");
            return;
        }
        //手机号
        var mpno = $("#OrderInfo_SendAddress_Mpno").val();
        if (mpno == "") {
            alert("请填写手机号");
            return;
        }
        //固话
        var phone = $("#OrderInfo_SendAddress_Phone").val();

        var orderid = $("#OrderInfo_ID").val();

        //请求地址
        var url = $("#hdEditAddressUrl").val();

        $.post(url, { ID: id, OID: orderid, Consignee: consignee, DistrictID: districtid, Address: address, Mpno: mpno, Phone: phone, Flag: 2 }, navTabAjaxDone);

    });


    //一键下单审核通过
    $("#btnAuditSuccess").click(function () {

        var orderid = $("#OrderInfo_ID").val();

        //请求地址
        var url = $("#hdAuditOrderUrl").val();

        $.post(url, { oid: orderid, flag: true }, navTabAjaxDone);

    });

    //一键下单审核拒绝
    $("#btnAuditFailure").click(function () {

        var orderid = $("#OrderInfo_ID").val();

        //请求地址
        var url = $("#hdAuditOrderUrl").val();

        $.post(url, { oid: orderid, flag: false }, navTabAjaxDone);

    });

    //退单按钮点击
    $("#btnRepay").click(function () {
        var orderid = $("#OrderInfo_ID").val();
        var ordernumber = $("#OrderInfo_OrderNumber").val();
        //alert(orderid);

        //请求地址
        var url = $("#hdOrderRepayUrl").val();

        //alert(url);

        $.post(url, { oid: orderid, omunber: ordernumber }, navTabAjaxDone);

    });

    //反洗按钮点击
    $("#btnWashAgain").click(function () {

        var orderid = $("#OrderInfo_ID").val();
        var list = "";
        $("#productlist input:checked").each(function (i, a) {
            if (a.name == "ckwashagain") {
                list += a.id + ",";

            }
        });

        if (list == "") {
            alert("请选择要反洗的产品");
            return false;
        }
        else
        {
            list = list.substring(0, list.length - 1);
            //alert(list);
            //请求地址
            var url = $("#hdWashAgainUrl").val();
            //alert(url);
            $.post(url, {oid: orderid, pidlist: list }, navTabAjaxDone);
        }

    });


    //普通订单调整
    $("#btnAdjustOrderProduct").click(function () {
        var orderid = $("#OrderInfo_ID").val();
        var list = "";
        $("#AdjustProductList tbody .unitBox").each(function () {
            var id = $(this).find("td").eq(1).find("input").val();
            if (id != "") {
                list += id + ",";
            }
            
        })

        if (list == "") {
            alert("请添加要调整的衣物");
            return false;
        }
        else {
            list = list.substring(0, list.length - 1);
            //alert(list);
            //请求地址
            var url = $("#hdAdjustOrderProductUrl").val();
            //alert(url);
            //$.post(url, { oid: orderid, pidlist: list }, navTabAjaxDone);

            var msg = "确认要调整衣物吗？";

            alertMsg.confirm(msg, {
                okCall: function () {
                    $.post(url, { oid: orderid, pidlist: list, ordertype: 1 }, navTabAjaxDone, "json");
                }

            });
        }
    })

    //一键下单重置产品
    $("#btnAdjustOneKeyOrderProduct").click(function () {
        var orderid = $("#OrderInfo_ID").val();
        var list = "";
        var url = $("#hdAdjustOrderProductUrl").val();

        var msg = "确认要重置衣物到初始化吗？";

        alertMsg.confirm(msg, {
            okCall: function () {
                $.post(url, { oid: orderid, pidlist: list, ordertype: 2 }, navTabAjaxDone, "json");
            }

        });
    })


    //修改订单进程
    $("#btnEditOrderStep").click(function () {
        var orderid = $("#OrderInfo_ID").val();

        var step = $("#OrderInfo_Step").val();

        //请求地址
        var url = $("#hdEditOrderStepUrl").val();

        $.post(url, { oid: orderid, step: step }, navTabAjaxDone, "json");
    })


    ////搜索用户信息
    //$("#btnSearchUser").click(function () {
    //    var mpno = $("#usermpno").val();

    //    //请求地址
    //    var url = $("#hdSearchUserUrl").val();

    //    $.post(url, { mpno: mpno }, navTabAjaxDone, "json");
    //})


    //搜索用户时候 把手机号作为参数拼在url后面
    $("#btnSearchUser").click(function () {
        var usermpno = $("#usermpno").val();
        var url = $("#hdSearchUserUrl").val();
        $(this).attr("href", url + "?usermpno=" + usermpno);

        //alert($(this).attr("href"));

    })

});
