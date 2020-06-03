using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDatabase.Models
{
    public class LookUpData
    {
        public int Id { get; set; }

        [Display(Name ="Item Type")]
        public ItemType ItemType { get; set; }

        public string Value { get; set; }
    }

    public enum ItemType
    {
        [Display(Name="Marital Status")]
        MaritalStatus,
        Gender
    }
}
