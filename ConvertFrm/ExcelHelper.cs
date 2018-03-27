using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConvertFrm
{
    public class ExcelHelper
    {
        public static IDictionary<string, string> ReadExcelToDataTable(string path)
        {
            IDictionary<string, string> dic = new Dictionary<string, string>();
            using (FileStream fs = File.OpenRead(path))
            {
                IWorkbook workbook = GetWorkbook(fs);
                int sheetNum = workbook.NumberOfSheets;
                for (var i = 0; i < sheetNum; i++)
                {
                    ISheet sheet = workbook.GetSheetAt(i);
                    int rowNum = sheet.LastRowNum;
                    for (int j = 0; j < rowNum; j++)
                    {
                        IRow row = sheet.GetRow(j);
                        if (row != null)
                        {
                            var cellNum = row.LastCellNum;
                            for (var k = 0; k < cellNum; k = k + 2)
                            {
                                var cellKey = row.GetCell(k);
                                var cellValue = row.GetCell(k + 1) == null ? "" : GetNormalString(row.GetCell(k + 1).ToString());
                                if (cellKey != null)
                                {
                                    var key = cellKey.ToString().Trim();
                                    dic[key] = cellValue;
                                }
                            }
                        }
                    }
                }
            }
            return dic;
        }

        private static string GetNormalString(string oldStr)
        {
            var newStr = new Regex("[\\s]+").Replace(oldStr, " ");
            return newStr.Trim().Trim('.');
        }

        private static IWorkbook GetWorkbook(FileStream fs)
        {
            //这里需要根据文件名格式判断一下
            //HSSF只能读取xls的
            //XSSF只能读取xlsx格式的
            if (Path.GetExtension(fs.Name) == ".xls")
            {
                return new HSSFWorkbook(fs);
            }
            else if (Path.GetExtension(fs.Name) == ".xlsx")
            {
                return new XSSFWorkbook(fs);
            }
            throw new FileNotFoundException("文件类型不正确");
        }
    }
}
