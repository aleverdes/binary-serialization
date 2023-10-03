using System;
using System.IO;

namespace AleVerDes.BinarySerialization.Converters
{
    public class SByteArrayBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(sbyte[]);

        public void Serialize(object value, BinaryWriter bw)
        {
            var typedValue = (sbyte[])value;
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
            var typedValue = new sbyte[arrayLength];
            for (var i = 0; i < arrayLength; i++)
            {
                typedValue[i] = br.ReadSByte();
            }
            return typedValue;
        }
    }
}