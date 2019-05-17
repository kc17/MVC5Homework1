using System;
using System.Linq;
using System.Collections.Generic;
	
namespace Homework1.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public 客戶資料 Find(int id)
        {
            return this.All().Where(p => p.Id == id).FirstOrDefault();
        }

        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(p => p.是否已刪除 != true);
        }
        public IQueryable<客戶資料> Search(string searchString, string Type, string sortOrder)
        {
            var 客戶資料 = All();
            if (!String.IsNullOrEmpty(searchString))
            {
                客戶資料 = 客戶資料.Where(s => s.客戶名稱.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(Type))
            {
                客戶資料 = 客戶資料.Where(s => s.客戶分類 == Type);
            }
              
            switch (sortOrder)
            {
                case "客戶名稱_desc":
                    客戶資料 = 客戶資料.OrderByDescending(s => s.客戶名稱);
                    break;
                case "客戶名稱":
                    客戶資料 = 客戶資料.OrderBy(s => s.客戶名稱);
                    break;
                case "統一編號_desc":
                    客戶資料 = 客戶資料.OrderByDescending(s => s.統一編號);
                    break;
                case "統一編號":
                    客戶資料 = 客戶資料.OrderBy(s => s.統一編號);
                    break;
                case "電話_desc":
                    客戶資料 = 客戶資料.OrderByDescending(s => s.電話);
                    break;
                case "電話":
                    客戶資料 = 客戶資料.OrderBy(s => s.電話);
                    break;
                case "傳真_desc":
                    客戶資料 = 客戶資料.OrderByDescending(s => s.傳真);
                    break;
                case "傳真":
                    客戶資料 = 客戶資料.OrderBy(s => s.傳真);
                    break;
                case "地址_desc":
                    客戶資料 = 客戶資料.OrderByDescending(s => s.地址);
                    break;
                case "地址":
                    客戶資料 = 客戶資料.OrderBy(s => s.地址);
                    break;
                case "Email_desc":
                    客戶資料 = 客戶資料.OrderByDescending(s => s.Email);
                    break;
                case "Email":
                    客戶資料 = 客戶資料.OrderBy(s => s.Email);
                    break;
                default: 
                    break;
            }

            return 客戶資料;
        }

    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}