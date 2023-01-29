# Serializable Message

A class and set of functions for serializing and deserializing class-content into a byte array.

# Introduction

I developed this add-on for my needs when working with Photon Realtime for a more convenient interaction with Network Events, based on strong typing. 
The plugin worked great, providing me with just one message type with a byte array body that allows me to serialize and deserialize messages in a matter of milliseconds.

# Installation

Just copy a the source code of this repository into your Unity Project (Assets folder).

# Usage

Declare a class that inherits from SerializableMessage and declare fields in it.

```csharp
private class TestSerializableMessage : SerializableMessage
{
    public int IntValue;
    public string StringValue;
    public Vector3 Vector3Value;
    public TestEnum EnumValue;
}
```

For serialization, use SerializableMessage's method `Serialize()`:

```csharp
var testSerializableMessage = new TestSerializableMessage()
{
    IntValue = 123,
    StringValue = "Test",
    Vector3Value = Vector3.forward,
    EnumValue = TestEnum.Second
};
var bytes = testSerializableMessage.Serialize();
```

You can send the received array of bytes and, having received it, deserialize it using the `NetworkMessage.Deserialize(byte[] bytes)` function.

```csharp
var result = SerializableMessage.Deserialize<TestSerializableMessage>(bytes);
Debug.Log(result.StringValue);
```

# Performance

* Initialization of the plugin ~90ms.

* First Serialization: ~90ms.

* First Deserialization ~3ms.

* Second Serialization: <1ms.

* Second Deserialization: <1ms.

# Supported Types

C#:

* bool
* bool[]
* byte
* byte[]
* sbyte
* sbyte[]
* char
* char[]
* double
* double[]
* short
* short[]
* ushort
* ushort[]
* int
* int[]
* uint
* uint[]
* long
* long[]
* ulong
* ulong[]
* float
* float[]
* string
* string[]
* enum

Unity:

* Vector2
* Vector2[]
* Vector3
* Vector3[]
* Vector4
* Vector4[]
* Vector2Int
* Vector2Int[]
* Vector3Int
* Vector3Int[]
* Rect
* Rect[]
* RectInt
* RectInt[]
* Quaternion
* Quaternion[]
* Color
* Color[]

# License

Code and documentation Copyright (c) 2023 Alexander Travkin.

Code released under the MIT license.