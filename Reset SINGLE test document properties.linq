<Query Kind="Statements">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

var httpClient =
	new HttpClient {
		BaseAddress = new Uri("https://api-ged-intra.int.maf.local/v1/Documents/")
	};
var _document = new { documentId = "" };
var fileContent = File.ReadAllText(@"C:\Users\deschaseauxr\Documents\DONUM\SINGLE_document.json");
var json = JsonDocument.Parse(fileContent).RootElement;
var array = json.GetProperty("value");
var nb_documents = array.GetArrayLength();
new { nb_documents }.Dump(); 
// dé-commenter cette ligne pour tester
// Environment.Exit(0);
for(var index = 0; index < nb_documents; index++) {
	var rawDocument = array[index].GetRawText();
	dynamic document = JsonSerializer.Deserialize(rawDocument, _document.GetType());
	string documentId = document.documentId;
	var requestContent = new StringContent(rawDocument, Encoding.UTF8, "application/json");
	var httpResponse = await httpClient.PatchAsync(documentId, requestContent);
	new {
		RequestUri = httpResponse.RequestMessage.RequestUri,
		IsSuccessStatusCode = httpResponse.IsSuccessStatusCode
	}.Dump();
}
