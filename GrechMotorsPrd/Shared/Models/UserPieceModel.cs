using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrechMotorsPrd.Shared.Models
{
    public class UserPieceModel
    {
        // Identificador único de la relación
        public int id { get; set; }

        // Clave foránea que referencia al ID del usuario asociado
        public int user_id { get; set; }

        // Clave foránea que referencia al ID de la pieza asociada
        public int piece_id { get; set; }

        // Propiedad de navegación que permite acceder al usuario asociado
        public UserModel? User { get; set; }

        // Propiedad de navegación que permite acceder a la pieza asociada
        public PieceModel? Piece { get; set; }
    }
}
