using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelTools = OfficeOpenXml;
using Excel = Microsoft.Office.Interop.Excel;

namespace ParserPreview
{

    //TO DO:
 
    class FromFileFactory
    {
        private List<Words> words = new List<Words>();
        // public abstract abstractWords CreateWords();
        // public abstract abstractDefinitions CreateDefinitions();
        public void FromExcel()
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\Izabela\Documents\words.xlsx"); 
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            Excel.Worksheet x = xlApp.ActiveSheet as Excel.Worksheet;
 
            int rowCount = xlWorksheet.UsedRange.Rows.Count;
            int colCount = xlWorksheet.UsedRange.Columns.Count;

            string lol = "";
            // Console.WriteLine("ilosc wierszy: " + rowCount + " ilość kolumn: " + colCount);
            List<string> def = new List<string>();
            List<string> word2 = new List<string>();
            for(int i=1; i<=rowCount; i++)
            {
                string w="";
                if (x.Cells[i, 1].Value2 != null)
                {
                    Console.WriteLine(x.Cells[i, 1].Value2);
                     w= x.Cells[i, 1].Value2;
                }
                    
                for (int j = 2; j <= colCount; j++)
                {
                    if (x.Cells[i, j].Value2 != null)
                    {
                        def.Add(x.Cells[i, j].Value2);
                        Console.WriteLine(x.Cells[i, j].Value2);
                    }
                    
                }
                words.Add(new Words(w, def));
                def.Clear();

            }
           
         //   words.ForEach(i => i.ToString());
            
        }

        public void FromCSV()
        {
            using (var fs = File.OpenRead(@"C:\Users\Izabela\Documents\words.csv"))
            using (var reader = new StreamReader(fs))
            {
                string word = "";
                List<string> defs = new List<string>();
                
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var cols = line.Split(',').Count();
                    var values = line.Split(',');
                    //word
                    word = values[0];
                    //definition
                    for (int i=1; i<=cols-1; i++)
                    {
                        defs.Add(values[i]);
                    }
                   // defs.ForEach(i => Console.WriteLine("{0}\t", i));
                  
                    words.Add(new Words(word, defs));
                    defs.Clear();
                }
            }
            foreach (var item in words)
            {
                if (item != null)
                {
                   Console.WriteLine("{0}\t", item.word);
                    //Console.WriteLine("{0}\t", item.defs[0]);
                }
            }
        }
    }
}
