$(function () {
    //城市三级联动代码
    $("#ddlDivisionL1").change(function () {
        $("#ddlDivisionL3").html('');
        $("#ddlDivisionL2").html('');
        //初始化第二个列表
        var id = $(this).val()
        if (id > 0) {
            $.post($("#txtDivisionUrl").val(), { level: 2, id: id }, function (data) {
                division.initSelectList("ddlDivisionL2", data);
                division.initSelectList("ddlDivisionL3", null);
            }, "json");
        } else {
            division.initSelectList("ddlDivisionL2", null);
            division.initSelectList("ddlDivisionL3", null);
        }
    });
    $("#ddlDivisionL2").change(function () {
        //初始化第三个列表
        $("#ddlDivisionL3").html('');
        var id = $(this).val();
        if (id > 0) {
            $.post($("#txtDivisionUrl").val(), { level: 3, id: id }, function (data) {
                division.initSelectList("ddlDivisionL3", data);
            }, "json");
        } else {
            division.initSelectList("ddlDivisionL3", null);
        }
    });





    $("#ExpNodeddlDivisionL1").change(function () {
        $("#ExpNodeddlDivisionL3").html('');
        $("#ExpNodeddlDivisionL2").html('');
        //初始化第二个列表
        var id = $(this).val()
        if (id > 0) {
            $.post($("#txtExpNodeDivisionUrl").val(), { level: 2, id: id }, function (data) {
                division.initSelectList("ExpNodeddlDivisionL2", data);
                division.initSelectList("ExpNodeddlDivisionL3", null);
            }, "json");
        } else {
            division.initSelectList("ExpNodeddlDivisionL2", null);
            division.initSelectList("ExpNodeddlDivisionL3", null);
        }
    });
    $("#ExpNodeddlDivisionL2").change(function () {
        //初始化第三个列表
        $("#ExpNodeddlDivisionL3").html('');
        var id = $(this).val();
        if (id > 0) {
            $.post($("#txtDivisionUrl").val(), { level: 3, id: id }, function (data) {
                division.initSelectList("ExpNodeddlDivisionL3", data);
            }, "json");
        } else {
            division.initSelectList("ExpNodeddlDivisionL3", null);
        }
    });



    $("#OneKeyddlDivisionL1").change(function () {
        $("#OneKeyddlDivisionL3").html('');
        $("#OneKeyddlDivisionL2").html('');
        //初始化第二个列表
        var id = $(this).val()
        if (id > 0) {
            $.post($("#txtOneKeyDivisionUrl").val(), { level: 2, id: id }, function (data) {
                division.initSelectList("OneKeyddlDivisionL2", data);
                division.initSelectList("OneKeyddlDivisionL3", null);
            }, "json");
        } else {
            division.initSelectList("OneKeyddlDivisionL2", null);
            division.initSelectList("OneKeyddlDivisionL3", null);
        }
    });
    $("#OneKeyddlDivisionL2").change(function () {
        //初始化第三个列表
        $("#OneKeyddlDivisionL3").html('');
        var id = $(this).val();
        if (id > 0) {
            $.post($("#txtDivisionUrl").val(), { level: 3, id: id }, function (data) {
                division.initSelectList("OneKeyddlDivisionL3", data);
            }, "json");
        } else {
            division.initSelectList("OneKeyddlDivisionL3", null);
        }
    });


    $("#SendAddressddlDivisionL1").change(function () {
        $("#SendAddressddlDivisionL3").html('');
        $("#SendAddressddlDivisionL2").html('');
        //初始化第二个列表
        var id = $(this).val()
        if (id > 0) {
            $.post($("#txtSendAddressDivisionUrl").val(), { level: 2, id: id }, function (data) {
                division.initSelectList("SendAddressddlDivisionL2", data);
                division.initSelectList("SendAddressddlDivisionL3", null);
            }, "json");
        } else {
            division.initSelectList("SendAddressddlDivisionL2", null);
            division.initSelectList("SendAddressddlDivisionL3", null);
        }
    });
    $("#SendAddressddlDivisionL2").change(function () {
        //初始化第三个列表
        $("#SendAddressddlDivisionL3").html('');
        var id = $(this).val();
        if (id > 0) {
            $.post($("#txtSendAddressDivisionUrl").val(), { level: 3, id: id }, function (data) {
                division.initSelectList("SendAddressddlDivisionL3", data);
            }, "json");
        } else {
            division.initSelectList("SendAddressddlDivisionL3", null);
        }
    });
});

var division = {};
//生成下拉框控件
division.initSelectList = function (id, data) {
    var html = '<option value="-1">请选择</option>';
    if (data && data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            html += '<option value="' + data[i].ID + '">' + data[i].Name + '</option>';
        }
    }
    $("#" + id).html(html);

}
