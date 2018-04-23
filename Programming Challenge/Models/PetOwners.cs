using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Programming_Challenge.Models
{
    /// <summary>
    /// Class to hold pet owner details
    /// </summary>
    public class PetOwners
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public List<Pet> Pets { get; set; }
    }
}