using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBot.utils
{
    public partial class MariaDB
    {
        private IDbConnection db;

        public MariaDB()
        {
            this.db = connectionFactory();
        }

        private IDbConnection connectionFactory()
        {
            /**
             * Dapper 이용방법 참고
             *  - https://jacking.tistory.com/1117
             *  - https://github.com/StackExchange/Dapper
             */

            string ConnString = "server=localhost;port=3306;database=stockdb;uid=root;password=037603";

            IDbConnection db = new MySql.Data.MySqlClient.MySqlConnection(ConnString);

            db.Open();

            return db;
        }

        public IDbConnection getOpenConnection()
        {
            return this.db;
        }
    }
}
