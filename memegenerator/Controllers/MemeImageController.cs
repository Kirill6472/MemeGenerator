using System.Collections.Generic;
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
            var memes = _context.MemeImages;
            List<MemePreview> memePreviews = null;

            foreach (var meme in memes)
            {
                memePreviews = new List<MemePreview> { new MemePreview(meme) };
            }

            return memePreviews;
        }
    }
}