var Tag = {
    getByName: function (input, callback)
    {
        $.ajax({
            url: "/Tags/GetTagByName?name=" + input,
            contentType: "application/json"
        })
        .done(callback)
        .fail(function (jqXHR, textStatus, errorThrown) {
            // Handle error
        });
    }
}