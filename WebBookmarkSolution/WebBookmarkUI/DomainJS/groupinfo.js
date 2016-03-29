$(function () {

    $("#btnSave").on('click', function () {
        SaveGroupInfo();
    });

    $('body').on("click", "[id='modifygroup']", function (e) {
        e.preventDefault();
        var $this = $(this);
        var groupInfoID = $this.attr("data-id");

        $('#my-confirm').modal({
            relatedTarget: this,
            onConfirm: function (options) {
                var $link = $(this.relatedTarget).prev('a');
                var msg = $link.length ? '你要删除的链接 ID 为 ' + $link.data('id') :
                  '确定了，但不知道要整哪样';
                alert(msg);
            },
            // closeOnConfirm: false,
            onCancel: function () {
                alert('算求，不弄了');
            }
        });

        e.stopPropagation();
    })


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
