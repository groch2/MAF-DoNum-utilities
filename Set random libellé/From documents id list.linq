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
	var update_documents =
		File.ReadAllLines(@"C:\Users\deschaseauxr\Documents\Donum\Set random libellé\doc_id_list.txt")
			.Select((documentId, index) => {
				var randomWord = GetRandomWord();
				var requestContent = new StringContent($@"{{""libelle"":""{index + 1:00} {randomWord}""}}", Encoding.UTF8, "application/json");
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

readonly Random dice = new();
string GetRandomWord() =>
	new string(new int[10].Select(_ => (char)(dice.Next(26) + (int)'A')).ToArray());
