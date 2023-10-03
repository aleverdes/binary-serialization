#if UNITY_5_3_OR_NEWER

using System;
using System.IO;
using UnityEngine;

namespace AleVerDes.BinarySerialization.Converters
{
    public class RectIntBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(RectInt);

        public void Serialize(object value, BinaryWriter bw)
        {
            var typedValue = (RectInt) value;
            bw.Write(BitConverter.GetBytes(typedValue.x));
            bw.Write(BitConverter.GetBytes(typedValue.y));
            bw.Write(BitConverter.GetBytes(typedValue.width));
            bw.Write(BitConverter.GetBytes(typedValue.height));
        }

        public object Deserialize(BinaryReader br)
        {
            return new RectInt(br.ReadInt32(), br.ReadInt32(), br.ReadInt32(), br.ReadInt32());
        }
    }
}

#endif