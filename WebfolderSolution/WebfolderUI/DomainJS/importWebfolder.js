$(function () {
    $('#uploadFile').fileupload({
        url: uploadUrl,
        dataType: 'json',
        done: function (e, rsp) {
            if (rsp.result.IsSuccess) {
                var a = "<a id='importFilePath' href='PreView?path=" + rsp.result.ResultID + "'target='_blank'>"
                    + rsp.result.ResultID + "</a>";
                $('#file-list').html(a);
            } else {
                $('#file-list').html(rsp.result.ErrorMessage);
            }
        }
    });
    $("#btnSaveToDB").bind("click", function (e) {
        e.preventDefault();
        var filePath = $("#importFilePath").html();
        var btnSave = $("#btnSaveToDB");
        btnSave.attr("disabled", true);
        btnSave.html("<i class='am-icon-spinner am-icon-spin'></i>正在努力保存中...");


        $.ajax({
            type: "post",
            url: saveToDBURL,
            data: { filePath },
            success:
                function (rsp) {
                    if (rsp.IsSuccess) {
                        btnSave.html(" <i class='am-icon-cloud-download'></i> 保存成功");
                    }
                    else {
                        btnSave.html(" <i class='am-icon-cloud-download'></i> 保存失败"); 
                    }
                    btnSave.attr("disabled", false);
                }
        });



        e.stopPropagation();
    });
})