namespace TilTool.Cli.Core;

using YamlDotNet.Serialization;
using YamlDotNet.Serialization.Converters;
using YamlDotNet.Serialization.NamingConventions;

public static class Yaml
{
    private const string DateOnlyFormat = "yyyy-MM-dd";

    private static readonly DateOnlyConverter DateOnlyConverter = new(null, false, DateOnlyFormat);

    public static T Deserialize<T>(string yaml) =>
        CreateDeserializer().Deserialize<T>(yaml);

    public static string Serialize(object graph) =>
        CreateSerializer().Serialize(graph)
            .Trim();

    private static IDeserializer CreateDeserializer() =>
        new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance)
            .WithTypeConverter(DateOnlyConverter)
            .Build();

    private static ISerializer CreateSerializer() =>
        new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance)
            .WithTypeConverter(DateOnlyConverter)
            .Build();
}
