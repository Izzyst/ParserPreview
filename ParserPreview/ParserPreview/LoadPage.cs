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
            List<string> links = new List<string>();
            FromWebpageFactory urlList = new FromWebpageFactory();
            links = urlList.getLetters();

            //links.ForEach(i => Console.WriteLine("{0} - {1}\t",  i));
            //dynamic ob = new ExpandoObject();
            //ob.name = "Colins";
            // ob.language = "en-US";
            //ob.defs = gettingNodesfromURL(html);
  

            foreach (var item in links)
            {
                words.Add(gettingNodesfromURL(item));
            }

            // string json = JsonConvert.SerializeObject(ob);
            // Console.WriteLine(json);
            // Console.WriteLine(gettingNodesfromURL());
            // gettingNodesfromURL(html);
            /*using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Public\WriteLines1.txt"))
            {
                words.ForEach(i => file.WriteLine("{0}, {1}\t",i.word, i.defs[0]));
            }*/
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
