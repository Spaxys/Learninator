var Tag = {
    GetByName: function(name) {
        var id = -1;
        var result = $.ajax({
            url: "/Tags/GetTagByName?name=" + name,
            success: function (data) {
                id = data
            },
            contentType: "application/json",
            async: false
        });
        console.log("Tag.GetByName returns: " + id);
        return id;
    }
}