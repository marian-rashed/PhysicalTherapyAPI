using System.ComponentModel.DataAnnotations.Schema;

namespace PhysicalTherapyAPI.DTOs
{
    public class ExerciseDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ExerciseLink { get; set; }
        public string? ExerciseType { get; set; }
        public int? CategoryId { get; set; }
    }
}
