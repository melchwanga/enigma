using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EmployeeDatabase.Models
{
    public class SubCounty
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name="County")]
        public int CountyId { get; set; }

        [JsonIgnore]
        public virtual County County { get; set; }

        public ICollection<Employee> Employees { get; set; }

    }
}
