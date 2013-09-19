using Cardapio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cardapio.Helpers
{
    public static class Html
    {
        public static HtmlString Menu(MenuItem item)
        {
            TagBuilder ul = new TagBuilder("ul");

            foreach (var child in item.Children)
                ul.InnerHtml += MenuItem(child).ToHtmlString();

            return new HtmlString(ul.ToString());
        }

        public static HtmlString MenuItem(MenuItem item)
        {
            TagBuilder li = new TagBuilder("li");

            li.InnerHtml = MenuItemLink(item).ToHtmlString();

            return new HtmlString(li.ToString());
        }

        public static HtmlString MenuItemLink(MenuItem item)
        {
            TagBuilder a = new TagBuilder("a");
            
            a.InnerHtml = item.Name;

            a.MergeAttribute("href", item.Url);

            if (item.OpensInNewWindow)
                a.MergeAttribute("target", "_blank");

            if (item.LinksToNode)
                a.MergeAttribute("data-linked-node-id", item.LinkedNodeId.ToString());

            if (item.HasSubMenu)
                a.InnerHtml += Menu(item).ToHtmlString();

            return new HtmlString(a.ToString());
        }
    }
}