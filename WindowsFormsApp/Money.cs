using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xNet;

namespace WindowsFormsApp
{
  public  class Money
  {
        List<Money> money = new List<Money>();

        public Money(int code, string code_word, string unit, string currency, float value)
        {
            this.code = code;
            this.code_word = code_word;
            this.unit = unit;
            this.currency = currency;
            this.value = value;
        }
        public int code { get; set; }
        public string code_word { get; set; }
        public string unit { get; set; }
        public string currency { get; set; }
        public float value { get; set; }
       
    }
   
}
