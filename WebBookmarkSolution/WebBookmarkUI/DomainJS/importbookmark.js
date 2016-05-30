$(function () {
    $('#uploadFile').fileupload({
        url: uploadUrl,
        dataType: 'json',
        done: function (e, rsp) {
            if (rsp.result.IsSuccess) {
                var a = "<a id='importFilePath' href='PreView?importLogID=" + rsp.result.ResultID + "'target='_blank' data-id='" + rsp.result.ResultID + "'>预览书签文件</a>";
                $('#file-list').html(a);
                $('#file-list').attr("class","am-text-success");
            } else {
                $('#file-list').html(rsp.result.ErrorMessage);
                $('#file-list').attr("class","am-text-danger");
            }
        }
    });
    $("#btnSaveToDB").bind("click", function (e) {
        e.preventDefault();
        var importLogID = $("#importFilePath").attr("data-id");
        var btnSave = $("#btnSaveToDB");
        btnSave.attr("disabled", true);
        btnSave.html("<i class='am-icon-spinner am-icon-spin'></i>正在努力保存中...");


        $.ajax({
            type: "post",
            url: saveToDBURL,
            data: { importLogID : importLogID },
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