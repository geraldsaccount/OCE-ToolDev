using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WpfApp1.TilePalette
{
    public interface IBinarySerializable
    {
        public void Serialize(ref BinaryWriter writer);
        public void Deserialize(BinaryReader reader);
    }
}
