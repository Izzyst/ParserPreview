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
// pobieranie danych od content definitions do copyrights
// wyłapać i wyrzucić wszystkie komentarze które może zawierać przeszukiwana strona
// załadować dane z javascriptu
// po załadowaniu usunąć z tych danych wszystkie fragmenty zawierające <html  itp.
// wrzucić dane do json



namespace ParserPreview
{
    class LoadPage
    {

       // string downloadString = new WebClient().DownloadString("https://www.collinsdictionary.com/dictionary/english/picturesque");
        public LoadPage()
        {
            dynamic ob = new ExpandoObject();
            ob.name = "Colins";
            ob.website = "https://www.collinsdictionary.com/dictionary/english/picturesque";
            ob.language = "en-US";

            ob.defs = gettingNodesfromURL();
  
            string json = JsonConvert.SerializeObject(ob);
            Console.WriteLine(json);
           // Console.WriteLine(gettingNodesfromURL());
            
        }

      /*  public void getSynonyms()
        {
            Regex ItemRegex = new Regex(@"class=.content definitions*.*div class=.div copyright.", RegexOptions.Compiled);
            foreach (Match ItemMatch in ItemRegex.Matches(downloadString))
            {
                //Console.WriteLine(ItemMatch);
                //saveToXml(ItemMatch, "def" + count);
                Console.WriteLine(" " + ItemMatch);
            }
        }*/

        public HtmlNodeCollection gettingNodesfromURL()
        {
  
                    var webGet = new HtmlWeb();
                    var doc = webGet.Load("https://www.collinsdictionary.com/dictionary/english/picturesque");
                   // HtmlNode node = doc.DocumentNode.SelectSingleNode("//div[@id='picturesque__1']");

                    HtmlNodeCollection Nodes = doc.DocumentNode.SelectNodes("//a[@class='def']");
                    foreach (var link in Nodes)
                    {
                        Console.WriteLine(link.Attributes.ToString()); 
                    }

            /*else
            {
               // return 0;
            }*/

            return Nodes;
        }

    }
    
}
