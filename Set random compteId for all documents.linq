<Query Kind="Program">
  <Namespace>System.Data.SqlClient</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Nodes</Namespace>
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
			$"?$filter=assigneRedacteur eq '{code_rédacteur}'&$select={string.Join(',', "documentId")}");
	var documentsId =
		JsonDocument
			.Parse(actual_documents)
			.RootElement.GetProperty("value")
			.EnumerateArray()
			.Select(jsonElement => jsonElement.GetProperty("documentId").ToString());
	var compteId = File.ReadAllLines(@"C:\Users\deschaseauxr\Documents\Donum\PersonneId_CompteId.txt")
		.Select(l => l.Split("\t"))
		.GroupBy(l => l[0])
		.Select(g => g.First().ToArray()[1])
		.ToArray();
	ShuffleArray(compteId);
	var results = await Task.WhenAll(documentsId.Select(async (documentId, index) => {
		var json = JsonSerializer.Serialize(new { documentId, compteId = compteId[index] });
		var requestContent = new StringContent(json, Encoding.UTF8, "application/json");
		return await httpClient.PatchAsync(documentId, requestContent);
	}));
	results.Select(httpResponse => new {
		RequestUri = httpResponse.RequestMessage.RequestUri,
		IsSuccessStatusCode = httpResponse.IsSuccessStatusCode
	}).Dump();
}

Random randy = new Random();
T[] ShuffleArray<T>(T[] array)
{
    for (var i = array.Length - 1; i > 0; i--)
    {
        var k = randy.Next(i + 1);
        var value = array[k];
        array[k] = array[i];
        array[i] = value;
    }
    return array;
}