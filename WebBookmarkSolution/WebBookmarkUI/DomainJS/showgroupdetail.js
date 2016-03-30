$(function ()
{
    ShowGroupUserList(groupID);


    $('body').on("click", "[id='groupuser']", function () {
        var $this = $(this);
        $("#groupinfo").parent().removeClass("am-active");
        $("#groupusernopass").parent().removeClass("am-active");
        $this.parent().addClass("am-active");
        ShowGroupUserList(groupID);
    });

    $('body').on("click", "[id='groupusernopass']", function () {
        var $this = $(this);
        $("#groupinfo").parent().removeClass("am-active");
        $("#groupuser").parent().removeClass("am-active");
        $this.parent().addClass("am-active");
        ShowGroupUserListNotPass(groupID);
    });


    $('body').on("click", "[id='groupinfo']", function () {
        var $this = $(this);
        $("#groupuser").parent().removeClass("am-active");
        $("#groupusernopass").parent().removeClass("am-active");
        $this.parent().addClass("am-active");
        ShowGroupUserList(groupID);
    });


    $('body').on("click", "[id='btnPass']", function () {
        var $this = $(this);
        PassGroupUser($this.attr("data-id"));
    });
});

function ApplyToGroup()
{
    var groupID = $("#applytogroup").attr("data-id");
    $.ajax({
        type: "post",
        url: applyToGroupURL,
        data: { groupID: groupID },
        success:
            function (result) {
                if (result.IsSuccess) {
                   
                }else
                {
                    alert(result.ErrorMessage);
                }
               
            }
    });
}


function ShowGroupUserList(groupID)
{
    $("#groupuserload").addClass("am-icon-spinner").addClass("am-icon-spin");

    $.ajax({
        type: "post",
        url: showGroupUserList,
        data: { groupID: groupID },
        success:
            function (data) {
                if (data != "") {
                    $("#overview").html(data);
                }
                $("#groupuserload").removeClass("am-icon-spinner").removeClass("am-icon-spin");
            }
    });
}


function ShowGroupUserListNotPass(groupID) {
    $("#groupusernopassload").addClass("am-icon-spinner").addClass("am-icon-spin");

    $.ajax({
        type: "post",
        url: showGroupUserListNotPass,
        data: { groupID: groupID },
        success:
            function (data) {
                if (data != "") {
                    $("#overview").html(data);
                }
                $("#groupusernopassload").removeClass("am-icon-spinner").removeClass("am-icon-spin");
            }
    });
}

function PassGroupUser(groupUserID)
{
    $.ajax({
        type: "post",
        url: passGroupUserURL,
        data: { groupUserID: groupUserID },
        success:
            function (result) {
                if (result.IsSuccess) {
                    ShowGroupUserListNotPass(groupID);
                } else {
                    alert(result.ErrorMessage);
                }

            }
    });
}