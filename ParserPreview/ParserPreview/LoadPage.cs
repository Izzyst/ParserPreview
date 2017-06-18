using System;
using System.Dynamic;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Xml;
using Newtonsoft.Json;
using HtmlAgilityPack;


//TO DO:
// wrzucić dane do json

namespace ParserPreview
{
    class LoadPage
    {
        private List<Words> words = new List<Words>();
  
        public LoadPage()
        {
            // pobranie listy zawierającej wyprodukowane adresy stron ze słowami // FromWebpageFactory
            Console.WriteLine("Pobieram słowa i ich definicje: ...");
            List<string> links = new List<string>();
            FromWebpageFactory urlList = new FromWebpageFactory();
            string dict= "PL";

            if(dict == "ang") {links = urlList.getLetters(); }
            else{ links = urlList.getLettersPL(); }


            Parallel.ForEach(links, item =>
             {

                 if (dict == "ang")
                 {
                     var temp = gettingNodesfromURL(item);
                     lock (words)//lock blokuje zasoby kolekci które są obecnie używane przez jeden z wątków
                    {
                         words.Add(temp);
                     }
                 }
                 else
                 {
                     var temp = gettingNodesFromURLPL(item);
                     if(temp !=null)
                     {
                         lock (words)
                         {
                             words.AddRange(temp);// w tym przypadku dopisuje całą listę
                         }
                     }

                 }

             });


          foreach (var item in words)
            {
                if(item !=null)
                {
                    Console.WriteLine("{0}, {1}\t", item.word, item.defs[0]);
                }
            }
        
        }


        public Words gettingNodesfromURL(string html)
        {
            List<string> definitions = new List<string>();
            var webGet = new HtmlWeb();
            var doc = webGet.Load(html);
            // word:
            try
            {
                Words w;
                HtmlNode node = doc.DocumentNode.SelectSingleNode("//span[@class='orth']");
                // definitions for word
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//div[@class='def']");
                if(nodes != null)
                {
                    foreach (HtmlNode link in nodes)
                    {
                        definitions.Add(Strip(link.InnerText));
                    }
                     w = new Words(node.InnerText, definitions);
                    return w;
                }
                else
                {
                   // return null;
                    throw new Exception("No matching nodes found!");                    
                }
                
            }
            catch(Exception ex)
            {
              //  throw;
                return null;
            } 
        }
        // w przypadku słownika kopalinskiego należ pobrac kilka słów i kilka definicji z jednej strony
        private List<Words> gettingNodesFromURLPL(string html)
        {
            List<string> definitions = new List<string>();
            List<string> defs = new List<string>();
            var webGet = new HtmlWeb();
            var doc = webGet.Load(html);
            List<Words> w = new List<Words>();
            // word:
            try
            {               
                HtmlNodeCollection node = doc.DocumentNode.SelectNodes("//td/b/font[@class='10ptGeorgia']");// słowa
                // definitions for word
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//td/font[@class='10ptGeorgia']");// definicje dla danego słowa
                if (nodes != null && node != null)
                {
                    for(int i=0; i<node.Count; i++)
                    {
                        if(node[i]!=null)
                        {
                            if(nodes[i] != null )
                            {
                                if(nodes[i].InnerText.Length>10)
                                {
                                    List<String> d = new List<String>();
                                    

                                    defs.Add(Strip(node[i].InnerText));
                                    definitions.Add(Strip(nodes[i].InnerText));
                                    d.Add(definitions.Last());
                                    //Console.WriteLine(defs.Last() + " - " + definitions.Last());
                                    Words n = new Words(defs.Last(), d);
                                    w.Add(n);
                                }                              
                            }
                        }                      
                    }
                    return w;
                }
                else
                {
                    // return null;
                    throw new Exception("No matching nodes found!");
                }
            }
            catch (Exception ex)
            {
                //  throw;
                return null;
            }
        }
  

        //usuwanie tagów html
        public static string Strip(string text)
        {
            //usuwanie komentarzy 
            text = Regex.Replace(text, @"(<![^<]*>)", string.Empty);
            //usuwanie skryptów oraz arkuszy styli
            text = Regex.Replace(text, @"(<script[^<]*</script>)|(<style[^<]*</style>)|(&[^;]*;)", string.Empty);
            text = Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
            return text;
        }

    }
    
}
