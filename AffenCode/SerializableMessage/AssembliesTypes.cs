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
            var sw = Stopwatch.StartNew();
            
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Types = assemblies.SelectMany(x => x.GetTypes());

            sw.Stop();
            Debug.Log("AssembliesTypes: " + sw.Elapsed);
        }

        public static IEnumerable<Type> GetTypes() => Types;
    }
}