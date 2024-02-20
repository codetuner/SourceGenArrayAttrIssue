using System.ComponentModel.DataAnnotations;

namespace SourceGen.SampleDomain
{
    public class Person
    {
        [Required]
        //[DeniedValues(0, 666)]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 chars.")]
        public string? Name { get; set; }
    }
}
