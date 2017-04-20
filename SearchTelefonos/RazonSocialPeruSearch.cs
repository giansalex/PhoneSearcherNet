using System.Net;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;

namespace SearchTelefonos
{
    public class RazonSocialPeruSearch : IPhoneSearch
    {
        private readonly string domain = "http://www.razonsocialperu.com/";

        public string Domain { get { return domain; } }

        public string SearchTelefonos(string value)
        {
            var url = domain + "/empresa/buscador?query=" + value;
            var json = GetJson(url);
            var first = JArray.Parse(json).First;
            if (first == null) return null;

            var link = (string)first["link"];
            var web = new HtmlWeb();
            var doc = web.Load(link);
            var telf = doc.DocumentNode.SelectSingleNode("//*[@id='content']/article/div/div[2]/div[1]/div[2]/ul/li[7]");
            if (telf == null) return null;
            return telf.InnerText;
        }


        private string GetJson(string url)
        {
            using (var client = new WebClient())
            {
                return client.DownloadString(url);
            } 
        }

        public ParamSerach Support
        {
            get { return ParamSerach.ByDocument; }
        }
    }
}
