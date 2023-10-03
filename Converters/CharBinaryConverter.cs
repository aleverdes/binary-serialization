using System;
using System.IO;

namespace AleVerDes.BinarySerialization.Converters
{
    public class CharBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(char);

        public void Serialize(object value, BinaryWriter bw)
        {
            bw.Write((char) value);
        }

        public object Deserialize(BinaryReader br)
        {
            return br.ReadChar();
        }
    }
}