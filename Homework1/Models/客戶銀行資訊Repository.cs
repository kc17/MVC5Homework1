using System;
using System.Linq;
using System.Collections.Generic;
	
namespace Homework1.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public 客戶銀行資訊 Find(int id)
        {
            return this.All().Where(p => p.Id == id).FirstOrDefault();
        }

        public override IQueryable<客戶銀行資訊> All()
        {
            //var 客戶銀行資訊 = db.客戶銀行資訊.Include(客 => 客.客戶資料).Where(s => s.是否已刪除 != true);
            return base.All().Where(p => p.是否已刪除 != true);
        }
        public IQueryable<客戶銀行資訊> Search(string searchString)
        {
            var 客戶銀行資訊 = All();
            if (!String.IsNullOrEmpty(searchString))
            {
                客戶銀行資訊 = 客戶銀行資訊.Where(s => s.帳戶名稱.Contains(searchString)
                                       || s.銀行名稱.Contains(searchString));
            }

            return 客戶銀行資訊;
        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}