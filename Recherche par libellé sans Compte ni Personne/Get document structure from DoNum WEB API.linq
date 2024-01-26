<Query Kind="Statements">
  <Namespace>System.Text.Json</Namespace>
</Query>

var rawJson =
	await new System.Net.Http.HttpClient()
		.GetStringAsync("https://api-donum-intra.int.maf.local/swagger/internal/swagger.json");
JsonDocument
	.Parse(rawJson)
	.RootElement
	.GetProperty("components")
	.GetProperty("schemas")
	.GetProperty("DocumentDto")
	.GetProperty("properties")
	.EnumerateObject()
	.OrderBy(property => property.Name, StringComparer.OrdinalIgnoreCase)
	.Select(property => {
		var hasPrimitiveType = property.Value.TryGetProperty("type", out var type);
		return new { property.Name, Type = hasPrimitiveType ? $"{type}" : "object" };
	})
	.Dump();