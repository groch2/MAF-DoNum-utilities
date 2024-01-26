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
		BaseAddress = new Uri("https://api-ged-intra.hom.maf.local/v1/Documents/")
	};
var documentId =
	await httpClient.GetStringAsync(
		"?$filter=libelle eq 'Lettre Accompagnement Bordereau Courtier Num  7910023 vendredi 2 juin 2023' and assigneRedacteur eq 'CYO' and personneId eq 59169&$select=documentId");
var documents =
	JsonDocument
		.Parse(documentId)
		.RootElement.GetProperty("value")
		.EnumerateArray()
		.Select(document =>
			new { documentId = document.GetProperty("documentId").GetString() });
documents.Dump();