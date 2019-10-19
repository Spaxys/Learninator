var Tag = {
    getByName: function (input)
    {
        return new Promise((resolve, reject) => {
            $.ajax({
                url: "/Tags/GetTagByName?name=" + input,
                contentType: "application/json",
                success: function (data) {
                    resolve(data);
                },
                error: function (error) {
                    reject(error);
                }
            })
        });
    }
}