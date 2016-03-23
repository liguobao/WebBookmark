$(function () {

    $('body').on('click', "[name='openfolder']", function () {
        var $this = $(this);
        var $divfolder = $this.find("[name='divFolderID']");
        var folderID = $divfolder.text();
        window.location.href = indexURL + '?folderID=' + folderID; 
    });


    $('body').on('click', "[id='folderedit']", function () {
        var $this = $(this);
        var strModel = $this.parent().find("[id='model']").html();

        $.ajax({
            type: "post",
            url: convertToUIWebFolderInfoURL,
            data: { strModel: strModel },
            success: function (model) {
                if (model != null) {
                    $("#editID").html(model.UserWebFolderID);
                    $("#bookmarkname").val(model.WebFolderName);
                    $("#folderSelect").attr("value",model.ParentWebfolderID);
                    $("#bookmarkSelect").attr("value", "folder");
                    $("#bookmarkSelect option[value='folder']").attr("selected", true);
                    $("#btnAddFolderOrBookmark").click();
                    $("#bookmarkhref").hide();
                  
                } else {
                    alert("可能是系统不开心了，刷新一下吧。");
                    location.reload();
                }
            }
        }); 
    });

    $('body').on('click', "[id='bookmarkedit']", function () {
        var $this = $(this);
        var strModel = $this.parent().find("[id='model']").html();

        $.ajax({
            type: "post",
            url: convertToUIBookmarkInfoURL,
            data: { strModel: strModel },
            success: function (model) {
                if (model != null) {
                    $("#editID").html(model.BookmarkInfoID);
                    $("#bookmarkname").val(model.BookmarkName);
                    $("#folderSelect").attr("value", model.UserWebFolderID);
                    $("#bookmarkSelect").attr("value", "bookmark");
                    $("#bookmarkSelect option[value='bookmark']").attr("selected", true);
                    $("#btnAddFolderOrBookmark").click();
                    $("#bookmarkhref").val(model.Href);
                    $("#bookmarkhref").show();

                } else {
                    alert("可能是系统不开心了，刷新一下吧。");
                    location.reload();
                }
            }
        });
    });


    $('body').on('click', "[id='delete']", function () {
        var $this = $(this);
        var infoID = $this.find("[name='infoID']").html();
        var type = $this.val();

        layer.confirm('真的要删除？(不可恢复)', {
            btn: ['确认', '取消'] //按钮
        }, function () {

            $.ajax({
                type: "post",
                url: deleteInfoURL,
                data: { infoID: infoID, type: type },
                success: function (model) {
                    if (model != null) {
                        location.reload();

                    } else {
                        alert("可能是系统不开心了，刷新一下吧。");
                        location.reload();
                    }
                }
            });
        }, function () {
            return ;
        });

    });

    

    $("#bookmarkSelect").change(function ()
    {
        if ($("#bookmarkSelect").val() == "bookmark")
        {
            $("#bookmarkURL").show();
            $("#bookmarkhref").show();
        }else
        {
            $("#bookmarkURL").hide();
            $("#bookmarkhref").hide();
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
        var infoID = $("#editID").html();
        if (infoID == "")
            infoID = "0";
        
        $.ajax({
            type: "post",
            url: saveBookmarkURL,
            data: { name: name, href: href, folderID: folderID,type:type , infoID:infoID},
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