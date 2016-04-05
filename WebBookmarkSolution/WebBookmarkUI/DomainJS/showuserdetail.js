$(function ()
{

    $('body').on('click', "[name='openfolder']", function () {
        var $this = $(this);
        var folderID = $this.attr("data-folderid");
        var userInfoID = $this.attr("data-uid");
        ShowFolder(userInfoID, folderID);

    });


    $('body').on("click", "[id='allfolder']", function () {
        var $this = $(this);
        $("#follower").parent().removeClass("am-active");
        $("#befollwed").parent().removeClass("am-active");
        $("#groupinfo").parent().removeClass("am-active");
        $("#groupuser").parent().removeClass("am-active");
        $this.parent().addClass("am-active");
        var uid = $this.attr("data-uid");
        ShowFolder(uid, 0);
    })

    $('body').on("click", "[id='follower']", function () {
        var $this = $(this);
        $("#befollwed").parent().removeClass("am-active");
        $("#allfolder").parent().removeClass("am-active");
        $("#groupinfo").parent().removeClass("am-active");
        $("#groupuser").parent().removeClass("am-active");
        $this.parent().addClass("am-active");
        var uid = $this.attr("data-uid");
        ShowFollow(uid);
    })

    $('body').on("click", "[id='befollwed']", function () {
        var $this = $(this);
        $("#follower").parent().removeClass("am-active");
        $("#allfolder").parent().removeClass("am-active");
        $("#groupinfo").parent().removeClass("am-active");
        $("#groupuser").parent().removeClass("am-active");
        $this.parent().addClass("am-active");
        var uid = $this.attr("data-uid");
        ShowBefollwed(uid);
    })


    $('body').on("click", "[id='groupinfo']", function () {
        var $this = $(this);
        $("#befollwed").parent().removeClass("am-active");
        $("#follower").parent().removeClass("am-active");
        $("#allfolder").parent().removeClass("am-active");
        $("#groupuser").parent().removeClass("am-active");
        $this.parent().addClass("am-active");
        var uid = $this.attr("data-uid");
        ShowUserCreateGroupList(uid);
    })


    $('body').on("click", "[id='groupuser']", function () {
        var $this = $(this);
        $("#befollwed").parent().removeClass("am-active");
        $("#follower").parent().removeClass("am-active");
        $("#allfolder").parent().removeClass("am-active");
        $("#groupinfo").parent().removeClass("am-active");
        $this.parent().addClass("am-active");
        var uid = $this.attr("data-uid");
        ShowUserGroupListAndPass(uid);
    })


    $('body').on("click", "[id='unFollowUser']", function (e) {
        e.preventDefault();
        var $this = $(this);
        var befollowUserID = $this.attr("data-id");;

        $.ajax({
            type: "post",
            url: unFollowUserURL,
            data: { beFollowUserID: befollowUserID },
            success:
                function (result) {
                    if (result.IsSuccess) {
                        $this.removeClass("am-btn-primary");
                        $this.addClass("btn-success");
                        $this.attr("id", "followUser");
                        $this.html("<span>关注</span>");
                    } else {
                        alert(result.ErrorMessage);
                    }

                }
        });
        e.stopPropagation();
    })

    $('body').on('click', "[id='followUser']", function (e) {
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



    $('body').on('click', "[id='follow']", function (e) {
        e.preventDefault();
        var $this = $(this);
        var befollowUserID = $this.parent().find("[id='userid']").html();;

        $.ajax({
            type: "post",
            url: followUserURL,
            data: { beFollowUserID: befollowUserID },
            success:
                function (result) {
                    if (result.IsSuccess) {
                        $this.removeClass('btn-success');
                        $this.addClass('am-btn-primary');
                        $this.attr('disabled', true);
                        $this.html('<span>已关注</span>');
                    } else {
                        alert(result.ErrorMessage);
                    }

                }
        });


        e.stopPropagation();

    })

    $('body').on('click', "[id='showUserInfo']", function (e) {
        e.preventDefault();
        var $this = $(this);
        var showUserInfoID = $this.attr("data-id");
        ShowUserInfo(showUserInfoID);
        e.stopPropagation();
    })


})


function ShowUserInfo(showUserInfoID) {
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




function ShowFolder(showUserInfoID, folderID) {

    $("#loadfolder").addClass("am-icon-spinner").addClass("am-icon-spin");

    $.ajax({
        type: "post",
        url: showUserFolderURL,
        data: { showUserInfoID: showUserInfoID, folderID: folderID },
        success:
            function (data) {
                if (data != null) {

                    $("#overview").html(data);
                }
                $("#loadfolder").removeClass("am-icon-spinner").removeClass("am-icon-spin");
            }
    });
}


function ShowFollow(uid) {
    $("#loadfollow").addClass("am-icon-spinner").addClass("am-icon-spin");

    $.ajax({
        type: "post",
        url: showUserFollowURL,
        data: { uid: uid },
        success:
            function (data) {
                if (data != null) {

                    $("#overview").html(data);
                    $("#overview").show();
                }
                $("#loadfollow").removeClass("am-icon-spinner").removeClass("am-icon-spin");
            }
    });
}


function ShowBefollwed(uid) {
    $("#loadbefollwed").addClass("am-icon-spinner").addClass("am-icon-spin");

    $.ajax({
        type: "post",
        url: showUserBeFollwedUR,
        data: { uid: uid },
        success:
            function (data) {
                if (data != null) {

                    $("#overview").html(data);
                }
                $("#loadbefollwed").removeClass("am-icon-spinner").removeClass("am-icon-spin");
            }
    });
}

function ShowUserCreateGroupList(uid) {
    $("#loadgroupinfo").addClass("am-icon-spinner").addClass("am-icon-spin");

    $.ajax({
        type: "post",
        url: showUserCreateGroupListURL,
        data: { userID: uid },
        success:
            function (data) {
                if (data != null) {

                    $("#overview").html(data);
                }
                $("#loadgroupinfo").removeClass("am-icon-spinner").removeClass("am-icon-spin");
            }
    });
}


function ShowUserGroupListAndPass(uid) {
    $("#loadgroupuser").addClass("am-icon-spinner").addClass("am-icon-spin");

    $.ajax({
        type: "post",
        url: showUserGroupListAndPassURL,
        data: { userID: uid },
        success:
            function (data) {
                if (data != null) {

                    $("#overview").html(data);
                }
                $("#loadgroupuser").removeClass("am-icon-spinner").removeClass("am-icon-spin");
            }
    });
}



