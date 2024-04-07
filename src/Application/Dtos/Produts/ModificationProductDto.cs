﻿using Microsoft.AspNetCore.Http;

namespace Application.Dtos.Produts
{
    public class ModificationProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile? Videopath { get; set; }
        public int SortNumber { get; set; }
    }
}
