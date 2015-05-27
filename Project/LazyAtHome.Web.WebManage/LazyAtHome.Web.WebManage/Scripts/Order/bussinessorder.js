$(document).ready(function () {

    $("#BCType").change(function () {

        var type = $(this).val()
        alert(type);
        //if (id == 1) {
        //    $("#BCAddress").val("地址");
        //}

        if (type > 0) {
            $.post($("#hdGetBCInfoUrl").val(), { BCType: type}, function (data) {
                $("#BCAddress").val(data.Address);
                $("#BCConsignee").val(data.Consignee);
                $("#BCMpno").val(data.Mpno);
                $("#BCProductName").val(data.ProductName);
                $("#BCPrice").val(data.Price);
            }, "json");
        } else {
            $("#BCAddress").val("");
            $("#BCConsignee").val("");
            $("#BCMpno").val("");
            $("#BCProductName").val("");
            $("#BCPrice").val("");
        }
    })


});