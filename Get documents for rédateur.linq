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
const string code_rédacteur = "ROD";
var actual_documents =
	await httpClient.GetStringAsync(
		$"?$filter=assigneRedacteur eq '{code_rédacteur}'&$select=documentId,libelle,deposeLe,traiteLe,qualiteValideeLe,qualiteValideeValide");
var documentType = new { documentId = "", libelle = "", deposeLe = "", traiteLe = "", qualiteValideeLe = "", qualiteValideeValide = (bool?)null }.GetType();
var documents =
	JsonDocument
		.Parse(actual_documents)
		.RootElement.GetProperty("value")
		.EnumerateArray()
		.Select(document => JsonSerializer.Deserialize(document, documentType));
documents.Dump();
