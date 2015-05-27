$(function(){
    $(".orderList tbody tr:odd").css("background", "#f6f6f7");
    $(".orderList tbody tr:even").css("background", "#fff");
})


function updatesfnumber(id) {

    var number = $("#txt" + id).val();
    //var number = document.getElementById(id);
    //alert(number);

    //请求地址
    var url = $("#hdSFNumberUrl").val();

    $.post(url, { oid: id, sfnumber: number }, navTabAjaxDone);
}


function callsf(id) {

    var number = $("#txt" + id).val();

    //请求地址
    var url = $("#hdCallSFUrl").val();

    $.post(url, { oid: id }, navTabAjaxDone);
}

