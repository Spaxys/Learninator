var Tag = {
    Tag: class {
        constructor(tagForm, inputElemId, nameElemId, tagSetId, idElemId, btnDeleteLastTagId, linkId, existingTags) {
            this.tagForm = tagForm;
            this.inputElemId = inputElemId;
            this.nameElemId = nameElemId;
            this.tagSetId = tagSetId;
            this.idElemId = idElemId;
            this.btnDeleteLastTagId = btnDeleteLastTagId;
            this.linkId = linkId;
            this.existingTags = existingTags;
            var inputTag = document.getElementById(inputElemId);
            var idTag = document.getElementById(idElemId);
            var nameTag = document.getElementById(nameElemId);
            var deleteLastTagButton = document.getElementById(btnDeleteLastTagId);
            var tagSet = document.getElementById(tagSetId);

            //Setup initial tags
            console.log("Start adding existing tags");
            debugger;
            for (var t of this.existingTags) {
                console.log("Adding existing tag: " + JSON.stringify(t));
                this.fillTagByData(t.Id, t.Name, this.inputElemId, this.tagSetId, this.idElemId);
            }
            console.log("Finished adding existing tags");
            //Register the search event
            $(inputTag).on("keyup", x => {
                var input = $(inputTag).val();
                var that = this; 
                this.searchByName(input)
                    .then(function (values) {
                        that.handleSearchResults(values, inputElemId, idElemId, nameElemId, tagSetId)
                    })
                    .catch(error => {
                        console.log(error)
                        $(idTag).html("");
                        $(nameTag).html("");
                    });
            });

            //Register the delete button event
            $(deleteLastTagButton).on("click", y => {
                tagSet.removeChild(tagSet.lastChild);
            });

            //Register the form submit event
            var myForm = document.getElementById(tagForm);
            var that2 = this;
            myForm.addEventListener('submit', function (event) {
                event.preventDefault();
                that2.saveTags();
            });

        }
        getByName(input) {
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
        searchByName(input) {
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
        }
        fillTag(el, inputId, tagSetId, tagIds) {
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
        }
        fillTagByData(id, name, inputId, tagSetId, tagIds) {
            var input = document.getElementById(inputId);
            var tagSet = document.getElementById(tagSetId);
            var tagIdsElem = document.getElementById(tagIds);
            input.innerHTML = '';
            input.value = '';
            var newTag = document.createElement("span");
            newTag.innerHTML = name;
            newTag.classList.add("tag");
            newTag.id = id;
            newTag.value = id;
            newTag.setAttribute("data-name", name);
            newTag.name = "Tags";
            tagSet.appendChild(newTag);
            tagIdsElem.innerHTML = id;
        }

        clearButtons(buttonsId) {
            var buttons = document.getElementById(buttonsId);
            while (buttons.firstChild) {
                buttons.removeChild(buttons.firstChild);
            }
        }
        handleSearchResults(values, tagInputId, idTagId, nameTagId, tagSetId) {
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
                new TagButton(tag, tagInputId, tagSetId, nameTagId, idTagId, this);
            }
        }
        saveTags() {
            //event.preventDefault();
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
                LinkId: this.linkId,
                Tags: tagsMapped
            };

            $.ajax({
                type: "POST",
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
        }
    }
}

class TagButton {
    constructor(domobj, tagInputId, tagSetId, nameTagId, idElemId, tagThis) {
        this.o = domobj;
        this.tagSetId = tagSetId;
        this.tagInputId = tagInputId;
        this.idElemId = idElemId;
        this.nameTagId = nameTagId;
        // Closure time! Preserve this 'this', using 'that'
        var that = this;
        domobj.onclick = function () { return that.clickHandler(tagThis); };
    }
    // Handler of clicks
    clickHandler(tagThis) {
        tagThis.fillTag(this.o, this.tagInputId, this.tagSetId, this.idElemId)
        tagThis.clearButtons(this.nameTagId);
    }
}
