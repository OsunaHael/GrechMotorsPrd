using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrechMotorsPrd.Shared.Models
{
    public class FurnitureModel
    {
        public int id { get; set; }
        public int furniture_number { get; set; }
        public string? qrIdentificationCode { get; set; }
        public string? furniture_status { get; set; }
        public string? comments { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime updated_at { get; set; }
        public DateTime deleted_at { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }

        // Propiedades de navegación
        public List<FurniturePieceModel>? FurniturePieces { get; set; } // Propiedad de navegación para la relación muchos a muchos con FurniturePieceModel
        public List<UnitFurnitureModel>? UnitFurnitures { get; set; } // Propiedad de navegación para la relación muchos a muchos con FurniturePieceModel
        public List<UserFurnitureModel>? UserFurnitures { get; set; } // Propiedad de navegación para la relación muchos a muchos con FurniturePieceModel
        //public List<PieceModel>? Pieces { get; set;}
    }
}
