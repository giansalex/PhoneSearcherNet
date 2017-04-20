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

        public IEnumerable<string> SearchTelefonos(string value)
        {
            var url = domain + "/persona/" + ClearText(value);
            var doc = new HtmlWeb().Load(url);
            var containers = doc.DocumentNode.SelectNodes("//li[@itemtype='http://schema.org/Person']");
            if (containers == null) return null;

            var phones = new Queue<string>();
            
            foreach (var node in containers)
            {
                var name = node.SelectSingleNode("./div[1]/div[1]/h3/a");
                var phone = node.SelectSingleNode("./div[2]/ul/li[1]/div/span[2]/a");
                if (phone == null || name == null) continue;
                phones.Enqueue(phone.InnerText.Trim() + " "+ name.InnerText.Trim());
            }

            return phones;
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
