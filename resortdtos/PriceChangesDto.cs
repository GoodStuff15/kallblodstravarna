
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resortdtos

{
    public class PriceChangesDto
    {
        public int Id { get; set; }
        public float PriceChange { get; set; }
        public string Type { get; set; } = string.Empty;
    }


}

