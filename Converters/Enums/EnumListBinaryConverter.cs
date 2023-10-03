using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace AleVerDes.BinarySerialization.Converters.Extensions
{
    public class EnumListBinaryConverter : IBinaryConverter
    {
        public Type SerializationType { get; }

        private readonly EnumBinaryConverter _singleValueConverter;

        public EnumListBinaryConverter(Type enumType)
        {
            _singleValueConverter = new EnumBinaryConverter(enumType);
            var listType = typeof(List<>);
            SerializationType = listType.MakeGenericType(enumType);
        }
        
        public void Serialize(object value, BinaryWriter bw)
        {
            var typedValue = (IList)value;
            var length = typedValue.Count;
            bw.Write(BitConverter.GetBytes(length));
            for (var i = 0; i < length; i++)
            {
                _singleValueConverter.Serialize(typedValue[i], bw);
            }
        }

        public object Deserialize(BinaryReader br)
        {
            var arrayLength = br.ReadInt32();
            var typedValue = (IList) Activator.CreateInstance(SerializationType);
            for (var i = 0; i < arrayLength; i++)
            {
                typedValue.Add(_singleValueConverter.Deserialize(br));
            }
            return typedValue;
        }
    }
}