#if UNITY_5_3_OR_NEWER

using System;
using System.IO;
using UnityEngine;

namespace AleVerDes.BinarySerialization.Converters
{
    public class Vector2IntBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(Vector2Int);

        public void Serialize(object value, BinaryWriter bw)
        {
            var typedValue = (Vector2Int) value;
            bw.Write(BitConverter.GetBytes(typedValue.x));
            bw.Write(BitConverter.GetBytes(typedValue.y));
        }

        public object Deserialize(BinaryReader br)
        {
            return new Vector2Int(br.ReadInt32(), br.ReadInt32());
        }
    }
}

#endif