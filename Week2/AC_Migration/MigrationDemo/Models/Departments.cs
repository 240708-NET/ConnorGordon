using System.ComponentModel.DataAnnotations;

namespace MigrationDemo.Models {
    public class Departments {
        [Key]
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }   
    }
}