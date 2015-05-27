$(function () {
    //三级联动代码
    $("#ddlWashClassL1").change(function () {
        $("#ddlWashClassL3").html('');
        $("#ddlWashClassL2").html('');
        //初始化第二个列表
        var id = $(this).val()
        if (id > 0) {
            $.post($("#txtCategoryComboxUrl").val(), { level: 2, id: id }, function (data) {
                CategoryCombox.initSelectList("ddlWashClassL2", data);
                CategoryCombox.initSelectList("ddlWashClassL3", null);
            }, "json");
        } else {
            CategoryCombox.initSelectList("ddlWashClassL2", null);
            CategoryCombox.initSelectList("ddlWashClassL3", null);
        }
    });
    $("#ddlWashClassL2").change(function () {
        //初始化第三个列表
        $("#ddlWashClassL3").html('');
        var id = $(this).val();
        if (id > 0) {
            $.post($("#txtCategoryComboxUrl").val(), { level: 3, id: id }, function (data) {
                CategoryCombox.initSelectList("ddlWashClassL3", data);
            }, "json");
        } else {
            CategoryCombox.initSelectList("ddlWashClassL3", null);
        }
    });
});

var CategoryCombox = {};
//生成下拉框控件
CategoryCombox.initSelectList = function (id, data) {
    var html = '<option value="-1">请选择</option>';
    if (data && data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            html += '<option value="' + data[i].ID + '">' + data[i].Name + '</option>';
        }
    }
    $("#" + id).html(html);

}
