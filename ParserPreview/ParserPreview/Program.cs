using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserPreview
{
    class Program
    {
        static void Main(string[] args)
        {
            FromWebpageFactory fac = new FromWebpageFactory();
          //  fac.getLetters();
          //  LoadPage collins = new LoadPage();
            FromFileFactory ob = new FromFileFactory();
            //ob.FromExcel();
            ob.FromCSV();
            Console.ReadKey();
        }
    }
}
