using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace SearchTelefonos
{
    /// <summary>
    /// Busquedas en Paginas Blancas.
    /// </summary>
    public class PaginasBlancasSearch : IPhoneSearch
    {
        private readonly string domain = "http://www.paginasblancas.pe";

        public string Domain { get { return domain; } }

        public string Name { get { return "PBL"; } }

        public string SearchTelefonos(string value)
        {
            var url = domain + "/persona/" + ClearText(value);
            var doc = new HtmlWeb().Load(url);
            var containers = doc.DocumentNode.SelectNodes("//span[@class='m-icon--single-phone']");

            var phones = new Queue<string>();

            foreach (var node in containers)
            {
                var a = node.ChildNodes.FindFirst("a");
                if (a == null) continue;
                phones.Enqueue(a.InnerText.Trim());
            }

            return string.Join(",", phones);
        }

        public ParamSerach[] Support
        {
            get { return new[] {ParamSerach.ByNames }; }
        }

        private string ClearText(string text)
        {
            return text
                .Replace("ñ", "n")
                .Replace('.', '\0');
        }
    }
}
