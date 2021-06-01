$(".generateButton").click(function () {
    let btn = $(this)
        $.ajax({
            type: "POST",
            url: btn.data("url"),
            data: {path: btn.data("path")},
            success: function (data) {
                btn.replaceWith(data.link)
            }
        })
    }
)