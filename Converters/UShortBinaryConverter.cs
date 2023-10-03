using System;
using System.IO;

namespace AleVerDes.BinarySerialization.Converters
{
    public class UShortBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(ushort);

        public void Serialize(object value, BinaryWriter bw)
        {
            bw.Write(BitConverter.GetBytes((ushort) value));
        }

        public object Deserialize(BinaryReader br)
        {
            return br.ReadUInt16();
        }
    }
}