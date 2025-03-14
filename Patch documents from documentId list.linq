<Query Kind="Statements">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Json</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Nodes</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <IncludeLinqToSql>true</IncludeLinqToSql>
</Query>

var httpClient =
	new HttpClient {
		BaseAddress = new Uri("https://api-ged-intra.int.maf.local/v2/Documents/")
	};
var chantierIdList = File.ReadAllLines(@"C:\Users\deschaseauxr\Documents\Donum\chantier_id.txt");
var patchDocumentsResponses =
	await Task.WhenAll(
		File
			.ReadAllLines(@"C:\Users\deschaseauxr\Documents\Donum\doc_id_patch.txt")
			.Select(async (documentId, index) => {
				var documentPatch =
					JsonSerializer.SerializeToNode(
						new {
							ChantierId = chantierIdList[index],
							ModifiePar = "ROD"
						});
				var patchDocumentResponse = await PatchSingleDocument(documentId, documentPatch);
				patchDocumentResponse.EnsureSuccessStatusCode();
				return patchDocumentResponse;
			}));
patchDocumentsResponses
	.Select(
		patchDocumentResponse =>
			new {
				documentId = patchDocumentResponse.RequestMessage.RequestUri.Segments[^1],
				patchDocumentResponse.ReasonPhrase,
				patchDocumentResponse.StatusCode
			})
	.Dump();

async Task<HttpResponseMessage> PatchSingleDocument(
	string documentId, 
	JsonNode documentPatch) {
		var requestContent = JsonContent.Create(documentPatch);
		return await httpClient.PatchAsync(documentId, requestContent);
}