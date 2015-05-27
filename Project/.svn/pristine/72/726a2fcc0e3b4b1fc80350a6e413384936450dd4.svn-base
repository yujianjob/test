function uploadifyCategoryImageSuccess(file, data, response) {
    var d = JSON.parse(data);

    if (d.status == 1) {
        //上传成功，把文件名放入隐藏域 以便数据保存
        $("#hd" + d.type).val(d.filename);
        //alert($("#hd" + d.type).val());
        //重置图片源
        document.getElementById(d.type).setAttribute("src", $("#hd" + d.type + "root").val() + d.filename);

        //if (d.type == "categoryimagel") {
        //    $("#hdcategoryimagel").val(d.filename);
        //    //alert($("#hdcategoryimagel").val());

        //    var path = $("#hdcategoryimagelroot").val() + d.filename;
        //    document.getElementById("categoryimagel").setAttribute("src", path);
        //}
        //else if (d.type == "categoryimagem")
        //{
        //    $("#hdcategoryimagem").val(d.filename);
        //    //alert($("#hdcategoryimagem").val());
        //}
        //else if (d.type == "categoryimages") {
        //    $("#hdcategoryimages").val(d.filename);
        //    //alert($("#hdcategoryimages").val());
        //}
        
    }
    else {
        alert(d.message);
    }
}

function uploadifyClassImageSuccess(file, data, response) {
    var d = JSON.parse(data);

    if (d.status == 1) {
        //上传成功，把文件名放入隐藏域
        $("#hdclassimage").val(d.filename);
        //alert($("#hdclassimageroot").val());

        var path = $("#hdclassimageroot").val() + d.filename;
        document.getElementById("classimage").setAttribute("src", path);

    }
    else {
        alert(d.message);
    }
}
//不处理属性列表
//$(function () {
//    $("#btnSave").click(function () {
//        var str = "";
//        $('input[type="checkbox"]:checked').each(function (i, e) {
//            str += e.id + ",";
//        });
//        str = str.substring(0, str.length - 1);
//        $("#Chosesecond").val(str);

//    });
//});


$(function () {
    $("#btnSave").click(function () {

        var sort = "";
        var a = document.getElementById("sortDrag").getElementsByTagName("div");
        for (var i = 0; i < a.length; i++) {
            sort += a[i].id + ",";
        }
        sort = sort.substring(0, sort.length - 1);

        alert(sort);


        //小类ID
        var classid = $("#ddlWashClassL2").val();
        alert(classid);
        //请求地址
        var url = $("#hdCategorySortUrl").val();

        $.post(url, { classid: classid, sort: sort }, navTabAjaxDone);

        //var str = "";
        //$('input[type="checkbox"]:checked').each(function (i, e) {
        //    str += e.id + ",";
        //});
        //str = str.substring(0, str.length - 1);
        //$("#Chosesecond").val(str);

    });
});