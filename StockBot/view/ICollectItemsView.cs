using StockBot.KiwoomAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockBot.view
{
    public interface ICollectItemsView
    {
        Button getTodayJumpItemButton();
        Button getYesterdayHighestVolumeItemButton();
        ConditionEventHandler getConditionEventHandler();
    }
}
