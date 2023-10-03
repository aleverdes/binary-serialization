#if UNITY_5_3_OR_NEWER

using System;
using System.IO;
using UnityEngine;

namespace AleVerDes.BinarySerialization.Converters
{
    public class RectBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(Rect);

        public void Serialize(object value, BinaryWriter bw)
        {
            var typedValue = (Rect) value;
            bw.Write(BitConverter.GetBytes(typedValue.x));
            bw.Write(BitConverter.GetBytes(typedValue.y));
            bw.Write(BitConverter.GetBytes(typedValue.width));
            bw.Write(BitConverter.GetBytes(typedValue.height));
        }

        public object Deserialize(BinaryReader br)
        {
            return new Rect(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
        }
    }
}

#endif