$(function ()
{
   // ShowBookmarkHTML(bookmarkID, url);
    ShowBookmarkComment(bookmarkID);
  

    $('body').on("click", "[id='bookmarkHTML']", function () {
        var $this = $(this);
        $("#commentInfo").parent().removeClass("am-active");
        $this.parent().addClass("am-active");
        ShowBookmarkHTML(bookmarkID, url);
    })

    $('body').on("click", "[id='commentInfo']", function () {
        var $this = $(this);
       
        $("#bookmarkHTML").parent().removeClass("am-active");
        $this.parent().addClass("am-active");
        ShowBookmarkComment(bookmarkID);
    })
   

})


function SaveComment()
{
   // var title = $("#title").val();
    var content = $("#content").val();

    $.ajax({
        type: "post",
        url: saveComment,
        data: { bookmarkID: bookmarkID, content: content },
        success:
            function (result) {
                if (result.IsSuccess) {
                    ShowBookmarkComment(bookmarkID);
                }
                else {
                    alert(result.ErrorMessage);
                }
            }
    });
}


function ShowBookmarkHTML(bookmarkID, url) {
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




function ShowBookmarkComment(bookmarkID) {
    $("#loadcomment").addClass("am-icon-spinner").addClass("am-icon-spin");

    $.ajax({
        type: "post",
        url: showBookmarkComment,
        data: { bookmarkID: bookmarkID},
        success:
            function (data) {
                if (data != null) {

                    $("#overview").html(data);
                }
                $("#loadcomment").removeClass("am-icon-spinner").removeClass("am-icon-spin");
            }
    });
}
