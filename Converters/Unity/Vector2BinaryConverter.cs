#if UNITY_5_3_OR_NEWER

using System;
using System.IO;
using UnityEngine;

namespace AleVerDes.BinarySerialization.Converters
{
    public class Vector2BinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(Vector2);

        public void Serialize(object value, BinaryWriter bw)
        {
            var typedValue = (Vector2) value;
            bw.Write(BitConverter.GetBytes(typedValue.x));
            bw.Write(BitConverter.GetBytes(typedValue.y));
        }

        public object Deserialize(BinaryReader br)
        {
            return new Vector2(br.ReadSingle(), br.ReadSingle());
        }
    }
}

#endif