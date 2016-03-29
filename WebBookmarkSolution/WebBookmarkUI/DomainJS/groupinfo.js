$(function () {

    $("#btnSave").on('click', function () {
        SaveGroupInfo();
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
       $('#my-confirm').modal({
           relatedTarget: this,
           onConfirm: function (options) {
              
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

    ShowGroupList(createUserID);





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
                    ShowGroupList(createUserID);
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
                    ShowGroupList(createUserID);
                }
                else {
                    alert(result.ErrorMessage);
                }
                $('#modal-modifygroup').modal('close');
            }
    });
}

function DeleteUserGroupInfo() {
    $.ajax({
        type: "post",
        url: deleteUserGroupInfo,
        data: {  },
        success:
            function (result) {
                if (result.IsSuccess) {
                    ShowGroupList(createUserID);
                }
                else {
                    alert(result.ErrorMessage);
                }
                $('#modal-modifygroup').modal('close');
            }
    });
}



function ShowGroupList(createUserID)
{
    $("#mygrouploading").addClass("am-icon-spinner").addClass("am-icon-spin");

    $.ajax({
        type: "post",
        url: showUserGroupListURL,
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
