using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrechMotorsPrd.Shared.Models
{
    public class UnitFurnitureModel
    {
        // Identificador único de la relación
        public int id { get; set; }

        // Clave foránea que referencia al ID de la unidad al que pertenece este mueble
        public int unit_id { get; set; }

        // Clave foránea que referencia al ID del mueble asociado
        public int furniture_id { get; set; }

        // Propiedad de navegación que permite acceder al mueble asociado
        public FurnitureModel? Furniture { get; set; }

        // Propiedad de navegación que permite acceder a la unidad asociada
        public UnitModel? Unit { get; set; }
    }
}
