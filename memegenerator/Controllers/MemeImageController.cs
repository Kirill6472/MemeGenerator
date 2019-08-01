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

        [HttpGet("{pageNumber}/{pageSize}")]
        public IEnumerable<MemePreview> Get(int pageNumber, int pageSize)
        {
            var skip = (pageNumber - 1) * pageSize;
            var memes = _context.MemeImages.AsQueryable().OrderBy(x => x.Id).Skip(skip).Take(pageSize);

            return memes.Select(meme => new MemePreview(meme)).ToList();
        }
    }
}