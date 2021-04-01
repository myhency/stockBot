using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockBot
{
    public partial class Bot : Form
    {
        //NLog 이용방법 참고 : https://m.blog.naver.com/PostView.nhn?blogId=sang9151&logNo=221222810693&proxyReferer=https:%2F%2Fwww.google.com%2F
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public Bot()
        {
            InitializeComponent();
        }

        private void 로그인ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axKHOpenAPI1.OnEventConnect += axKHOpenAPI1_OnEventConnect;
            login();
        }

        private void login()
        {
            logger.Info("로그인 시도 중...");
            axKHOpenAPI1.CommConnect();
        }

        private void axKHOpenAPI1_OnEventConnect(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent e)
        {
            if (e.nErrCode == 0) //로그인 성공시
            {
                logger.Info("로그인 성공.");
                var db = connectionFactory();
                db.Open();
                logger.Info(db.Database.ToString());
            }
        }

        private IDbConnection connectionFactory()
        {
            //Dapper 이용방법 참고 : https://jacking.tistory.com/1117
            string ConnString = "server=localhost;port=3306;database=stockdb;uid=root;password=037603";

            IDbConnection db = new MySql.Data.MySqlClient.MySqlConnection(ConnString);

            return db;
        }
    }
}
