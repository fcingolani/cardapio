using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using uComponents.DataTypes.UrlPicker.Dto;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace Cardapio.Models
{
    public class UmbracoContent
    {
        static public MenuItem GetInheritedMenu(string name, IPublishedContent content)
        {
            return MenuItem.Create(GetInheritedMenuNode(name, content));
        }

        static public IPublishedContent GetInheritedMenuNode(string name, IPublishedContent content)
        {
            var nodeWithMenu = content
                .AncestorsOrSelf()
                .Where(node => NodeHasChildMenuNamed(node, name))
                .FirstOrDefault();

            if (nodeWithMenu == null)
                return null;

            var menuNode = nodeWithMenu
                .Descendants(MenuItem.DocumentTypeAlias)
                .Where(node => node.Name.Equals(name))
                .FirstOrDefault();

            return menuNode != null ? menuNode : null;
        }

        static protected Boolean NodeHasChildMenuNamed(IPublishedContent node, string name)
        {
            return node.Children != null && node.Children.Any(child => NodeIsMenuNamed(child, name));
        }

        static protected Boolean NodeIsMenuNamed(IPublishedContent node, string name)
        {
            return node.DocumentTypeAlias.Equals(MenuItem.DocumentTypeAlias) && node.Name.ToLower().Equals(name.ToLower());
        }
    }
}