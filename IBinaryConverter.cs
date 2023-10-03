using System;
using System.IO;

namespace AleVerDes.BinarySerialization
{
    public interface IBinaryConverter
    {
        Type SerializationType { get; } 
        void Serialize(object value, BinaryWriter bw);
        object Deserialize(BinaryReader br);
    }
}