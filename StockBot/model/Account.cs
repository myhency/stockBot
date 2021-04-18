using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBot.model
{
    public class Account
    {
        public string accountNumber { get; set; }
        public List<Opw00018VO> itemList { get; set; }

        public Account(string accountNumber)
        {
            this.accountNumber = accountNumber;
        }

        public class Opw00018VO
        {
            public string 종목번호 { get; set; }
            public string 종목명 { get; set; }
            public string 평가손익 { get; set; }
        }
    }
}
