using Cardapio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace Cardapio.Controllers.SurfaceControllers
{
    public class CardapioSurfaceController : SurfaceController
    {
        [ChildActionOnly]
        public ActionResult Menu(string name, IPublishedContent content)
        {
            return PartialView("Menu", UmbracoContent.GetInheritedMenu(name, content));
        }
    }
}