<Query Kind="Statements">
  <Namespace>System.Text.Json</Namespace>
</Query>

var rawJson =
	await new System.Net.Http.HttpClient()
		.GetStringAsync("https://api-ged-intra.int.maf.local/swagger/v1/swagger.json");
JsonDocument
	.Parse(rawJson)
	.RootElement
	.GetProperty("components")
	.GetProperty("schemas")
	.GetProperty("Document")
	.GetProperty("properties")
	.EnumerateObject()
	//.Select(property => property.Name)
	//.OrderBy(name => name, StringComparer.OrdinalIgnoreCase)
	.OrderBy(property => property.Name, StringComparer.OrdinalIgnoreCase)
	.Select(property => new { property.Name, Type = $"{property.Value.GetProperty("type")}" })
	.Dump();