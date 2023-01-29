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
            StringValue = "Test",
            Vector3Value = Vector3.forward,
            EnumValue = TestEnum.Second
        };

        var sw = Stopwatch.StartNew();
        var bytes = testSerializableMessage.Serialize();
        Debug.Log("Serialization: " + sw.Elapsed);
        
        Debug.Log(bytes.Aggregate("", (s, b) => s + BitConverter.ToString(new [] {b}) + " "));
        
        sw = Stopwatch.StartNew();
        var result = SerializableMessage.Deserialize<TestSerializableMessage>(bytes);
        Debug.Log("Deserialization: " + sw.Elapsed);
        
        Assert.AreEqual(123, result.IntValue);
        Assert.AreEqual("Test", result.StringValue);
        Assert.AreEqual(Vector3.forward, result.Vector3Value);
        Assert.AreEqual(TestEnum.Second, result.EnumValue);
    }
    
    private class TestSerializableMessage : SerializableMessage
    {
        public int IntValue;
        public string StringValue;
        public Vector3 Vector3Value;
        public TestEnum EnumValue;
    }

    private enum TestEnum
    {
        First,
        Second,
        Third
    }
}
