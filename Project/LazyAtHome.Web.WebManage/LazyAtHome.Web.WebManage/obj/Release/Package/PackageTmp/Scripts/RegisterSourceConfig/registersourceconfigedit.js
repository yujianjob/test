

$(document).ready(function () {

    var link = $("#RegisterSourceConfigInfo_Content").val();
    //alert(link);
    if (link != "") {

        link = utf16to8(link);
        //alert(link);

        //$("#qrcodeCanvas").html("");

        jQuery('#qrcodeCanvas').qrcode({
            text: link,
            render: "canvas",//设置渲染方式  
            width: 256,     //设置宽度  
            height: 256,     //设置高度  
            typeNumber: -1,      //计算模式  
            correctLevel: QRErrorCorrectLevel.H,//纠错等级  
            background: "#ffffff",//背景颜色  
            foreground: "#000000" //前景颜色 
        });
    }
    


    $("#RegisterSourceConfigInfo_Content").blur(function () {
        var link1 = this.value;
        //alert(link1);
        if (link1 != "") {

            link1 = utf16to8(link1);
            //alert(link1);

            $("#qrcodeCanvas").html("");

            jQuery('#qrcodeCanvas').qrcode({
                text: link1,
                render: "canvas",//设置渲染方式  
                width: 256,     //设置宽度  
                height: 256,     //设置高度  
                typeNumber: -1,      //计算模式  
                correctLevel: QRErrorCorrectLevel.H,//纠错等级  
                background: "#ffffff",//背景颜色  
                foreground: "#000000" //前景颜色 
            });
        }
        

    })

    //function ToCode(link) {
    //    link = utf16to8(link);
    //    alert(link);

    //    $("#qrcodeCanvas").html("");

    //    jQuery('#qrcodeCanvas').qrcode({
    //        text: link,
    //        render: "canvas",//设置渲染方式  
    //        width: 256,     //设置宽度  
    //        height: 256,     //设置高度  
    //        typeNumber: -1,      //计算模式  
    //        correctLevel: QRErrorCorrectLevel.H,//纠错等级  
    //        background: "#ffffff",//背景颜色  
    //        foreground: "#000000" //前景颜色 
    //    });
    //}

});



//处理有中文的字符 在二维码编码前把字符串转换成UTF-8，具体代码如下：
function utf16to8(str) {
    var out, i, len, c;
    out = "";
    len = str.length;
    for (i = 0; i < len; i++) {
        c = str.charCodeAt(i);
        if ((c >= 0x0001) && (c <= 0x007F)) {
            out += str.charAt(i);
        } else if (c > 0x07FF) {
            out += String.fromCharCode(0xE0 | ((c >> 12) & 0x0F));
            out += String.fromCharCode(0x80 | ((c >> 6) & 0x3F));
            out += String.fromCharCode(0x80 | ((c >> 0) & 0x3F));
        } else {
            out += String.fromCharCode(0xC0 | ((c >> 6) & 0x1F));
            out += String.fromCharCode(0x80 | ((c >> 0) & 0x3F));
        }
    }
    return out;
}