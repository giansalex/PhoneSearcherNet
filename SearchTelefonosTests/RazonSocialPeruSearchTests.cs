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
    public class RazonSocialPeruSearchTests
    {
        [TestMethod()]
        public void SearchTelefonosTest()
        {
            IPhoneSearch client = new RazonSocialPeruSearch();
            Assert.IsTrue(client.Support.Contains(ParamSerach.ByDocument));
            var phones = client.SearchTelefonos("20332600592");
            Assert.IsNotNull(phones);
            Trace.Write(phones);
        }
    }
}