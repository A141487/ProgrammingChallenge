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
        /// Method load the view on application load
        /// </summary>
        /// <returns>Pets view</returns>
        public ActionResult DisplayCatList()
        {
            try
            {
                CatModel catModel = new CatModel();
                List<PetOwners> response = new List<PetOwners>();
                response = GetJsonData();
                catModel = GetCatList(response);
                return View("Pets", catModel);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Error detail: " + e.Message);
                return View("Error");
            }
        }

        /// <summary>
        /// Private method to get cat list based on owner gender
        /// </summary>
        /// <param name="list">List of PetOwners</param>
        /// <returns>CatModel</returns>
        private CatModel GetCatList(List<PetOwners> list)
        {
            CatModel catModel = new CatModel();
            catModel.FemaleCatList = new List<string>();
            catModel.MaleCatList = new List<string>();

            try
            {
                if (list != null && list.Count > 0)
                {
                    foreach (PetOwners item in list)
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
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Error detail: " + e.Message);
            }
            return catModel;
        }

        /// <summary>
        /// Method to get List of Pet Owners from the Json data
        /// </summary>
        /// <returns>List of PetOwners</returns>
        private List<PetOwners> GetJsonData()
        {
            List<PetOwners> response = new List<PetOwners>();
            try
            {
                WebClient wc = new WebClient();
                var jsonData = wc.DownloadString("http://agl-developer-test.azurewebsites.net/people.json");
                response = JsonConvert.DeserializeObject<List<PetOwners>>(jsonData);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Error detail: " + e.Message);
            }
            return response;
        }
    }
}