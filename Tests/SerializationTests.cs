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

        var sw = Stopwatch.StartNew();
        var bytes = testSerializableMessage.Serialize();
        Debug.Log("Serialization: " + sw.Elapsed);
        
        Debug.Log(bytes.Aggregate("", (s, b) => s + BitConverter.ToString(new [] {b}) + " "));
        
        sw = Stopwatch.StartNew();
        var result = SerializableMessage.Deserialize<TestSerializableMessage>(bytes);
        Debug.Log("Deserialization: " + sw.Elapsed);
        
        Assert.AreEqual(123, result.IntValue);
        Assert.AreEqual(21.05f, result.FloatValue);
        Assert.AreEqual("Test", result.StringValue);
        Assert.AreEqual(Vector3.forward, result.Vector3Value);
        Assert.AreEqual(TestEnum.Second, result.EnumValue);
        Assert.AreEqual(true, result.BoolValue);
        Assert.AreEqual(2f, result.FloatArray[1]);
        Assert.AreEqual(4, result.IntArray[1]);
        Assert.AreEqual(5, result.UshortArray[1]);
        Assert.AreEqual(1f, result.ColorValue.r);
    }
}
