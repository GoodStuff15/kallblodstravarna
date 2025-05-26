using resortlibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restortlibrary.Factories.IFactories
{
    public interface IAdditionalOptionFactory
    {
        AdditionalOption CreateAdditionalOption(string name, string description, decimal price);
    }
}
