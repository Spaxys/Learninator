using Learninator.Models;
using Learninator.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learninator.Tests.Services
{
    public static class LinksServiceObject
    {
        public static LinkWithTagsVM GetLink()
        {
            var model = new ViewModels.LinkWithTagsVM
            {
                Link = new Models.Link
                {
                    Title = "foo",
                    Url = "Bar"
                },
                Tags = null
            };
            return model;
        }

        public static LinkWithTagsVM WithTags(this LinkWithTagsVM thisObject)
        {
            thisObject.Tags = new List<Tag>
            {
                new Tag
                {
                    Id = 1,
                    Name = "Foo"
                }
            };
            return thisObject;
        }
    }
}
