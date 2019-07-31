using System.Collections.Generic;
using System.Linq;
using MemeGenerator.DAL;
using MemeGenerator.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MemeGenerator.UI.Controllers
{
    [Route("api/memes")]
    public class MemeImageController : Controller
    {
        private readonly MemeGeneratorDbContext _context;

        public MemeImageController(MemeGeneratorDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<MemePreview> GetMemes()
        {
            var memes = _context.MemeImages.ToList();
            List<MemePreview> memePreviews = new List<MemePreview>(memes.Count);

            foreach (var meme in memes)
            {
                memePreviews.Add(new MemePreview(meme));
            }

            return memePreviews;
        }

        [HttpGet("{id}")]
        public MemePreview Get(int id)
        {
            var meme = _context.MemeImages.FirstOrDefault(x => x.Id == id);
            var memePreview = new MemePreview(meme);

            return memePreview;
        }
    }
}