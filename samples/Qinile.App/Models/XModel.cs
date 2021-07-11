using System;
using Qinile.Core.Models;

namespace Qinile.App.Models
{
    public class XModel : BaseModel<string>
    {
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Bio { get; set; }
        public string Gender { get; set; }
        public string Preference { get; set; }
        public string Occupation { get; set; }
        public string Organisation { get; set; }
        public string Instagram { get; set; }
        public int Rating { get; set; }
        public int Kids { get; set; }
        public double Income { get; set; }
    }
}