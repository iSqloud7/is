﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Domain.Models.Domain.DTO
{
    public class AddBookToCartDTO
    {
        public Guid BookID { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
