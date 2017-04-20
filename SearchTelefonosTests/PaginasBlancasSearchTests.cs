using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;

namespace SearchTelefonos.Tests
{
    [TestClass()]
    public class PaginasBlancasSearchTests
    {
        [TestMethod()]
        public void SearchTelefonosTest()
        {
            var name = "graña y montero";

            IPhoneSearch client = new PaginasBlancasSearch();
            Assert.IsTrue(client.Support.Contains(ParamSerach.ByNames));
            var phones = client.SearchTelefonos(name);
            Assert.IsNotNull(phones);
            Trace.Write(phones);
        }
    }
}