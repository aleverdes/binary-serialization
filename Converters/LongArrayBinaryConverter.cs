using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace AleVerDes.BinarySerialization.Converters
{
    public class LongArrayBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(long[]);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Serialize(object value, BinaryWriter bw)
        {
            var typedValue = (long[])value;
            var length = typedValue.Length;
            bw.Write(BitConverter.GetBytes(length));
            for (var i = 0; i < length; i++)
            {
                bw.Write(BitConverter.GetBytes(typedValue[i]));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object Deserialize(BinaryReader br)
        {
            var arrayLength = br.ReadInt32();
            var typedValue = new long[arrayLength];
            for (var i = 0; i < arrayLength; i++)
            {
                typedValue[i] = br.ReadInt64();
            }
            return typedValue;
        }
    }
}