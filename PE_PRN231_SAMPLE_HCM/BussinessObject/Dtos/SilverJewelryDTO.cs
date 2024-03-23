using System.ComponentModel.DataAnnotations;

namespace PE_PRN231_SAMPLE_HCM.Dtos
{
    public class SilverJewelryDTO
    {
        public class CreateSilverJewelryDTO
        {
            [Required(ErrorMessage = "SilverJewelryId is required")]
            public string SilverJewelryId { get; set; } = null!;

            [Required(ErrorMessage = "SilverJewelryName is required")]
            [RegularExpression(@"^([A-Z][a-zA-Z0-9]*\s*)+$", ErrorMessage = "SilverJewelryName must start with a capital letter and contain only letters, digits, and spaces")]
            public string SilverJewelryName { get; set; } = null!;

            public string? SilverJewelryDescription { get; set; }

            [Required(ErrorMessage = "MetalWeight is required")]
            public decimal? MetalWeight { get; set; }

            [Required(ErrorMessage = "Price is required")]
            [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 0")]
            public decimal? Price { get; set; }

            [Required(ErrorMessage = "ProductionYear is required")]
            [Range(1900, int.MaxValue, ErrorMessage = "ProductionYear must be greater than or equal to 1900")]
            public int? ProductionYear { get; set; }

            public DateTime? CreatedDate { get; set; }

            public string? CategoryId { get; set; }
        }
        public class GetSilverJewelryDTO
        {
            public string SilverJewelryId { get; set; } = null!;

            public string SilverJewelryName { get; set; } = null!;

            public string? SilverJewelryDescription { get; set; }

            public decimal? MetalWeight { get; set; }

            public decimal? Price { get; set; }

            public int? ProductionYear { get; set; }
            public DateTime? CreatedDate { get; set; }

            public string? CategoryId { get; set; }
            public string? CategoryName { get; set; }
        }
    }
}
