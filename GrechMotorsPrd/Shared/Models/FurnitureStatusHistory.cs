using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrechMotorsPrd.Shared.Models
{
    public class FurnitureStatusHistory
    {
        int id { get; set; }
        int unit_id { get; set; }
        int furniture_id { get; set; }
        string? furniture_status { get; set; }
        DateTime created_at { get; set; } = DateTime.Now;
        public FurnitureModel? Furniture { get; set; }
        public UnitModel? Unit { get; set; }
    }
}
