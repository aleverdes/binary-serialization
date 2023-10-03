using System;
using System.IO;

namespace AleVerDes.BinarySerialization.Converters
{
    public class SByteBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(sbyte);

        public void Serialize(object value, BinaryWriter bw)
        {
            bw.Write((sbyte) value);
        }

        public object Deserialize(BinaryReader br)
        {
            return br.ReadSByte();
        }
    }
}