using System;
using System.IO;

namespace AleVerDes.BinarySerialization.Converters
{
    public class ULongBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(ulong);

        public void Serialize(object value, BinaryWriter bw)
        {
            bw.Write(BitConverter.GetBytes((ulong) value));
        }

        public object Deserialize(BinaryReader br)
        {
            return br.ReadUInt64();
        }
    }
}