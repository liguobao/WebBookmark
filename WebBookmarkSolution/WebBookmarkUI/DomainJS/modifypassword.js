$(function () {

    $('#btnSavePassword').bind('click', function (e) {
        e.preventDefault();
       
        var oldpassword = $("#oldpassword").val();
        var newpassword = $("#newpassword").val();

        $.ajax({
            type: "post",
            url: savepasswprdURL,
            data: { oldpassword:oldpassword,newpassword:newpassword },
            success: function (rsp) {
                if (rsp.IsSuccess) {
                    alert(rsp.SuccessMessage);
                    window.location.href = '../login';
                } else {
                    alert(rsp.ErrorMessage);
                }
            }
        });


        
        e.stopPropagation();
    });


    


})