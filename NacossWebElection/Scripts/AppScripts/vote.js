$("a.vote").on("click", function (event) {
    event.preventDefault();
    var urlGet = $(this).attr("href");
    var id = $(this).attr("id");
    var formatedID = $(this).attr("data-formated-id");
    var $btnID = $("#img_" + formatedID);
   //mxk
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
            if (response.value === 200) {
            $("#msgText").text(response.message);
            $("#myModal").modal("show");
            }
            else if (response.value === 1) {
                //redirect to disloaction 
                url = $(location).attr("origin");
                url2 = response.url;
                redirect = url + url2;
                location.replace(redirect);
                $("#msgText1").text(response.message);
                $("#url").attr("href", redirect);
                $("#myModal1").modal("show");
            }
            else {
                $("#msgText").text(response.message);
                $("#myModal").modal("show");
            }
           
            //   alert("Deleted Successfully :" + urlGet + ": " + response.message, +": " + "Status: " + response.status);
            $("#tblError").text("");
        },
        error: function (response) {
            //show errors
            $("#msgText").text(response.message);
            $("#myModal").modal("show");
            console.log(response)
        },
    });
})