using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restortlibrary.Converters
{
    public interface IConverter<T, V>
    {
        public T FromDTOtoObject(V dto);

        public V FromObjecttoDTO(T obj);

    }
}
