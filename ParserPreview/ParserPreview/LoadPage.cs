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
        private List<Words> words = new List<Words>();
  
        public LoadPage()
        {
            // pobranie listy zawierającej wyprodukowane adresy stron ze słowami // FromWebpageFactory
            List<string> links = new List<string>();
            FromWebpageFactory urlList = new FromWebpageFactory();
            links = urlList.getLetters();

            dynamic ob = new ExpandoObject();
            ob.name = "Colins";
            //ob.website = "https://www.collinsdictionary.com/dictionary/english/picturesque";
            ob.language = "en-US";
            string html = "https://www.collinsdictionary.com/dictionary/english/picturesque";
            //ob.defs = gettingNodesfromURL(html);
            foreach(var item in links)
            {
                Console.WriteLine(item);
                //words.Add(gettingNodesfromURL(item));
            }

            // string json = JsonConvert.SerializeObject(ob);
            // Console.WriteLine(json);
            // Console.WriteLine(gettingNodesfromURL());
            // gettingNodesfromURL(html);
            /*using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Public\WriteLines1.txt"))
            {
                words.ForEach(i => file.WriteLine("{0}, {1}\t",i.word, i.defs[0]));
            }*/
            words.ForEach(i => Console.WriteLine("{0}, {1}\t", i.word, i.defs[0]));
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
                    
                }
                else
                {
                    throw new Exception("No matching nodes found!");
                }
                return w;
            }
            catch(Exception ex)
            {
                throw;
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
