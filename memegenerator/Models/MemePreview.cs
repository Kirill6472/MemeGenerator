using MemeGenerator.Core.Entities;

namespace MemeGenerator.UI.Models
{
    public class MemePreview
    {
        public MemePreview(MemeImage meme)
        {
            Id = meme.Id;
            Image = meme.Data;
            Name = meme.Name;
        }

        public int Id { get; set; }
        
        public string Image { get; set; }
        
        public string Name { get; set; }        
    }
}
