using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine;

namespace AffenCode
{
    public abstract class SerializableMessage
    {
        private static readonly Dictionary<Type, ushort> TypeIndexByType = new Dictionary<Type, ushort>();
        private static readonly Dictionary<ushort, Type> TypeByTypeIndex = new Dictionary<ushort, Type>();
        
        private static readonly Dictionary<Type, FieldInfo[]> FieldInfos = new Dictionary<Type, FieldInfo[]>();

        static SerializableMessage()
        {
            var serializableMessageTypes = SerializableMessageTypes.Types.OrderBy(x => x.FullName);

            ushort index = 0;
            foreach (var serializableMessageType in serializableMessageTypes)
            {
                TypeIndexByType.Add(serializableMessageType, index);
                TypeByTypeIndex.Add(index, serializableMessageType);
                index++;
            }
        }
        
        public byte[] Serialize()
        {
            var fieldInfos = GetFieldInfos(GetType());
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write(BitConverter.GetBytes(GetStructTypeIndex(this)));
            
            foreach (var fieldInfo in fieldInfos)
            {
                var value = fieldInfo.GetValue(this);
                var type = fieldInfo.FieldType;
                if (type == typeof(bool))
                {
                    bw.Write((bool) value);
                }
                else if (type == typeof(byte))
                {
                    bw.Write((byte) value);
                }
                else if (type == typeof(sbyte))
                {
                    bw.Write((sbyte) value);
                }
                else if (type == typeof(byte[]))
                {
                    var typedValue = (byte[])value;
                    var length = typedValue.Length;
                    bw.Write(BitConverter.GetBytes(length));
                    for (int i = 0; i < length; i++)
                    {
                        bw.Write(typedValue[i]);
                    }
                }
                else if (type == typeof(char))
                {
                    bw.Write((char) value);
                }
                else if (type == typeof(char[]))
                {
                    var typedValue = (char[])value;
                    var length = typedValue.Length;
                    bw.Write(BitConverter.GetBytes(length));
                    for (int i = 0; i < length; i++)
                    {
                        bw.Write(typedValue[i]);
                    }
                }
                else if (type == typeof(double))
                {
                    bw.Write(BitConverter.GetBytes((double) value));
                }
                else if (type == typeof(double[]))
                {
                    var typedValue = (double[])value;
                    var length = typedValue.Length;
                    bw.Write(BitConverter.GetBytes(length));
                    for (int i = 0; i < length; i++)
                    {
                        bw.Write(BitConverter.GetBytes(typedValue[i]));
                    }
                }
                else if (type == typeof(short))
                {
                    bw.Write(BitConverter.GetBytes((short) value));
                }
                else if (type == typeof(short[]))
                {
                    var typedValue = (short[])value;
                    var length = typedValue.Length;
                    bw.Write(BitConverter.GetBytes(length));
                    for (int i = 0; i < length; i++)
                    {
                        bw.Write(BitConverter.GetBytes(typedValue[i]));
                    }
                }
                else if (type == typeof(ushort))
                {
                    bw.Write(BitConverter.GetBytes((ushort) value));
                }
                else if (type == typeof(ushort[]))
                {
                    var typedValue = (ushort[])value;
                    var length = typedValue.Length;
                    bw.Write(BitConverter.GetBytes(length));
                    for (int i = 0; i < length; i++)
                    {
                        bw.Write(BitConverter.GetBytes(typedValue[i]));
                    }
                }
                else if (type == typeof(int))
                {
                    bw.Write(BitConverter.GetBytes((int) value));
                }
                else if (type == typeof(int[]))
                {
                    var typedValue = (int[])value;
                    var length = typedValue.Length;
                    bw.Write(BitConverter.GetBytes(length));
                    for (int i = 0; i < length; i++)
                    {
                        bw.Write(BitConverter.GetBytes(typedValue[i]));
                    }
                }
                else if (type == typeof(uint))
                {
                    bw.Write(BitConverter.GetBytes((uint) value));
                }
                else if (type == typeof(uint[]))
                {
                    var typedValue = (uint[])value;
                    var length = typedValue.Length;
                    bw.Write(BitConverter.GetBytes(length));
                    for (int i = 0; i < length; i++)
                    {
                        bw.Write(BitConverter.GetBytes(typedValue[i]));
                    }
                }
                else if (type == typeof(long))
                {
                    bw.Write(BitConverter.GetBytes((long) value));
                }
                else if (type == typeof(long[]))
                {
                    var typedValue = (long[])value;
                    var length = typedValue.Length;
                    bw.Write(BitConverter.GetBytes(length));
                    for (int i = 0; i < length; i++)
                    {
                        bw.Write(BitConverter.GetBytes(typedValue[i]));
                    }
                }
                else if (type == typeof(ulong))
                {
                    bw.Write(BitConverter.GetBytes((ulong) value));
                }
                else if (type == typeof(ulong[]))
                {
                    var typedValue = (ulong[])value;
                    var length = typedValue.Length;
                    bw.Write(BitConverter.GetBytes(length));
                    for (int i = 0; i < length; i++)
                    {
                        bw.Write(BitConverter.GetBytes(typedValue[i]));
                    }
                }
                else if (type == typeof(float))
                {
                    bw.Write(BitConverter.GetBytes((float) value));
                }
                else if (type == typeof(float[]))
                {
                    var typedValue = (float[])value;
                    var length = typedValue.Length;
                    bw.Write(BitConverter.GetBytes(length));
                    for (int i = 0; i < length; i++)
                    {
                        bw.Write(BitConverter.GetBytes(typedValue[i]));
                    }
                }
                else if (type == typeof(string))
                {
                    var chars = ((string) value).ToCharArray();
                    var length = chars.Length;
                    bw.Write(BitConverter.GetBytes(length));
                    for (int i = 0; i < length; i++)
                    {
                        bw.Write(chars[i]);
                    }
                }
                else if (type == typeof(string[]))
                {
                    var typedValue = (string[]) value;

                    var arrayLength = typedValue.Length;
                    
                    bw.Write(BitConverter.GetBytes(arrayLength));

                    for (int j = 0; j < arrayLength; j++)
                    {
                        var chars = typedValue[j].ToCharArray();
                        var length = chars.Length;
                        bw.Write(BitConverter.GetBytes(length));
                        for (int i = 0; i < length; i++)
                        {
                            bw.Write(chars[i]);
                        }
                    }
                }
#if UNITY_5_3_OR_NEWER
                else if (type == typeof(Vector2))
                {
                    var typedValue = (Vector2) value;
                    bw.Write(BitConverter.GetBytes(typedValue.x));
                    bw.Write(BitConverter.GetBytes(typedValue.y));
                }
                else if (type == typeof(Vector2[]))
                {
                    var typedValue = (Vector2[])value;
                    var length = typedValue.Length;
                    bw.Write(BitConverter.GetBytes(length));
                    for (int i = 0; i < length; i++)
                    {
                        bw.Write(BitConverter.GetBytes(typedValue[i].x));
                        bw.Write(BitConverter.GetBytes(typedValue[i].y));
                    }
                }
                else if (type == typeof(Vector3))
                {
                    var typedValue = (Vector3) value;
                    bw.Write(BitConverter.GetBytes(typedValue.x));
                    bw.Write(BitConverter.GetBytes(typedValue.y));
                    bw.Write(BitConverter.GetBytes(typedValue.z));
                }
                else if (type == typeof(Vector3[]))
                {
                    var typedValue = (Vector3[])value;
                    var length = typedValue.Length;
                    bw.Write(BitConverter.GetBytes(length));
                    for (int i = 0; i < length; i++)
                    {
                        bw.Write(BitConverter.GetBytes(typedValue[i].x));
                        bw.Write(BitConverter.GetBytes(typedValue[i].y));
                        bw.Write(BitConverter.GetBytes(typedValue[i].z));
                    }
                }
                else if (type == typeof(Vector4))
                {
                    var typedValue = (Vector4) value;
                    bw.Write(BitConverter.GetBytes(typedValue.x));
                    bw.Write(BitConverter.GetBytes(typedValue.y));
                    bw.Write(BitConverter.GetBytes(typedValue.z));
                    bw.Write(BitConverter.GetBytes(typedValue.w));
                }
                else if (type == typeof(Vector4[]))
                {
                    var typedValue = (Vector4[])value;
                    var length = typedValue.Length;
                    bw.Write(BitConverter.GetBytes(length));
                    for (int i = 0; i < length; i++)
                    {
                        bw.Write(BitConverter.GetBytes(typedValue[i].x));
                        bw.Write(BitConverter.GetBytes(typedValue[i].y));
                        bw.Write(BitConverter.GetBytes(typedValue[i].z));
                        bw.Write(BitConverter.GetBytes(typedValue[i].w));
                    }
                }
                else if (type == typeof(Vector2Int))
                {
                    var typedValue = (Vector2Int) value;
                    bw.Write(BitConverter.GetBytes(typedValue.x));
                    bw.Write(BitConverter.GetBytes(typedValue.y));
                }
                else if (type == typeof(Vector2Int[]))
                {
                    var typedValue = (Vector2Int[])value;
                    var length = typedValue.Length;
                    bw.Write(BitConverter.GetBytes(length));
                    for (int i = 0; i < length; i++)
                    {
                        bw.Write(BitConverter.GetBytes(typedValue[i].x));
                        bw.Write(BitConverter.GetBytes(typedValue[i].y));
                    }
                }
                else if (type == typeof(Vector3Int))
                {
                    var typedValue = (Vector3Int) value;
                    bw.Write(BitConverter.GetBytes(typedValue.x));
                    bw.Write(BitConverter.GetBytes(typedValue.y));
                    bw.Write(BitConverter.GetBytes(typedValue.z));
                }
                else if (type == typeof(Vector3Int))
                {
                    var typedValue = (Vector3Int[])value;
                    var length = typedValue.Length;
                    bw.Write(BitConverter.GetBytes(length));
                    for (int i = 0; i < length; i++)
                    {
                        bw.Write(BitConverter.GetBytes(typedValue[i].x));
                        bw.Write(BitConverter.GetBytes(typedValue[i].y));
                        bw.Write(BitConverter.GetBytes(typedValue[i].z));
                    }
                }
                else if (type == typeof(Rect))
                {
                    var typedValue = (Rect) value;
                    bw.Write(BitConverter.GetBytes(typedValue.x));
                    bw.Write(BitConverter.GetBytes(typedValue.y));
                    bw.Write(BitConverter.GetBytes(typedValue.width));
                    bw.Write(BitConverter.GetBytes(typedValue.height));
                }
                else if (type == typeof(Rect))
                {
                    var typedValue = (Rect[])value;
                    var length = typedValue.Length;
                    bw.Write(BitConverter.GetBytes(length));
                    for (int i = 0; i < length; i++)
                    {
                        bw.Write(BitConverter.GetBytes(typedValue[i].x));
                        bw.Write(BitConverter.GetBytes(typedValue[i].y));
                        bw.Write(BitConverter.GetBytes(typedValue[i].width));
                        bw.Write(BitConverter.GetBytes(typedValue[i].height));
                    }
                }
                else if (type == typeof(RectInt))
                {
                    var typedValue = (RectInt) value;
                    bw.Write(BitConverter.GetBytes(typedValue.xMin));
                    bw.Write(BitConverter.GetBytes(typedValue.yMin));
                    bw.Write(BitConverter.GetBytes(typedValue.width));
                    bw.Write(BitConverter.GetBytes(typedValue.height));
                }
                else if (type == typeof(RectInt[]))
                {
                    var typedValue = (RectInt[])value;
                    var length = typedValue.Length;
                    bw.Write(BitConverter.GetBytes(length));
                    for (int i = 0; i < length; i++)
                    {
                        bw.Write(BitConverter.GetBytes(typedValue[i].xMin));
                        bw.Write(BitConverter.GetBytes(typedValue[i].yMin));
                        bw.Write(BitConverter.GetBytes(typedValue[i].width));
                        bw.Write(BitConverter.GetBytes(typedValue[i].height));
                    }
                }
                else if (type == typeof(Quaternion))
                {
                    var typedValue = (Quaternion) value;
                    bw.Write(BitConverter.GetBytes(typedValue.x));
                    bw.Write(BitConverter.GetBytes(typedValue.y));
                    bw.Write(BitConverter.GetBytes(typedValue.z));
                    bw.Write(BitConverter.GetBytes(typedValue.w));
                }
                else if (type == typeof(Quaternion[]))
                {
                    var typedValue = (Quaternion[])value;
                    var length = typedValue.Length;
                    bw.Write(BitConverter.GetBytes(length));
                    for (int i = 0; i < length; i++)
                    {
                        bw.Write(BitConverter.GetBytes(typedValue[i].x));
                        bw.Write(BitConverter.GetBytes(typedValue[i].y));
                        bw.Write(BitConverter.GetBytes(typedValue[i].z));
                        bw.Write(BitConverter.GetBytes(typedValue[i].w));
                    }
                }
                else if (type == typeof(Color))
                {
                    var typedValue = (Color) value;
                    bw.Write(BitConverter.GetBytes(typedValue.r));
                    bw.Write(BitConverter.GetBytes(typedValue.g));
                    bw.Write(BitConverter.GetBytes(typedValue.b));
                    bw.Write(BitConverter.GetBytes(typedValue.a));
                }
                else if (type == typeof(Color[]))
                {
                    var typedValue = (Color[])value;
                    var length = typedValue.Length;
                    bw.Write(BitConverter.GetBytes(length));
                    for (int i = 0; i < length; i++)
                    {
                        bw.Write(BitConverter.GetBytes(typedValue[i].r));
                        bw.Write(BitConverter.GetBytes(typedValue[i].g));
                        bw.Write(BitConverter.GetBytes(typedValue[i].b));
                        bw.Write(BitConverter.GetBytes(typedValue[i].a));
                    }
                }
#endif
                else if (type.IsEnum)
                {
                    var enumTypeIndex = EnumSerialization.GetEnumTypeIndex(type);
                    var enumValueIndex = (int)value;
                    bw.Write(BitConverter.GetBytes(enumTypeIndex));
                    bw.Write(BitConverter.GetBytes(enumValueIndex));
                }
            }

            var array = ms.ToArray();
            
            ms.Dispose();
            bw.Dispose();

            return array;
        }

        public static T Deserialize<T>(byte[] bytes) where T : SerializableMessage, new()
        {
            var ms = new MemoryStream(bytes);
            var br = new BinaryReader(ms);

            var structType = GetStructType(br.ReadUInt16());
            if (structType != typeof(T))
            {
                throw new ArgumentException("Invalid struct Type: target is " + typeof(T) + ", but binary message type is " + structType);
            }

            var SerializableMessage = new T();
            
            var fieldInfos = GetFieldInfos(structType);

            foreach (var fieldInfo in fieldInfos)
            {
                object value = null;
                var type = fieldInfo.FieldType;
                if (type == typeof(bool))
                {
                    value = br.ReadBoolean();
                }
                else if (type == typeof(byte))
                {
                    value = br.ReadByte();
                }
                else if (type == typeof(sbyte))
                {
                    value = br.ReadSByte();
                }
                else if (type == typeof(byte[]))
                {
                    var length = br.ReadInt32();
                    value = br.ReadBytes(length);
                }
                else if (type == typeof(char))
                {
                    value = br.ReadChar();
                }
                else if (type == typeof(char[]))
                {
                    var length = br.ReadInt32();
                    value = br.ReadChars(length);
                }
                else if (type == typeof(double))
                {
                    value = br.ReadDouble();
                }
                else if (type == typeof(double[]))
                {
                    var arrayLength = br.ReadInt32();
                    var typedValue = new double[arrayLength];
                    for (int i = 0; i < arrayLength; i++)
                    {
                        typedValue[i] = br.ReadDouble();
                    }
                    value = typedValue;
                }
                else if (type == typeof(short))
                {
                    value = br.ReadInt16();
                }
                else if (type == typeof(short[]))
                {
                    var arrayLength = br.ReadInt32();
                    var typedValue = new short[arrayLength];
                    for (int i = 0; i < arrayLength; i++)
                    {
                        typedValue[i] = br.ReadInt16();
                    }
                    value = typedValue;
                }
                else if (type == typeof(ushort))
                {
                    value = br.ReadUInt16();
                }
                else if (type == typeof(ushort[]))
                {
                    var arrayLength = br.ReadInt32();
                    var typedValue = new ushort[arrayLength];
                    for (int i = 0; i < arrayLength; i++)
                    {
                        typedValue[i] = br.ReadUInt16();
                    }
                    value = typedValue;
                }
                else if (type == typeof(int))
                {
                    value = br.ReadInt32();
                }
                else if (type == typeof(int[]))
                {
                    var arrayLength = br.ReadInt32();
                    var typedValue = new int[arrayLength];
                    for (int i = 0; i < arrayLength; i++)
                    {
                        typedValue[i] = br.ReadInt32();
                    }
                    value = typedValue;
                }
                else if (type == typeof(uint))
                {
                    value = br.ReadUInt32();
                }
                else if (type == typeof(uint[]))
                {
                    var arrayLength = br.ReadInt32();
                    var typedValue = new uint[arrayLength];
                    for (int i = 0; i < arrayLength; i++)
                    {
                        typedValue[i] = br.ReadUInt32();
                    }
                    value = typedValue;
                }
                else if (type == typeof(long))
                {
                    value = br.ReadInt64();
                }
                else if (type == typeof(long[]))
                {
                    var arrayLength = br.ReadInt32();
                    var typedValue = new long[arrayLength];
                    for (int i = 0; i < arrayLength; i++)
                    {
                        typedValue[i] = br.ReadInt64();
                    }
                    value = typedValue;
                }
                else if (type == typeof(ulong))
                {
                    value = br.ReadUInt64();
                }
                else if (type == typeof(ulong[]))
                {
                    var arrayLength = br.ReadInt32();
                    var typedValue = new ulong[arrayLength];
                    for (int i = 0; i < arrayLength; i++)
                    {
                        typedValue[i] = br.ReadUInt64();
                    }
                    value = typedValue;
                }
                else if (type == typeof(float))
                {
                    value = br.ReadSingle();
                }
                else if (type == typeof(float[]))
                {
                    var arrayLength = br.ReadInt32();
                    var typedValue = new float[arrayLength];
                    for (int i = 0; i < arrayLength; i++)
                    {
                        typedValue[i] = br.ReadSingle();
                    }
                    value = typedValue;
                }
                else if (type == typeof(string))
                {
                    var length = br.ReadInt32();
                    value = new string(br.ReadChars(length));
                }
                else if (type == typeof(string[]))
                {
                    var arrayLength = br.ReadInt32();
                    var typedValue = new string[arrayLength];
                    for (int i = 0; i < arrayLength; i++)
                    {
                        var stringLength = br.ReadInt32();
                        typedValue[i] = new string(br.ReadChars(stringLength));
                    }
                    value = typedValue;
                }
#if UNITY_5_3_OR_NEWER
                else if (type == typeof(Vector2))
                {
                    value = new Vector2(br.ReadSingle(), br.ReadSingle());
                }
                else if (type == typeof(Vector2[]))
                {
                    var arrayLength = br.ReadInt32();
                    var typedValue = new Vector2[arrayLength];
                    for (int i = 0; i < arrayLength; i++)
                    {
                        typedValue[i] = new Vector2(br.ReadSingle(), br.ReadSingle());
                    }
                    value = typedValue;
                }
                else if (type == typeof(Vector3))
                {
                    value = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
                }
                else if (type == typeof(Vector3[]))
                {
                    var arrayLength = br.ReadInt32();
                    var typedValue = new Vector3[arrayLength];
                    for (int i = 0; i < arrayLength; i++)
                    {
                        typedValue[i] = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
                    }
                    value = typedValue;
                }
                else if (type == typeof(Vector4))
                {
                    value = new Vector4(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
                }
                else if (type == typeof(Vector4[]))
                {
                    var arrayLength = br.ReadInt32();
                    var typedValue = new Vector4[arrayLength];
                    for (int i = 0; i < arrayLength; i++)
                    {
                        typedValue[i] = new Vector4(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
                    }
                    value = typedValue;
                }
                else if (type == typeof(Vector2Int))
                {
                    value = new Vector2Int(br.ReadInt32(), br.ReadInt32());
                }
                else if (type == typeof(Vector2Int[]))
                {
                    var arrayLength = br.ReadInt32();
                    var typedValue = new Vector2Int[arrayLength];
                    for (int i = 0; i < arrayLength; i++)
                    {
                        typedValue[i] = new Vector2Int(br.ReadInt32(), br.ReadInt32());
                    }
                    value = typedValue;
                }
                else if (type == typeof(Vector3Int))
                {
                    value = new Vector3Int(br.ReadInt32(), br.ReadInt32(), br.ReadInt32());
                }
                else if (type == typeof(Vector3Int[]))
                {
                    var arrayLength = br.ReadInt32();
                    var typedValue = new Vector3Int[arrayLength];
                    for (int i = 0; i < arrayLength; i++)
                    {
                        typedValue[i] = new Vector3Int(br.ReadInt32(), br.ReadInt32(), br.ReadInt32());
                    }
                    value = typedValue;
                }
                else if (type == typeof(Rect))
                {
                    value = new Rect(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
                }
                else if (type == typeof(Rect[]))
                {
                    var arrayLength = br.ReadInt32();
                    var typedValue = new Rect[arrayLength];
                    for (int i = 0; i < arrayLength; i++)
                    {
                        typedValue[i] = new Rect(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
                    }
                    value = typedValue;
                }
                else if (type == typeof(RectInt))
                {
                    value = new RectInt(br.ReadInt32(), br.ReadInt32(), br.ReadInt32(), br.ReadInt32());
                }
                else if (type == typeof(RectInt[]))
                {
                    var arrayLength = br.ReadInt32();
                    var typedValue = new RectInt[arrayLength];
                    for (int i = 0; i < arrayLength; i++)
                    {
                        typedValue[i] = new RectInt(br.ReadInt32(), br.ReadInt32(), br.ReadInt32(), br.ReadInt32());
                    }
                    value = typedValue;
                }
                else if (type == typeof(Quaternion))
                {
                    value = new Quaternion(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
                }
                else if (type == typeof(Quaternion[]))
                {
                    var arrayLength = br.ReadInt32();
                    var typedValue = new Quaternion[arrayLength];
                    for (int i = 0; i < arrayLength; i++)
                    {
                        typedValue[i] = new Quaternion(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
                    }
                    value = typedValue;
                }
                else if (type == typeof(Color))
                {
                    value = new Color(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
                }
                else if (type == typeof(Color[]))
                {
                    var arrayLength = br.ReadInt32();
                    var typedValue = new Color[arrayLength];
                    for (int i = 0; i < arrayLength; i++)
                    {
                        typedValue[i] = new Color(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
                    }
                    value = typedValue;
                }
#endif
                else if (type.IsEnum)
                {
                    var enumTypeIndex = br.ReadUInt16();
                    var enumValue = br.ReadInt32();
                    value = Convert.ChangeType(enumValue, Enum.GetUnderlyingType(EnumSerialization.GetEnumType(enumTypeIndex)));
                }
                
                fieldInfo.SetValue(SerializableMessage, value);
            }
            
            ms.Dispose();
            br.Dispose();

            return SerializableMessage;
        }
        
        private static ushort GetStructTypeIndex(object networkCommand)
        {
            return TypeIndexByType[networkCommand.GetType()];
        }

        private static Type GetStructType(ushort networkCommandSerializedTypeIndex)
        {
            return TypeByTypeIndex[networkCommandSerializedTypeIndex];
        }
        
        private static FieldInfo[] GetFieldInfos(Type networkCommandType)
        {
            if (!FieldInfos.TryGetValue(networkCommandType, out var fieldInfos))
            {
                FieldInfos[networkCommandType] = networkCommandType.GetFields().Where(x => x.DeclaringType == networkCommandType).ToArray();
                fieldInfos = FieldInfos[networkCommandType];
            }

            return fieldInfos;
        }
    }
}