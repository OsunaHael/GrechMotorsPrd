using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrechMotorsPrd.Shared.Models
{
    public class EditRolModel
    {
        public int user_id { get; set; }
        public string role { get; set; } = null!;
    }
}
