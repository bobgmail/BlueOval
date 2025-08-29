using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlueOvalBatteryPark.SAPinterface.DataConverter;

public class StringToDoubleConverter : JsonConverter<double>
{
    public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            if (double.TryParse(reader.GetString(), out double value))
            {
                return value;
            }
        }
        return reader.GetDouble();
    }

    public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("N3"));
    }
}

public class StringToDecimalConverter : JsonConverter<decimal>
{
    public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var str= reader.GetString();
            if (decimal.TryParse(str, out decimal value))
            {
                return value;
            }
            else
            {
                return -1; // Indicate an invalid decimal value
                //throw new JsonException($"Unable to convert \"{str}\" to decimal.");
            }
        }
        return reader.GetDecimal();
    }

    public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("N3"));
    }
}

public class StringToFloatConverter : JsonConverter<float>
{
    public override float Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            if (float.TryParse(reader.GetString(), out float value))
            {
                return value;
            }
            else
                return -1; // Indicate an invalid float value
        }
        return reader.GetSingle();  //In .NET, a float is an alias for the System.Single type
    }

    public override void Write(Utf8JsonWriter writer, float value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("N3"));
    }
}

public class StringToIntConverter : JsonConverter<int>
{
    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            if (int.TryParse(reader.GetString(), out int value))
            {
                return value;
            }
            else
                return -1; // Indicate an invalid int value
                           //throw new JsonException($"Unable to convert \"{reader.GetString()}\" to int.");
        }
        return reader.GetInt32();  
    }

    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("N3"));
    }
}