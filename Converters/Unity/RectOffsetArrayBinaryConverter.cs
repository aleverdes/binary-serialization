#if UNITY_5_3_OR_NEWER

using System;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AleVerDes.BinarySerialization.Converters
{
    public class RectOffsetArrayBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(RectOffset[]);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Serialize(object value, BinaryWriter bw)
        {
            var typedValue = (RectOffset[])value;
            var length = typedValue.Length;
            bw.Write(BitConverter.GetBytes(length));
            for (var i = 0; i < length; i++)
            {
                bw.Write(BitConverter.GetBytes(typedValue[i].top));
                bw.Write(BitConverter.GetBytes(typedValue[i].right));
                bw.Write(BitConverter.GetBytes(typedValue[i].bottom));
                bw.Write(BitConverter.GetBytes(typedValue[i].left));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object Deserialize(BinaryReader br)
        {
            var arrayLength = br.ReadInt32();
            var typedValue = new RectOffset[arrayLength];
            for (var i = 0; i < arrayLength; i++)
            {
                typedValue[i] = new RectOffset(br.ReadInt32(), br.ReadInt32(), br.ReadInt32(), br.ReadInt32());
            }
            return typedValue;
        }
    }
}

#endif