$(function ()
{
    ShowNotReadMessage();

    $('body').on("click", "[id='allmessaeg']", function () {
        var $this = $(this);
        $("#notreadmessage").parent().removeClass("am-active");
        $("#hasreadmessage").parent().removeClass("am-active");
        $this.parent().addClass("am-active");
        ShowAllMessage();
    });

    $('body').on("click", "[id='notreadmessage']", function () {
        var $this = $(this);
        $("#hasreadmessage").parent().removeClass("am-active");
        $("#allmessaeg").parent().removeClass("am-active");
        $this.parent().addClass("am-active");
        ShowNotReadMessage();
    });


    $('body').on("click", "[id='hasreadmessage']", function () {
        var $this = $(this);
        $("#notreadmessage").parent().removeClass("am-active");
        $("#allmessaeg").parent().removeClass("am-active");
        $this.parent().addClass("am-active");
        ShowHasReadMessage();
    });





})


function ShowAllMessage() {
    $("#allmessaegloading").addClass("am-icon-spinner").addClass("am-icon-spin");

    $.ajax({
        type: "post",
        url: showAllMessageURL,
        success:
            function (data) {
                if (data != "") {
                    $("#overview").html(data);
                } else
                {
                    $("#overview").html("并没有任何的数据....");
                }
                $("#allmessaegloading").removeClass("am-icon-spinner").removeClass("am-icon-spin");
            }
    });
}


function ShowNotReadMessage() {
    $("#notreadmessageloading").addClass("am-icon-spinner").addClass("am-icon-spin");

    $.ajax({
        type: "post",
        url: showNotReadMessageURL,
        success:
            function (data) {
                if (data != "") {
                    $("#overview").html(data);
                } else
                {
                    $("#overview").html("并没有任何的数据....");
                }
                $("#notreadmessageloading").removeClass("am-icon-spinner").removeClass("am-icon-spin");
            }
    });
}


function ShowHasReadMessage() {
    $("#hasreadmessageloading").addClass("am-icon-spinner").addClass("am-icon-spin");

    $.ajax({
        type: "post",
        url: showHasReadMessageURL,
        success:
            function (data) {
                if (data != "") {
                    $("#overview").html(data);
                } else
                {
                    $("#overview").html("并没有任何的数据....");
                }
                $("#hasreadmessageloading").removeClass("am-icon-spinner").removeClass("am-icon-spin");
            }
    });
}
