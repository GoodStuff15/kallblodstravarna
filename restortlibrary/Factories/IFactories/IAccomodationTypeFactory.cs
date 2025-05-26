using resortlibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restortlibrary.Factories.IFactories
{
    public interface IAccomodationTypeFactory
    {
        AccomodationType CreateAccomodationType(string name, decimal basePrice);
    }
}
