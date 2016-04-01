$(function () {
    $('body').on("click", "[id='quitgroup']", function (e) {
        var $this = $(this);
        var groupUserID = $this.attr("data-id");
        $.ajax({
            type: "post",
            url: quitgroupURL,
            data: { groupUserID: groupUserID },
            success:
                function (result) {
                    if (result.IsSuccess) {
                        location.reload();
                    }
                    else {
                        alert(result.ErrorMessage);
                    }
                }
        });
    });

    $('body').on("click", "[id='addgroupagain']", function (e) {
        var $this = $(this);
        var groupUserID = $this.attr("data-id");
        $.ajax({
            type: "post",
            url: addToGroupAgainURL,
            data: { groupUserID: groupUserID },
            success:
                function (result) {
                    if (result.IsSuccess) {
                        location.reload();
                    }
                    else {
                        alert(result.ErrorMessage);
                    }
                }
        });
    });

})