
$(document).ready(function () {
    //called when key is pressed in textbox
    $("#Homework").keypress(function (e) {
        //if the letter is not digit then display error and don't type anything
        $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
        if ((e.which != 46 || $(this).val().indexOf('.') != -1) && (e.which < 48 || e.which > 57)) {
            //display error message
            $("#errmsg2").html("Digits Only").show().fadeOut("slow");
            return false;
        }
    });
});

$(document).ready(function () {
    //called when key is pressed in textbox
    $("#Excercise").keypress(function (e) {
        //if the letter is not digit then display error and don't type anything
        $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
        if ((e.which != 46 || $(this).val().indexOf('.') != -1) && (e.which < 48 || e.which > 57)) {
            //display error message
            $("#errmsg3").html("Digits Only").show().fadeOut("slow");
            return false;
        }
    });
});

$(document).ready(function () {
    //called when key is pressed in textbox
    $("#Participation").keypress(function (e) {
        //if the letter is not digit then display error and don't type anything
        $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
        if ((e.which != 46 || $(this).val().indexOf('.') != -1) && (e.which < 48 || e.which > 57)) {
            //display error message
            $("#errmsg4").html("Digits Only").show().fadeOut("slow");
            return false;
        }
    });
});

    //Enable SAVE button
    (function () {
        $('#Student_ID,#Book_ID,#Chapter_ID,#Homework,#Excercise,#Participation').change(function () {

            var empty = false;
            $('#Student_ID,#Book_ID,#Chapter_ID,#Homework,#Excercise,#Participation').each(function () {
                if ($(this).val() == '') {
                    empty = true;
                }
            });

            if (empty) {
                $('#SaveGrade').attr('disabled', 'disabled');
            } else {
                $('#SaveGrade').removeAttr('disabled');
            }
        });
    })();