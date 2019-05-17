using System;
using System.Linq;
using System.Collections.Generic;
	
namespace Homework1.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public 客戶聯絡人 Find(int id)
        {
            return this.All().Where(p => p.Id == id).FirstOrDefault();
        }

        public override IQueryable< 客戶聯絡人> All()
        { 
            return base.All().Where(p => p.是否已刪除 != true);
        }
        public IQueryable<客戶聯絡人> Search(string searchString, string JobTitle, string sortOrder)
        {
            var 客戶聯絡人 = All();
            if (!String.IsNullOrEmpty(searchString))
            {
                客戶聯絡人 = 客戶聯絡人.Where(s => s.姓名.Contains(searchString)
                                       || s.Email.Contains(searchString)
                                       || s.手機.Contains(searchString)
                                       || s.電話.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(JobTitle))
            {
                客戶聯絡人 = 客戶聯絡人.Where(s => s.職稱.Contains(JobTitle));
            }

            switch (sortOrder)
            {
                case "職稱_desc":
                    客戶聯絡人 = 客戶聯絡人.OrderByDescending(s => s.職稱);
                    break;
                case "職稱":
                    客戶聯絡人 = 客戶聯絡人.OrderBy(s => s.職稱);
                    break;
                case "姓名_desc":
                    客戶聯絡人 = 客戶聯絡人.OrderByDescending(s => s.姓名);
                    break;
                case "姓名":
                    客戶聯絡人 = 客戶聯絡人.OrderBy(s => s.姓名);
                    break;
                case "Email_desc":
                    客戶聯絡人 = 客戶聯絡人.OrderByDescending(s => s.Email);
                    break;
                case "Email":
                    客戶聯絡人 = 客戶聯絡人.OrderBy(s => s.Email);
                    break;
                case "手機_desc":
                    客戶聯絡人 = 客戶聯絡人.OrderByDescending(s => s.手機);
                    break;
                case "手機":
                    客戶聯絡人 = 客戶聯絡人.OrderBy(s => s.手機);
                    break;
                case "電話_desc":
                    客戶聯絡人 = 客戶聯絡人.OrderByDescending(s => s.電話);
                    break;
                case "電話":
                    客戶聯絡人 = 客戶聯絡人.OrderBy(s => s.電話);
                    break;
                case "客戶名稱_desc":
                    客戶聯絡人 = 客戶聯絡人.OrderByDescending(s => s.客戶資料.客戶名稱);
                    break;
                case "客戶名稱":
                    客戶聯絡人 = 客戶聯絡人.OrderBy(s => s.客戶資料.客戶名稱);
                    break;
                default:
                    break;
            }

            return 客戶聯絡人;
        }
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}