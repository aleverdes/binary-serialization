using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Debug = UnityEngine.Debug;

namespace AffenCode
{
    internal static class AssembliesTypes
    {
        private static readonly IEnumerable<Type> Types;

        static AssembliesTypes()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Types = assemblies.SelectMany(x => x.GetTypes());
        }

        public static IEnumerable<Type> GetTypes() => Types;
    }
}