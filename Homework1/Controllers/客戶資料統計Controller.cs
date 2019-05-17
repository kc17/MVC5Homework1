using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Homework1.Models;

namespace Homework1.Controllers
{
    public class 客戶資料統計Controller : Controller
    {
        客戶資料統計Repository repo;
        public 客戶資料統計Controller()
        {
            repo = RepositoryHelper.Get客戶資料統計Repository();
        }

        // GET: 客戶資料統計
        public ActionResult Index()
        {
            return View(repo.All().ToList());
        }

        // GET: 客戶資料統計
        public ActionResult GetJson()
        {
            return Json(repo.All().ToList(), JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
