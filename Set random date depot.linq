<Query Kind="Program">
  <Namespace>System.Data.SqlClient</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <IncludeLinqToSql>true</IncludeLinqToSql>
</Query>

async Task Main()
{
	var httpClient =
		new HttpClient {
			BaseAddress = new Uri("https://api-ged-intra.int.maf.local/v1/Documents/")
		};
	const string code_rédacteur = "ROD";
	var actual_documents =
		await httpClient.GetStringAsync(
			$"?$filter=assigneRedacteur eq '{code_rédacteur}'&$select=documentId");
	var _document = new { documentId = "" };
	var update_documents =
		ExtractDocumentsId(actual_documents)
			.Select(documentId => {
				var randomDateDepot = GetRandomDate().ToString("yyyy-MM-dd");
				var requestContent = new StringContent($@"{{""deposeLe"":""{randomDateDepot}Z""}}", Encoding.UTF8, "application/json");
				return httpClient.PatchAsync(documentId, requestContent);
			});
	(await Task.WhenAll(update_documents))
		.Where(httpRequest => !httpRequest.IsSuccessStatusCode)
		.Select(httpRequest =>
			new {
				RequestUri = httpRequest.RequestMessage.RequestUri,
				IsSuccessStatusCode = httpRequest.IsSuccessStatusCode
			}).Dump();
	"terminé".Dump();
}

readonly DateTime min_date = new DateTime(year: 1990, month: 01, day: 01, kind: DateTimeKind.Local, hour: 0, minute: 0, second: 0);
readonly DateTime max_date = DateTime.Now;
readonly Random dice = new();
DateTime GetRandomDate() {
	var random_date_ticks = dice.NextInt64(min_date.Ticks, max_date.Ticks + 1);
	return new DateTime(ticks: random_date_ticks, kind: DateTimeKind.Local).Date;
}

IEnumerable<string> ExtractDocumentsId(string json_content) {
	var documentsList = JsonDocument.Parse(json_content).RootElement.GetProperty("value");
	var nb_documents = documentsList.GetArrayLength();
	return Enumerable.Range(0, nb_documents).Select(i => documentsList[i].GetProperty("documentId").GetString());
}
