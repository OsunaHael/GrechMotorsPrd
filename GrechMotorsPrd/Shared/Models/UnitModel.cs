using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrechMotorsPrd.Shared.Models
{
    // Definición de la clase UnitModel
    public class UnitModel
    {
        // Propiedades de la unidad
        public int id { get; set; } // Identificador único de la unidad
        public int g_number { get; set; } // Número G de la unidad
        public int user_id { get; set; } // Identificador del usuario asociado a la unidad
        public string? color { get; set; } // Color de la unidad (puede ser nulo)
        public string? opt { get; set; } // Opción de la unidad (puede ser nulo)
        public int line { get; set; } // Línea de la unidad
        public int ext { get; set; } // Variable que indica si es extendida o no
        public string? model { get; set; } // Modelo de la unidad (puede ser nulo)
        public DateTime created_at { get; set; } = DateTime.Now; // Fecha de creación de la unidad (se establece por defecto a la fecha y hora actual)
        public DateTime deleted_at { get; set; } // Fecha de eliminación de la unidad
        public DateTime cut_off_date { get; set; } = DateTime.Today; // Fecha de corte de la unidad (se establece por defecto a la fecha actual)
        public DateTime updated_at { get; set; } // Fecha de actualización de la unidad
        public DateTime start_date { get; set; } // Fecha de inicio de la unidad
        public DateTime end_date { get; set; } // Fecha de finalización de la unidad

        // Propiedad de navegación
        public UserModel? User { get; set; } // Relación muchos a uno con UserModel
        public List<UnitFurnitureModel>? UnitFurnitures { get; set; } // Propiedad de navegación para la relación muchos a muchos con UnitFurnitureModel
        public List<UnitPieceCodeModel>? UnitPiecesCodes { get; set; } // Propiedad de navegación para la relación muchos a muchos con UnitPieceCodeModel
        public List<UnitFurnitureCodeModel>? UnitFurnituresCodes { get; set; } // Propiedad de navegación para la relación muchos a muchos con UnitFurnitureCodeModel
    }
}