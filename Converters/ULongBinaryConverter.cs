using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace AleVerDes.BinarySerialization.Converters
{
    public class ULongBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(ulong);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Serialize(object value, BinaryWriter bw)
        {
            bw.Write(BitConverter.GetBytes((ulong) value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object Deserialize(BinaryReader br)
        {
            return br.ReadUInt64();
        }
    }
}