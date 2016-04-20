$(function ()
{    $('body').on("click", "[id='btnPass']", function () {
    var $this = $(this);
    PassGroupUser($this.attr("data-id"));
});

    $('body').on("click", "[id='btnReject']", function () {
        var $this = $(this);
        RejectGroupUser($this.attr("data-id"));
    });
})


function PassGroupUser(groupUserID) {
    $.ajax({
        type: "post",
        url: passGroupUserURL,
        data: { groupUserID: groupUserID },
        success:
            function (result) {
                if (result.IsSuccess) {
                    ShowUserAllGroupMessage();
                } else {
                    alert(result.ErrorMessage);
                }

            }
    });
}


function RejectGroupUser(groupUserID) {
    $.ajax({
        type: "post",
        url: rejectGroupUserURL,
        data: { groupUserID: groupUserID },
        success:
            function (result) {
                if (result.IsSuccess) {
                    ShowUserAllGroupMessage();
                } else {
                    alert(result.ErrorMessage);
                }

            }
    });

}
