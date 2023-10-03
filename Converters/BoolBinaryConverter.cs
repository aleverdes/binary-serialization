using System;
using System.IO;

namespace AleVerDes.BinarySerialization.Converters
{
    public class BoolBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(bool);

        public void Serialize(object value, BinaryWriter bw)
        {
            bw.Write((bool) value);
        }

        public object Deserialize(BinaryReader br)
        {
            return br.ReadBoolean();
        }
    }
}