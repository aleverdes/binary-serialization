using UnityEngine;

namespace AleVerDes.BinarySerialization.Tests
{
    public class TestSerializableMessage : IBinarySerializable
    {
        public bool BoolValue;
        public bool[] BoolArray;
        public byte ByteValue;
        public byte[] ByteArray;
        public sbyte SByteValue;
        public sbyte[] SByteArray;
        public char CharValue;
        public char[] CharArray;
        public double DoubleValue;
        public double[] DoubleArray;
        public short ShortValue;
        public short[] ShortArray;
        public ushort UShortValue;
        public ushort[] UShortArray;
        public int IntValue;
        public int[] IntArray;
        public uint UIntValue;
        public uint[] UIntArray;
        public long LongValue;
        public long[] LongArray;
        public ulong ULongValue;
        public ulong[] ULongArray;
        public float FloatValue;
        public float[] FloatArray;
        public string StringValue;
        public string[] StringArray;
        public TestEnum EnumValue;
        public Vector2 Vector2Value;
        public Vector2[] Vector2Array;
        public Vector3 Vector3Value;
        public Vector3[] Vector3Array;
        public Vector4 Vector4Value;
        public Vector4[] Vector4Array;
        public Vector2Int Vector2IntValue;
        public Vector2Int[] Vector2IntArray;
        public Vector3Int Vector3IntValue;
        public Vector3Int[] Vector3IntArray;
        public Rect RectValue;
        public Rect[] RectArray;
        public RectInt RectIntValue;
        public RectInt[] RectIntArray;
        public Quaternion QuaternionValue;
        public Quaternion[] QuaternionArray;
        public Color ColorValue;
        public Color[] ColorArray;
    }
}