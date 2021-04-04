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

        public Account(string accountNumber)
        {
            this.accountNumber = accountNumber;
        }
    }
}
