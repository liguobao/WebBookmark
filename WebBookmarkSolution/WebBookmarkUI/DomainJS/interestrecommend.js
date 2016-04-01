$(function () {
    $("#btnSearch").bind("click", function (e)
    {
        e.preventDefault();
     
        var btnSearch = $("#btnSearch");
        btnSearch.attr("disabled", true);
        btnSearch.html("<i class='am-icon-spinner am-icon-spin'></i>正在努力寻找...");
        var nameOrEmail = $("#searchname").val();
        var divContent = $("#content");


        $.ajax({
            type: "post",
            url: searchURL,
            data: { nameOrEmail: nameOrEmail },
            success:
                function (data) {
                    if (data != null) {
                        btnSearch.html(" <i class='am-icon-search'></i>搜索");
                        divContent.html(data);
                    }
                    btnSearch.html(" <i class='am-icon-search'></i>搜索");
                    btnSearch.attr("disabled", false);
                }
        });
        e.stopPropagation();


    })


  
})




