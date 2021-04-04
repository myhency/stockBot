using NLog;
using StockBot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBot.utils
{
    public class GoogleSheet
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private int rangeRowStart = 2;
        private string sheetName = "관심종목";
        public void updateCodeListToGoogleSheet(Opt10001VO opt10001VO, string conditionName)
        {
            var gsh = new GoogleSheetsHelper("swing-293507-ca9c2651b2d1.json", "13I2pgJbXZjuqX8EbI3WnNCCuK8j3HpFNbqyAQypEYYg");
            var row = new GoogleSheetRow();
            var cells = new List<GoogleSheetCell>();

            cells.Add(new GoogleSheetCell() { CellValue = conditionName });
            cells.Add(new GoogleSheetCell() { CellValue = DateTime.Now.ToString() });
            cells.Add(new GoogleSheetCell() { CellValue = opt10001VO.종목명 });
            cells.Add(new GoogleSheetCell() { CellValue = opt10001VO.종목코드 });
            cells.Add(new GoogleSheetCell() { CellValue = opt10001VO.시가총액.ToString() });
            cells.Add(new GoogleSheetCell() { CellValue = opt10001VO.유통주식.ToString() });
            cells.Add(new GoogleSheetCell() { CellValue = opt10001VO.시가.ToString() });
            cells.Add(new GoogleSheetCell() { CellValue = opt10001VO.고가.ToString() });
            cells.Add(new GoogleSheetCell() { CellValue = opt10001VO.저가.ToString() });
            cells.Add(new GoogleSheetCell() { CellValue = opt10001VO.현재가.ToString() });
            cells.Add(new GoogleSheetCell() { CellValue = opt10001VO.등락율.ToString() });
            cells.Add(new GoogleSheetCell() { CellValue = opt10001VO.거래량.ToString() });

            row.Cells.AddRange(cells);
            var rows = new List<GoogleSheetRow>() { row };
            gsh.AddCells(new GoogleSheetParameters() { SheetName = sheetName, RangeColumnStart = 1, RangeRowStart = rangeRowStart }, rows);
            rangeRowStart++;
            logger.Info($"{opt10001VO.종목명} 업데이트 완료 ({rangeRowStart})");
        }
    }
}
