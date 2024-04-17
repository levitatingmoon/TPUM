using LogicServer;
using PresentationServer;
using Newtonsoft.Json.Schema;
using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;
using JsonSchema = NJsonSchema.JsonSchema;

namespace Generator;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine("JSON Schema: \n\n");
        JsonSchema schema = JsonSchema.FromType<SellItemCommand>();
        string stringSchema = schema.ToJson();

        Console.WriteLine(stringSchema);

        Console.WriteLine("C#: \n\n");

        JsonSchema jsonSchema = await JsonSchema.FromJsonAsync(stringSchema);
        CSharpGenerator csharpGenerator = new CSharpGenerator(jsonSchema);
        Console.WriteLine(csharpGenerator.GenerateFile());
    }
}