$(function () {
    //产品分类品类二级联动代码
    $("#ddlWashClassL1").change(function () {
        //初始化第二个列表
        $("#ddlWashClassL2").html('');
        var id = $(this).val();
        if (id > 0) {
            $.post($("#txtWashClassUrl").val(), { level: 2, id: id }, function (data) {
                washclass.initSelectList("ddlWashClassL2", data);
            }, "json");
        } else {
            washclass.initSelectList("ddlWashClassL2", null);
        }
    });
});

var washclass = {};
//生成下拉框控件
washclass.initSelectList = function (id, data) {
    var html = '<option value="-1">请选择</option>';
    if (data && data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            html += '<option value="' + data[i].ID + '">' + data[i].Name + '</option>';
        }
    }
    $("#" + id).html(html);

}



function attributelistselect() {

    var firstlist = ""; // 第一级选中项集合，以逗号相隔。
    var secondlist = ""; //第二级选中项集合集合。
    //jquery循环tt下的所有选中的复选框
    $("#tt input:checked").each(function (i, a) {
        alert(a.value); alert(a.name);
        if (a.name == "first") {
            firstlist += a.value + ",";
            
        }
        else if (a.name == "second") {
            secondlist += a.value + ",";
        }
    });
    //alert(firstlist);
    //alert(secondlist);

    //$("#Chosefirst").val(firstlist.substring(0, firstlist.length - 1));
    $("#Chosesecond").val(secondlist.substring(0, secondlist.length - 1));


    //var oidStr = ""; //定义一个字符串用来装值的集合  

    //jquery循环t2下的所有选中的复选框  
    //$("#tt input:checked").each(function (i, a) {
    //    //alert(a.value);  
    //    oidStr += a.value + ',';  //拼接字符串  
    //});
    //alert(oidStr);
}

