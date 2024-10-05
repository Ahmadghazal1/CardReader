﻿using Microsoft.AspNetCore.Http;

namespace ProgressSoft.Core.Dtos
{
    public class CreateCardReaderDto
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public IFormFile Photo { get; set; } // Base64 encoded
        public string Address { get; set; }
    }
}
