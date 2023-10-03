# 💾 Binary Serialization

A class and set of functions for serializing and deserializing class-content into a byte array.

# Introduction

I developed this add-on for my needs when working with Photon Realtime for a more convenient interaction with Network Events, based on strong typing. 
The plugin worked great, providing me with just one message type with a byte array body that allows me to serialize and deserialize messages in a matter of milliseconds.

# Installation

Just copy a the source code of this repository into your Unity Project (Assets folder).

# Usage

Declare an any class and declare fields in it.

```csharp
private class TestSerializableMessage
{
    public int IntValue;
    public string StringValue;
    public Vector3 Vector3Value;
    public TestEnum EnumValue;
}
```

For initialization of the default binary converters use `BinarySerializer.AddDefaultConverters()`:

```csharp
BinarySerializer.AddDefaultConverters();
```

For registration new types for binary serialization use `BinarySerializer.RegisterType<T>()`:

```csharp
BinarySerializer.RegisterType<TestSerializableMessage>()
```

For serialization, use method `BinarySerializer.Serialize(object)`:

```csharp
var testSerializableMessage = new TestSerializableMessage()
{
    IntValue = 123,
    StringValue = "Test",
    Vector3Value = Vector3.forward,
    EnumValue = TestEnum.Second
};
var bytes = BinarySerializer.Serialize(testSerializableMessage);
```

You can send the received array of bytes and, having received it, deserialize it using the `BinarySerializer.Deserialize(byte[] bytes)` function.

```csharp
var result = BinarySerializer.Deserialize<TestSerializableMessage>(bytes);
Debug.Log(result.StringValue);
```

Unknown type deserialization:

```csharp
object unknownTypeDeserialization = BinarySerializer.Deserialize(bytes);
```

# Custom serializator/deserializator

You need to declare class and inherit it from `IBinaryConverter` and add it to BinarySerializer's Converters:

```csharp
public class BoolBinaryConverter : IBinaryConverter
{
    public Type SerializationType => typeof(bool);

    public void Serialize(object value, BinaryWriter bw)
    {
        bw.Write((bool) value);
    }

    public object Deserialize(BinaryReader br)
    {
        return br.ReadBoolean();
    }
}
```
```csharp
// also add extension -converter for arrays of this type
BinarySerializer.AddConverterWithExtensions<BoolBinaryConverter>();
```

# Performance

| State                        | Duration         |
|------------------------------|------------------|
| Adding default converters    | 00:00:00.0044790 |
| Type registration            | 00:00:00.0019408 |
| First Serialization          | 00:00:00.0050046 |
| First Deserialization        | 00:00:00.0061455 |
| Second Serialization         | 00:00:00.0001749 |
| Second Deserialization       | 00:00:00.0001831 |
| Unknown Type Deserialization | 00:00:00.0002780 |

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
* RectOffset
* RectOffset[]
* Quaternion
* Quaternion[]
* Color
* Color[]

# License

Code and documentation Copyright (c) 2023 Alexander Travkin.

Code released under the MIT license.