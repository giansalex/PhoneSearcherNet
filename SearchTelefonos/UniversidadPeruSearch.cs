using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using HtmlAgilityPack;

namespace SearchTelefonos
{
    public class UniversidadPeruSearch : IPhoneSearch
    {
        private readonly string domain = "https://www.universidadperu.com";

        /// <inheritdoc cref="Domain"/>
        public string Domain { get { return domain; } }

        public string Name { get { return "UNV"; } }

        public IEnumerable<string> SearchTelefonos(string value)
        {
            var url = "https://www.universidadperu.com/empresas/busqueda/";
            var param = "buscaempresa=" + value;
            var html = LoadPage(url, param);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var listContainer = doc.DocumentNode.SelectSingleNode("//*[@id='middlecolumnhome']/div[2]/ul[2]/li/ul");
            if (listContainer == null) return null;
            var phones = new Queue<string>();

            foreach (var node in listContainer.ChildNodes)
            {
                if (node.Name != "li") continue;
                phones.Enqueue(node.ChildNodes.FindFirst("span").InnerText);
            }

            return phones;
        }

        public ParamSerach[] Support
        {
            get { return new[] { ParamSerach.ByDocument }; }
        }

        private string LoadPage(string url, string args)
        {
            var http = (HttpWebRequest)WebRequest.Create(url);
            http.Method = "POST";
            http.ContentType = "application/x-www-form-urlencoded";
            http.AllowAutoRedirect = true;

            var bytes = Encoding.UTF8.GetBytes(args);
            http.ContentLength = bytes.Length;
            using (var wr = http.GetRequestStream())
            {
                wr.Write(bytes, 0, bytes.Length);
            }
            var resp = http.GetResponse();
            using (var rd = new StreamReader(resp.GetResponseStream(), Encoding.UTF8))
            {
                return rd.ReadToEnd();
            }
        }
    }
}
