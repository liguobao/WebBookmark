$(function () {

    $('body').on('click', "[name='openfolder']", function () {
        var $this = $(this);
        var $divfolder = $this.find("[name='divFolderID']");
        var folderID = $divfolder.text();
        window.location.href = indexURL + '?folderID=' + folderID; // 跳转到B目录
    });

    $("#btnBack").on('click', function ()
    {
        history.back();
    });

})