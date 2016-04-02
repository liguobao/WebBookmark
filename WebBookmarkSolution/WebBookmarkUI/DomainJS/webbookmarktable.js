$(function () {

    $('body').on('click', "[name='openfolder']", function () {
        var $this = $(this);
        var $divfolder = $this.find("[name='divFolderID']");
        var folderID = $divfolder.text();
        ShowFolder(folderID);
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
                    $("#h5title").html("编辑书签夹");
                    $("#editID").html(model.UserWebFolderID);
                    $("#bookmarkname").val(model.WebFolderName);
                    $("#folderSelect").attr("value",model.ParentWebfolderID);
                    $("#bookmarkSelect").attr("value", "folder");
                    $("#bookmarkSelect option[value='folder']").attr("selected", true);
                    $('#AddfolderOrbookmarkModal').modal('toggle');
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
                    $("#h5title").html("编辑书签");
                    $("#editID").html(model.BookmarkInfoID);
                    $("#bookmarkname").val(model.BookmarkName);
                    $("#folderSelect").attr("value", model.UserWebFolderID);
                    $("#bookmarkSelect").attr("value", "bookmark");
                    $("#bookmarkSelect option[value='bookmark']").attr("selected", true);
                    $('#AddfolderOrbookmarkModal').modal('toggle');
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
        var folderID = $this.find("[name='ParentWebfolderID']").html();

        layer.confirm('真的要删除么？(不可恢复)', {
            btn: ['确认', '取消'] //按钮
        }, function () {

            $.ajax({
                type: "post",
                url: deleteInfoURL,
                data: { infoID: infoID, type: type },
                success: function (result) {
                    if (result.IsSuccess) {
                        layer.msg('删除成功。', {
                            icon: 1,
                            time:500
                        });
                        ShowFolder(folderID);
                        ShowAddFolderOrBookmarkView(folderID);
                    } else
                    {
                        alert(result.ErrorMessage);
                    }
                }
                
            });
        });

    });

    //bookmarkSelect

    $('body').on('change', "[id='bookmarkSelect']",function ()
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

    //btnSaveBookmark

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
                    $("#btnBookmarkCancel").click();
                    ShowFolder(folderID);
                    ShowAddFolderOrBookmarkView(folderID);
                } else {
                    alert(rsp.ErrorMessage);
                }
            }
        });



    });

    $("#btnAddFolderOrBookmark").on('click', function ()
    {
        $("#h5title").html("新增书签/书签夹");
        $('#AddfolderOrbookmarkModal').modal('toggle');
        $("#editID").html("");
        $("#bookmarkname").val("");
        $("#folderSelect").attr("value", 0);
        $("#bookmarkSelect").attr("value", "bookmark");
        $("#bookmarkSelect option[value='bookmark']").attr("selected", true);
        $("#bookmarkhref").val("");
        $("#bookmarkhref").show();
    })


    ShowFolder(0);
    ShowAddFolderOrBookmarkView(0);

})

function ShowFolder(folderID)
{
    $("#loadfolder").addClass("am-icon-spinner").addClass("am-icon-spin");

    $.ajax({
        type: "post",
        url: showFolderTableURL,
        data: {folderID: folderID },
        success:
            function (data) {
                if (data != null) {
                  
                    $("#folderBody").html(data);
                }
                $("#loadfolder").removeClass("am-icon-spinner").removeClass("am-icon-spin");
            }
    });
}


function ShowAddFolderOrBookmarkView(folderID)
{
    $.ajax({
        type: "post",
        url: showAddFolderOrBookmarkView,
        data: { folderID: folderID },
        success:
            function (data) {
                if (data != null) {
                    $("#divUIContent").html(data);
                }
            }
    });
}