using System.Text.Json.Serialization;

namespace ClipEvent;

[JsonSerializable(typeof(ClipboardEvent))]
internal partial class SourceGenerationContext : JsonSerializerContext;
