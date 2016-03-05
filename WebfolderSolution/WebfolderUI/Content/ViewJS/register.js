
$(document).ready(function () {// DOM的onload事件处理函数  
    $('#btnRegister').bind('click', function (e) {
        e.preventDefault();
        RegisterUser();
        e.stopPropagation();
    });
});

function RegisterUser() {
    var email = $("#UserEmail").val();
    if (email == "") {
        alert("请输入邮箱地址。");
        return;
    }

    if ($("#UserPassword").val() == "") {
        alert("请输入密码。");
        return;
    }


    $.ajax(
      {
          type: "post",
          url: "CheckUserEmail",
          data: { email},
          success:
              function (rsp) {
                  if (!rsp.IsSuccess) {
                      alert(rsp.ErrorMessage);
                      return;
                  } else {
                      AddUser();
                  }
              }
      });
}

function AddUser() {
    var userInfo =
    {
        UserEmail: $("#UserEmail").val(),
        Password: $("#UserPassword").val(),
        UserName: $("#UserName").val(),
        Phone: $("#UserPhone").val(),
        QQ: $("#UserQQ").val(),
    };

    $.ajax({
        type: "post",
        url: "RegisterUser",
        data: { UIUserInfo: userInfo },
        success:
            function (rsp) {
                if (rsp.IsSuccess) {
                    alert(rsp.ResultMessage);
                    window.location.href = '../login?userID=' + rsp.ResultID; // 跳转到B目录
                }
                else
                    alert(rsp.ErrorMessage);
            }
    });
}