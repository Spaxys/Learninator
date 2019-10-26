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
        newTag.setAttribute("data-name", el.getAttribute("data-name"));
        newTag.name = "Tags";
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
        for (var v of values) {
            var tag = document.createElement("span");
            tag.value = v.id;
            tag.id = "tag_" + v.id;
            tag.setAttribute("data-name", v.name);
            tag.innerText = v.name;
            tag.classList.add("btn");
            tag.classList.add("btn-default");
            tag.classList.add("tagButton");
            nameTag.appendChild(tag);
            new TagButton(tag, tagInputId, tagSetId, nameTagId, idTagId);
        }
    },
    saveTags: function (linkId) {
        event.preventDefault();
        console.log("saveTags called!");
        var tagSet = document.getElementById("tagSet");
        var tags = tagSet.children;
        var tagsArray = [].slice.call(tags);
        var tagsMapped = tagsArray.map(function (el) {
            return {
                id: el.id,
                name: el.attributes["data-name"].value
            }
        });

        var postObject =
        {
            LinkId: linkId,
            Tags: tagsMapped
        };

        $.ajax({
            type: "POST",
            //url: "/test/testpost",
            url: "/Tags/SaveTagsOnLink",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(postObject),
            error: function (xhr) {
                alert('Error: ' + xhr.statusText)
            },
            success: function (msg) {
                console.log(msg.result);
            }
        });
    },
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

