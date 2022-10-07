<Query Kind="Statements">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

var httpClient =
	new HttpClient {
		BaseAddress = new Uri("https://api-ged-intra.int.maf.local/v1/Documents/")
	};
var raw_documents = await httpClient.GetStringAsync("?$filter=statut eq 'INDEXE' and assigneRedacteur eq 'ROD' and (traitePar eq null or traitePar eq '') and traiteLe eq null&$select=documentId");
var _document = new { documentId = "" };
var array = JsonDocument.Parse(raw_documents).RootElement.GetProperty("value");
var nb_documents = array.GetArrayLength();
var document_id_list = Enumerable.Range(0, nb_documents).Select((index) => {
	var rawDocument = array[index].GetRawText();
	dynamic document = JsonSerializer.Deserialize(rawDocument, _document.GetType());
	return (string)document.documentId;
});
document_id_list.Dump();
 Environment.Exit(0);
//for(var index = 0; index < nb_documents; index++) {
//	var rawDocument = array[index].GetRawText();
//	dynamic document = JsonSerializer.Deserialize(rawDocument, _document.GetType());
//	string documentId = document.documentId;
//	var requestContent = new StringContent(rawDocument, Encoding.UTF8, "application/json");
//	var httpResponse = await httpClient.PatchAsync(documentId, requestContent);
//	new {
//		RequestUri = httpResponse.RequestMessage.RequestUri,
//		IsSuccessStatusCode = httpResponse.IsSuccessStatusCode
//	}.Dump();
//}
//