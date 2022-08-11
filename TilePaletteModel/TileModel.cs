using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TilePaletteModel
{
    public class TileModel
    {
        public string Name { get; set; }
        public bool IsWalkable { get; set; }
        public string MovementCost { get; set; }
        public string DefenceBonus { get; set; }
    }
}
