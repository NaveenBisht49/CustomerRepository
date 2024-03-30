using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_1.Models
{
    public class CustomerModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Aadhar_Number { get; set; }

        public string Aadhar_frontpath { get;set; }
        public string Aadhar_backpath { get; set; }

        [NotMapped]
        public IFormFile Aadhar_front { get; set;}
        [NotMapped]
        public IFormFile Aadhar_Back { get; set;}

        [NotMapped]
        public bool checkbox { get; set; }
    }
}
