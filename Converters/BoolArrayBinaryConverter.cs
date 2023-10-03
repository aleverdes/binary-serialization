using System;
using System.IO;

namespace AleVerDes.BinarySerialization.Converters
{
    public class BoolArrayBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(bool[]);

        public void Serialize(object value, BinaryWriter bw)
        {
            var typedValue = (bool[])value;
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
            var typedValue = new bool[arrayLength];
            for (var i = 0; i < arrayLength; i++)
            {
                typedValue[i] = br.ReadBoolean();
            }
            return typedValue;
        }
    }
}