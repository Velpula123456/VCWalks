using System.ComponentModel.DataAnnotations;

namespace VCWalks.Models.DTO
{
    public class UpdateWalkRequestDTO
    {
        [Required]
        [MaxLength(105)]
        public string Name { get; set; }
        [Required]
        [MaxLength(1005)]
        public string Description { get; set; }
        [Required]
        [Range(0, 50)]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
