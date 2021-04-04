using AxKHOpenAPILib;
using NLog;
using StockBot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBot.KiwoomAPI
{
    public partial class AccountEventHandler
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private AxKHOpenAPILib.AxKHOpenAPI axKHOpenAPI1;
        private List<Account> accounts;

        public AccountEventHandler(object sender, AxKHOpenAPI axKHOpenAPI1)
        {
            this.axKHOpenAPI1 = axKHOpenAPI1;
        }

        public List<Account> getAccountList()
        {
            this.accounts = new List<Account>();
            string[] accountsArr = axKHOpenAPI1.GetLoginInfo("ACCLIST").TrimEnd(';').Split(';');

            foreach(string account in accountsArr)
            {
                this.accounts.Add(new Account(account));
            }

            return this.accounts;
        }
    }
}
