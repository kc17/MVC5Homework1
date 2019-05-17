using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Homework1.DataTypeAttributes
{
    public class 手機格式Attribute : DataTypeAttribute
    {
        public 手機格式Attribute() : base(DataType.Text)
        { 
            ErrorMessage = "手機的電話格式錯誤( e.g. 0911-111111 )";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            string str = (string)value;

            Regex regex1 = new Regex(@"\d{4}-\d{6}");
            if (!regex1.IsMatch(str))
                return false;

            return true;
        }
    }
}