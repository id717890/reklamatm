using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;


namespace Reklama.Utils
{
    public static class HtmlUtils
    {
        public static string RemoveUnwantedTags(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }
            var document = new HtmlDocument();
            document.LoadHtml(data);

            var nodes = new Queue<HtmlNode>(document.DocumentNode.SelectNodes("./*|./text()"));
            while (nodes.Count > 0)
            {
                var node = nodes.Dequeue();
                var parentNode = node.ParentNode;

                if (node.Name != "strong" && node.Name != "div" && node.Name != "p" && node.Name != "#text" && node.Name!="b")
                {

                    var childNodes = node.SelectNodes("./*|./text()");

                    if (childNodes != null)
                    {
                        foreach (var child in childNodes)
                        {
                            nodes.Enqueue(child);
                            parentNode.InsertBefore(child, node);
                        }
                    }

                    parentNode.RemoveChild(node);

                }
            }

            return document.DocumentNode.InnerHtml;
        }
    }
}