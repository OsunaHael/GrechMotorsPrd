using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrechMotorsPrd.Shared.Models
{
    public class PieceModel
    {
        public int id { get; set; }
        public int piece_number { get; set; }
        public string? piece_status { get; set; }
        public string? comments { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime updated_at { get; set; }
        public DateTime deleted_at { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }

        // Propiedades de navegación
        public List<FurniturePieceModel>? FurniturePieces { get; set; } //Relacion Muchos a Muchos con FurnitureModel
        public List<UserPieceModel>? UserPieces { get; set; } //Relacion Muchos a Muchos con UserModel
        public List<UnitPieceCodeModel>? UnitPiecesCodes { get; set; } //Relacion Muchos a Muchos con UnitModel
        public List<PieceStatusHistory>? PieceStatusHistories { get; set; } //Relacion Uno a Muchos con PieceStatusHistory
    }
}