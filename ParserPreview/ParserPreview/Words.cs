using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserPreview
{
    
    //gotowy produkt
    class Words
    {
        public string word;
        public List<string> defs;

        public Words(string word, List<string>defs)
        {
            this.word = word;
            Console.WriteLine(this.word);
            this.defs = defs;
            this.defs.ForEach(i => Console.WriteLine(i));
        }

        public void ToString()
        {
            Console.WriteLine(this.word + ": \t");
            this.defs.ForEach(i => Console.WriteLine("{0}\t", i));
        }

       // public List<>
    }
}
