function uploadifyImageSuccess(file, data, response) {
    var d = JSON.parse(data);

    if (d.status == 1) {
        //上传成功，把文件名放入隐藏域 以便数据保存
        $("#hd" + d.type).val(d.filename);
        //alert($("#hd" + d.type).val());
        //alert($("#hd" + d.type + "root").val());
        //alert($("#hd" + d.type + "root").val() + d.filename);
        //重置图片源
        document.getElementById(d.type).setAttribute("src", $("#hd" + d.type + "root").val() + d.filename);


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