using System;
using System.IO;

namespace AleVerDes.BinarySerialization.Converters
{
    public class FloatBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(float);

        public void Serialize(object value, BinaryWriter bw)
        {
            bw.Write(BitConverter.GetBytes((float) value));
        }

        public object Deserialize(BinaryReader br)
        {
            return br.ReadSingle();
        }
    }
}