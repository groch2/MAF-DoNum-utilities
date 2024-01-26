<Query Kind="Statements">
  <Namespace>System.Data.SqlClient</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <IncludeLinqToSql>true</IncludeLinqToSql>
</Query>

var httpClient =
	new HttpClient {
		BaseAddress = new Uri("https://api-ged-intra.int.maf.local/v1/Documents/")
	};
const string documentId = "20230612153359255374578533";
var requestContent = new StringContent($@"{{""assigneRedacteur"":""ROD""}}", Encoding.UTF8, "application/json");
var response = await httpClient.PatchAsync(documentId, requestContent);
new { response.IsSuccessStatusCode }.Dump();