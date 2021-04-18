using Newtonsoft.Json.Linq;
using NLog;
using StockBot.KiwoomAPI;
using StockBot.view;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockBot.controller
{
    public partial class ChejanViewController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IChejanView iChejanView;
        private ComboBox chejanAccountListComboBox;
        private Button chejanAccountExplorerButton;
        private Opw00018EventHandler opw00018EventHandler;

        public ChejanViewController(IChejanView iChejanView)
        {
            this.iChejanView = iChejanView;
            this.opw00018EventHandler = iChejanView.getOpw00018EventHandler();

            initialize();
        }

        private void initialize() 
        {
            logger.Info("ChejanViewController");

            this.chejanAccountListComboBox = iChejanView.getChejanAccountListcomboBox();
            this.chejanAccountExplorerButton = iChejanView.getChejanAccountExplorerButton();

            this.chejanAccountExplorerButton.Click += chejanAccountExplorerButton_Click;

            try
            {
                StreamReader r = new StreamReader(Application.StartupPath + "\\accounts.json");
                string json = r.ReadToEnd();
                r.Close();

                var readJson = JObject.Parse(json.ToString());
                if (readJson.ContainsKey("accountList"))
                {
                    var jArr = JArray.Parse(readJson["accountList"].ToString());
                    foreach (var i in jArr)
                    {
                        this.chejanAccountListComboBox.Items.Add(i.ToString().Replace("\"", ""));
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception exception)
            {
                logger.Error(exception.Message.ToString());
            }
        }

        void chejanAccountExplorerButton_Click(object sender, EventArgs e)
        {
            logger.Debug("chejanAccountExplorerButton_Click");
            string accountNum = chejanAccountListComboBox.SelectedItem.ToString();
            string password = "";
            this.opw00018EventHandler.requestTrOpw00018(accountNum,password);
        }
    }
}
