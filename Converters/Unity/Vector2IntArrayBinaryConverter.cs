#if UNITY_5_3_OR_NEWER

using System;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AleVerDes.BinarySerialization.Converters
{
    public class Vector2IntArrayBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(Vector2Int[]);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Serialize(object value, BinaryWriter bw)
        {
            var typedValue = (Vector2Int[])value;
            var length = typedValue.Length;
            bw.Write(BitConverter.GetBytes(length));
            for (var i = 0; i < length; i++)
            {
                bw.Write(BitConverter.GetBytes(typedValue[i].x));
                bw.Write(BitConverter.GetBytes(typedValue[i].y));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object Deserialize(BinaryReader br)
        {
            var arrayLength = br.ReadInt32();
            var typedValue = new Vector2Int[arrayLength];
            for (var i = 0; i < arrayLength; i++)
            {
                typedValue[i] = new Vector2Int(br.ReadInt32(), br.ReadInt32());
            }
            return typedValue;
        }
    }
}

#endif