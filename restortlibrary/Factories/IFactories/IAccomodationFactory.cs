using restortlibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restortlibrary.Factories.IFactories
{
    public interface IAccomodationFactory
    {
        Accomodation CreateAccomodation(string? name, int maxOccupancy, AccomodationType accomodationType);
    }
}
