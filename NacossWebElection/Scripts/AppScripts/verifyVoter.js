/// <reference path="verifyVoter.js" />
$(function () {
    $("#form1").submit(function (event) {

        event.preventDefault();

        $.ajax({
            url: $("#form1").attr("action"),
            data: $("#form1").serialize(),
            type: "post",
            beforeSend: function () {
                $("#imgLoad").removeClass("hide-loader");
                $("#btnSubmit").attr("disabled");
            },

            complete: function () {
                $("#imgLoad").addClass("hide-loader");
                $("#btnSubmit").removeAttr("disabled");
            },

            success: function (response) {
       
                if (response.value === 200) {
                    console.log(response.post);
                    console.log(response.value);
                    $("#msgText").text(response.message);
                    $("#myModal").modal("show");
                }
                else if (response.value === 0) {
                    $("#alertText").text(response.message);
                    $("#myAlert").removeClass("hide-loader");
                    $("#msgText").text(response.message);
                    $("#myModal").modal("show");
                }
            },
            error: function (response) {
                $("#alertText").text("Error Something went wrong");
                $("#myAlert").removeClass("hide-loader");
                console.log(response);
            }
        });
    });

});
