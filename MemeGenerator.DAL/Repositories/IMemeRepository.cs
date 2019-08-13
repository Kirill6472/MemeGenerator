﻿using System.Collections.Generic;
using MemeGenerator.Domain.Entities;
using MemeGenerator.Domain.Models;

namespace MemeGenerator.DAL.Repositories
{
    public interface IMemeRepository
    {
        IEnumerable<MemeImage> GetAll();
        int Count();
        void Insert(MemeImage meme);
        void Save();
        IEnumerable<MemeImage> GetPage(PageRequest request);
    }
}
