using AxKHOpenAPILib;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBot.KiwoomAPI
{
    public partial class Opw00018EventHandler
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private AxKHOpenAPILib.AxKHOpenAPI axKHOpenAPI1;
        private int screenNumber = 1018;

        public Opw00018EventHandler(object sender, AxKHOpenAPI axKHOpenAPI1)
        {
            this.axKHOpenAPI1 = axKHOpenAPI1;
            this.axKHOpenAPI1.OnReceiveTrData += axKHOpenAPI1_OnReceiveTrData;
        }

        public void requestTrOpw00018(string accountNum, string password, int sPrevNext=0)
        {
            this.axKHOpenAPI1.SetInputValue("계좌번호", accountNum);
            this.axKHOpenAPI1.SetInputValue("비밀번호", password);
            this.axKHOpenAPI1.SetInputValue("비밀번호입력매체구분", "00");
            this.axKHOpenAPI1.SetInputValue("조회구분", "1");
            int x = this.axKHOpenAPI1.CommRqData("계좌평가잔고내역", "opw00018", sPrevNext, screenNumber.ToString());
            logger.Debug($"requestTrOpw00018 result : {x}");
            
        }

        private void axKHOpenAPI1_OnReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            logger.Debug("axKHOpenAPI1_OnReceiveTrData");

            int repCnt = this.axKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName);



            if (e.sRQName.Contains("계좌평가잔고내역"))
            {
                //TODO. repCnt 로 루프를 돌려서 전체 종목을 받아오기
                string 종목번호 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "종목번호").Trim();
                logger.Debug(종목번호);
            }
        }
    }
}
