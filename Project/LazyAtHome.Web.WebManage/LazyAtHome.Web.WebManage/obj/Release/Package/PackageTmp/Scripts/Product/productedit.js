//$(function () {
//    //产品下拉框选择
//    $("#ddlWashClassL3").change(function () {
//        var cid = $(this).val();
//        var pid = $("#ProductInfo_ID").val();

//        //请求地址
//        var url = $("#hdcategoryselectUrl").val();

//        $.post(url, { categoryid: cid, productid : pid}, ShowMsg);
        
//    });
//});

//function ShowMsg(data, status)
//{
//    //alert("数据：" + data + "\n状态：" + status);
//    //alert(data);
//    $("#htmlResult").html(data);    
//}


$(function () {
    $("#btnSave").click(function () {
        //移除属性备选列表
        //var list = "";
        //$("#htmlResult input:checked").each(function (i, a) {
        //    //alert(a.value); alert(a.name); alert(a.id);
        //    if (a.name == "itemcheck") {
        //        list += a.id + ",";

        //    }
        //});
        //alert(list);
        //$("#ChoseItem").val(list.substring(0, list.length - 1));
        
        //把禁止的下拉框解禁，以便数据提交
        var b = document.getElementsByTagName("select");
        for (var i = 0; i < b.length; i++) {
            b[i].disabled = false;
        }

    });

});


//function uploadifySuccessMy(file, data, response)
//{
//    var d = JSON.parse(data);

//    if (d.status == 1)
//    {
//        //上传成功，把文件名放入隐藏域
//        $("#hdproductimagel").val(d.filename);
//        alert($("#hdproductimagel").val());
//    }
//    else
//    {
//        alert(d.message);
//    }

//}



