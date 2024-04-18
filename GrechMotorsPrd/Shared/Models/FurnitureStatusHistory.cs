using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrechMotorsPrd.Shared.Models
{
    public class FurnitureStatusHistory
    {
        public int id { get; set; }
        public int furniture_id { get; set; }
        public int unit_id { get; set; }
        public int user_id { get; set; }
        public string? furniture_status { get; set; }
        public DateTime updated_at { get; set; } = DateTime.Now;
        public FurnitureModel? Furniture { get; set; }
        public UnitModel? Unit { get; set; }
        public UserModel? User { get; set; }
    }
}