using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.TilePalette
{
    public class Tile : NotifiableObjectBase
    {
        private string name;

        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    RaisePropertyChanged();
                }
            }
        }
        public bool IsWalkable { get; set; }
        public string MovementCost { get; set; }
        public string DefenceBonus { get; set; }
    }
}
