<Query Kind="Program">
  <Reference>C:\TeamProjects\GED API\MAF.GED.API.Host\bin\Debug\net6.0\MAF.GED.Domain.Model.dll</Reference>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Net.Http.Json</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Nodes</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <IncludeLinqToSql>true</IncludeLinqToSql>
</Query>

async Task Main() {	
	var documentOrigine = await GetDocumentFromGED_SOURCE("20241023114410718522616782");
	var propertiesToRemove = new [] {
		"@odata.context",
		"docn",
		"documentId",
		"fichierTaille"
	};
	documentOrigine = RemovePropertiesFromJsonNode(documentOrigine, propertiesToRemove);
	var canalId =
		await GetCanalIdByCanalPrincipalAndCanalSecondaire(
			canalPrincipal: documentOrigine["canalPrincipal"].GetValue<string>(),
			canalSecondaire: documentOrigine["canalSecondaire"].GetValue<string>());
	documentOrigine["canalId"] = canalId;
	//new { documentOrigine }.Dump();
	//return;
	var newDocumentId = await UploadDocumentToGed(
		filePath: @"C:\Users\deschaseauxr\Documents\Donum\documents de test d'insertion\AA.txt",
		documentMetadata: documentOrigine);
	new { newDocumentId }.Dump();
}

static readonly HttpClient httpClient_SOURCE =
	new HttpClient {
		BaseAddress = new Uri("https://api-ged-intra.HOM.maf.local/v2/Documents/")};
static async Task<JsonNode> GetDocumentFromGED_SOURCE(string documentId) {
	var actual_document = await httpClient_SOURCE.GetStringAsync(documentId);
	var documentOrigine =
		JsonNode.Parse(
			JsonDocument
				.Parse(actual_document)
				.RootElement
				.ToString());
	return documentOrigine;
}

const string GED_API_ADDRESS_CIBLE =
	$"https://localhost:44326/v2/";
static readonly HttpClient gedApiHttpClient_CIBLE =
	new HttpClient { BaseAddress = new Uri(GED_API_ADDRESS_CIBLE) };
static async Task<string> UploadDocumentToGed(
	string filePath,
	JsonNode documentMetadata) {
	var fileName = Path.GetFileName(filePath);
	var documentUploadId =
		await UploadFile(
			filePath: filePath,
			fileName: fileName);
	var documentId =
		await FinalizeUpload(
			documentUploadId: documentUploadId,
			fileName: fileName,
			documentMetadata: documentMetadata);
	return documentId;

	async Task<string> UploadFile(string filePath, string fileName) {
		await using var stream =
			File.OpenRead(filePath);
		using var request =
			new HttpRequestMessage(
				HttpMethod.Post, 
				new Uri(
					"upload",
					UriKind.Relative));
		using var content =
			new MultipartFormDataContent { 
				{
					new StreamContent(stream), 
					"file",
					fileName
				}
			};
		request.Content = content;
		using var response = await gedApiHttpClient_CIBLE.SendAsync(request);
		response.EnsureSuccessStatusCode();
		var responseContent = await response.Content.ReadAsStringAsync();
		return JsonNode.Parse(responseContent)["guidFile"].GetValue<string>();
	}

	async Task<string> FinalizeUpload(
		string documentUploadId,
		string fileName,
		JsonNode documentMetadata) {
		documentMetadata["fileId"] = documentUploadId;
		documentMetadata["fichierNom"] = fileName;
		using var documentUploadJson = JsonContent.Create(documentMetadata);
		using var response =
			await gedApiHttpClient_CIBLE.PostAsync(
				new Uri(
					"finalizeUpload",
					UriKind.Relative),
				documentUploadJson);
		response.EnsureSuccessStatusCode();
		var uploadResponseContent = await response.Content.ReadAsStringAsync();
		return JsonNode
			.Parse(uploadResponseContent)["documentId"]
			.GetValue<string>();
	}
}

static JsonNode RemovePropertiesFromJsonNode(JsonNode jsonNode, IEnumerable<string> propertiesToRemove) {
	var jsonObject = jsonNode.AsObject();
	foreach(var property in propertiesToRemove) {
		jsonObject.Remove(property);
	}
	return JsonNode.Parse(jsonObject.ToString());
}

static async Task<int> GetCanalIdByCanalPrincipalAndCanalSecondaire(
	string canalPrincipal,
	string canalSecondaire) {
	var responseContent = await httpClient_SOURCE.GetStringAsync($"https://api-ged-intra.hom.maf.local/v2/Canaux/?$filter=canalPrincipal eq '{canalPrincipal}' and canalSecondaire eq '{canalSecondaire}'");
	return JsonDocument.Parse(responseContent).RootElement.GetProperty("value").EnumerateArray().Single().GetProperty("id").GetInt32();
}