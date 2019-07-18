$("#makeAdmin").on("click", function (event) {
    event.preventDefault();
    var urlGet = $(this).attr("href");
    var formatedID = $(this).attr("data-formated-id");
    var $btnID = $("#img_" + formatedID);
    $.ajax({
        url: urlGet,
        type: "get",
        beforeSend: function () {
            //show loading for btn  
            $btnID.removeClass("hide-loader");
            $btnID.attr("disabled", "disabled");
            console.log("Target Location :" + urlGet);
        },
        complete: function () {
            //hide Loading for btn
            $btnID.addClass("hide-loader");
            //$("#btnSubmit").removeAttr("disabled");
            $btnID.removeAttr("disabled");
        },
        success: function (response) {
            //remove the deleted row
            $("#msgText").text(response.message);
            $("#myModal").modal();
            console.log(response.message);
            console.log(response);
            //   alert("Deleted Successfully :" + urlGet + ": " + response.message, +": " + "Status: " + response.status);
            $("#tblError").text("");
        },
        error: function (response) {
            //show errors
            $("#tblError").text(response.message);
            console.log(response)
        },
    });
})



$("#removeRole").on("click", function (event) {
    event.preventDefault();
    var urlGet = $(this).attr("href");
    var formatedID = $(this).attr("data-formated-id");
    var $btnID = $("#img_" + formatedID);
    $.ajax({
        url: urlGet,
        type: "get",
        beforeSend: function () {
            //show loading for btn  
            $btnID.removeClass("hide-loader");
            $btnID.attr("disabled", "disabled");
            console.log("Target Location :" + urlGet);
        },
        complete: function () {
            //hide Loading for btn
            $btnID.addClass("hide-loader");
            //$("#btnSubmit").removeAttr("disabled");
            $btnID.removeAttr("disabled");
        },
        success: function (response) {
            //remove the deleted row
            $("#msgText").text(response.messsage);
            $("#myModal").modal();

            //   alert("Deleted Successfully :" + urlGet + ": " + response.message, +": " + "Status: " + response.status);
            $("#tblError").text("");
        },
        error: function (response) {
            //show errors
            $("#tblError").text(response.messsage);
            console.log(response)
        },
    });
})
