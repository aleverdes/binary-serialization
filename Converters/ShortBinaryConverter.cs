using System;
using System.IO;

namespace AleVerDes.BinarySerialization.Converters
{
    public class ShortBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(short);

        public void Serialize(object value, BinaryWriter bw)
        {
            bw.Write(BitConverter.GetBytes((short) value));
        }

        public object Deserialize(BinaryReader br)
        {
            return br.ReadInt16();
        }
    }
}