﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;

namespace Client.Pages.SilverJewelries
{
    public class DetailsModel : PageModel
    {
        private readonly BussinessObject.Models.SilverJewelry2023DbContext _context;

        public DetailsModel(BussinessObject.Models.SilverJewelry2023DbContext context)
        {
            _context = context;
        }

      public SilverJewelry SilverJewelry { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.SilverJewelries == null)
            {
                return NotFound();
            }

            var silverjewelry = await _context.SilverJewelries.FirstOrDefaultAsync(m => m.SilverJewelryId == id);
            if (silverjewelry == null)
            {
                return NotFound();
            }
            else 
            {
                SilverJewelry = silverjewelry;
            }
            return Page();
        }
    }
}
