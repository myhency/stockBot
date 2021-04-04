using StockBot.KiwoomAPI;
using System.Windows.Forms;

namespace StockBot.view
{
    public interface ICollectItemsView
    {
        Button getTodayJumpItemButton();
        Button getYesterdayHighestVolumeItemButton();
        ConditionEventHandler getConditionEventHandler();
        ToolStripProgressBar getToolStripProgressBar();
        ToolStripStatusLabel getToolStripStatusLabel();
    }
}
