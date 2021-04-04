using System;
using System.Data;
using MySql.Data.MySqlClient;
using NLog;
using StockBot.model;

namespace StockBot.utils
{
    public partial class MariaDB
    {
        //private IDbConnection db;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static string mysqlConnStr = "Server=localhost;Database=stockdb;Uid=root;Pwd=037603;Charset=utf8";
        private MySqlConnection conn = new MySqlConnection(mysqlConnStr);

        public MariaDB()
        {
            //this.db = connectionFactory();
            try
            {
                conn.Open();
            }
            catch (Exception exception)
            {
                logger.Error(exception.Message.ToString());
            }
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

        //public IDbConnection getOpenConnection()
        //{
        //    return this.db;
        //}

        public void saveItemInfo(Opt10001VO opt10001VO, string conditionName)
        {
            string query = "INSERT INTO stockdb.searched_items" +
                                "(" +
                                "condition_name," +
                                "item_code," +
                                "item_name," +
                                "settlement_month," +
                                "face_value," +
                                "capital," +
                                "num_of_listed_shares," +
                                "credit_ratio," +
                                "highest_year_round," +
                                "lowest_year_round," +
                                "market_capitalization," +
                                "external_burnout_rate," +
                                "substitute_stock," +
                                "per," +
                                "eps," +
                                "roe," +
                                "pbr," +
                                "bps," +
                                "take," +
                                "operating_profit," +
                                "net_income," +
                                "highest_250," +
                                "lowest_250," +
                                "staring_price," +
                                "highest_price," +
                                "lowest_price," +
                                "upper_limit," +
                                "lower_limit," +
                                "reference_price," +
                                "highest_250_at," +
                                "highest_250_ratio," +
                                "lowest_250_at," +
                                "lowest_250_ratio," +
                                "present_price," +
                                "price_state_flag," +
                                "price_by_prev_day," +
                                "fluctuation_rate," +
                                "volume," +
                                "volume_by_prev_day," +
                                "stocks_in_circulation," +
                                "circulation_ratio," +
                                "created_at)" +
                                " VALUES (" +
                                $"'{conditionName}'," +
                                $"'{opt10001VO.종목코드}'," +
                                $"'{opt10001VO.종목명}'," +
                                $"'{opt10001VO.결산월}'," +
                                $"{opt10001VO.액면가}," +
                                $"{opt10001VO.자본금}," +
                                $"{opt10001VO.상장주식}," +
                                $"{opt10001VO.신용비율}," +
                                $"{opt10001VO.연중최고}," +
                                $"{opt10001VO.연중최저}," +
                                $"{opt10001VO.시가총액}," +
                                $"{opt10001VO.외인소진률}," +
                                $"{opt10001VO.대용가}," +
                                $"{opt10001VO.PER}," +
                                $"{opt10001VO.EPS}," +
                                $"{opt10001VO.ROE}," +
                                $"{opt10001VO.PBR}," +
                                $"{opt10001VO.BPS}," +
                                $"{opt10001VO.매출액}," +
                                $"{opt10001VO.영업이익}," +
                                $"{opt10001VO.당기순이익}," +
                                $"{opt10001VO.최고250}," +
                                $"{opt10001VO.최저250}," +
                                $"{opt10001VO.시가}," +
                                $"{opt10001VO.고가}," +
                                $"{opt10001VO.저가}," +
                                $"{opt10001VO.상한가}," +
                                $"{opt10001VO.하한가}," +
                                $"{opt10001VO.기준가}," +
                                $"STR_TO_DATE('{opt10001VO.최고가일250.ToString("yyyyMMdd")}','%Y%m%d')," +
                                $"{opt10001VO.최고가대비율250}," +
                                $"STR_TO_DATE('{opt10001VO.최저가일250.ToString("yyyyMMdd")}','%Y%m%d')," +
                                $"{opt10001VO.최저가대비율250}," +
                                $"{opt10001VO.현재가}," +
                                $"{opt10001VO.대비기호}," +
                                $"{opt10001VO.전일대비}," +
                                $"{opt10001VO.등락율}," +
                                $"{opt10001VO.거래량}," +
                                $"{opt10001VO.거래대비}," +
                                $"{opt10001VO.유통주식}," +
                                $"{opt10001VO.유통비율}," +
                                $"STR_TO_DATE('{DateTime.Now.ToString("yyyyMMdd")}','%Y%m%d')" +
                                ")";

            logger.Debug(query);

            MySqlCommand cmd = new MySqlCommand(query, conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message.ToString());
            }
            finally
            {
                cmd.Dispose();
            }
        }
    }
}
