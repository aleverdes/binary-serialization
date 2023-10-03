using System.Diagnostics;
using NUnit.Framework;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace AleVerDes.BinarySerialization.Tests
{
    public class SerializationTests
    {
        private const bool UsePadRightForBenchmarkMessages = false;
        
        [Test]
        public void SerializeAndDeserializeValue()
        {
            var benchmark = new Benchmark(nameof(TestValueData));
            
            BinarySerializer.AddDefaultConverters();
            benchmark.Round("Adding default converters");

            BinarySerializer.RegisterType<TestValueData>();
            benchmark.Round("Type registration");

            // Declaration of test serialization message
            var testSerializableMessage = new TestValueData()
            {
                Bool = true,
                Byte = 123,
                SByte = -1,
                Char = 'q',
                Double = double.MaxValue - 1,
                Short = short.MaxValue - 1,
                UShort = 123,
                Int = 123,
                UInt = 234,
                Long = 234,
                ULong = 123,
                Float = 123f,
                String = "test",
                Enum = TestEnum.Second,
                Vector2 = new Vector2(1f, 2f),
                Vector3 = new Vector3(1f, 2f, 3f),
                Vector4 = new Vector4(1f, 2f, 3f, 4f),
                Vector2Int = new Vector2Int(1, 2),
                Vector3Int = new Vector3Int(1, 2, 3),
                Rect = new Rect(0f, 0f, 640f, 480f),
                RectOffset = new RectOffset(1, 2, 3, 4),
                RectInt = new RectInt(0, 0, 640, 480),
                Quaternion = Quaternion.identity,
                Color = Color.red,
            };

            // First Serialization
            var firstSerialization = BinarySerializer.Serialize(testSerializableMessage);
            benchmark.Round("First Serialization");

            // First Deserialization
            var firstDeserialization = BinarySerializer.Deserialize<TestValueData>(firstSerialization);
            benchmark.Round("First Deserialization");
            
            // Asserts
            Assert.AreEqual(true, firstDeserialization.Bool, "Bool is wrong");
            Assert.AreEqual(123, firstDeserialization.Byte, "Byte is wrong");
            Assert.AreEqual(-1, firstDeserialization.SByte, "SByte is wrong");
            Assert.AreEqual('q', firstDeserialization.Char, "Char is wrong");
            Assert.AreEqual(double.MaxValue - 1, firstDeserialization.Double, "Double is wrong");
            Assert.AreEqual(short.MaxValue - 1, firstDeserialization.Short, "Short is wrong");
            Assert.AreEqual(123, firstDeserialization.UShort, "UShort is wrong");
            Assert.AreEqual(123, firstDeserialization.Int, "Int is wrong");
            Assert.AreEqual(234, firstDeserialization.UInt, "UInt is wrong");
            Assert.AreEqual(234, firstDeserialization.Long, "Long is wrong");
            Assert.AreEqual(123, firstDeserialization.ULong, "ULong is wrong");
            Assert.AreEqual(123f, firstDeserialization.Float, "Float is wrong");
            Assert.AreEqual("test", firstDeserialization.String, "String is wrong");
            Assert.AreEqual(TestEnum.Second, firstDeserialization.Enum, "Enum is wrong");
            Assert.AreEqual(1f, firstDeserialization.Vector2.x, "Vector2 is wrong");
            Assert.AreEqual(1f, firstDeserialization.Vector3.x, "Vector3 is wrong");
            Assert.AreEqual(1f, firstDeserialization.Vector4.x, "Vector4 is wrong");
            Assert.AreEqual(1, firstDeserialization.Vector2Int.x, "Vector2Int is wrong");
            Assert.AreEqual(1, firstDeserialization.Vector3Int.x, "Vector3Int is wrong");
            Assert.AreEqual(640, firstDeserialization.Rect.width, "Rect is wrong");
            Assert.AreEqual(640, firstDeserialization.RectInt.width, "RectInt is wrong");
            Assert.AreEqual(3, firstDeserialization.RectOffset.top, "RectOffset is wrong");
            Assert.AreEqual(0f, firstDeserialization.Quaternion.x, "Quaternion is wrong");
            Assert.AreEqual(1f, firstDeserialization.Color.r, "Color is wrong");

            // Second Serialization
            var secondSerialization = BinarySerializer.Serialize(testSerializableMessage);
            benchmark.Round("Second Deserialization");

            // Second Deserialization
            var secondDeserialization = BinarySerializer.Deserialize<TestValueData>(secondSerialization);
            benchmark.Round("Second Deserialization");

            // Object Deserialization
            var unknownTypeDeserialization = BinarySerializer.Deserialize(secondSerialization);
            benchmark.Round("Deserialization without type");

            benchmark.Stop();
            Debug.Log(benchmark.GetMessage(UsePadRightForBenchmarkMessages));
        }

        [Test]
        public void SerializeAndDeserializeArray()
        {
            var benchmark = new Benchmark(nameof(TestArrayData));
            
            BinarySerializer.AddDefaultConverters();
            benchmark.Round("Adding default converters");

            BinarySerializer.RegisterType<TestArrayData>();
            benchmark.Round("Type registration");

            // Declaration of test serialization message
            var testSerializableMessage = new TestArrayData()
            {
                Bool = new[] { true, false, true },
                Byte = new byte[] { 12, 23, 34 },
                SByte = new sbyte[] { -1, 2, -3 },
                Char = new char[] { 'a', 'b', 'c' },
                Double = new double[] { 1, 2, 3 },
                Short = new short[] { 1, 2, 3 },
                UShort = new ushort[] { 1, 2, 3 },
                Int = new int[] { 1, 2, 3 },
                UInt = new uint[] { 1, 2, 3 },
                Long = new long[] { 1, 2, 3 },
                ULong = new ulong[] { 1, 2, 3 },
                Float = new float[] { 1f, 2f, 3f },
                String = new[] { "first", "second", "third" },
                Enum = new [] {TestEnum.First, TestEnum.Third},
                Vector2 = new Vector2[] { new Vector2(1f, 2f), new Vector2(3f, 4f) },
                Vector3 = new Vector3[] { new Vector3(1f, 2f, 3f), new Vector3(3f, 4f, 5f) },
                Vector4 = new Vector4[] { new Vector4(1f, 2f, 3f, 4f), new Vector4(3f, 4f, 5f, 6f) },
                Vector2Int = new Vector2Int[] { new Vector2Int(1, 2), new Vector2Int(3, 4) },
                Vector3Int = new Vector3Int[] { new Vector3Int(1, 2, 3), new Vector3Int(3, 4, 5) },
                Rect = new Rect[] { new Rect(0f, 0f, 640f, 480f), new Rect(128f, 128f, 800f, 600f) },
                RectOffset = new[] { new RectOffset(1, 2, 3, 4), new RectOffset(5, 6, 7, 8) },
                RectInt = new RectInt[] { new RectInt(0, 0, 640, 480), new RectInt(128, 128, 800, 600) },
                Quaternion = new Quaternion[] { Quaternion.identity, new Quaternion(1f, 0, 0, 0) },
                Color = new Color[] { Color.red, Color.green, Color.blue },
            };

            // First Serialization
            var firstSerialization = BinarySerializer.Serialize(testSerializableMessage);
            benchmark.Round("First Serialization");

            // First Deserialization
            var firstDeserialization = BinarySerializer.Deserialize<TestArrayData>(firstSerialization);
            benchmark.Round("First Deserialization");

            // Asserts
            Assert.AreEqual(true, firstDeserialization.Bool[2], "Bool is wrong");
            Assert.AreEqual(12, firstDeserialization.Byte[0], "Byte is wrong");
            Assert.AreEqual(-1, firstDeserialization.SByte[0], "SByte is wrong");
            Assert.AreEqual('b', firstDeserialization.Char[1], "Char is wrong");
            Assert.AreEqual(1, firstDeserialization.Double[0], "Double is wrong");
            Assert.AreEqual(1, firstDeserialization.Short[0], "Short is wrong");
            Assert.AreEqual(1, firstDeserialization.UShort[0], "UShort is wrong");
            Assert.AreEqual(1, firstDeserialization.Int[0], "Int is wrong");
            Assert.AreEqual(1, firstDeserialization.UInt[0], "UInt is wrong");
            Assert.AreEqual(1, firstDeserialization.Long[0], "Long is wrong");
            Assert.AreEqual(1, firstDeserialization.ULong[0], "ULong is wrong");
            Assert.AreEqual(1f, firstDeserialization.Float[0], "Float is wrong");
            Assert.AreEqual("second", firstDeserialization.String[1], "String is wrong");
            Assert.AreEqual(TestEnum.Third, firstDeserialization.Enum[1], "Enum is wrong");
            Assert.AreEqual(1f, firstDeserialization.Vector2[0].x, "Vector2 is wrong");
            Assert.AreEqual(1f, firstDeserialization.Vector3[0].x, "Vector3 is wrong");
            Assert.AreEqual(1f, firstDeserialization.Vector4[0].x, "Vector4 is wrong");
            Assert.AreEqual(1, firstDeserialization.Vector2Int[0].x, "Vector2Int is wrong");
            Assert.AreEqual(1, firstDeserialization.Vector3Int[0].x, "Vector3Int is wrong");
            Assert.AreEqual(800, firstDeserialization.Rect[1].width, "Rect is wrong");
            Assert.AreEqual(800, firstDeserialization.RectInt[1].width, "RectInt is wrong");
            Assert.AreEqual(5, firstDeserialization.RectOffset[1].left, "RectOffset is wrong");
            Assert.AreEqual(1f, firstDeserialization.Quaternion[1].x, "Quaternion is wrong");
            Assert.AreEqual(1f, firstDeserialization.Color[1].g, "Color is wrong");

            // Second Serialization
            var secondSerialization = BinarySerializer.Serialize(testSerializableMessage);
            benchmark.Round("Second Deserialization");

            // Second Deserialization
            var secondDeserialization = BinarySerializer.Deserialize<TestArrayData>(secondSerialization);
            benchmark.Round("Second Deserialization");

            // Object Deserialization
            var unknownTypeDeserialization = BinarySerializer.Deserialize(secondSerialization);
            benchmark.Round("Deserialization without type");
            
            Debug.Log(benchmark.GetMessage(UsePadRightForBenchmarkMessages));
        }

        [Test]
        public void SerializeAndDeserializeList()
        {
            var benchmark = new Benchmark(nameof(TestListData));
            
            BinarySerializer.AddDefaultConverters();
            benchmark.Round("Adding default converters");

            BinarySerializer.RegisterType<TestListData>();
            benchmark.Round("Type registration");

            // Declaration of test serialization message
            var testSerializableMessage = new TestListData()
            {
                Bool = new() { true, false, true },
                Byte = new() { 12, 23, 34 },
                SByte = new () { -1, 2, -3 },
                Char = new () { 'a', 'b', 'c' },
                Double = new () { 1, 2, 3 },
                Short = new () { 1, 2, 3 },
                UShort = new () { 1, 2, 3 },
                Int = new () { 1, 2, 3 },
                UInt = new () { 1, 2, 3 },
                Long = new () { 1, 2, 3 },
                ULong = new () { 1, 2, 3 },
                Float = new () { 1f, 2f, 3f },
                String = new() { "first", "second", "third" },
                Enum = new () {TestEnum.First, TestEnum.Third},
                Vector2 = new () { new Vector2(1f, 2f), new Vector2(3f, 4f) },
                Vector3 = new () { new Vector3(1f, 2f, 3f), new Vector3(3f, 4f, 5f) },
                Vector4 = new () { new Vector4(1f, 2f, 3f, 4f), new Vector4(3f, 4f, 5f, 6f) },
                Vector2Int = new () { new Vector2Int(1, 2), new Vector2Int(3, 4) },
                Vector3Int = new () { new Vector3Int(1, 2, 3), new Vector3Int(3, 4, 5) },
                Rect = new () { new Rect(0f, 0f, 640f, 480f), new Rect(128f, 128f, 800f, 600f) },
                RectOffset = new() { new RectOffset(1, 2, 3, 4), new RectOffset(5, 6, 7, 8) },
                RectInt = new () { new RectInt(0, 0, 640, 480), new RectInt(128, 128, 800, 600) },
                Quaternion = new () { Quaternion.identity, new Quaternion(1f, 0, 0, 0) },
                Color = new () { Color.red, Color.green, Color.blue },
            };

            // First Serialization
            var firstSerialization = BinarySerializer.Serialize(testSerializableMessage);
            benchmark.Round("First Serialization");

            // First Deserialization
            var firstDeserialization = BinarySerializer.Deserialize<TestListData>(firstSerialization);
            benchmark.Round("First Deserialization");

            // Asserts
            Assert.AreEqual(true, firstDeserialization.Bool[2], "Bool is wrong");
            Assert.AreEqual(12, firstDeserialization.Byte[0], "Byte is wrong");
            Assert.AreEqual(-1, firstDeserialization.SByte[0], "SByte is wrong");
            Assert.AreEqual('b', firstDeserialization.Char[1], "Char is wrong");
            Assert.AreEqual(1, firstDeserialization.Double[0], "Double is wrong");
            Assert.AreEqual(1, firstDeserialization.Short[0], "Short is wrong");
            Assert.AreEqual(1, firstDeserialization.UShort[0], "UShort is wrong");
            Assert.AreEqual(1, firstDeserialization.Int[0], "Int is wrong");
            Assert.AreEqual(1, firstDeserialization.UInt[0], "UInt is wrong");
            Assert.AreEqual(1, firstDeserialization.Long[0], "Long is wrong");
            Assert.AreEqual(1, firstDeserialization.ULong[0], "ULong is wrong");
            Assert.AreEqual(1f, firstDeserialization.Float[0], "Float is wrong");
            Assert.AreEqual("second", firstDeserialization.String[1], "String is wrong");
            Assert.AreEqual(TestEnum.Third, firstDeserialization.Enum[1], "Enum is wrong");
            Assert.AreEqual(1f, firstDeserialization.Vector2[0].x, "Vector2 is wrong");
            Assert.AreEqual(1f, firstDeserialization.Vector3[0].x, "Vector3 is wrong");
            Assert.AreEqual(1f, firstDeserialization.Vector4[0].x, "Vector4 is wrong");
            Assert.AreEqual(1, firstDeserialization.Vector2Int[0].x, "Vector2Int is wrong");
            Assert.AreEqual(1, firstDeserialization.Vector3Int[0].x, "Vector3Int is wrong");
            Assert.AreEqual(800, firstDeserialization.Rect[1].width, "Rect is wrong");
            Assert.AreEqual(800, firstDeserialization.RectInt[1].width, "RectInt is wrong");
            Assert.AreEqual(5, firstDeserialization.RectOffset[1].left, "RectOffset is wrong");
            Assert.AreEqual(1f, firstDeserialization.Quaternion[1].x, "Quaternion is wrong");
            Assert.AreEqual(1f, firstDeserialization.Color[1].g, "Color is wrong");

            // Second Serialization
            var secondSerialization = BinarySerializer.Serialize(testSerializableMessage);
            benchmark.Round("Second Deserialization");

            // Second Deserialization
            var secondDeserialization = BinarySerializer.Deserialize<TestListData>(secondSerialization);
            benchmark.Round("Second Deserialization");

            // Object Deserialization
            var unknownTypeDeserialization = BinarySerializer.Deserialize(secondSerialization);
            benchmark.Round("Deserialization without type");

            Debug.Log(benchmark.GetMessage(UsePadRightForBenchmarkMessages));
        }
    }
}