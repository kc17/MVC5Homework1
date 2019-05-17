using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using Homework1.Models;
using Newtonsoft.Json;

namespace Homework1.Controllers
{
    public class 客戶資料Controller : Controller
    {
        客戶資料Repository repo;
        public 客戶資料Controller()
        {
            repo = RepositoryHelper.Get客戶資料Repository();
        }

        // GET: 客戶資料
        public ActionResult Index(string searchString, string Type, string sortOrder)
        { 
            //類別
            ViewBag.SelectList = GetType("");
            //排序
            ViewBag.客戶名稱SortParm = String.IsNullOrEmpty(sortOrder) ? "客戶名稱_desc" : "";
            ViewBag.統一編號SortParm = sortOrder == "統一編號" ? "統一編號_desc" : "統一編號";
            ViewBag.電話SortParm = sortOrder == "電話" ? "電話_desc" : "電話";
            ViewBag.傳真SortParm = sortOrder == "傳真" ? "傳真_desc" : "傳真";
            ViewBag.地址SortParm = sortOrder == "地址" ? "地址_desc" : "地址";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "Email_desc" : "Email";

            var 客戶資料 = repo.Search(searchString, Type, sortOrder); 
            return View(客戶資料.ToList());
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        { 
            //類別
            ViewBag.SelectList = GetType("");

            return View();
        }

        // POST: 客戶資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                客戶資料.是否已刪除 = false;
                repo.Add(客戶資料);
                repo.UnitOfWork.Commit(); 
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            
            //類別
            ViewBag.SelectList = GetType(客戶資料.客戶分類);

            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                客戶資料.是否已刪除 = false;
                repo.UnitOfWork.Context.Entry(客戶資料).State = EntityState.Modified;
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = repo.Find(id);
            客戶資料.是否已刪除 = true;
            repo.UnitOfWork.Context.Entry(客戶資料).State = EntityState.Modified;
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }

        protected List<SelectListItem> GetType(string selectValue)
        {
            var selectList = new List<SelectListItem>()
            {
                new SelectListItem {Text="請選擇", Value="" },
                new SelectListItem {Text="分類A", Value="分類A" },
                new SelectListItem {Text="分類B", Value="分類B" },
                new SelectListItem {Text="分類C", Value="分類C" },
            };

            //預設選擇哪一筆
            if (selectList.Where(q => q.Value == selectValue).FirstOrDefault() != null)
                selectList.Where(q => q.Value == selectValue).First().Selected = true;

            return selectList;
        }

        public ActionResult Export()
        { 
            using (XLWorkbook wb = new XLWorkbook())
            { 
                var data = repo.All().Select(c => new { c.客戶名稱, c.統一編號, c.電話, c.傳真, c.地址, c.Email, c.客戶分類 });
                 
                var ws = wb.Worksheets.Add("sheet1", 1); 
                ws.Cell(1, 1).InsertData(data);
                 
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    wb.SaveAs(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    return this.File(memoryStream.ToArray(), "application/vnd.ms-excel", "客戶資料.xlsx");
                }
            }  
         }
    }
}
