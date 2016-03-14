$(document).ready(function () {// DOM的onload事件处理函数  
    $('#login_submit').bind('click', function (e) {
        e.preventDefault();
        login();
        e.stopPropagation();
    });
});

function login()
{
    var email = $("#aw-login-user-name").val();
    if (email == "") {
        alert("登陆邮箱你都不给我？！！！");
        return;
    }

    var password = $("#aw-login-user-password").val();
    if (password == "") {
        alert("莫有密码怎么登陆呀？");
        return;
    }

    var userInfo =
    {
        UserEmail:email,
        Password: password,
    };

    $.ajax({
        type:"post",
        url: loginUrl,
        data: { UIUserInfo: userInfo },
        success: function (rsp)
        {
            if(rsp.IsSuccess)
            {
                alert(rsp.SuccessMessage);
                window.location.href = '../Default?uid=' + rsp.ResultID; // 跳转到B目录
            }else
            {
                alert(rsp.ErrorMessage);
            }
        }
        });





}