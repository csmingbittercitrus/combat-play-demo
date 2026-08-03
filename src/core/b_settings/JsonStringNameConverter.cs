namespace BitterCitrus.SRC.Core.BSettings;

using Godot;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

// Json Serailizer가 StringName <-> string 간 전환을 자동으로 할 수 있도록 하는 컨버터

public class JsonStringNameConverter : JsonConverter<StringName>
{
    public override StringName Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return new StringName(reader.GetString() ?? string.Empty);
    }

    public override void Write(Utf8JsonWriter writer, StringName value, JsonSerializerOptions options)
    {
        writer.WriteStringValue((string)value);
    }

    public override StringName ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return new StringName(reader.GetString() ?? string.Empty);
    }

    public override void WriteAsPropertyName(Utf8JsonWriter writer, StringName value, JsonSerializerOptions options)
    {
        writer.WritePropertyName((string)value);
    }
}
