//to handle get request


//
//$(function () {
//.ajax
//})
$("#btnSubmit").on("click", function (event) {
   
    var btn = $(this);
    preventRefresh = true;
    var url =  btn.attr("data-url")
    $.ajax({
       
        url: "Admin/UploadAvatar",
        dataType: 'json', type: 'post',
        data: { 'id': $('#UserID').val(), 'imgData': $('#item-img-output').attr('src') },
        beforeSend: function () {
            $("#imgLoad").removeClass("hide-loader");
            $(this).attr("disabled");

        },
        complete: function () {
            $(this).removeAttr("disabled");
            $("#imgLoad").addClass("hide-loader");
        },
        success: function (result) {
            if (result.status === 200) {
                alert(result.message);

                preventRefresh = false;
                $('#btn_saveavatar').disable();
                $('.avatar-pre-select').fadeIn();
                $('.croppie-container').hide();
                $('#controlbar').fadeOut();
            } else {
                alert( result.message);
            }
        },
        error: function () {
            alert('Error\n' + errorMsg);
        }
    });
})