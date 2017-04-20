using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchTelefonos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SearchTelefonos.Tests
{
    [TestClass()]
    public class UniversidadPeruSearchTests
    {
        [TestMethod()]
        public void SearchTelefonosTest()
        {
            var ruc = "20332600592";

            IPhoneSearch client = new UniversidadPeruSearch();
            Assert.IsTrue(client.Support == ParamSerach.ByDocument);
            var phones = client.SearchTelefonos(ruc);
            Assert.IsNotNull(phones);
            Trace.Write(phones);
        }
    }
}