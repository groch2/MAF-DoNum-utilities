<Query Kind="Statements">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Nodes</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

var triptyques = 
	File
		.ReadAllLines(@"C:\Users\deschaseauxr\Documents\GED\triptyques.csv")
		.Select(line => {
			var data = line.Split(',');
			return new { Famille = data[0], Cote = data[1], TypeDoc = data[2] };
		})
		.GroupBy(triptyque => triptyque.Famille, triptyque => new { triptyque.Cote, triptyque.TypeDoc })
		.ToDictionary(
			group => group.Key,
			group =>
				group
					.GroupBy(
						triptyque => triptyque.Cote,
						triptyque => triptyque.TypeDoc)
					.ToDictionary(
						group => group.Key,
						group => group.ToArray()));
//triptyques["DOCUMENTS CONTRAT"]["CONTENTIEUX"].Dump();
//Environment.Exit(0);
var httpClient =
	new HttpClient {
		BaseAddress = new Uri("https://api-ged-intra.int.maf.local/v2/Documents/")
	};
var documents_id_list = File.ReadAllLines(@"C:\Users\deschaseauxr\Documents\Donum\documents_id_list.txt");
var familleDocumentsContrat = triptyques["DOCUMENTS CONTRAT"];
var document_contrat_cotes = familleDocumentsContrat.Keys.ToArray();
var documents_with_new_triptyque =
	documents_id_list.Select((document_id, index) => {
		var cote = document_contrat_cotes[index % document_contrat_cotes.Length];
		var typesDoc = familleDocumentsContrat[cote];
		var typeDoc = typesDoc[index % typesDoc.Length];
		var raw_document_data = new Dictionary<string, string>{
			{ "documentId", document_id },
			{ "categoriesFamille", "DOCUMENTS CONTRAT" },
	    	{ "categoriesCote", cote },
	    	{ "categoriesTypeDocument", typeDoc }
		};
		return JsonNode.Parse(JsonSerializer.Serialize(raw_document_data));
	});
//documents_with_new_triptyque.Dump();
//Environment.Exit(0);
foreach (var document in documents_with_new_triptyque) {
	var documentId = document["documentId"].GetValue<string>();
	var requestContent = new StringContent(document.ToJsonString(), Encoding.UTF8, "application/json");
	var httpResponse = await httpClient.PatchAsync(documentId, requestContent);
	//new {
	//	RequestUri = httpResponse.RequestMessage.RequestUri,
	//	IsSuccessStatusCode = httpResponse.IsSuccessStatusCode
	//}.Dump();
	if (!httpResponse.IsSuccessStatusCode) {
		document.Dump();
		httpResponse.StatusCode.Dump();
	}
}
