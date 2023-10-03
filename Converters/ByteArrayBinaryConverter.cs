using System;
using System.IO;

namespace AleVerDes.BinarySerialization.Converters
{
    public class ByteArrayBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(byte[]);
        
        public void Serialize(object value, BinaryWriter bw)
        {
            var typedValue = (byte[])value;
            var length = typedValue.Length;
            bw.Write(BitConverter.GetBytes(length));
            for (var i = 0; i < length; i++)
            {
                bw.Write(typedValue[i]);
            }
        }

        public object Deserialize(BinaryReader br)
        {
            var arrayLength = br.ReadInt32();
            var typedValue = new byte[arrayLength];
            for (var i = 0; i < arrayLength; i++)
            {
                typedValue[i] = br.ReadByte();
            }
            return typedValue;
        }
    }
}