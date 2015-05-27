$(document).ready(function () {



    $("#QuestionInfo_Type").change(function () {
        
        var a = $(this).children('option:selected').val()
        //alert(a);

        if (a == 3) {
            $("#selectinfo").hide();
        }
        else {
            $("#selectinfo").show();
        }
    });



});
