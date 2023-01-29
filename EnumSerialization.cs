using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Debug = UnityEngine.Debug;

namespace AffenCode
{
    public static class EnumSerialization
    {
        private static readonly Dictionary<Type, ushort> TypeIndexByType = new Dictionary<Type, ushort>();
        private static readonly Dictionary<ushort, Type> TypeByTypeIndex = new Dictionary<ushort, Type>();

        static EnumSerialization()
        {
            var types = AssembliesTypes.GetTypes().Where(IsEnum);

            ushort index = 0;
            foreach (var enumType in types)
            {
                TypeIndexByType.Add(enumType, index);
                TypeByTypeIndex.Add(index, enumType);
                index++;
            }
        }

        public static ushort GetEnumTypeIndex(Type enumType)
        {
            return TypeIndexByType[enumType];
        }

        public static Type GetEnumType(ushort enumTypeIndex)
        {
            return TypeByTypeIndex[enumTypeIndex];
        }

        private static bool IsEnum(this Type type) => type.IsEnum;
    }
}