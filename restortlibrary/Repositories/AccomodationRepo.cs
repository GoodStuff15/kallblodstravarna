using Microsoft.EntityFrameworkCore;
using restortlibrary.Data;
using restortlibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restortlibrary.Repositories
{
    public class AccomodationRepo : AbstractRepo<Accomodation>
    {
        public AccomodationRepo(ResortContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ICollection<Accomodation>> GetAvailableAsync(DateTime start, DateTime end)
        {
            var availableRooms = _context.Set<Booking>()
                    .Where(b => b.CheckIn < end && start < b.CheckOut)
                    .Select(a => a.Accomodation);

            return await availableRooms.ToListAsync();
        }
    }
}
