using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using uComponents.DataTypes.UrlPicker.Dto;
using Umbraco.Core.Models;

namespace Cardapio.Models
{
    public class MenuItem
    {
        public static string DocumentTypeAlias = "CardapioMenuItem";

        public String Name { get; set; }
        public String Url { get; set; }
        public Boolean OpensInNewWindow { get; set; }
        public int? LinkedNodeId { get; set; }
        public List<MenuItem> Children { get; set; }

        public Boolean HasSubMenu
        {
            get { return Children.Count > 0; }
        }

        public Boolean LinksToNode
        {
            get { return LinkedNodeId != null; }
        }

        public MenuItem()
        {
            Name = "";
            Url = "";
            OpensInNewWindow = false;
            Children = new List<MenuItem>();
        }

        public static MenuItem Create(IPublishedContent node)
        {
            if (node == null)
                return null;

            MenuItem item = new MenuItem();
            UrlPickerState urlData = UrlPickerState.Deserialize(node["Url"].ToString());

            item.Name = node.Name;

            if (urlData != null)
            {
                item.Url = urlData.Url;
                item.OpensInNewWindow = urlData.NewWindow;
                item.LinkedNodeId = urlData.NodeId;
            }

            foreach (var child in node.Children)
                item.Children.Add(Create(child));

            return item;
        }
    }

}