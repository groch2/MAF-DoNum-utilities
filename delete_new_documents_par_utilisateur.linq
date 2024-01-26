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
		$"?$filter=assigneRedacteur eq '{code_rédacteur}'&$select=documentId");
var _document = new { documentId = "" };
var get_documents_id_list = new Func<string, IEnumerable<string>>(json_content => {
	var documentsList = JsonDocument.Parse(json_content).RootElement.GetProperty("value");
	var nb_documents = documentsList.GetArrayLength();
	return Enumerable.Range(0, nb_documents).Select(i => documentsList[i].GetProperty("documentId").GetString());
});
var documents_id_list = get_documents_id_list(actual_documents).ToArray();

// fichier json qui contient une version antérieure des documents du rédacteur
const string previous_rédacteur_documents_file_path = @"C:\Users\deschaseauxr\Documents\DONUM\documents.json";
var fileContent = File.ReadAllText(previous_rédacteur_documents_file_path);
var previous_documents_id_list = get_documents_id_list(fileContent).ToArray();
//previous_documents_id_list = new []{ "20221125113056617380186367" };

var documents_id_à_supprimer = documents_id_list.Except(previous_documents_id_list).ToArray();
if (documents_id_à_supprimer.Length == 0) {
	"Aucun nouveau document à supprimer".Dump();
	Environment.Exit(0);
}
documents_id_à_supprimer.Dump();

documents_id_à_supprimer = documents_id_à_supprimer.Select(id => $"'{id}'").ToArray();
var concatenated_documents_id_to_delete = string.Join(',', documents_id_à_supprimer);
using var connection = new SqlConnection("Server=bdd-donum.int.maf.local;Database=GEDMAF;Integrated Security=True");
connection.Open();
using var command = connection.CreateCommand();
command.CommandText = $"DELETE FROM [dbo].[ARCHEAMAF] WHERE [ID_DOC] IN ({concatenated_documents_id_to_delete})";
var nb_documents_supprimés = (int)(await command.ExecuteNonQueryAsync());
new { nb_documents_supprimés }.Dump();
