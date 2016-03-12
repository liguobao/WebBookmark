using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebfolderUI.Controllers
{
    public class WebfolderTableController : Controller
    {
        // GET: WebfolderTable
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ImportWebfolder()
        {
            return View();
        }
    }
}