<Query Kind="Statements">
  <Reference>C:\TeamProjects\GED API\MAF.GED.API.Host\bin\Debug\net6.0\MAF.GED.Domain.Model.dll</Reference>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Text.Json</Namespace>
</Query>

var httpClient =
	new HttpClient {
		BaseAddress = new Uri("https://api-ged-intra.int.maf.local/v2/Documents/")
	};
const string userCode = "ROD";
var documents_json =
	await httpClient
		.GetStringAsync($"?$filter=statut eq 'INDEXE' and assigneRedacteur eq '{userCode}' and (traitePar eq null or traitePar eq '') and traiteLe eq null");
var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
JsonDocument
	.Parse(documents_json)
	.RootElement
	.GetProperty("value")
	.EnumerateArray()
	.Select(json =>
		JsonSerializer.Deserialize<MAF.GED.Domain.Model.Document>(json, jsonSerializerOptions))
	.Select(document => document.DocumentId)
	.Dump();
