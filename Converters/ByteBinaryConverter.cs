using System;
using System.IO;

namespace AleVerDes.BinarySerialization.Converters
{
    public class ByteBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(byte);

        public void Serialize(object value, BinaryWriter bw)
        {
            bw.Write((byte)value);
        }

        public object Deserialize(BinaryReader br)
        {
            return br.ReadByte();
        }
    }
}