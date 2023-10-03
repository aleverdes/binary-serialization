#if UNITY_5_3_OR_NEWER

using System;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AleVerDes.BinarySerialization.Converters
{
    public class RectIntArrayBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(RectInt[]);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Serialize(object value, BinaryWriter bw)
        {
            var typedValue = (RectInt[])value;
            var length = typedValue.Length;
            bw.Write(BitConverter.GetBytes(length));
            for (var i = 0; i < length; i++)
            {
                bw.Write(BitConverter.GetBytes(typedValue[i].x));
                bw.Write(BitConverter.GetBytes(typedValue[i].y));
                bw.Write(BitConverter.GetBytes(typedValue[i].width));
                bw.Write(BitConverter.GetBytes(typedValue[i].height));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object Deserialize(BinaryReader br)
        {
            var arrayLength = br.ReadInt32();
            var typedValue = new RectInt[arrayLength];
            for (var i = 0; i < arrayLength; i++)
            {
                typedValue[i] = new RectInt(br.ReadInt32(), br.ReadInt32(), br.ReadInt32(), br.ReadInt32());
            }
            return typedValue;
        }
    }
}

#endif