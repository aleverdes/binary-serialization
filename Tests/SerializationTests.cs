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
            IntValue = 123,
            FloatValue = 21.05f,
            StringValue = "Test",
            Vector3Value = Vector3.forward,
            EnumValue = TestEnum.Second,
            BoolValue = true,
            FloatArray = new []{1f, 2f, 3f},
            IntArray = new []{ 2, 4, 6 },
            UshortArray = new ushort[] { 3, 5, 7 },
            ColorValue = Color.red
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
        Assert.AreEqual(123, firstDeserialization.IntValue);
        Assert.AreEqual(21.05f, firstDeserialization.FloatValue);
        Assert.AreEqual("Test", firstDeserialization.StringValue);
        Assert.AreEqual(Vector3.forward, firstDeserialization.Vector3Value);
        Assert.AreEqual(TestEnum.Second, firstDeserialization.EnumValue);
        Assert.AreEqual(true, firstDeserialization.BoolValue);
        Assert.AreEqual(2f, firstDeserialization.FloatArray[1]);
        Assert.AreEqual(4, firstDeserialization.IntArray[1]);
        Assert.AreEqual(5, firstDeserialization.UshortArray[1]);
        Assert.AreEqual(1f, firstDeserialization.ColorValue.r);

        // Second Serialization
        sw = Stopwatch.StartNew();
        var secondSerialization = testSerializableMessage.Serialize();
        Debug.Log("Second Serialization: " + sw.Elapsed);
        
        // Second Deserialization
        sw = Stopwatch.StartNew();
        var secondDeserialization = SerializableMessage.Deserialize<TestSerializableMessage>(firstSerialization);
        Debug.Log("First Deserialization: " + sw.Elapsed);
        
        // Clear
        firstSerialization = null;
        firstDeserialization = null;
        secondSerialization = null;
        secondDeserialization = null;
        sw.Stop();
        sw = null;
    }
}
