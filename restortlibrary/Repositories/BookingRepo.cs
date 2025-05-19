using restortlibrary.Data;
using restortlibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restortlibrary.Repositories
{
    public class BookingRepo : AbstractRepo<Booking>
    {
        public BookingRepo(ResortContext context) : base(context)
        {
            _context = context;
        }
    }
}
