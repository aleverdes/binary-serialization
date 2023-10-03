using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace AleVerDes.BinarySerialization.Converters
{
    public class FloatBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(float);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Serialize(object value, BinaryWriter bw)
        {
            bw.Write(BitConverter.GetBytes((float) value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object Deserialize(BinaryReader br)
        {
            return br.ReadSingle();
        }
    }
}