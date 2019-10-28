var TagSave = {
    registerCreateTag: function (btnCreateId, inputElemId) {
        $("#" + btnCreateId).on("click", function () {
            debugger;
            var tagName = $("#" + inputElemId).val();
            TagSave.createOrGetTag(tagName)
                .then(x => {
                    alert(JSON.stringify(x));
                })
                .catch(x => {
                    alert(JSON.stringify(x))
                });
        });
    },
    createOrGetTag: function (input) {
        return new Promise((resolve, reject) => {
            $.ajax({
                type: "POST",
                url: "/Tags/CreateOrGetTagByName",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(input),
                error: function (error) {
                    reject(error);
                },
                success: function (data) {
                    resolve(data);
                }
            });

        });

    }

}

