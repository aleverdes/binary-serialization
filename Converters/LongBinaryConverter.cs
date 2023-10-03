using System;
using System.IO;

namespace AleVerDes.BinarySerialization.Converters
{
    public class LongBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(long);

        public void Serialize(object value, BinaryWriter bw)
        {
            bw.Write(BitConverter.GetBytes((long) value));
        }

        public object Deserialize(BinaryReader br)
        {
            return br.ReadInt64();
        }
    }
}