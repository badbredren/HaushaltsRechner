using HaushaltsRechner.Business.Manager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using HaushaltsRechner.Data;
using System.Linq;
using HaushaltsRechner.Data.Model;

namespace HaushaltsRechner.Test
{
    
    
    /// <summary>
    ///Dies ist eine Testklasse für "UserManagerTest" und soll
    ///alle UserManagerTest Komponententests enthalten.
    ///</summary>
    [TestClass()]
    public class UserManagerTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Ruft den Testkontext auf, der Informationen
        ///über und Funktionalität für den aktuellen Testlauf bietet, oder legt diesen fest.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Zusätzliche Testattribute
        // 
        //Sie können beim Verfassen Ihrer Tests die folgenden zusätzlichen Attribute verwenden:
        //
        //Mit ClassInitialize führen Sie Code aus, bevor Sie den ersten Test in der Klasse ausführen.
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Mit ClassCleanup führen Sie Code aus, nachdem alle Tests in einer Klasse ausgeführt wurden.
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Mit TestInitialize können Sie vor jedem einzelnen Test Code ausführen.
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Mit TestCleanup können Sie nach jedem einzelnen Test Code ausführen.
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///Ein Test für "GetAllUser"
        ///</summary>
        [TestMethod()]
        public void GetAllUserTest()
        {
            IQueryable<USER> expected = null; // TODO: Passenden Wert initialisieren
            IQueryable<USER> actual;
            actual = UserManager.GetAllUser();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Überprüfen Sie die Richtigkeit dieser Testmethode.");
        }

        /// <summary>
        ///Ein Test für "GetUserById"
        ///</summary>
        [TestMethod()]
        public void GetUserByIdTest()
        {
            Guid id = new Guid(); // TODO: Passenden Wert initialisieren
            USER expected = null; // TODO: Passenden Wert initialisieren
            USER actual;
            actual = UserManager.GetUserById(id);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Überprüfen Sie die Richtigkeit dieser Testmethode.");
        }

        /// <summary>
        ///Ein Test für "GetUserByName"
        ///</summary>
        [TestMethod()]
        public void GetUserByNameTest()
        {
            string name = string.Empty; // TODO: Passenden Wert initialisieren
            USER expected = null; // TODO: Passenden Wert initialisieren
            USER actual;
            actual = UserManager.GetUserByName(name);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Überprüfen Sie die Richtigkeit dieser Testmethode.");
        }
    }
}
