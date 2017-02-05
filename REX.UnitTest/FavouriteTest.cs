using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using REX.Core.Services;
using REX.Data;

namespace REX.UnitTest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class FavouriteTest:TestBase
    {
        public FavouriteTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
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

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
        public IFavouriteService favouriteService;
        [TestInitialize()]
        public void Init()
        {
            favouriteService = this.GetService<IFavouriteService>();
        }

        [TestMethod]
        public void TestMergeFavourites_FirstItem()
        {
            //
            // TODO: Add test logic here
            //
            var fav1 = new Favourite
            {
                IsCurrently = true,
                Price1 = 400,
                Price2 = 700,
                RiceTypeId = 1,
                Weight = 6
            };

            var mergedList = favouriteService.MergeFavourites(fav1, null);
            Assert.AreEqual(1, mergedList.Count);

        }
        [TestMethod]
        public void TestMergeFavourites_SecondItem()
        {
            //
            // TODO: Add test logic here
            //
            var fav1 = new Favourite
            {
                IsCurrently = true,
                Price1 = 400,
                Price2 = 700,
                RiceTypeId = 1,
                Weight = 6
            };

            var fav2 = new Favourite
            {
                IsCurrently = true,
                Price1 = 400,
                Price2 = 800,
                RiceTypeId = 2,
                Weight = 6
            };

            var mergedList = favouriteService.MergeFavourites(fav2, new List<Favourite> { fav1 });
            Assert.AreEqual(2, mergedList.Count);

            Assert.AreEqual(1, mergedList.Where(x=>x.IsCurrently).Count());

        }
        [TestMethod]
        public void TestMergeFavourites_ThirdItem_TheSame()
        {
            //
            // TODO: Add test logic here
            //
            var fav1 = new Favourite
            {
                IsCurrently = false,
                Price1 = 400,
                Price2 = 700,
                RiceTypeId = 1,
                Weight = 6
            };

            var fav2 = new Favourite
            {
                IsCurrently = true,
                Price1 = 400,
                Price2 = 800,
                RiceTypeId = 2,
                Weight = 6
            };

            var favNew = new Favourite
            {
                IsCurrently = true,
                Price1 = 400,
                Price2 = 800,
                RiceTypeId = 2,
                Weight = 6
            };

            var mergedList = favouriteService.MergeFavourites(favNew, new List<Favourite> { fav1, fav2 });
            Assert.AreEqual(2, mergedList.Count);
            Assert.AreEqual(1, mergedList.Where(x => x.IsCurrently).Count());
            Assert.AreEqual(800, mergedList.Where(x => x.IsCurrently).First().Price2);

        }

    }
}
