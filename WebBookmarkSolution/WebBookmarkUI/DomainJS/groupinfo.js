$(function () {

    $("#btnSave").on('click', function () {
        SaveGroupInfo();
    });


    $("#btnAddGroup").on('click', function () {
         $("#groupName").val("");
         $("#groupIntro").val("");
    });

    $('body').on("click", "[id='showGroupDetail']", function (e) {
        e.preventDefault();
        var $this = $(this);
        var groupID = $this.attr("data-id");
        window.location = showGroupDetail + groupID;
        e.stopPropagation();
    });


    $('body').on("click", "[id='modifygroup']", function (e) {
        e.preventDefault();
        var $this = $(this);
        $("#modifyID").html($this.attr("data-id"));
        $("#modifyName").val($this.attr("data-name"));
        $("#modifyIntro").val($this.attr("data-intro"));
        $('#modal-modifygroup').modal('toggle');
        e.stopPropagation();
    });


    $('body').on("click", "[id='deletegroup']", function (e) {
        var $this = $(this);
        var deleteId = $this.attr("data-id");

       $('#my-confirm').modal({
           relatedTarget: this,
           onConfirm: function (options) {
               DeleteUserGroupInfo(deleteId);
           },
           // closeOnConfirm: false,
           onCancel: function () {
               $("my-confirm").modal("close");
           }
       });
   });




    $("#btnModify").on('click', function () {
        ModifyUserGroupInfo();
    });

    ShowMyGroupList(createUserID);


    $('body').on("click", "[id='mygroup']", function () {
        var $this = $(this);
        $("#groupuser").parent().removeClass("am-active");
        $("#groupmessage").parent().removeClass("am-active");
        $this.parent().addClass("am-active");
        ShowMyGroupList(createUserID);
    });

    $('body').on("click", "[id='groupuser']", function () {
        var $this = $(this);
        $("#mygroup").parent().removeClass("am-active");
        $("#groupmessage").parent().removeClass("am-active");
        $this.parent().addClass("am-active");
        ShowUserGroupListHasPass(createUserID);
    });

    $('body').on("click", "[id='groupmessage']", function () {
        var $this = $(this);
        $("#mygroup").parent().removeClass("am-active");
        $("#groupuser").parent().removeClass("am-active");
        $this.parent().addClass("am-active");
        ShowALLUserGroupList(createUserID);
    })



});


function SaveGroupInfo()
{
    var groupName = $("#groupName").val();
    var groupIntro = $("#groupIntro").val();
    if(groupName=="")
    {
        alert("没有群组名称不能保存呀....");
        return;
    }
    $.ajax({
        type: "post",
        url: saveUserGroupInfoURL,
        data: { groupName: groupName, groupIntro: groupIntro },
        success:
            function (result) {
                if (result.IsSuccess) {
                    ShowMyGroupList(createUserID);
                }
                else {
                    alert(result.ErrorMessage);
                }
                $("#btnCancel").click();
            }
    });
}


function ModifyUserGroupInfo() {
    var groupName = $("#modifyName").val();
    var groupIntro = $("#modifyIntro").val();
    var groupID = $("#modifyID").html();
    if (groupName == "") {
        alert("没有群组名称不能保存呀....");
        return;
    }

    if (groupID == "") {
        alert("没有群组ID不能保存呀....");
        return;
    }

    $.ajax({
        type: "post",
        url: modifyUserGroupInfo,
        data: { groupName: groupName, groupIntro: groupIntro, groupID: groupID },
        success:
            function (result) {
                if (result.IsSuccess) {
                    ShowMyGroupList(createUserID);
                }
                else {
                    alert(result.ErrorMessage);
                }
                $('#modal-modifygroup').modal('close');
            }
    });
}

function DeleteUserGroupInfo(deleteId) {
    $.ajax({
        type: "post",
        url: deleteUserGroupInfo,
        data: { groupID: deleteId },
        success:
            function (result) {
                if (result.IsSuccess) {
                    ShowMyGroupList(createUserID);
                }
                else {
                    alert(result.ErrorMessage);
                }
                $('#modal-modifygroup').modal('close');
            }
    });
}



function ShowMyGroupList(createUserID)
{
    $("#mygrouploading").addClass("am-icon-spinner").addClass("am-icon-spin");

    $.ajax({
        type: "post",
        url: showMyGroupList,
        data: { createUserID: createUserID },
        success:
            function (data) {
                if(data!="")
                {
                    $("#overview").html(data);
                }
                $("#mygrouploading").removeClass("am-icon-spinner").removeClass("am-icon-spin");
            }
    });
}


function ShowUserGroupListHasPass(userID) {
    $("#groupuserloading").addClass("am-icon-spinner").addClass("am-icon-spin");

    $.ajax({
        type: "post",
        url: showUserGroupListHasPassURL,
        data: { userID: userID },
        success:
            function (data) {
                if (data != "") {
                    $("#overview").html(data);
                }
                $("#groupuserloading").removeClass("am-icon-spinner").removeClass("am-icon-spin");
            }
    });
}
