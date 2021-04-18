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
using Newtonsoft.Json;

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
        private Button monitoringAccountSaveButton;
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
            this.monitoringAccountSaveButton = iSettingsView.getMonitoringAccountSaveButton();
            this.addAccountToMonitoringButton.Click += addAccountToMonitoringButton_Click;
            this.deleteAccountToMonitoringButton.Click += deleteAccountToMonitoringButton_Click;
            this.monitoringAccountSaveButton.Click += monitoringAccountSaveButton_Click;

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
                 * 그리고 계좌리스트를 noMonitoringAccountListBox에 로드한다.
                 */
                FileStream f = File.Create(Application.StartupPath + "\\accounts.json");
                logger.Info("File Created!! " + Application.StartupPath + "\\accounts.json");
                f.Close();
                loadAccountListToNoMonitoringAccountListBox();
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
                    if(readJson.ContainsKey("accountList"))
                    {
                        //COMPLETE. accountList가 있다면
                        //accounts 에서 account.json 에 있는 계좌를 빼서 noMonitoringAccountListBox 에 표현
                        var jArr = JArray.Parse(readJson["accountList"].ToString());
                        foreach(var i in jArr)
                        {
                            monitoringAccountListBox.Items.Add(i.ToString().Replace("\"",""));
                        }

                        foreach (var j in accounts)
                        {
                            //accounts 에 있지만 account.json에 없는 계좌는 noMonitoringAccountListBox 에 표현
                            if (!monitoringAccountListBox.Items.Contains(j.accountNumber))
                                noMonitoringAccountListBox.Items.Add(j.accountNumber);
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
                    loadAccountListToNoMonitoringAccountListBox();
                }
            }
        }

        private void loadAccountListToNoMonitoringAccountListBox()
        {
            foreach (Account account in accounts)
            {
                noMonitoringAccountListBox.Items.Add(account.accountNumber);
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

        private void monitoringAccountSaveButton_Click(object sender, EventArgs e)
        {
            /**
             * monitoringAccountSaveButton 눌렀을 때 실행과정
             *  - 만약에 감시대상에 아무 계좌도 없으면 알람을 띄운다.
             *  - 감시대상이 있다면 json 에 저장한다.
             */
            if(monitoringAccountListBox.Items.Count < 1)
            {
                logger.Info("모니터링 대상 계좌를 선택하지 않았습니다.");
            }
            else
            {
                //COMPLETE. json 저장로직 구현해야함.
                // - 우선 accounts.json 을 읽어서 리스트로 저장해 둔다.
                // - 그리고 monitoringAccountListBox 에 있는 계좌를 가져와서
                //   json에 있으면 pass 없으면 write 한다.
                JArray array = new JArray();
                foreach(string account in monitoringAccountListBox.Items)
                {
                    string json = JsonConvert.SerializeObject(account);
                    array.Add(json);
                }

                JObject o = new JObject();
                o["accountList"] = array;

                try
                {
                    File.WriteAllText(Application.StartupPath + "\\accounts.json", o.ToString());
                    MessageBox.Show("모니터링 대상 계좌로 저장되었습니다.");
                }
                catch (Exception exception)
                {
                    logger.Error(exception.Message.ToString());
                }
            }
        }

        public List<String> getMonitoringAccountList()
        {
            List<String> result = new List<String>();
            foreach (string item in monitoringAccountListBox.Items)
            {
                result.Add(item);
            }

            return result;
        }
    }
}
