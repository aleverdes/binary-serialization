using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace AleVerDes.BinarySerialization.Converters
{
    public class StringBinaryConverter : IBinaryConverter
    {
        public Type SerializationType => typeof(string);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Serialize(object value, BinaryWriter bw)
        {
            var chars = ((string) value).ToCharArray();
            var length = chars.Length;
            bw.Write(BitConverter.GetBytes(length));
            for (var i = 0; i < length; i++)
            {
                bw.Write(chars[i]);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object Deserialize(BinaryReader br)
        {
            var length = br.ReadInt32();
            return new string(br.ReadChars(length));
        }
    }
}