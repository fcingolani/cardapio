using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using uComponents.DataTypes.UrlPicker.Dto;
using umbraco.cms.businesslogic.packager;
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

        /**
         * @TODO Find this a better place
         */

        public static int uComponentsMajorVersionNumber()
        {
            InstalledPackage uComponents = InstalledPackage.GetAllInstalledPackages().First(p => p.Data.Name.Equals("uComponents"));
            return int.Parse(uComponents.Data.Version.Split('.').First());
        }

        public static MenuItem Create(IPublishedContent node)
        {
            if (node == null)
                return null;

            MenuItem item = new MenuItem();
            
            int uVersion = uComponentsMajorVersionNumber();

            UrlPickerState urlData = uVersion < 6
                                   ? UrlPickerState.Deserialize(node["Url"].ToString())
                                   : node["Url"] as UrlPickerState;

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