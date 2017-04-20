using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;

namespace SearchTelefonos
{
   
    public class DatosPeruSearch : IPhoneSearch
    {
        private readonly string domain = "https://www.datosperu.org";

        public string Domain { get { return domain; } }

        public string SearchTelefonos(string value)
        {
            var web = new HtmlWeb();
            var doc = web.Load(domain + "/buscador_empresas.php?buscar=" + value);
            var aHref = doc.DocumentNode.SelectSingleNode("//*[@id='categorias']/div[3]/div/div/a");
            if (aHref == null) return null;
            
            var link = aHref.Attributes["href"].Value;
            doc = web.Load(domain + "/" + link);
            var infoDiv = doc.DocumentNode.SelectSingleNode("//*[@itemtype='http://schema.org/Organization']");
            if (infoDiv == null) return null;

            var phones = new Queue<string>();
            var lastItem = infoDiv.LastChild;
            while (lastItem != null && lastItem.Name != "div")
            {
                if (lastItem.Name == "li")
                {
                    var ct = lastItem.ChildNodes.FindFirst("span");
                    phones.Enqueue(ct == null ? null : ct.InnerText);
                }
                lastItem = lastItem.PreviousSibling;
            }
            return string.Join(",", phones);
        }

        public ParamSerach Support
        {
            get { return ParamSerach.ByDocument; } 
        }
    }
}
