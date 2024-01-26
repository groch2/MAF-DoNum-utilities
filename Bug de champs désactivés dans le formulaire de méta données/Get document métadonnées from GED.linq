<Query Kind="Statements">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Net.Http.Json</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Nodes</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

const string apiVersion = "v2";
var client = new HttpClient { BaseAddress = new Uri("https://api-ged-intra.hom.maf.local/") };
const string documentId = "20230602165454606387835562";
var documentAddress = new Uri($"/{apiVersion}/documents/{documentId}", UriKind.Relative);
var rawJson = await client.GetStringAsync(documentAddress);
var keep = new HashSet<string>(new [] { "[]", "false", "true" });
var documentJson =
	JsonNode.Parse(rawJson)
		.AsObject()
		.Where(node => !string.Equals(node.Key, "@odata.context", StringComparison.OrdinalIgnoreCase))
		.OrderBy(node => node.Key)
		.Select(node => new { key = node.Key, value = node.Value?.ToString() })
		.Aggregate(
			new StringBuilder("{"),
			(state, item) =>
				state.Append(
					$"\"{item.key}\" : {(item.value == null ? "null" : keep.Contains(item.value) ? item.value : $"\"{item.value}\"")}")
						.Append(","),
			final => final.Remove(final.Length - 1, 1).Append("}").ToString());
var niceJson = JsonNode.Parse(documentJson).ToJsonString (new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
//JsonNode.Parse(documentJson).ToJsonString (new System.Text.Json.JsonSerializerOptions { WriteIndented = true }).Dump();
//Environment.Exit(0);
const string tempFilePath = @"C:\Users\deschaseauxr\Documents\Donum\Copy document between environments\tmpF4B8.json";
File.WriteAllText(tempFilePath, niceJson);
Path.GetFileNameWithoutExtension(tempFilePath).Dump();
