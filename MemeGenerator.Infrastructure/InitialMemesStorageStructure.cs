﻿using System.Collections.Generic;
using MemeGenerator.Core.Entities;

namespace MemeGenerator.Infrastructure
{
    public class InitialMemesStorageStructure
    {
        public string Folder { get; set; }
        public List<MemeImage> MemeImages { get; set; }
    }
}
