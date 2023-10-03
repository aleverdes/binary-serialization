#if UNITY_5_3_OR_NEWER

using System;
using System.IO;
using UnityEngine;

namespace AleVerDes.BinarySerialization.Converters
{
    public class QuaternionArrayBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(Quaternion[]);

        public void Serialize(object value, BinaryWriter bw)
        {
            var typedValue = (Quaternion[])value;
            var length = typedValue.Length;
            bw.Write(BitConverter.GetBytes(length));
            for (var i = 0; i < length; i++)
            {
                bw.Write(BitConverter.GetBytes(typedValue[i].x));
                bw.Write(BitConverter.GetBytes(typedValue[i].y));
                bw.Write(BitConverter.GetBytes(typedValue[i].z));
                bw.Write(BitConverter.GetBytes(typedValue[i].w));
            }
        }

        public object Deserialize(BinaryReader br)
        {
            var arrayLength = br.ReadInt32();
            var typedValue = new Quaternion[arrayLength];
            for (var i = 0; i < arrayLength; i++)
            {
                typedValue[i] = new Quaternion(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
            }
            return typedValue;
        }
    }
}

#endif