#if UNITY_5_3_OR_NEWER

using System;
using System.IO;
using UnityEngine;

namespace AleVerDes.BinarySerialization.Converters
{
    public class Vector4BinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(Vector4);

        public void Serialize(object value, BinaryWriter bw)
        {
            var typedValue = (Vector4) value;
            bw.Write(BitConverter.GetBytes(typedValue.x));
            bw.Write(BitConverter.GetBytes(typedValue.y));
            bw.Write(BitConverter.GetBytes(typedValue.z));
            bw.Write(BitConverter.GetBytes(typedValue.w));
        }

        public object Deserialize(BinaryReader br)
        {
            return new Vector4(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
        }
    }
}

#endif