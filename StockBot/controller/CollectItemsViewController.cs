﻿using NLog;
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
        private Button todayJumpItemButton;
        private Button yesterdayHighestVolumeItemButton;

        public CollectItemsViewController(ICollectItemsView iCollectItemView)
        {
            this.iCollectItemView = iCollectItemView;

            initialize();
        }
        private void initialize()
        {
            this.todayJumpItemButton = iCollectItemView.getTodayJumpItemButton();
            this.yesterdayHighestVolumeItemButton = iCollectItemView.getYesterdayHighestVolumeItemButton();

            this.todayJumpItemButton.Click += todayJumpItemButton_Click;
            this.yesterdayHighestVolumeItemButton.Click += yesterdayHighestVolumeItemButton_Click;
        }

        private void todayJumpItemButton_Click(object sender, EventArgs e)
        {
            logger.Debug("todayJumpItemButton_Click");
        }

        private void yesterdayHighestVolumeItemButton_Click(object sender, EventArgs e)
        {
            logger.Debug("yesterdayHighestVolumeItemButton_Click");
        }
    }
}
