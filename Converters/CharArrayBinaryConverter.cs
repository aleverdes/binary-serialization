using System;
using System.IO;

namespace AleVerDes.BinarySerialization.Converters
{
    public class CharArrayBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(char[]);

        public void Serialize(object value, BinaryWriter bw)
        {
            var typedValue = (char[])value;
            var length = typedValue.Length;
            bw.Write(BitConverter.GetBytes(length));
            for (var i = 0; i < length; i++)
            {
                bw.Write(typedValue[i]);
            }
        }

        public object Deserialize(BinaryReader br)
        {
            var length = br.ReadInt32();
            return br.ReadChars(length);
        }
    }
}