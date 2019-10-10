using HtmlAgilityPack;
using Learninator.Library.MetaData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learninator.Library.MetaData
{
        public static class MetaScraper
        {
            /// <summary>
            /// Uses HtmlAgilityPack to get the meta information from a url
            /// </summary>
            /// <param name="url"></param>
            /// <returns></returns>
            public static MetaInformation GetMetaDataFromUrl(string url)
            {
                // Get the URL specified
                var webGet = new HtmlWeb();
                var document = webGet.Load(url);
                var metaTags = document.DocumentNode.SelectNodes("//meta");
                MetaInformation metaInfo = new MetaInformation(url);
                if (metaTags != null)
                {
                    int matchCount = 0;
                    foreach (var tag in metaTags)
                    {
                        var tagName = tag.Attributes["name"];
                        var tagContent = tag.Attributes["content"];
                        var tagProperty = tag.Attributes["property"];
                        if (tagName != null && tagContent != null)
                        {
                            switch (tagName.Value.ToLower())
                            {
                                case "title":
                                    metaInfo.Title = tagContent.Value;
                                    matchCount++;
                                    break;
                                case "description":
                                    metaInfo.Description = tagContent.Value;
                                    matchCount++;
                                    break;
                                case "twitter:title":
                                    metaInfo.Title = string.IsNullOrEmpty(metaInfo.Title) ? tagContent.Value : metaInfo.Title;
                                    matchCount++;
                                    break;
                                case "twitter:description":
                                    metaInfo.Description = string.IsNullOrEmpty(metaInfo.Description) ? tagContent.Value : metaInfo.Description;
                                    matchCount++;
                                    break;
                                case "keywords":
                                    metaInfo.Keywords = tagContent.Value;
                                    matchCount++;
                                    break;
                                case "twitter:image":
                                    metaInfo.ImageUrl = string.IsNullOrEmpty(metaInfo.ImageUrl) ? tagContent.Value : metaInfo.ImageUrl;
                                    matchCount++;
                                    break;
                            }
                        }
                        else if (tagProperty != null && tagContent != null)
                        {
                            switch (tagProperty.Value.ToLower())
                            {
                                case "og:title":
                                    metaInfo.Title = string.IsNullOrEmpty(metaInfo.Title) ? tagContent.Value : metaInfo.Title;
                                    matchCount++;
                                    break;
                                case "og:description":
                                    metaInfo.Description = string.IsNullOrEmpty(metaInfo.Description) ? tagContent.Value : metaInfo.Description;
                                    matchCount++;
                                    break;
                                case "og:image":
                                    metaInfo.ImageUrl = string.IsNullOrEmpty(metaInfo.ImageUrl) ? tagContent.Value : metaInfo.ImageUrl;
                                    matchCount++;
                                    break;
                            }
                        }
                    }
                    metaInfo.HasData = matchCount > 0;
                }
                return metaInfo;
            }
        }
    public class MetaInformation
    {
        public bool HasData { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string ImageUrl { get; set; }
        public string SiteName { get; set; }

        public MetaInformation(string url)
        {
            Url = url;
            HasData = false;
        }

        public MetaInformation(string url, string title, string description, string keywords, string imageUrl, string siteName)
        {
            Url = url;
            Title = title;
            Description = description;
            Keywords = keywords;
            ImageUrl = imageUrl;
            SiteName = siteName;
        }
    }
}
