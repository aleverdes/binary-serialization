using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace AleVerDes.BinarySerialization.Converters
{
    public class EnumBinaryConverter : IBinaryConverter
    {
        public Type SerializationType { get; }

        public EnumBinaryConverter(Type enumType)
        {
            SerializationType = enumType;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Serialize(object value, BinaryWriter bw)
        {
            bw.Write(BitConverter.GetBytes( (int)value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object Deserialize(BinaryReader br)
        {
            var enumValue = br.ReadInt32();
            return Convert.ChangeType(enumValue, Enum.GetUnderlyingType(SerializationType));
        }
    }
}