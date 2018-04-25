using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Programming_Challenge.Models
{
    /// <summary>
    /// Class to hold Cat list based on owner gender
    /// </summary>
    public class CatModel
    {
        public List<string> MaleCatList { get; set; }
        public List<string> FemaleCatList { get; set; }
    }
}