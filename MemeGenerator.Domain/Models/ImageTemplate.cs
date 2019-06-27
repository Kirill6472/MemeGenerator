namespace MemeGenerator.Domain.Models
{
    public class ImageTemplate
    {
        public int ImageTemplateId { get; set; }
        public string Folder { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
