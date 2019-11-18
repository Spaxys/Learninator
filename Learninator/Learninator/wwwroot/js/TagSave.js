var TagSave = {
    registerCreateTag: function (btnCreateId, inputElemId, tagger) {
        $("#" + btnCreateId).on("click", function () {
            var tagName = $("#" + inputElemId).val();
            TagSave.createOrGetTag(tagName)
                .then(x => {
                    tagger.fillTagByData(x.id, x.name, tagger.inputElemId, tagger.tagSetId, tagger.idElemId);
                    tagger.clearButtons(tagger.nameElemId);
                })
                .catch(x => {
                    console.log(JSON.stringify(x))
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

