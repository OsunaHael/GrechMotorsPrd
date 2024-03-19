using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrechMotorsPrd.Shared.Models
{
    public class UnitModel
    {
        public int id { get; set; }
        public int g_number { get; set; }
        public int user_id { get; set; }
        public string? color { get; set; }
        public string? opt { get; set; }
        public int line { get; set; }
        public int ext { get; set; }
        public string? model { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime deleted_at { get; set; }
        public DateTime cut_off_date { get; set; } = DateTime.Today;
        public DateTime updated_at { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }

        // Propiedad de navegación
        public UserModel? User { get; set; } // Relación muchos a uno con UserModel
        public List<UnitFurnitureModel>? UnitFurnitures { get; set; } // Propiedad de navegación para la relación muchos a muchos con UnitFurnitureModel
    }
}
