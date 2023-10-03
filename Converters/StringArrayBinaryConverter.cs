using System;
using System.IO;

namespace AleVerDes.BinarySerialization.Converters
{
    public class StringArrayBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(string[]);

        public void Serialize(object value, BinaryWriter bw)
        {
            var typedValue = (string[]) value;

            var arrayLength = typedValue.Length;
                    
            bw.Write(BitConverter.GetBytes(arrayLength));

            for (var j = 0; j < arrayLength; j++)
            {
                var chars = typedValue[j].ToCharArray();
                var length = chars.Length;
                bw.Write(BitConverter.GetBytes(length));
                for (var i = 0; i < length; i++)
                {
                    bw.Write(chars[i]);
                }
            }
        }

        public object Deserialize(BinaryReader br)
        {
            var arrayLength = br.ReadInt32();
            var typedValue = new string[arrayLength];
            for (var i = 0; i < arrayLength; i++)
            {
                var stringLength = br.ReadInt32();
                typedValue[i] = new string(br.ReadChars(stringLength));
            }
            return typedValue;
        }
    }
}