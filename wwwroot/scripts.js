$(".generateButton").click(function () {
        $.ajax({
            type: "POST",
            url: "Share",
            data: "path=" + $(this).data("path"),
            success: function (data) {
                $(this).replaceWith(data.link)
            }
        })
    }
)