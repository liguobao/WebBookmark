$(function () {

    $('body').on('click', "[id='followuser']", function (e) {
        e.preventDefault();
        var $this = $(this);
        var befollowUserID = $this.attr("data-id");;

        $.ajax({
            type: "post",
            url: followUserURL,
            data: { beFollowUserID: befollowUserID },
            success:
                function (result) {
                    if (result.IsSuccess) {
                        $this.removeClass("btn-success");
                        $this.addClass("am-btn-primary");
                        $this.html("<span>取消关注</span>");
                        $this.attr("id", "unFollowUser");
                    } else {
                        alert(result.ErrorMessage);
                    }

                }
        });
        e.stopPropagation();
    })



  

    $('body').on('click', "[id='showUserDetail']", function (e) {
        e.preventDefault();
        var $this = $(this);
        var showUserInfoID = $this.attr("data-id");
        ShowUserDetail(showUserInfoID);
        e.stopPropagation();
    })


})


function ShowUserDetail(showUserInfoID) {
    $.ajax({
        type: "post",
        url: showUserInfoURL,
        data: { showUserInfoID: showUserInfoID },
        success:
            function (data) {
                $("#divContent").html(data);
                ShowFolder(showUserInfoID, 0);
            }
    });
}


