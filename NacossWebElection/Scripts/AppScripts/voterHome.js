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
                //var trow = '<tr id="{0}"> <td>1</td> <td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td><a href="{5}" class="btn btn-xs btn-primary">Edit</a> | <a href="{6}" data-formated-id="{7}" class="btn btn-xs btn-default delete">Delete <img src="/Images/ajax/LoadingCircle.gif" class="hide-loader" id="{8}" width="20" height="20"></a></td> </tr>';

                if (response.value === 200) {
                    console.log(response.post);

                    console.log(response.value);
                    //var id = response.post.MatNo, serverpost = "/Admin/DeleteCandidate/" + id;
                    //var formatedId = id.replace(/\//g, "_");
                    //var imgID = "img_" + formatedId;
                    //var rowID = "row_" + formatedId, $rowID = "#row_" + formatedId;
                    ////VoteID               "186972"
                    ////     var row = String.format(trow, rowID, response.post.FirstName + " " + response.post.LastName, response.post.CurrentLevel, response.post.MatNo, "#", serverpost, formatedId, imgID);
                    //var row = '<tr id="' + rowID + '"> <td>' + cn + '</td> <td>' + response.post.FirstName + " " + response.post.LastName + '</td><td>' + response.post.VoteID + '</td><td>' + response.post.MatNo + '</td><td>' + response.post.CurrentLevel + '</td><td>' + response.post.Phone + '</td><td><a href="" class="btn btn-xs btn-primary">Edit</a> | <a href="' + serverpost + '" data-formated-id="' + formatedId + '" class="btn btn-xs btn-default delete">Delete <img src="/Images/ajax/LoadingCircle.gif" class="hide-loader" id="' + imgID + '" width="20" height="20"></a></td> </tr>';
                    //$("tbody").append(row);
                   
                  var url = $(location).attr("origin");
                    url2 = response.url;
                    redirect = url + url2;
                    location.replace(redirect);
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
