using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystemAI
{
    class ExcelReader
    {

        public static DataTable ImportSheet(string fileName)
        {
            var datatable = new DataTable();
            var workbook = new XLWorkbook(fileName);
            var xlWorksheet = workbook.Worksheet(1);
            var range = xlWorksheet.Range(xlWorksheet.FirstCellUsed(), xlWorksheet.LastCellUsed());

            var col = range.ColumnCount();
            var row = range.RowCount();

            Logger.Log("ExcelReader finished setting variables");
            //if a datatable already exists, clear the existing table 
            datatable.Clear();
            Logger.Log("ExcelReader Cleared existing datatable");

            for (var i = 1; i <= col; i++)
            {
                var column = xlWorksheet.Cell(1, i);
                datatable.Columns.Add(column.Value.ToString());
            }
            Logger.Log("ExcelReader added columns to datatable");

            var firstHeadRow = 0;
            foreach (var item in range.Rows())
            {
                if (firstHeadRow != 0)
                {
                    var array = new object[col];
                    for (var y = 1; y <= col; y++)
                    {
                        array[y - 1] = item.Cell(y).Value;
                    }

                    datatable.Rows.Add(array);
                }
                firstHeadRow++;
            }
            Logger.Log("ExcelReader addded values to datatable");
            Logger.Log("ExcelReader finished");
            return datatable;
        }

        // delimiter is a comma
        public static String DataTableToString(DataTable table)
        {
            string data = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (null != table && null != table.Rows)
            {
                foreach (DataRow dataRow in table.Rows)
                {
                    foreach (var item in dataRow.ItemArray)
                    {
                        sb.Append(item);
                        sb.Append(',');
                    }
                    sb.AppendLine();
                }

                data = sb.ToString();
            }
            return data;
        }

        public static List<Dictionary<String,Object>> ConvertToList(DataTable dt)
        {
           
            List<Dictionary<String, Object>> list = new List<Dictionary<String, Object>>();
            foreach (DataRow dataRow in dt.Rows)
            {
                Dictionary<String, Object> dict = new Dictionary<String, Object>();
                foreach (DataColumn column in dt.Columns)
                {
                   // Dictionaries kunnen niet goed omgaan met null waardes
                    Object ob = dataRow[column];
                    if (ob == null || ob == "")
                    {
                        ob = "0"; 
                    }
                    dict.Add(column.ColumnName, ob);
                }
                list.Add(dict);
            }
            return list;
        }
    }
}
