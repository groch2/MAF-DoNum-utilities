<Query Kind="Statements">
  <Namespace>System.Data.SqlClient</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
  <IncludeLinqToSql>true</IncludeLinqToSql>
</Query>

var httpClient =
	new HttpClient {
		BaseAddress = new Uri("https://api-ged-intra.int.maf.local/v1/Documents/")
	};
const string code_rédacteur = "ROD";
var actual_documents =
	await httpClient.GetStringAsync(
		$"?$filter=statut eq 'INDEXE' and assigneRedacteur eq '{code_rédacteur}' and (traitePar eq null or traitePar eq '') and traiteLe eq null&$select=documentId");
var _document = new { documentId = "" };
var get_documents_id_list = new Func<JsonElement, IEnumerable<string>>(jsonArray => {
	var nb_documents = jsonArray.GetArrayLength();
	return Enumerable.Range(0, nb_documents).Select((index) => {
		var rawDocument = jsonArray[index].GetRawText();
		dynamic document = JsonSerializer.Deserialize(rawDocument, _document.GetType());
		return (string)document.documentId;
	});
});
var array = JsonDocument.Parse(actual_documents).RootElement.GetProperty("value");
var documents_id_list = get_documents_id_list(array).ToArray();

// fichier json qui contient une version antérieure des documents du rédacteur
var fileContent = File.ReadAllText(@"C:\Users\deschaseauxr\Documents\DONUM\documents.json");
var previous_documents = JsonDocument.Parse(fileContent).RootElement.GetProperty("value");
var previous_documents_id_list = get_documents_id_list(previous_documents).ToArray();

var documents_id_à_supprimer = documents_id_list.Except(previous_documents_id_list).ToArray();
if (documents_id_à_supprimer.Length == 0) {
	"Aucun nouveau document à supprimer".Dump();
	Environment.Exit(0);
}
documents_id_à_supprimer.Dump();

documents_id_à_supprimer = documents_id_à_supprimer.Select(id => $"'{id}'").ToArray();
var concatenated_documents_id_to_delete = string.Join(',', documents_id_à_supprimer);
using var connection = new SqlConnection("Server=DNSINTBDDGECO01;Database=GEDMAF;Integrated Security=True");
connection.Open();
using var command = connection.CreateCommand();
command.CommandText = $"DELETE FROM [dbo].[ARCHEAMAF] WHERE [ID_DOC] IN ({concatenated_documents_id_to_delete})";
var nb_documents_supprimés = (int)(await command.ExecuteNonQueryAsync());
new { nb_documents_supprimés }.Dump();
