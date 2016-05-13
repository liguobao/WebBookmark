$(function () {

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


