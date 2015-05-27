$(document).ready(function () {

    $("#btnOneKeySubmit").click(function () {
        var CouponCode = $("#couponcode").val();
        var UserRemark = $("#userremark").val();
        var ExpectTime = $("#expectdate").val();

    })
    

});

function onekeySubmit(UserID, Consignee, Address, MPNo, DistrictID) {
    //alert(UserID);
    //alert(Consignee);
    //alert(Address);
    //alert(MPNo);
    //alert(DistrictID);

    var CouponCode = $("#onekeycouponcode").val();
    var CouponID = $("#onekeycouponList").val();
    var UserRemark = $("#onekeyuserremark").val();
    var ExpectTime = $("#onekeyexpectdate").val();
    //alert(CouponID);

    //地址检查
    var checkurl = $("#hdCheckAddressByOneKeyUrl").val();
    var postData = { "address": Address };

    $.ajax({
        type: "post",
        url: checkurl,
        data: postData,
        dataType: "json",
        success: function (resultJson) {
            if (resultJson.code == 1) {
                //地址在服务范围
                var msg = "您确认要对该用户进行一键下单？<br/>客户：[" + Consignee + "]" + MPNo + "<br/>" + "地址：" + Address;

                if (CouponCode != "") {
                    msg += "<br/>并使用优惠券码[" + CouponCode + "]";
                }
                else {
                    if (CouponID != 0) {
                        msg += "<br/>并使用已有优惠券[" + CouponID + "]";
                    }
                }
                //请求地址
                var url = $("#hdOneKeySubmitUrl").val();

                alertMsg.confirm(msg, {
                    okCall: function () {
                        $.post(url, { userid: UserID, consignee: Consignee, address: Address, mpno: MPNo, districtid: DistrictID, couponcode: CouponCode, couponid: CouponID, expecttime: ExpectTime, userremark: UserRemark, type: 1 }, navTabAjaxDone, "json");
                    }

                });
            }
            else if (resultJson.code == 0) {
                //地址不在服务范围
                var msg = "地址不在服务范围内，您确认要对该用户进行一键下单？下单后系统将不会自动分配，需要手动分配！<br/>客户：[" + Consignee + "]" + MPNo + "<br/>" + "地址：" + Address;

                if (CouponCode != "") {
                    msg += "<br/>并使用优惠券码[" + CouponCode + "]";
                }
                else {
                    if (CouponID != 0) {
                        msg += "<br/>并使用已有优惠券[" + CouponID + "]";
                    }
                }
                //请求地址
                var url = $("#hdOneKeySubmitUrl").val();

                alertMsg.confirm(msg, {
                    okCall: function () {
                        $.post(url, { userid: UserID, consignee: Consignee, address: Address, mpno: MPNo, districtid: DistrictID, couponcode: CouponCode, couponid: CouponID, expecttime: ExpectTime, userremark: UserRemark, type: 1 }, navTabAjaxDone, "json");
                    }

                });
            }
            else {
                alert("wcf异常，请稍后再试。");
            }
        },
        error: function () {
            alert("系统异常，请稍后再试。");
        }
    });







    
}




function onekeySubmitNewAddress(UserID)
{
    var CouponCode = $("#onekeycouponcode").val();
    var CouponID = $("#onekeycouponList").val();
    var UserRemark = $("#onekeyuserremark").val();
    var ExpectTime = $("#onekeyexpectdate").val();

    var Consignee = $("#onekeyconsignee").val();
    var Address = $("#onekeyaddress").val();
    var MPNo = $("#onekeympno").val();
    var DistrictID = $("#OneKeyddlDivisionL3").val();
    
    if (Consignee == "") {
        alert("请填写客户名称");
        return false;
    }
    if (Address == "") {
        alert("请填写取件地址");
        return false;
    }
    if (DistrictID == -1) {
        alert("请选择省市区");
        return false;
    }
    if (MPNo == "") {
        alert("请填写联系方式");
        return false;
    }

    //地址检查
    var checkurl = $("#hdCheckAddressByOneKeyUrl").val();
    var postData = { "address": Address };

    $.ajax({
        type: "post",
        url: checkurl,
        data: postData,
        dataType: "json",
        success: function (resultJson) {
            if (resultJson.code == 1) {
                //地址在服务范围
                Address = $("#OneKeyddlDivisionL1").find("option:selected").text() + $("#OneKeyddlDivisionL2").find("option:selected").text() + $("#OneKeyddlDivisionL3").find("option:selected").text() + Address;

                var msg = "您确认要对该用户进行一键下单？<br/>客户：[" + Consignee + "]" + MPNo + "<br/>" + "地址：" + Address;

                if (CouponCode != "") {
                    msg += "<br/>并使用优惠券码[" + CouponCode + "]";
                }
                else {
                    if (CouponID != 0) {
                        msg += "<br/>并使用已有优惠券[" + CouponID + "]";
                    }
                }

                //请求地址
                var url = $("#hdOneKeySubmitUrl").val();

                alertMsg.confirm(msg, {
                    okCall: function () {
                        $.post(url, { userid: UserID, consignee: Consignee, address: Address, mpno: MPNo, districtid: DistrictID, couponcode: CouponCode, couponid: CouponID, expecttime: ExpectTime, userremark: UserRemark, type: 2 }, navTabAjaxDone, "json");
                    }

                });
            }
            else if (resultJson.code == 0) {
                //地址不在服务范围
                Address = $("#OneKeyddlDivisionL1").find("option:selected").text() + $("#OneKeyddlDivisionL2").find("option:selected").text() + $("#OneKeyddlDivisionL3").find("option:selected").text() + Address;

                var msg = "地址不在服务范围内，您确认要对该用户进行一键下单？下单后系统将不会自动分配，需要手动分配！<br/>客户：[" + Consignee + "]" + MPNo + "<br/>" + "地址：" + Address;

                if (CouponCode != "") {
                    msg += "<br/>并使用优惠券码[" + CouponCode + "]";
                }
                else {
                    if (CouponID != 0) {
                        msg += "<br/>并使用已有优惠券[" + CouponID + "]";
                    }
                }

                //请求地址
                var url = $("#hdOneKeySubmitUrl").val();

                alertMsg.confirm(msg, {
                    okCall: function () {
                        $.post(url, { userid: UserID, consignee: Consignee, address: Address, mpno: MPNo, districtid: DistrictID, couponcode: CouponCode, couponid: CouponID, expecttime: ExpectTime, userremark: UserRemark, type: 2 }, navTabAjaxDone, "json");
                    }

                });
            }
            else {
                alert("wcf异常，请稍后再试。");
            }
        },
        error: function () {
            alert("系统异常，请稍后再试。");
        }
    });




    
}