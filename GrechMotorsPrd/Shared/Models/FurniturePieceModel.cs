using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrechMotorsPrd.Shared.Models
{
    public class FurniturePieceModel
    {
        // Identificador único de la relación
        public int id { get; set; }

        // Clave foránea que referencia al ID del mueble asociado
        public int furniture_id { get; set; }

        // Clave foránea que referencia al ID de la pieza asociada
        public int piece_id { get; set; }
        
        // Cantidad de piezas necesarias para el mueble
        public int quantity { get; set; }

        // Propiedad de navegación que permite acceder al mueble asociado
        public FurnitureModel? Furniture { get; set; }

        // Propiedad de navegación que permite acceder a la pieza asociada
        public PieceModel? Piece { get; set; }
    }
}