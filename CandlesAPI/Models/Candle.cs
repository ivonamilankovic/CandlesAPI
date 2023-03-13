using System.ComponentModel.DataAnnotations;

namespace CandlesAPI.Models
{
    public record Candle
    {
        public int Id { get; init; }
        [Required]
        public string Name { get; init; }
        [Required]
        public string Scent { get; init; }
        [Required]
        public int Price { get; init; }
        [Required]
        public int LightingTime { get; init; }

    }
}
