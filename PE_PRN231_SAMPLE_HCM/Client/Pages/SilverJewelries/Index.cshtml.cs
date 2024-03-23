using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;

namespace Client.Pages.SilverJewelries
{
    public class IndexModel : PageModel
    {
        private readonly BussinessObject.Models.SilverJewelry2023DbContext _context;

        public IndexModel(BussinessObject.Models.SilverJewelry2023DbContext context)
        {
            _context = context;
        }

        public IList<SilverJewelry> SilverJewelry { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.SilverJewelries != null)
            {
                SilverJewelry = await _context.SilverJewelries
                .Include(s => s.Category).ToListAsync();
            }
        }
    }
}
