using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AleVerDes.BinarySerialization.Converters;
using AleVerDes.BinarySerialization.Converters.Extensions;
using UnityEngine;

namespace AleVerDes.BinarySerialization
{
    public static class BinarySerializer
    {
        private static readonly TypeIndexDictionary CachedTypes = new();
        private static readonly Dictionary<Type, FieldInfo[]> CachedFieldInfos = new();

        private static readonly Dictionary<Type, Action<object, BinaryWriter>> SerializeMethods = new();
        private static readonly Dictionary<Type, Func<BinaryReader, object>> DeserializeMethods = new();

        private static int _typeIndex;

        static BinarySerializer()
        {
            Reset();
        }

        #region Public methods: Types registration

        public static void RegisterType<T>() where T : class, new()
        {
            RegisterType(typeof(T));
        }
        
        public static void RegisterType(Type type)
        {
            RegisterType(type, _typeIndex);
            _typeIndex++;
        }

        public static void RegisterType<T>(int typeIndex) where T : class, new()
        {
            RegisterType(typeof(T), typeIndex);
        }
        
        public static void RegisterType(Type type, int typeIndex)
        {
            CachedTypes.AddType(type, typeIndex);
            InitializeFieldInfos(type);
        }
        
        #endregion

        #region Public methods: Registration of the serialization and deserialization methods

        public static void AddConverter(IBinaryConverter converter)
        {
            SerializeMethods[converter.SerializationType] = converter.Serialize;
            DeserializeMethods[converter.SerializationType] = converter.Deserialize;
        }

        public static void AddConverterWithExtensions<T>() where T : class, IBinaryConverter, new()
        {
            AddConverter(new T());
            AddConverter(new ArrayBinaryConverter<T>());
            AddConverter(new ListBinaryConverter<T>());
        }
        
        public static void AddDefaultConverters()
        {
            AddConverterWithExtensions<BoolBinaryConverter>();
            AddConverterWithExtensions<ByteBinaryConverter>();
            AddConverterWithExtensions<CharBinaryConverter>();
            AddConverterWithExtensions<DoubleBinaryConverter>();
            AddConverterWithExtensions<FloatBinaryConverter>();
            AddConverterWithExtensions<IntBinaryConverter>();
            AddConverterWithExtensions<LongBinaryConverter>();
            AddConverterWithExtensions<SByteBinaryConverter>();
            AddConverterWithExtensions<ShortBinaryConverter>();
            AddConverterWithExtensions<StringBinaryConverter>();
            AddConverterWithExtensions<UIntBinaryConverter>();
            AddConverterWithExtensions<ULongBinaryConverter>();
            AddConverterWithExtensions<UShortBinaryConverter>();

#if UNITY_5_3_OR_NEWER
            AddConverterWithExtensions<ColorBinaryConverter>();
            AddConverterWithExtensions<QuaternionBinaryConverter>();
            AddConverterWithExtensions<RectBinaryConverter>();
            AddConverterWithExtensions<RectIntBinaryConverter>();
            AddConverterWithExtensions<RectOffsetBinaryConverter>();
            AddConverterWithExtensions<Vector2BinaryConverter>();
            AddConverterWithExtensions<Vector2IntBinaryConverter>();
            AddConverterWithExtensions<Vector3BinaryConverter>();
            AddConverterWithExtensions<Vector3IntBinaryConverter>();
            AddConverterWithExtensions<Vector4BinaryConverter>();
#endif
        }

        #endregion

        #region Public methods: Serialization and deserialization
        
        public static byte[] Serialize(object obj)
        {
            var binarySerializableType = obj.GetType();
            
            var fieldInfos = CachedFieldInfos[binarySerializableType];
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            var typeIndex = CachedTypes[binarySerializableType];
            bw.Write(BitConverter.GetBytes(typeIndex));

            foreach (var fieldInfo in fieldInfos)
            {
                var value = fieldInfo.GetValue(obj);
                var type = fieldInfo.FieldType;
                Serialize(type, value, bw);
            }

            var array = ms.ToArray();
            
            ms.Dispose();
            bw.Dispose();

            return array;
        }
        
        public static object Deserialize(byte[] bytes)
        {
            var ms = new MemoryStream(bytes);
            var br = new BinaryReader(ms);

            var typeIndex = br.ReadInt32();
            var type = CachedTypes[typeIndex];
            
            var binarySerializable = Activator.CreateInstance(type);
            Deserialize(type, binarySerializable, br);
            ms.Dispose();
            return binarySerializable;
        }

        public static T Deserialize<T>(byte[] bytes) where T : class, new()
        {
            var ms = new MemoryStream(bytes);
            var br = new BinaryReader(ms);

            var typeIndex = br.ReadInt32();
            var type = CachedTypes[typeIndex];
            if (type != typeof(T))
            {
                throw new ArgumentException("Invalid BinarySerializable Type: target is " + typeof(T) + ", but binary data's type is " + type);
            }

            var serializableMessage = new T();
            Deserialize(typeof(T), serializableMessage, br);
            ms.Dispose();
            return serializableMessage;
        }

        private static void Deserialize(Type binarySerializableType, object binarySerializable, BinaryReader binaryReader)
        {
            var fieldInfos = CachedFieldInfos[binarySerializableType];
            foreach (var fieldInfo in fieldInfos)
            {
                var type = fieldInfo.FieldType;
                var value = Deserialize(type, binaryReader);
                fieldInfo.SetValue(binarySerializable, value);
            }
        }

        #endregion

        #region Private methods

        private static void InitializeFieldInfos(Type type)
        {
            if (CachedFieldInfos.TryGetValue(type, out var fieldInfos))
            {
                return;
            }
            
            CachedFieldInfos[type] = type.GetFields();
            fieldInfos = CachedFieldInfos[type];

            foreach (var fieldInfo in fieldInfos)
            {
                var fieldType = fieldInfo.FieldType;
                if (fieldType.IsEnum)
                {
                    AddConverter(new EnumBinaryConverter(fieldType));
                }
                else if (fieldType.IsArray && fieldType.GetElementType()!.IsEnum)
                {
                    AddConverter(new EnumArrayBinaryConverter(fieldType.GetElementType()));
                }
                else if (typeof(IList).IsAssignableFrom(fieldType) && fieldType.GenericTypeArguments.Length > 0 && fieldType.GenericTypeArguments.First().IsEnum)
                {
                    AddConverter(new EnumListBinaryConverter(fieldType.GenericTypeArguments.First()));
                }
            }
        }

        private static void Serialize(Type type, object obj, BinaryWriter binaryWriter)
        {
            SerializeMethods[type](obj, binaryWriter);
        }

        private static object Deserialize(Type type, BinaryReader binaryReader)
        {
            try
            {
                return DeserializeMethods[type](binaryReader);
            }
            catch (InvalidCastException)
            {
                Debug.LogError($"Invalid cast: {type} to {DeserializeMethods[type].Method.ReturnType}");
                throw;
            }
        }

        #endregion


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Reset()
        {
            _typeIndex = 0;
            CachedTypes.Clear();
        }
        
        private class TypeIndexDictionary
        {
            private readonly Dictionary<Type, int> _typeToTypeIndex = new Dictionary<Type, int>();
            private readonly Dictionary<int, Type> _typeIndexToType = new Dictionary<int, Type>();

            public int this[Type type] => _typeToTypeIndex[type];
            public Type this[int typeIndex] => _typeIndexToType[typeIndex];
            
            public void AddType(Type type, int typeIndex)
            {
                _typeToTypeIndex.Add(type, typeIndex);
                _typeIndexToType.Add(typeIndex, type);
            }

            public void Clear()
            {
                _typeToTypeIndex.Clear();
                _typeIndexToType.Clear();
            }
        }
    }
}