using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrechMotorsPrd.Shared.Models
{
    public class UserModel
    {
        public int id { get; set; }
        public int identityUserId { get; set; }
        public string? user_number { get; set; }
        public string? username { get; set; }
        public string? pwd { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime updated_at { get; set; }
        public DateTime deleted_at { get; set; }

        // Propiedades de navegación
        public List<UnitModel>? Unit { get; set; } // Relación uno a muchos con UnitModel
        public List<UserPieceModel>? UserPieces { get; set; } // Propiedad de navegación para la relación muchos a muchos con UserPieceModel
        public List<UserFurnitureModel>? UserFurnitures { get; set; } // Propiedad de navegación para la relación muchos a muchos con UserFurnitureModel
        public List<PieceStatusHistory>? PieceStatusHistories { get; set; } // Propiedad de navegación para la relación uno a muchos con PieceStatusHistory
        public List<FurnitureStatusHistory>? FurnitureStatusHistories { get; set; } // Propiedad de navegación para la relación uno a muchos con FurnitureStatusHistory
    }
}