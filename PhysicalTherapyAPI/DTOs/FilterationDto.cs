namespace PhysicalTherapyAPI.DTOs
{
    public class FilterationDto
    {
        public int? CategoryId { get; set; }
        public string? ExerciseType { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
