$(function ()
{
   // ShowBookmarkHTML(bookmarkID, url);
    ShowBookmarkComment(bookmarkID);
  

    $('body').on("click", "[id='bookmarkHTML']", function () {
        var $this = $(this);
        $("#commentInfo").parent().removeClass("am-active");
       // $("#tagInfo").parent().removeClass("am-active");
        $this.parent().addClass("am-active");
        ShowBookmarkHTML(bookmarkID, url);
    })

    $('body').on("click", "[id='commentInfo']", function () {
        var $this = $(this);
      //  $("#tagInfo").parent().removeClass("am-active");
        $("#bookmarkHTML").parent().removeClass("am-active");
        $this.parent().addClass("am-active");
        ShowBookmarkComment(bookmarkID);
    })

    $('body').on("click", "[id='tagInfo']", function () {
        var $this = $(this);
        $("#bookmarkHTML").parent().removeClass("am-active");
        $("#commentInfo").parent().removeClass("am-active");
        $this.parent().addClass("am-active");
        
    })
   
    $("#inputBookmarkTagName").blur(function () {
        var $this = $(this);
        var bookmarkID = $this.attr("data-bookmarkid");
        var tagName = $this.val();
        if (tagName == "")
            return;
        SaveBookmarkTag(tagName,bookmarkID);
    });

    $('body').on("click", "[id='removebookmarkTagInfo']", function () {
        var $this = $(this);
        var bookmarkTagInfoID = $this.attr("data-id");
        var tagName = $this.attr("data-name");

        $.ajax({
            type: "post",
            url: removeBookmarkTagURL,
            data: { bookmarkID: bookmarkID, bookmarkTagInfoID: bookmarkTagInfoID, tagName: tagName },
            success:
                function (result) {
                    if (result.IsSuccess) {
                        $this.parent().hide();
                    }
                    else {
                        alert(result.ErrorMessage);
                    }
                }
        });

    })


    $('body').on("click", "[id='CollectBookmark']", function () {
        var $this = $(this);
        var bookmarkInfoID = $this.attr("data-id");
        $.ajax({
            type: "post",
            url: collectBookmarkToUserDefaultFolderURL,
            data: { bookmarkID: bookmarkInfoID},
            success:
                function (result) {
                    if (result.IsSuccess) {
                        $this.text("收藏成功！");
                    }
                    else {
                        alert(result.ErrorMessage);
                    }
                }
        });

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

function SaveBookmarkTag(tagName,bookmarkID) {
    $.ajax({
        type: "post",
        url: saveBookmarkTagURL,
        data: { bookmarkID:bookmarkID,tagInfoID:0,tagName:tagName},
        success:
            function (result) {
                if (result.IsSuccess) {
                    var taghtml = " <a>" + tagName + " <span class='am-icon-close' id='removebookmarkTagInfo' data-id='0' data-name='" + tagName + "'>&nbsp;&nbsp;</span></a>";
                    $("#TagList").html($("#TagList").html() + taghtml);
                    $("#inputBookmarkTagName").val("");
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
