using System;
using System.IO;

namespace AleVerDes.BinarySerialization.Converters
{
    public class IntBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(int);

        public void Serialize(object value, BinaryWriter bw)
        {
            bw.Write(BitConverter.GetBytes((int) value));
        }

        public object Deserialize(BinaryReader br)
        {
            return br.ReadInt32();
        }
    }
}