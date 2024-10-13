using PhysicalTherapyAPI.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhysicalTherapyAPI.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? PhotoUrl { get; set; }
        public string? ExerciseLink { get; set; }
        public string? ExerciseType { get; set; } 
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
