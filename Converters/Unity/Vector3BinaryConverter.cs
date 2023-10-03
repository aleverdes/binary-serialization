#if UNITY_5_3_OR_NEWER

using System;
using System.IO;
using UnityEngine;

namespace AleVerDes.BinarySerialization.Converters
{
    public class Vector3BinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(Vector3);

        public void Serialize(object value, BinaryWriter bw)
        {
            var typedValue = (Vector3) value;
            bw.Write(BitConverter.GetBytes(typedValue.x));
            bw.Write(BitConverter.GetBytes(typedValue.y));
            bw.Write(BitConverter.GetBytes(typedValue.z));
        }

        public object Deserialize(BinaryReader br)
        {
            return new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
        }
    }
}

#endif