using NLog;
using StockBot.KiwoomAPI;
using StockBot.view;
using System;
using System.Windows.Forms;

namespace StockBot.controller
{
    public partial class AccountAlarmViewController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IAccountAlarmView iAccountAlarmView;
        private Button startAccountAlarmButton;
        private AccountEventHandler accountEventHandler;

        public AccountAlarmViewController(IAccountAlarmView iAccountAlarmView)
        {
            this.iAccountAlarmView = iAccountAlarmView;

            initialize();
        }

        private void initialize()
        {
            this.startAccountAlarmButton = iAccountAlarmView.getStartAccountAlarmButton();
            this.accountEventHandler = iAccountAlarmView.getAccountEventHandler();

            this.startAccountAlarmButton.Click += startAccountAlarmButton_Click;
        }

        private void startAccountAlarmButton_Click(object sender, EventArgs e)
        {
            logger.Debug("startAccountAlarmButton_Click");
            this.accountEventHandler.getAccountList();
        }
    }
}
