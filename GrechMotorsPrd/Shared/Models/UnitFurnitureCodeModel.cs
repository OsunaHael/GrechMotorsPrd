using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrechMotorsPrd.Shared.Models
{
    public class UnitFurnitureCodeModel
    {
        public int id { get; set; }
        public int unit_id { get; set; }
        public int furniture_id { get; set; }
        public string? qr_code_number { get; set; }

        public UnitModel? Unit { get; set; }
        public FurnitureModel? Furniture { get; set; }
    }
}
