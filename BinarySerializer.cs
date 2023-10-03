using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AleVerDes.BinarySerialization
{
    public static class BinarySerializer
    {
        private const bool CheckInitializationTime = true;
        
        private static readonly TypeIndexDictionary CachedTypes = new();
        private static readonly Dictionary<Type, FieldInfo[]> CachedFieldInfos = new();

        private static readonly Dictionary<Type, Action<object, BinaryWriter>> SerializeMethods = new();
        private static readonly Dictionary<Type, Func<BinaryReader, object>> DeserializeMethods = new();

        static BinarySerializer()
        {
            Stopwatch sw;
            if (CheckInitializationTime)
            {
                sw = Stopwatch.StartNew();
            }
            
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var types = assemblies.SelectMany(x => x.GetTypes());

            var typeIndex = 0;
            foreach (var type in types)
            {
                if (type != typeof(IBinarySerializable) && typeof(IBinarySerializable).IsAssignableFrom(type))
                {
                    CachedTypes.AddType(type, typeIndex);
                    CachedFieldInfos[type] = InitializeFieldInfos(type);
                    typeIndex++;
                }
                else if (type != typeof(IBinaryConverter) && typeof(IBinaryConverter).IsAssignableFrom(type))
                {
                    var converter = (IBinaryConverter) Activator.CreateInstance(type);
                    AddSerializeMethod(converter.SerializationType, converter.Serialize);
                    AddDeserializeMethod(converter.SerializationType, converter.Deserialize);
                }
            }

            if (CheckInitializationTime)
            {
                var message = "BinarySerializer.InitializationTime = " + sw.Elapsed;
                sw.Stop();
#if UNITY_2017_1_OR_NEWER
                UnityEngine.Debug.Log(message);
#else
                Console.WriteLine(message);
#endif
                sw = null;
            }
        }

        #region Registration of the serialization and deserialization methods

        public static void AddSerializeMethod(Type type, Action<object, BinaryWriter> method)
        {
            SerializeMethods[type] = method;
        }

        public static void AddDeserializeMethod(Type type, Func<BinaryReader, object> method)
        {
            DeserializeMethods[type] = method;
        }

        #endregion

        #region Private methods

        private static FieldInfo[] InitializeFieldInfos(Type serializableMessageType)
        {
            if (!CachedFieldInfos.TryGetValue(serializableMessageType, out var fieldInfos))
            {
                CachedFieldInfos[serializableMessageType] = serializableMessageType.GetFields();
                fieldInfos = CachedFieldInfos[serializableMessageType];
            }
            return fieldInfos;
        }

        private static void Serialize(Type type, object obj, BinaryWriter binaryWriter)
        {
            SerializeMethods[type](obj, binaryWriter);
        }

        private static object Deserialize(Type type, BinaryReader binaryReader)
        {
            return DeserializeMethods[type](binaryReader);
        }

        private static bool IsBinarySerializable(Type type) => typeof(IBinarySerializable).IsAssignableFrom(type);

        #endregion

        #region Serialization, deserialization and get info
        
        public static byte[] Serialize(this IBinarySerializable binarySerializable)
        {
            var binarySerializableType = binarySerializable.GetType();
            
            var fieldInfos = CachedFieldInfos[binarySerializableType];
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            var typeIndex = CachedTypes[binarySerializableType];
            bw.Write(BitConverter.GetBytes(typeIndex));

            foreach (var fieldInfo in fieldInfos)
            {
                var value = fieldInfo.GetValue(binarySerializable);
                var type = fieldInfo.FieldType;
                if (type.IsEnum)
                {
                    var enumValueIndex = (int)value;
                    bw.Write(BitConverter.GetBytes(enumValueIndex));
                }
                else
                {
                    Serialize(type, value, bw);
                }
            }

            var array = ms.ToArray();
            
            ms.Dispose();
            bw.Dispose();

            return array;
        }
        
        public static object Deserialize(this byte[] bytes)
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

        public static T Deserialize<T>(this byte[] bytes) where T : class, IBinarySerializable, new()
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
                object value;
                if (type.IsEnum)
                {
                    var enumValue = binaryReader.ReadInt32();
                    value = Convert.ChangeType(enumValue, Enum.GetUnderlyingType(type));
                }
                else
                {
                    value = Deserialize(type, binaryReader);
                }
                fieldInfo.SetValue(binarySerializable, value);
            }
        }

        public static string GetInfo(this IBinarySerializable binarySerializable)
        {
            var type = binarySerializable.GetType();
            var sb = new StringBuilder();
            sb.Append(type);
            sb.Append("\n");
            foreach (var fieldInfo in CachedFieldInfos[type])
            {
                sb.Append($"    {fieldInfo.Name} = {fieldInfo.GetValue(binarySerializable)}\n");
            }
            return sb.ToString();
        }

        #endregion
        
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
        }
    }
}