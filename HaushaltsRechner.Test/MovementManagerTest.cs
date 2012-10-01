using HaushaltsRechner.Business.Manager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using HaushaltsRechner.Data.Model;
using System.Linq;

namespace HaushaltsRechner.Test
{
    
    
    /// <summary>
    ///Dies ist eine Testklasse für "MovementManagerTest" und soll
    ///alle MovementManagerTest Komponententests enthalten.
    ///</summary>
    [TestClass()]
    public class MovementManagerTest
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
        ///Ein Test für "GetAllMovements"
        ///</summary>
        [TestMethod()]
        public void GetAllMovementsTest()
        {
            IQueryable<MOVEMENT> expected = null; // TODO: Passenden Wert initialisieren
            IQueryable<MOVEMENT> actual;
            actual = MovementManager.GetAllMovements();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Überprüfen Sie die Richtigkeit dieser Testmethode.");
        }

        /// <summary>
        ///Ein Test für "GetMovementById"
        ///</summary>
        [TestMethod()]
        public void GetMovementByIdTest()
        {
            Guid id = new Guid(); // TODO: Passenden Wert initialisieren
            MOVEMENT expected = null; // TODO: Passenden Wert initialisieren
            MOVEMENT actual;
            actual = MovementManager.GetMovementById(id);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Überprüfen Sie die Richtigkeit dieser Testmethode.");
        }

        /// <summary>
        ///Ein Test für "GetMovementsByAccount"
        ///</summary>
        [TestMethod()]
        public void GetMovementsByAccountTest()
        {
            ACCOUNT ac = null; // TODO: Passenden Wert initialisieren
            IQueryable<MOVEMENT> expected = null; // TODO: Passenden Wert initialisieren
            IQueryable<MOVEMENT> actual;
            actual = MovementManager.GetMovementsByAccount(ac);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Überprüfen Sie die Richtigkeit dieser Testmethode.");
        }

        /// <summary>
        ///Ein Test für "GetMovementsByAccountId"
        ///</summary>
        [TestMethod()]
        public void GetMovementsByAccountIdTest()
        {
            Guid id = new Guid(); // TODO: Passenden Wert initialisieren
            IQueryable<MOVEMENT> expected = null; // TODO: Passenden Wert initialisieren
            IQueryable<MOVEMENT> actual;
            actual = MovementManager.GetMovementsByAccountId(id);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Überprüfen Sie die Richtigkeit dieser Testmethode.");
        }
    }
}
