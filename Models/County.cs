using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDatabase.Models
{
    public class County
    {
        public int Id { get; set; }

        [StringLength(3,ErrorMessage ="County Code must be three digits", MinimumLength =3)]
        public string Code { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<SubCounty> SubCounties { get; set; }
    }
}
