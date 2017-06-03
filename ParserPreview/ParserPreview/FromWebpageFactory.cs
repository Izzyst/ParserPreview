using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
//TO DO:
// poprawic zapis linkow 
// sprawdzic czy zapisalo odpowiednie linki
namespace ParserPreview
{
    class FromWebpageFactory
    {

        private List<string> words = new List<string>();
        Random rnd = new Random();

        // zwraca listę linków do słów danego słownika
        public List<string> getLetters()
        {
            List<string> subwords = new List<string>();
            var webGet = new HtmlWeb();
            var doc = webGet.Load("https://www.collinsdictionary.com/dictionary/english");
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//ul[@class='browse-letters']/li/a");
            string html = "";
            int count = 0;
          //  using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Public\WriteLines2.txt"))
          //  {        
                foreach (HtmlNode item in nodes)
                {
                    html = "https://www.collinsdictionary.com" + item.Attributes["href"].Value;// letterz A-Z
                    subwords = getWords(html, 5);// zwraca liste słów zawierającą się w danym przedziale literowym np. ab..., abg...., ath...
                    List<string> w = new List<string>();
                    if(count!=0)// ponieważ słowa w przedziale # nie posiadają podprzedzialów, dlatego je omijam
                    {
                        Parallel.ForEach(subwords, (sub) => 
                        {
                            w = getWords(sub, 1);
                            lock (words)
                            { 
                                words.AddRange(w);// AddRange - dopisuje liste do listy
                            }
                        });
                         /*   for (int i = 0; i < subwords.Count; i++)
                        {
                             w=getWords(subwords[i], 1);
                             words.AddRange(w);// AddRange - dopisuje liste do listy
                        }*/
                    }
                    else
                    {
                        words.AddRange(subwords);// dodanie słów dla przedziału #
                    }
                    
                    count++;  
                }
                
               // words.ForEach(i => file.WriteLine("{0}\t", i));
           // }
            return words;
        }
        // wyciaganie dla każdej poszczegolnej literki( A-Z ) słówek
        private List<string> getWords(string html, int amount)
        {
            List<String> links = new List<string>();
 
            try
            {
                var webGet = new HtmlWeb();
                var doc = webGet.Load(html);
                string html2 = "";
                int i = rnd.Next(0, 6);
                int next=0;
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//ul[@class='columns2 browse-list']/li/a");
                if (nodes != null)
                {             
                    while(amount>0)
                    {
                        int j = rnd.Next(0, nodes.Count);
                        html2 = "https://www.collinsdictionary.com" + nodes[j].Attributes["href"].Value;
                        links.Add(html2.ToString());
                        amount--;
                    }
                }
                else
                {
                    throw new Exception("No matching nodes found!");
                }
                return links;
            }
            catch (Exception ex)
            {
                throw;
            }
           // return links;
        }
    }
}
