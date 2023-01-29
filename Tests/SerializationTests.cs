using System;
using System.Diagnostics;
using System.Linq;
using AffenCode;
using NUnit.Framework;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class SerializationTests
{
    [Test]
    public void Deserialize()
    {
        // Declaration of test serialization message
        var testSerializableMessage = new TestSerializableMessage()
        {
            BoolValue = true,
            BoolArray = new []{ true, false, true},
            ByteValue = 123,
            ByteArray = new byte[] { 12, 23, 34 },
            SByteValue = -1,
            SByteArray = new sbyte[] { -1, 2, -3 },
            CharValue = 'q',
            CharArray = new char[] { 'a', 'b', 'c' },
            DoubleValue = double.MaxValue - 1,
            DoubleArray = new double[] { 1, 2, 3 },
            ShortValue = short.MaxValue - 1,
            ShortArray = new short[] { 1, 2, 3 },
            UShortValue = 123,
            UShortArray = new ushort[] { 1, 2, 3 },
            IntValue = 123,
            IntArray = new int[] { 1, 2, 3 },
            UIntValue = 234,
            UIntArray = new uint[] { 1, 2, 3 },
            LongValue = 234,
            LongArray = new long[] { 1, 2, 3 },
            ULongValue = 123,
            ULongArray = new ulong[] { 1, 2, 3 },
            FloatValue = 123f,
            FloatArray = new float[] { 1f, 2f, 3f },
            StringValue = "test",
            StringArray = new []{ "first", "second", "third" },
            EnumValue = TestEnum.Second,
            Vector2Value = new Vector2(1f, 2f),
            Vector2Array = new Vector2[] { new Vector2(1f, 2f), new Vector2(3f, 4f) },
            Vector3Value = new Vector3(1f, 2f, 3f),
            Vector3Array = new Vector3[] { new Vector3(1f, 2f, 3f), new Vector3(3f, 4f, 5f) },
            Vector4Value = new Vector4(1f, 2f, 3f, 4f),
            Vector4Array = new Vector4[] { new Vector4(1f, 2f, 3f, 4f), new Vector4(3f, 4f, 5f, 6f) },
            Vector2IntValue = new Vector2Int(1, 2),
            Vector2IntArray = new Vector2Int[] { new Vector2Int(1, 2), new Vector2Int(3, 4) },
            Vector3IntValue = new Vector3Int(1, 2, 3),
            Vector3IntArray = new Vector3Int[] { new Vector3Int(1, 2, 3), new Vector3Int(3, 4, 5) },
            RectValue = new Rect(0f, 0f, 640f, 480f),
            RectArray = new Rect[] { new Rect(0f, 0f, 640f, 480f), new Rect(128f, 128f, 800f, 600f)},
            RectIntValue = new RectInt(0, 0, 640, 480),
            RectIntArray = new RectInt[] { new RectInt(0, 0, 640, 480), new RectInt(128, 128, 800, 600)},
            QuaternionValue = Quaternion.identity,
            QuaternionArray = new Quaternion[] { Quaternion.identity, new Quaternion(1f, 0, 0, 0) },
            ColorValue = Color.red,
            ColorArray = new Color[] { Color.red, Color.green, Color.blue },
        };

        // First Serialization
        var sw = Stopwatch.StartNew();
        var firstSerialization = testSerializableMessage.Serialize();
        Debug.Log("First Serialization: " + sw.Elapsed);
        
        // Log byte array
        Debug.Log(firstSerialization.Aggregate("", (s, b) => s + BitConverter.ToString(new [] {b}) + " "));
        
        // First Deserialization
        sw = Stopwatch.StartNew();
        var firstDeserialization = SerializableMessage.Deserialize<TestSerializableMessage>(firstSerialization);
        Debug.Log("First Deserialization: " + sw.Elapsed);
        
        // Asserts
        Assert.AreEqual(true, firstDeserialization.BoolValue, "BoolValue is wrong");
        Assert.AreEqual(true, firstDeserialization.BoolArray[2], "BoolArray is wrong");
        Assert.AreEqual(123, firstDeserialization.ByteValue, "ByteValue is wrong");
        Assert.AreEqual(12, firstDeserialization.ByteArray[0], "ByteArray is wrong");
        Assert.AreEqual(-1, firstDeserialization.SByteValue, "SByteValue is wrong");
        Assert.AreEqual(-1, firstDeserialization.SByteArray[0], "SByteArray is wrong");
        Assert.AreEqual('q', firstDeserialization.CharValue, "CharValue is wrong");
        Assert.AreEqual('b', firstDeserialization.CharArray[1], "CharArray is wrong");
        Assert.AreEqual(double.MaxValue - 1, firstDeserialization.DoubleValue, "DoubleValue is wrong");
        Assert.AreEqual(1, firstDeserialization.DoubleArray[0], "DoubleArray is wrong");
        Assert.AreEqual(short.MaxValue - 1, firstDeserialization.ShortValue, "ShortValue is wrong");
        Assert.AreEqual(1, firstDeserialization.ShortArray[0], "ShortArray is wrong");
        Assert.AreEqual(123, firstDeserialization.UShortValue, "UShortValue is wrong");
        Assert.AreEqual(1, firstDeserialization.UShortArray[0], "UShortArray is wrong");
        Assert.AreEqual(123, firstDeserialization.IntValue, "IntValue is wrong");
        Assert.AreEqual(1, firstDeserialization.IntArray[0], "IntArray is wrong");
        Assert.AreEqual(234, firstDeserialization.UIntValue, "UIntValue is wrong");
        Assert.AreEqual(1, firstDeserialization.UIntArray[0], "UIntArray is wrong");
        Assert.AreEqual(234, firstDeserialization.LongValue, "LongValue is wrong");
        Assert.AreEqual(1, firstDeserialization.LongArray[0], "LongArray is wrong");
        Assert.AreEqual(123, firstDeserialization.ULongValue, "ULongValue is wrong");
        Assert.AreEqual(1, firstDeserialization.ULongArray[0], "ULongArray is wrong");
        Assert.AreEqual(123f, firstDeserialization.FloatValue, "FloatValue is wrong");
        Assert.AreEqual(1f, firstDeserialization.FloatArray[0], "FloatArray is wrong");
        Assert.AreEqual("test", firstDeserialization.StringValue, "StringValue is wrong");
        Assert.AreEqual("second", firstDeserialization.StringArray[1], "StringArray is wrong");
        Assert.AreEqual(TestEnum.Second, firstDeserialization.EnumValue, "EnumValue is wrong");
        Assert.AreEqual(1f, firstDeserialization.Vector2Value.x, "Vector2Value is wrong");
        Assert.AreEqual(1f, firstDeserialization.Vector2Array[0].x, "Vector2Array is wrong");
        Assert.AreEqual(1f, firstDeserialization.Vector3Value.x, "Vector3Value is wrong");
        Assert.AreEqual(1f, firstDeserialization.Vector3Array[0].x, "Vector3Array is wrong");
        Assert.AreEqual(1f, firstDeserialization.Vector4Value.x, "Vector4Value is wrong");
        Assert.AreEqual(1f, firstDeserialization.Vector4Array[0].x, "Vector4Array is wrong");
        Assert.AreEqual(1, firstDeserialization.Vector2IntValue.x, "Vector2IntValue is wrong");
        Assert.AreEqual(1, firstDeserialization.Vector2IntArray[0].x, "Vector2IntArray is wrong");
        Assert.AreEqual(1, firstDeserialization.Vector3IntValue.x, "Vector3IntValue is wrong");
        Assert.AreEqual(1, firstDeserialization.Vector3IntArray[0].x, "Vector3IntArray is wrong");
        Assert.AreEqual(640, firstDeserialization.RectValue.width, "RectValue is wrong");
        Assert.AreEqual(800, firstDeserialization.RectArray[1].width, "RectArray is wrong");
        Assert.AreEqual(640, firstDeserialization.RectIntValue.width, "RectIntValue is wrong");
        Assert.AreEqual(800, firstDeserialization.RectIntArray[1].width, "RectIntArray is wrong");
        Assert.AreEqual(0f, firstDeserialization.QuaternionValue.x, "QuaternionValue is wrong");
        Assert.AreEqual(1f, firstDeserialization.QuaternionArray[1].x, "QuaternionArray is wrong");
        Assert.AreEqual(1f, firstDeserialization.ColorValue.r, "ColorValue is wrong");
        Assert.AreEqual(1f, firstDeserialization.ColorArray[1].g, "ColorArray is wrong");

        // Second Serialization
        sw = Stopwatch.StartNew();
        var secondSerialization = testSerializableMessage.Serialize();
        Debug.Log("Second Serialization: " + sw.Elapsed);
        
        // Second Deserialization
        sw = Stopwatch.StartNew();
        var secondDeserialization = SerializableMessage.Deserialize<TestSerializableMessage>(firstSerialization);
        Debug.Log("Second Deserialization: " + sw.Elapsed);
        
        // Clear
        firstSerialization = null;
        firstDeserialization = null;
        secondSerialization = null;
        secondDeserialization = null;
        sw.Stop();
        sw = null;
    }
}
