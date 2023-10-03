#if UNITY_5_3_OR_NEWER

using System;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AleVerDes.BinarySerialization.Converters
{
    public class ColorArrayBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(Color[]);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Serialize(object value, BinaryWriter bw)
        {
            var typedValue = (Color[])value;
            var length = typedValue.Length;
            bw.Write(BitConverter.GetBytes(length));
            for (var i = 0; i < length; i++)
            {
                bw.Write(BitConverter.GetBytes(typedValue[i].r));
                bw.Write(BitConverter.GetBytes(typedValue[i].g));
                bw.Write(BitConverter.GetBytes(typedValue[i].b));
                bw.Write(BitConverter.GetBytes(typedValue[i].a));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object Deserialize(BinaryReader br)
        {
            var arrayLength = br.ReadInt32();
            var typedValue = new Color[arrayLength];
            for (var i = 0; i < arrayLength; i++)
            {
                typedValue[i] = new Color(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
            }
            return typedValue;
        }
    }
}

#endif