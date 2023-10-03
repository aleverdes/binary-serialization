#if UNITY_5_3_OR_NEWER

using System;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AleVerDes.BinarySerialization.Converters
{
    public class ColorBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(Color);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Serialize(object value, BinaryWriter bw)
        {
            var typedValue = (Color) value;
            bw.Write(BitConverter.GetBytes(typedValue.r));
            bw.Write(BitConverter.GetBytes(typedValue.g));
            bw.Write(BitConverter.GetBytes(typedValue.b));
            bw.Write(BitConverter.GetBytes(typedValue.a));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object Deserialize(BinaryReader br)
        {
            return new Color(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
        }
    }
}

#endif