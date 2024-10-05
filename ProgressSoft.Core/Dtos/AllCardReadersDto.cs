﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressSoft.Core.Dtos
{
    public class AllCardReadersDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte[] Photo { get; set; } // Base64 encoded
        public string Address { get; set; }
    }
}
