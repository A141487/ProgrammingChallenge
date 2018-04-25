using Newtonsoft.Json;
using Programming_Challenge.Models;
using System;
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
        public ActionResult GetCatList()
        {
            try
            {
                CatModel catModel = new CatModel();
                catModel.FemaleCatList = new List<string>();
                catModel.MaleCatList = new List<string>();
                WebClient wc = new WebClient();
                //string jsonData = "[{'name':'Bob','gender':'Male','age':23,'pets':[{'name':'Garfield','type':'Cat'},{'name':'Fido','type':'Dog'}]},{'name':'Jennifer','gender':'Female','age':18,'pets':[{'name':'Garfield','type':'Cat'}]},{'name':'Steve','gender':'Male','age':45,'pets':null},{'name':'Fred','gender':'Male','age':40,'pets':[{'name':'Tom','type':'Cat'},{'name':'Max','type':'Cat'},{'name':'Sam','type':'Dog'},{'name':'Jim','type':'Cat'}]},{'name':'Samantha','gender':'Female','age':40,'pets':[{'name':'Tabby','type':'Cat'}]},{'name':'Alice','gender':'Female','age':64,'pets':[{'name':'Simba','type':'Cat'},{'name':'Nemo','type':'Fish'}]}]";
                var jsonData = wc.DownloadString("http://agl-developer-test.azurewebsites.net/people.json");
                var response = JsonConvert.DeserializeObject<List<PetOwners>>(jsonData);
                if(response != null && response.Count > 0)
                {
                    foreach (PetOwners item in response)
                    {
                        if (item.Pets != null && item.Pets.Count > 0 && item.Gender != string.Empty)
                        {
                            foreach (Pet pet in item.Pets)
                            {
                                if (string.Equals(pet.Type.Trim().ToUpper(), "CAT") && string.Equals(item.Gender.Trim().ToUpper(), "FEMALE"))
                                {
                                    catModel.FemaleCatList.Add(pet.Name);
                                }
                                else if (string.Equals(pet.Type.Trim().ToUpper(), "CAT") && string.Equals(item.Gender.Trim().ToUpper(), "MALE"))
                                {
                                    catModel.MaleCatList.Add(pet.Name);
                                }
                            }
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Error detail: No data to list");
                }
                
                return View("Pets", catModel);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Error detail: " + e.Message);
                return View("Error");
            }
        }
    }
}