using System.ComponentModel.DataAnnotations;

namespace MemeGenerator.Domain.Entities
{
    public class MemeImage
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Data { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
