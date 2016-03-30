$(function ()
{
    ShowGroupUserList(groupID);


    $('body').on("click", "[id='groupuser']", function () {
        var $this = $(this);
        $("#groupinfo").parent().removeClass("am-active");
        $this.parent().addClass("am-active");
        ShowGroupUserList(groupID);
    });


    $('body').on("click", "[id='groupinfo']", function () {
        var $this = $(this);
        $("#groupuser").parent().removeClass("am-active");
        $this.parent().addClass("am-active");
        ShowGroupUserList(groupID);
    });
});

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