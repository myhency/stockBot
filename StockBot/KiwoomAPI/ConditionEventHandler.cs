using AxKHOpenAPILib;
using NLog;
using StockBot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBot.KiwoomAPI
{
    public partial class ConditionEventHandler
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private AxKHOpenAPILib.AxKHOpenAPI axKHOpenAPI1;
        private List<Condition> conditionList;

        public ConditionEventHandler(AxKHOpenAPI axKHOpenAPI1)
        {
            this.axKHOpenAPI1 = axKHOpenAPI1;
        }
    }
}
