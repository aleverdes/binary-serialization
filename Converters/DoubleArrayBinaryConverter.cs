using System;
using System.IO;

namespace AleVerDes.BinarySerialization.Converters
{
    public class DoubleArrayBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(double[]);

        public void Serialize(object value, BinaryWriter bw)
        {
            var typedValue = (double[])value;
            var length = typedValue.Length;
            bw.Write(BitConverter.GetBytes(length));
            for (var i = 0; i < length; i++)
            {
                bw.Write(BitConverter.GetBytes(typedValue[i]));
            }
        }

        public object Deserialize(BinaryReader br)
        {
            var arrayLength = br.ReadInt32();
            var typedValue = new double[arrayLength];
            for (var i = 0; i < arrayLength; i++)
            {
                typedValue[i] = br.ReadDouble();
            }
            return typedValue;
        }
    }
}