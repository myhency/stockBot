using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockBot.view;
using System.Windows.Forms;
using StockBot.KiwoomAPI;
using StockBot.model;
using System.IO;
using Newtonsoft.Json.Linq;

namespace StockBot.controller
{
    public partial class SettingsViewController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private ISettingsView iSettingsView;
        private IAccountAlarmView iAccountAlarmView;
        private ListBox noMonitoringAccountListBox;
        private ListBox monitoringAccountListBox;
        private Button addAccountToMonitoringButton;
        private Button deleteAccountToMonitoringButton;
        private AccountEventHandler accountEventHandler;
        private List<Account> accounts;

        public SettingsViewController(ISettingsView iSettingsView, IAccountAlarmView iAccountAlarmView)
        {
            this.iSettingsView = iSettingsView;
            this.iAccountAlarmView = iAccountAlarmView;

            initialize();
        }

        private void initialize()
        {
            this.accountEventHandler = iAccountAlarmView.getAccountEventHandler();
            this.noMonitoringAccountListBox = iSettingsView.getNoMonitoringAccountListBox();
            this.monitoringAccountListBox = iSettingsView.getMonitoringAccountListBox();
            this.addAccountToMonitoringButton = iSettingsView.getAddAccountToMonitoringButton();
            this.deleteAccountToMonitoringButton = iSettingsView.getDeleteAccountToMonitoringButton();
            this.addAccountToMonitoringButton.Click += addAccountToMonitoringButton_Click;
            this.deleteAccountToMonitoringButton.Click += deleteAccountToMonitoringButton_Click;

            /**
             * ListBox 가 초기화 되는 과정
             *  - 우선 GetLoginInfo("ACCLIST"); 로 가져온다
             *  - 기존에 감시계좌로 설정된 계좌가 있는지 json 파일을 뒤져서 가져온다.
             *  - 있으면 monitoringAccountListBox 에 표현한다.
             *  - 없으면 가져온 계좌를 noMonitoringAccountListBox 에 표현한다.
             */

            accounts = this.accountEventHandler.getAccountList();

            if (!File.Exists(Application.StartupPath + "\\accounts.json"))
            {
                /**
                 * account.json 파일이 없을 때는 파일을 만들어준다. 다만 여기서는 그냥 빈 파일만 생성한다.
                 */
                FileStream f = File.Create(Application.StartupPath + "\\accounts.json");
                logger.Info("File Created!! " + Application.StartupPath + "\\accounts.json");
                f.Close();
            }
            else
            {
                /**
                 * account.json 파일이 있다면 accountList key 가 있는지 확인한다.
                 * 있다면 accounts 에서 account.json 에 있는 계좌를 빼서 noMonitoringAccountListBox 에 표현
                 * 없다면 accounts 를 monitoringAccountListBox 에 표현 (catch 절 안에서 하면 됨)
                 */
                logger.Info("File Exists!! " + Application.StartupPath + "\\accounts.json");
                try
                {
                    StreamReader r = new StreamReader(Application.StartupPath + "\\accounts.json");
                    string json = r.ReadToEnd();
                    r.Close();

                    var readJson = JObject.Parse(json.ToString());
                    var jArr = JArray.Parse(readJson["accountList"].ToString());
                }
                catch (Exception exception)
                {
                    logger.Error(exception.Message.ToString());
                    foreach(Account account in accounts)
                    {
                        noMonitoringAccountListBox.Items.Add(account.accountNumber);
                    }
                }
            }
        }

        private void addAccountToMonitoringButton_Click(object sender, EventArgs e)
        {
            /**
             * addAccountToMonitoringButton 눌렀을 때 실행과정
             *  - 만약에 사용자가 noMonitoringAccountListBox 에서 아무것도 선택을 안했을 때 알람을 띄운다.
             *  - 선택된 아이템이 있으면 monitoringAccountListBox 로 추가해준다.
             */
            if(noMonitoringAccountListBox.SelectedIndex == -1)
            {
                logger.Info("계좌를 선택하지 않았습니다.");
            } 
            else
            {
                logger.Info($"{noMonitoringAccountListBox.SelectedItem} 계좌를 감시합니다.");
                monitoringAccountListBox.Items.Add(noMonitoringAccountListBox.SelectedItem);
                noMonitoringAccountListBox.Items.Remove(noMonitoringAccountListBox.SelectedItem);
            }
        }

        private void deleteAccountToMonitoringButton_Click(object sender, EventArgs e)
        {
            /**
             * addAccountToMonitoringButton 눌렀을 때 실행과정
             *  - 만약에 사용자가 noMonitoringAccountListBox 에서 아무것도 선택을 안했을 때 알람을 띄운다.
             *  - 선택된 아이템이 있으면 monitoringAccountListBox 로 추가해준다.
             */
            if (monitoringAccountListBox.SelectedIndex == -1)
            {
                logger.Info("계좌를 선택하지 않았습니다.");
            }
            else
            {
                logger.Info($"{monitoringAccountListBox.SelectedItem} 계좌를 감시해제 합니다.");
                noMonitoringAccountListBox.Items.Add(monitoringAccountListBox.SelectedItem);
                monitoringAccountListBox.Items.Remove(monitoringAccountListBox.SelectedItem);
            }
        }
    }
}
