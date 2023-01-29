using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Debug = UnityEngine.Debug;

namespace AffenCode
{
    public static class SerializableMessageTypes
    {
        public static readonly IEnumerable<Type> Types;
        
        static SerializableMessageTypes()
        {
            Types = AssembliesTypes.GetTypes().Where(IsSerializableMessageType);
        }

        private static bool IsSerializableMessageType(this Type type) => typeof(SerializableMessage).IsAssignableFrom(type);
    }
}