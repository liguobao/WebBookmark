$(function ()
{
    ShowBefollwed(bookmarkID,url);

})



function ShowBefollwed(bookmarkID,url) {
    $("#loadHTML").addClass("am-icon-spinner").addClass("am-icon-spin");

    $.ajax({
        type: "post",
        url: showHTML,
        data: { bookmarkID:bookmarkID,url :url},
        success:
            function (data) {
                if (data != null) {

                    $("#overview").html(data);
                }
                $("#loadHTML").removeClass("am-icon-spinner").removeClass("am-icon-spin");
            }
    });
}

