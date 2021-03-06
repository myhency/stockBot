using NLog;
using StockBot.controller;
using StockBot.KiwoomAPI;
using StockBot.view;
using System;
using System.Windows.Forms;

namespace StockBot
{
    public partial class Bot : Form, ICollectItemsView, IAccountAlarmView, ISettingsView, IChejanView
    {
        /**
         * NLog 이용방법 참고
         *  - https://m.blog.naver.com/PostView.nhn?blogId=sang9151&logNo=221222810693&proxyReferer=https:%2F%2Fwww.google.com%2F
         */
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private ConditionEventHandler conditionEventHandler;
        private CollectItemsViewController collectItemsViewController;
        private AccountEventHandler accountEventHandler;
        private AccountAlarmViewController accountAlarmViewController;
        private SettingsViewController settingsViewController;
        private ChejanViewController chejanViewController;
        private Opw00018EventHandler opw00018EventHandler;

        public Bot()
        {
            InitializeComponent();
        }

        private void 로그인ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripProgressBar.Value = 0;
            axKHOpenAPI1.OnEventConnect += axKHOpenAPI1_OnEventConnect;
            login();
        }

        private void login()
        {
            logger.Info("로그인 시도 중...");
            toolStripProgressBar.Value = 50;
            toolStripStatusLabel.Text = "로그인 시도 중...";
            axKHOpenAPI1.CommConnect();
        }

        private void axKHOpenAPI1_OnEventConnect(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent e)
        {
            if (e.nErrCode == 0)
            {
                /**
                 * 로그인 성공시:
                 *  - progressbar update
                 *  - DB를 접속해서 어제 수집한 종목들을 gridview에 추가해 준다.
                 *  - Handler 들을 초기화한다?
                 */
                toolStripProgressBar.Value = 100;
                logger.Info($"로그인 성공. {DateTime.Now}");
                toolStripStatusLabel.Text = $"로그인 성공. {DateTime.Now}";

                opw00018EventHandler = new Opw00018EventHandler(this, axKHOpenAPI1);
                conditionEventHandler = new ConditionEventHandler(this, axKHOpenAPI1);
                accountEventHandler = new AccountEventHandler(this, axKHOpenAPI1);

                collectItemsViewController = new CollectItemsViewController(this);
                accountAlarmViewController = new AccountAlarmViewController(this);
                settingsViewController = new SettingsViewController(this, this);
                chejanViewController = new ChejanViewController(this);
            }
        }

        public Button getTodayJumpItemButton()
        {
            return todayJumpItemButton;
        }

        public Button getYesterdayHighestVolumeItemButton()
        {
            return yesterdayHighestVolumeItemButton;
        }

        public ConditionEventHandler getConditionEventHandler()
        {
            return this.conditionEventHandler;
        }

        public ToolStripProgressBar getToolStripProgressBar()
        {
            return toolStripProgressBar;
        }

        public ToolStripStatusLabel getToolStripStatusLabel()
        {
            return toolStripStatusLabel;
        }

        public Button getStartAccountAlarmButton()
        {
            return startAccountAlarmButton;
        }

        public AccountEventHandler getAccountEventHandler()
        {
            return this.accountEventHandler;
        }

        public ListBox getNoMonitoringAccountListBox()
        {
            return noMonitoringAccountListBox;
        }

        public ListBox getMonitoringAccountListBox()
        {
            return monitoringAccountListBox;
        }

        public Button getAddAccountToMonitoringButton()
        {
            return addAccountToMonitoringButton;
        }

        public Button getDeleteAccountToMonitoringButton()
        {
            return deleteAccountToMonitoringButton;
        }

        public Button getMonitoringAccountSaveButton()
        {
            return monitoringAccountSaveButton;
        }

        public ComboBox getChejanAccountListcomboBox()
        {
            return chejanAccountListcomboBox;
        }

        public Button getChejanAccountExplorerButton()
        {
            return chejanAccountExplorerButton;
        }

        public Opw00018EventHandler getOpw00018EventHandler()
        {
            return opw00018EventHandler;
        }
    }
}
