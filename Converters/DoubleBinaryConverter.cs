using System;
using System.IO;

namespace AleVerDes.BinarySerialization.Converters
{
    public class DoubleBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(double);

        public void Serialize(object value, BinaryWriter bw)
        {
            bw.Write(BitConverter.GetBytes((double) value));
        }

        public object Deserialize(BinaryReader br)
        {
            return br.ReadDouble();
        }
    }
}