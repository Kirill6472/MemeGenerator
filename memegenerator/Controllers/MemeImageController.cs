﻿using System.Collections.Generic;
using System.Linq;
using MemeGenerator.Core;
using MemeGenerator.Core.Entities;
using MemeGenerator.Core.Models;
using MemeGenerator.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MemeGenerator.UI.Controllers
{
    [Route("api/memes")]
    public class MemeImageController : Controller
    {
        private readonly IMemeRepository _memeRepository;

        public MemeImageController(IMemeRepository memeRepository)
        {
            _memeRepository = memeRepository;
        }

        [HttpGet("{pageNumber}/{pageSize}")]
        public IEnumerable<MemePreview> Get(int pageNumber, int pageSize)
        {
            var request = new PageRequest<MemeImage>(pageNumber, pageSize, x => x.Id);

            return _memeRepository.GetPage(request).Select(meme => new MemePreview(meme)).ToList();
        }
    }
}