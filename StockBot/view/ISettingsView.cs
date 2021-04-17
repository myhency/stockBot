using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockBot.view
{
    public interface ISettingsView
    {
        ListBox getNoMonitoringAccountListBox();
        ListBox getMonitoringAccountListBox();
        Button getAddAccountToMonitoringButton();
        Button getDeleteAccountToMonitoringButton();
        Button getMonitoringAccountSaveButton();
    }
}
