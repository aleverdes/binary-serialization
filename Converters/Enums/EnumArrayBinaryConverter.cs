using System;
using System.IO;

namespace AleVerDes.BinarySerialization.Converters.Extensions
{
    public class EnumArrayBinaryConverter : IBinaryConverter
    {
        public Type SerializationType { get; }

        private readonly EnumBinaryConverter _singleValueConverter;

        public EnumArrayBinaryConverter(Type enumType)
        {
            _singleValueConverter = new EnumBinaryConverter(enumType);
            SerializationType = enumType.MakeArrayType();
        }
        
        public void Serialize(object value, BinaryWriter bw)
        {
            var typedValue = (Array)value;
            var length = typedValue.Length;
            bw.Write(BitConverter.GetBytes(length));
            for (var i = 0; i < length; i++)
            {
                _singleValueConverter.Serialize(typedValue.GetValue(i), bw);
            }
        }

        public object Deserialize(BinaryReader br)
        {
            var arrayLength = br.ReadInt32();
            var typedValue = Array.CreateInstance(_singleValueConverter.SerializationType, arrayLength);
            for (var i = 0; i < arrayLength; i++)
            {
                typedValue.SetValue(_singleValueConverter.Deserialize(br), i);
            }
            return typedValue;
        }
    }
}