using NLog;
using StockBot.KiwoomAPI;
using StockBot.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockBot.controller
{
    public partial class CollectItemsViewController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private ICollectItemsView iCollectItemView;
        private ConditionEventHandler conditionEventHandler;
        private Button todayJumpItemButton;
        private Button yesterdayHighestVolumeItemButton;
        private ToolStripProgressBar toolStripProgressBar;
        private ToolStripStatusLabel toolStripStatusLabel;

        public CollectItemsViewController(ICollectItemsView iCollectItemView)
        {
            this.iCollectItemView = iCollectItemView;

            initialize();
        }
        private void initialize()
        {
            this.conditionEventHandler = iCollectItemView.getConditionEventHandler();
            this.todayJumpItemButton = iCollectItemView.getTodayJumpItemButton();
            this.yesterdayHighestVolumeItemButton = iCollectItemView.getYesterdayHighestVolumeItemButton();
            this.toolStripProgressBar = iCollectItemView.getToolStripProgressBar();

            this.todayJumpItemButton.Click += todayJumpItemButton_Click;
            this.yesterdayHighestVolumeItemButton.Click += yesterdayHighestVolumeItemButton_Click;
        }

        private void todayJumpItemButton_Click(object sender, EventArgs e)
        {
            this.toolStripProgressBar.Value = 0;
            logger.Debug("todayJumpItemButton_Click");
            this.conditionEventHandler.searchTodayJumpItem();
            this.toolStripStatusLabel.Text = "조회완료.";
            this.toolStripProgressBar.Value = 100;
        }

        private void yesterdayHighestVolumeItemButton_Click(object sender, EventArgs e)
        {
            logger.Debug("yesterdayHighestVolumeItemButton_Click");
        }
    }
}
