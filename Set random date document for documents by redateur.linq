<Query Kind="Program">
  <Namespace>System.Data.SqlClient</Namespace>
  <Namespace>System.Globalization</Namespace>
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
	var updateDocuments =
		JsonDocument
			.Parse(actual_documents)
			.RootElement.GetProperty("value")
			.EnumerateArray()
			.Select(document => {
				var documentId = document.GetProperty("documentId").GetString();
				var randowDate = GetRandomDate().ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
				var requestContent = new StringContent($@"{{""dateDocument"":""{randowDate}""}}", Encoding.UTF8, "application/json");
				return httpClient.PatchAsync(documentId, requestContent);
			});
		(await Task.WhenAll(updateDocuments))
			.Where(r => !r.IsSuccessStatusCode).Dump();
		"terminé".Dump();
}

static DateTime GetLocalDateTime(int year, int month, int day) =>
	new DateTime(year, month, day, kind: DateTimeKind.Local, hour: 0, minute: 0, second: 0);
static readonly DateTime min_date = GetLocalDateTime(year: 1990, month: 01, day: 01);
static readonly DateTime max_date = GetLocalDateTime(year: 2015, month: 12, day: 31);
static readonly Random dice = new();
static DateOnly GetRandomDate() {
	var random_date_ticks = dice.NextInt64(min_date.Ticks, max_date.Ticks + 1);
	var random_date = new DateTime(ticks: random_date_ticks, kind: DateTimeKind.Local);
	return DateOnly.FromDateTime(random_date);
}

