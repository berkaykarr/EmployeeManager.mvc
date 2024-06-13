using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManager.Mvc.Models
{
    [Table("Country")]
    public class Country
    {
        [Key]
        public int CountryID { get; set; }
        public string Name { get; set; }
    }

}
