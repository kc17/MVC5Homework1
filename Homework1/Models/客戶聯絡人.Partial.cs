namespace Homework1.Models
{
    using Homework1.DataTypeAttributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
        private Entities db = new Entities();
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        { 
            var 客戶聯絡人 = db.客戶聯絡人.Where(s => s.是否已刪除 != true && s.客戶Id == this.客戶Id && s.Email == this.Email && s.Id != this.Id).ToList();
            if (客戶聯絡人.Count > 0)
            {
                yield return new ValidationResult("同一個客戶下的聯絡人，其 Email 不能重複。", new string[] { "客戶Id", "Email" });
            } 
        }
    }
    
    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "請輸入正確的電子信箱")]
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        [Required]
        public string Email { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [手機格式]
        public string 手機 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
