using System.ComponentModel.DataAnnotations;

namespace MigrationDemo.Models {
    public class Employees {
        [Key]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}