using Newtonsoft.Json;
using Programming_Challenge.Models;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace Programming_Challenge.Controllers
{
    /// <summary>
    /// Home Controller to handle all the actions directed to it
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Method to get the cat list and load the view on application load
        /// </summary>
        /// <returns>Pets view</returns>
        public ActionResult GetPetList()
        {
            CatModel catModel = new CatModel();
            catModel.femaleCatList = new List<string>();
            catModel.maleCatList = new List<string>();
            WebClient wc = new WebClient();
            //string jsonData = "[{'name':'Bob','gender':'Male','age':23,'pets':[{'name':'Garfield','type':'Cat'},{'name':'Fido','type':'Dog'}]},{'name':'Jennifer','gender':'Female','age':18,'pets':[{'name':'Garfield','type':'Cat'}]},{'name':'Steve','gender':'Male','age':45,'pets':null},{'name':'Fred','gender':'Male','age':40,'pets':[{'name':'Tom','type':'Cat'},{'name':'Max','type':'Cat'},{'name':'Sam','type':'Dog'},{'name':'Jim','type':'Cat'}]},{'name':'Samantha','gender':'Female','age':40,'pets':[{'name':'Tabby','type':'Cat'}]},{'name':'Alice','gender':'Female','age':64,'pets':[{'name':'Simba','type':'Cat'},{'name':'Nemo','type':'Fish'}]}]";
            var jsonData = wc.DownloadString("http://agl-developer-test.azurewebsites.net/people.json");
            var response = JsonConvert.DeserializeObject<List<PetOwners>>(jsonData);
            foreach (PetOwners item in response)
            {
                if (item.Pets != null && item.Pets.Count > 0)
                {
                    foreach (Pet pet in item.Pets ?? item.Pets)
                    {
                        if (string.Equals(pet.Type.Trim().ToUpper(), "CAT") && string.Equals(item.Gender.Trim().ToUpper(), "FEMALE"))
                        {
                            catModel.femaleCatList.Add(pet.Name);
                        }
                        else if (string.Equals(pet.Type.Trim().ToUpper(), "CAT") && string.Equals(item.Gender.Trim().ToUpper(), "MALE"))
                        {
                            catModel.maleCatList.Add(pet.Name);
                        }
                    }
                }
            }

            return View("Pets", catModel);
        }
    }
}