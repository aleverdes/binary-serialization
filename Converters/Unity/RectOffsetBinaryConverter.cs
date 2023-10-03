#if UNITY_5_3_OR_NEWER

using System;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AleVerDes.BinarySerialization.Converters
{
    public class RectOffsetBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(RectOffset);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Serialize(object value, BinaryWriter bw)
        {
            var typedValue = (RectOffset) value;
            bw.Write(BitConverter.GetBytes(typedValue.left));
            bw.Write(BitConverter.GetBytes(typedValue.right));
            bw.Write(BitConverter.GetBytes(typedValue.top));
            bw.Write(BitConverter.GetBytes(typedValue.bottom));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object Deserialize(BinaryReader br)
        {
            return new RectOffset(br.ReadInt32(), br.ReadInt32(), br.ReadInt32(), br.ReadInt32());
        }
    }
}

#endif