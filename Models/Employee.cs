using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDatabase.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Display(Name="First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get
            {
                return FirstName + " " + LastName;
            } }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }

        [Required]
        [Display(Name = "Gender")]

        public int GenderId { get; set; }
        public LookUpData Gender { get; set; }

        [Required]
        [Display(Name = "Marital Status")]
        public int MaritalStatusId { get; set; }
        public LookUpData MaritalStatus { get; set; }

        [Display(Name = "Sub-county")]
        public int SubCountyID { get; set; }
        public virtual SubCounty SubCounty { get; set; }

        public int Age
        {
            get
            {
                DateTime now = DateTime.Today;
                int age = now.Year - DOB.Year;
                if (now < DOB.AddYears(age)) age--;

                return age;
            }
        }
        [DataType(DataType.Date)]
        public DateTime NextBirthday { get; set; }

    }
}
