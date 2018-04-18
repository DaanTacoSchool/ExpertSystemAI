using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystemAI
{
    class Program
    {
        // for student intakes, requires .xlsx or .csv file as input
        static void Main(string[] args)
        {
            RunAI();
            Logger.Display("Exit? y/y");
            Console.ReadKey();
        }

        public static void RunAI()
        {
            string fileName = "D:\\intakefile.xlsx";
            DataTable _dt = new DataTable();
            _dt = ExcelReader.ImportSheet(fileName);

            List<Dictionary<String, Object>> tmp = ExcelReader.ConvertToList(_dt);
            ProcessBaseConstraints pbc = new ProcessBaseConstraints();
            foreach (Dictionary<String, Object> d in tmp)
            {
                int total = 0;
                foreach (KeyValuePair<String, Object> kvp in d)
                {
                    /* hier kan je checken welke colom het is met kvp.key 
                     * met kvp.value kan je de waarde ophalen en gebruiken
                     * misschien hier een functie aanroepen in een andere klasse die de business rules uitvoert en alles uitrekend
                      == hier geen logica ==
                      */
                    // Display hier als voorbeeld. straks alleen studentnummer en eindadvies weergeven. //
                    // Logger.Display(kvp.Key + " : " + kvp.Value);
                   
                    total += pbc.ColumnConstraints(kvp.Key, kvp.Value);
                    Logger.Log("subtotal: " + total);
                }
                Logger.Log("total: " + total);

                Logger.DisplaySingleLine(" points: " + total);
                if(total <= 50)
                {
                    Logger.Display(" AI: Negatief");
                }else if(total <= 70)
                {
                    Logger.Display(" AI: Twijfel");
                }else if(total > 70)
                {
                    Logger.Display(" AI: Positief");
                }


               /* Logger.Log("totalb: " + (total+50));
                Logger.Display(" pointsb: " + (total+50));*/
            }
        }


    }
}
