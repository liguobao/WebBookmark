$(function () {

    $('body').on('click', "[name='folder']", function () {
        var $this = $(this);
        var $divfolder = $this.find("[name='divFolderID']");
        var folderID = $divfolder.text();
        $.ajax({
            type: "post",
            url: showFolderURL,
            data: {
                strAllFolder: strAllFolder,
                showFolderID: folderID
            }
        });

    });

})