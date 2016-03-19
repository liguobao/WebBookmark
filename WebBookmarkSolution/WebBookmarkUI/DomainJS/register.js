
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
          url: "./Register/CheckUserEmail",
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
    var uiUserInfo =
    {
        UserEmail: $("#UserEmail").val(),
        Password: $("#UserPassword").val(),
        UserName: $("#UserName").val(),
        Phone: $("#UserPhone").val(),
        QQ: $("#UserQQ").val(),
    };

    $.ajax({
        type: "post",
        url: "./Register/RegisterUser",
        data: uiUserInfo,
        success:
            function (rsp) {
                if (rsp.IsSuccess) {
                    alert(rsp.SuccessMessage);
                    window.location.href = '../login?uid=' + rsp.ResultID; // 跳转到B目录
                }
                else
                    alert(rsp.ErrorMessage);
            }
    });
}