$(document).ready(function () {
    $("#Page").blur(function () {  
        var link = this.value;
        //alert(link);

        var index = link.indexOf("?");
        if (index >= 0) {
            link = link.substring(0, index)
        }

        //alert(link);

        link = replaceKey(link, "http://www.landaojia.com", "");
        //alert(link);

        link = replaceKey(link, "www.landaojia.com", "");
        //alert(link);
        this.value = link;
    })

});


function replaceKey(stringObj, replaceText, newText) {
    var regExp = new RegExp(replaceText, "g");
    stringObj = stringObj.replace(regExp, newText);
    return stringObj;
}


