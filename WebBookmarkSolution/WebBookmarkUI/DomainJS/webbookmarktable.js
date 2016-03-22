$(function () {

    $('body').on('click', "[name='openfolder']", function () {
        var $this = $(this);
        var $divfolder = $this.find("[name='divFolderID']");
        var folderID = $divfolder.text();
        window.location.href = indexURL + '?folderID=' + folderID; // 跳转到B目录
    });


    $('body').on('click', "[id='edit']", function () {
        var $this = $(this);
        var model = $this.parent().find("[id='model']");
        alert(model.html());
    });


    $("#bookmarkSelect").change(function ()
    {
        if ($("#bookmarkSelect").val() == "bookmark")
        {
            $("#bookmarkURL").show();
        }else
        {
            $("#bookmarkURL").hide();
        }
    });

    $("#btnBack").on('click', function () {
        history.back();
    });

    $("#btnSaveBookmark").on('click', function () {
       
        var name = $("#bookmarkname").val();
        var href = $("#bookmarkhref").val();
        var folderID = $("#folderSelect").val();
        var type = $("#bookmarkSelect").val();
        
        $.ajax({
            type: "post",
            url: saveBookmarkURL,
            data: { name: name, href: href, folderID: folderID,type:type },
            success: function (rsp) {
                if (rsp.IsSuccess) {
                    alert(rsp.SuccessMessage);
                    $("#btnBookmarkCancel").click();
                    location.reload();
                } else {
                    alert(rsp.ErrorMessage);
                }
            }
        });



    });

})