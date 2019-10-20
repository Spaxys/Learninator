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
    },
    searchByName: function (input)
    {
        return new Promise((resolve, reject) => {
            $.ajax({
                url: "/Tags/SearchTagByName?name=" + input,
                contentType: "application/json",
                success: function (data) {
                    resolve(data);
                },
                error: function (error) {
                    reject(error);
                }
            })
        });
    },
    fillTag: function (el, inputId, tagNameId, tagIds) {
        var input = document.getElementById(inputId);
        var tagName = document.getElementById(tagNameId);
        var tagIdsElem = document.getElementById(tagIds);
        input.innerHTML = '';
        input.value = '';
        tagName.innerHTML = el.innerHTML;
        tagIdsElem.innerHTML = el.value;
    },
    clearButtons: function (buttonsId) {
        var buttons = document.getElementById(buttonsId);
        while (buttons.firstChild) {
            buttons.removeChild(buttons.firstChild);
        }
    }
}