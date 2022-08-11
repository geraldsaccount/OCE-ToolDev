using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.TilePalette.ViewModels
{
    public class Tile : NotifiableObjectBase, IBinarySerializable
    {
        private string name;

        public Tile(BinaryReader reader)
        {
            Deserialize(reader);
        }

        public Tile() {
            Name = "";
            IsWalkable = false;
            MovementCost = "";
            DefenceBonus = "";
        }

        public string Name {
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

        public void Serialize(ref BinaryWriter writer)
        {
            writer.Write(Name);
            writer.Write(IsWalkable);
            writer.Write(MovementCost);
            writer.Write(DefenceBonus);
        }

        public void Deserialize(BinaryReader reader)
        {
            Name = reader.ReadString();
            IsWalkable = reader.ReadBoolean();
            MovementCost = reader.ReadString();
            DefenceBonus = reader.ReadString();
        }
    }
}
