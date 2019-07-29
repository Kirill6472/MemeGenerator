using System.Collections.Generic;
using System.Linq;
using MemeGenerator.DAL;
using MemeGenerator.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MemeGenerator.UI.Controllers
{
    public class MemeImageController : Controller
    {
        private readonly MemeGeneratorDbContext _context;

        public MemeImageController(MemeGeneratorDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<MemePreview> Get()
        {
            var memes = _context.MemeImages.ToList();
            List<MemePreview> memePreviews = new List<MemePreview>(memes.Count);

            foreach (var meme in memes)
            {
                memePreviews.Add(new MemePreview(meme));
            }

            return memePreviews;
        }
    }
}