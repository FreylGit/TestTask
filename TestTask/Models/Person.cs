using System.ComponentModel.DataAnnotations;

namespace TestTask.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public DateTime DateBirth { get; set; }
        [Required]
        public string Gender { get; set; }
    }
}
