/// <reference path="../_ref.js" />
$(function () {
    $("#load").hide();
    //load table with script with table


    var getUrl = $(location).attr("href");
    $("#load").hide();
    $.ajax({
        url: getUrl,
        type: "get",
        beforeSend: function () {
            $("#load").show();
        },
        complete: function () {
            $("#load").hide();
        },
        success: function (response) {

            //    $('#tbl_candidate tbody').append(row);
            console.log(response.items)
        },
        error: function () {


        }


    })
});

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
                //0=Formatedid,1=name,2=position,3=level,4=mat_id, 5="#",6="hrefaction", 7="formatedID", 8="ImageFormatedID"
                var trow = '<tr id="{0}"> <td>1</td> <td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td><a href="{5}" class="btn btn-xs btn-primary">Edit</a> | <a href="{6}" data-formated-id="{7}" class="btn btn-xs btn-default delete">Delete <img src="/Images/ajax/LoadingCircle.gif" class="hide-loader" id="{8}" width="20" height="20"></a></td> </tr>';
          
                if (response.value === 200) {
                    console.log(response.post);

                    console.log(response.value);
                    var id = response.post.MatNo, serverpost = "/Admin/DeleteCandidate/" + id;
                    var formatedId = id.replace(/\//g, "_");
                    var imgID = "img_" + formatedId;
                    var rowID = "row_" + formatedId, $rowID = "#row_" + formatedId;
                    //VoteID               "186972"
                    //     var row = String.format(trow, rowID, response.post.FirstName + " " + response.post.LastName, response.post.CurrentLevel, response.post.MatNo, "#", serverpost, formatedId, imgID);
                    var row = '<tr id="' + rowID + '"> <td>' + cn + '</td> <td>' + response.post.FirstName + " " + response.post.LastName + '</td><td>' + response.post.VoteID + '</td><td>' + response.post.MatNo + '</td><td>' + response.post.CurrentLevel + '</td><td>' + response.post.Phone + '</td><td><a href="" class="btn btn-xs btn-primary">Edit</a> | <a href="' + serverpost + '" data-formated-id="' + formatedId + '" class="btn btn-xs btn-default delete">Delete <img src="/Images/ajax/LoadingCircle.gif" class="hide-loader" id="' + imgID + '" width="20" height="20"></a></td> </tr>';
                    $("tbody").append(row);
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

$(function () {

});

function appendCandidateTb(name, position, dept) {
    var row = "<tr> <th>  {0} </th><th>{1}</th><th>{2}</th> <th>{3}</th>  <th>{4}</th>  </tr>";
    var row1 = "<tr> <th>  {0} </th><th>Name</th><th>Position</th> <th>Department</th>  <th>Actions</th>  </tr";
    // var row = String.format(chatRow, name, item.Message, item.DateStamp);
    //  $('#tbl_chat tbody').append(row);
}

$("a.btn.btn-xs.btn-default.delete").on("click", function (event) {
    event.preventDefault();
    var urlGet = $(this).attr("href");
    var id = $(this).attr("id");
    var formatedID = $(this).attr("data-formated-id");
    var $btnID = $("#img_" + formatedID);
    var $rowID = $("#row_" + formatedID);
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
            $rowID.remove();
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


