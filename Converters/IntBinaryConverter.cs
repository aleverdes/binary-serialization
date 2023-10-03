using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace AleVerDes.BinarySerialization.Converters
{
    public class IntBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(int);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Serialize(object value, BinaryWriter bw)
        {
            bw.Write(BitConverter.GetBytes((int) value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object Deserialize(BinaryReader br)
        {
            return br.ReadInt32();
        }
    }
}