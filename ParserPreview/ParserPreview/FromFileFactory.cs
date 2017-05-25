using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelTools = OfficeOpenXml;
using Excel = Microsoft.Office.Interop.Excel;

namespace ParserPreview
{
    class FromFileFactory
    {
        // public abstract abstractWords CreateWords();
        // public abstract abstractDefinitions CreateDefinitions();
        public void FromExcel()
        {
            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\Izabela\Documents\words.xlsx"); 
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;


            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based!!
            int rowCount = 10;
            int colCount = 2;
            for (int i = 1; i <= rowCount; i++)
            {
                for (int j = 1; j <= colCount; j++)
                {
                    //new line
                    if (j == 1)
                        Console.Write("\r\n");

                    //write the value to the console
                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                        Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");

                    //add useful things here!   
                }
            }
        }
    }
}
