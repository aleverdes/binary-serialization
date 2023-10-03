using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace AleVerDes.BinarySerialization.Converters
{
    public class LongBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(long);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Serialize(object value, BinaryWriter bw)
        {
            bw.Write(BitConverter.GetBytes((long) value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object Deserialize(BinaryReader br)
        {
            return br.ReadInt64();
        }
    }
}