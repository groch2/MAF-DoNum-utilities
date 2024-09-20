<Query Kind="Program">
  <Namespace>System.Data.SqlClient</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <IncludeLinqToSql>true</IncludeLinqToSql>
</Query>

const string connectionString = "Server=bdd-ged.int.maf.local;Database=GEDMAF;Trusted_Connection=True;";
static HttpClient httpClient = new HttpClient { BaseAddress = new Uri("https://api-ged-intra.int.maf.local/v2/Documents/") };
async Task Main() {
	var documentsIdList = (await GetDocumentsIdList()).ToArray();
	documentsIdList.Dump();
	await DeleteDocuments(documentsIdList);
	var nbDeletedDocuments = await DeleteDocuments(Enumerable.Range(1, 3).Select(n => $"{n}"));
	new { nbDeletedDocuments }.Dump();
	return;

	using var connection = new SqlConnection(connectionString);
	using var command = connection.CreateCommand();
	connection.Open();
	command.CommandText = "SELECT @@VERSION";
	var result = command.ExecuteScalar();
	result.Dump();
}

static async Task<IEnumerable<string>> GetDocumentsIdList() {
	const string userCode = "ROD";
	var documents_json =
		await httpClient
			.GetStringAsync($"?$filter=statut eq 'INDEXE' and assigneRedacteur eq '{userCode}'");
	return
		JsonDocument
			.Parse(documents_json)
			.RootElement
			.GetProperty("value")
			.EnumerateArray()
			.Select(json =>
				json.GetProperty("documentId").GetString());
}

static async Task<int> DeleteDocuments(IEnumerable<string> documentsIdList) {
	using var connection = new SqlConnection(connectionString);
	using var command = connection.CreateCommand();
	var documentIdListFormated = $"('{string.Join("','", documentsIdList)}')";
	command.CommandText = @$"DELETE FROM [dbo].[ARCHEAMAF] WHERE [ID_DOC] IN {documentIdListFormated}";
	await connection.OpenAsync();
	var nbAffectedRows = await command.ExecuteNonQueryAsync();
	return nbAffectedRows;
}
