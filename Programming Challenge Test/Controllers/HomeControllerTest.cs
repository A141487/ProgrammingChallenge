using Microsoft.VisualStudio.TestTools.UnitTesting;
using Programming_Challenge.Models;
using System.Web.Mvc;

namespace Programming_Challenge.Controllers.Tests
{
    /// <summary>
    /// Test controller class to test for Home Controller methods
    /// </summary>
    [TestClass()]
    public class HomeControllerTest
    {
        /// <summary>
        /// Method to test GetCatList Action method of Home Controller
        /// </summary>
        [TestMethod()]
        public void GetCatList_Test()
        {
            HomeController homeCtlr = new HomeController();
            var response = homeCtlr.GetCatList();
            Assert.IsNotNull(response);
            Assert.AreEqual(typeof(ViewResult), response.GetType());
            Assert.IsTrue(((ViewResultBase)response).ViewEngineCollection.Count > 0);
            Assert.AreEqual(((ViewResultBase)response).ViewName, "Pets");
            Assert.IsTrue(((CatModel)((ViewResultBase)response).Model).FemaleCatList.Count == 3);
            Assert.IsTrue(((CatModel)((ViewResultBase)response).Model).MaleCatList.Count == 4);
        }
    }
}