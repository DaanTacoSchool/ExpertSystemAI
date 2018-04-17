using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystemAI
{
    class Logger
    {
        public static void Log(Object o)
        {
            Debug.WriteLine(o);
        }

        public static void Display(Object o)
        {
            Console.WriteLine(o);
        }
        public static void DisplaySingleLine(Object o)
        {
            Console.Write(o);
        }
        public static void DumpTable_Cells(DataTable dt)
        {
            foreach (DataRow dataRow in dt.Rows)
            {
                foreach (var item in dataRow.ItemArray)
                {
                    Console.WriteLine(item);
                }
            }
        }

        public static void DumpTable_Rows(DataTable dt)
        {
            foreach (DataRow dataRow in dt.Rows)
            {
                foreach (var item in dataRow.ItemArray)
                {
                    Console.Write(item+ ",");
                }
                Console.WriteLine();
            }
        }
       
    }
}
