var Tag = {
    Tag: function (inputElemId, nameElemId, tagSetId, idElemId, btnDeleteLastTagId) {
        var inputTag = document.getElementById(inputElemId);
        var idTag = document.getElementById(idElemId);
        var nameTag = document.getElementById(nameElemId);
        var deleteLastTagButton = document.getElementById(btnDeleteLastTagId);
        var tagSet = document.getElementById(tagSetId);
        $(inputTag).on("keyup", x => {
            var input = $(inputTag).val();
            Tag.searchByName(input)
                .then(function (values) {
                    Tag.handleSearchResults(values, inputElemId, idElemId, nameElemId, tagSetId)
                })
                .catch(error => {
                    console.log(error)
                    $(idTag).html("");
                    $(nameTag).html("");
                });
        });
        $(deleteLastTagButton).on("click", y => {
            tagSet.removeChild(tagSet.lastChild);
        });

    },
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
    fillTag: function (el, inputId, tagSetId, tagIds) {
        var input = document.getElementById(inputId);
        var tagSet = document.getElementById(tagSetId);
        var tagIdsElem = document.getElementById(tagIds);
        input.innerHTML = '';
        input.value = '';
        var newTag = document.createElement("span");
        newTag.innerHTML = el.innerHTML;
        newTag.classList.add("tag");
        newTag.id = el.value;
        newTag.value = el.value;
        tagSet.appendChild(newTag);
        tagIdsElem.innerHTML = el.value;
    },
    clearButtons: function (buttonsId) {
        var buttons = document.getElementById(buttonsId);
        while (buttons.firstChild) {
            buttons.removeChild(buttons.firstChild);
        }
    },
    handleSearchResults: function (values, tagInputId, idTagId, nameTagId, tagSetId) {
        var idTag = document.getElementById(idTagId);
        var nameTag = document.getElementById(nameTagId);
        $(nameTag).html("");
        $(idTag).html("");
        console.log(values);
        console.log("Typeof values: " + typeof values);
        for (var v of values) {
            console.log("Value: " + JSON.stringify(v));
            console.log("Typeof value: " + typeof v);
            var tag = document.createElement("span");
            tag.value = v.id;
            tag.id = "tag_" + v.id;
            tag.innerText = v.name;
            tag.classList.add("btn");
            tag.classList.add("btn-default");
            nameTag.appendChild(tag);
            new TagButton(tag, tagInputId, tagSetId, nameTagId, idTagId);
        }
    }
}
function TagButton(domobj, tagInputId, tagSetId, nameTagId, idElemId) {
    this.o = domobj;
    this.tagSetId = tagSetId;
    this.tagInputId = tagInputId;
    this.idElemId = idElemId;
    this.nameTagId = nameTagId;
    // Closure time! Preserve this 'this', using 'that'
    var that = this;
    domobj.onclick = function () { return that.clickHandler(); };
}
// Handler of clicks
TagButton.prototype.clickHandler = function () {
    Tag.fillTag(this.o, this.tagInputId, this.tagSetId, this.idElemId)
    Tag.clearButtons(this.nameTagId);
}