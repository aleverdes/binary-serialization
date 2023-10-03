#if UNITY_5_3_OR_NEWER

using System;
using System.IO;
using UnityEngine;

namespace AleVerDes.BinarySerialization.Converters
{
    public class RectArrayBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(Rect[]);

        public void Serialize(object value, BinaryWriter bw)
        {
            var typedValue = (Rect[])value;
            var length = typedValue.Length;
            bw.Write(BitConverter.GetBytes(length));
            for (var i = 0; i < length; i++)
            {
                bw.Write(BitConverter.GetBytes(typedValue[i].x));
                bw.Write(BitConverter.GetBytes(typedValue[i].y));
                bw.Write(BitConverter.GetBytes(typedValue[i].width));
                bw.Write(BitConverter.GetBytes(typedValue[i].height));
            }
        }

        public object Deserialize(BinaryReader br)
        {
            var arrayLength = br.ReadInt32();
            var typedValue = new Rect[arrayLength];
            for (var i = 0; i < arrayLength; i++)
            {
                typedValue[i] = new Rect(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
            }
            return typedValue;
        }
    }
}

#endif