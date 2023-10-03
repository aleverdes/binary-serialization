using System;
using System.IO;

namespace AleVerDes.BinarySerialization.Converters
{
    public class UIntBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(uint);

        public void Serialize(object value, BinaryWriter bw)
        {
            bw.Write(BitConverter.GetBytes((uint) value));
        }

        public object Deserialize(BinaryReader br)
        {
            return br.ReadUInt32();
        }
    }
}