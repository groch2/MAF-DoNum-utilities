<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Json</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Nodes</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <IncludeLinqToSql>true</IncludeLinqToSql>
</Query>

const string environment = "int";
async Task Main() {
	var patchDocumentsResponses =
		await Task.WhenAll(
			File
				.ReadAllLines(@"C:\Users\deschaseauxr\Documents\Donum\Document carte d'identité et KBIS en lecture seule\jeu de test.txt")
				.Select((line, index) => {
					var data = line.Split(',');
					var documentId = data[0];
					var famille = data[1];
					var cote = data[2];
					var typeDocument = data[3];
					return new { documentId, famille, cote, typeDocument };
				})
				.GroupBy(item => new { item.famille, item.cote, item.typeDocument })
				.SelectMany(group =>
					group.Select((item, index) =>
						new {
							familleCodeIndex = index + 1,
							item.documentId,
							item.famille,
							item.cote,
							item.typeDocument,
						}))
				//.Select(item => Task.FromResult(item)));
				.Select(async item => {
					var documentId = item.documentId;
					var famille = item.famille;
					var cote = item.cote;
					var typeDocument = item.typeDocument;
					var libelle = $"{GetLibellePrefixByTypeDoc(typeDocument)} {item.familleCodeIndex}";
					var documentPatch =
						JsonSerializer.SerializeToNode(
							new {
								ModifiePar = "ROD",
								CategoriesFamille = famille,
								CategoriesCote = cote,
								CategoriesTypeDocument = typeDocument,
								Libelle = libelle,
								//vuLe = new DateTimeOffset(new DateTime(year: 1997, month: 11, day: 21)),
								//vuPar = "ROD"
								//vuLe = (object)null,
								//vuPar = (object)null,
								//qualiteValideeLe = (object)null,
								//qualiteValideePar = (object)null,
								//qualiteValideeValide = (object)null
								//Statut = "INDEXE"
							});
					var patchDocumentResponse =
						await PatchSingleDocument(documentId, documentPatch);
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
}

static string GetLibellePrefixByTypeDoc(string typeDoc) =>
	typeDoc.ToUpperInvariant() switch {
		"PIECE IDENTITE" => "IDENTITE",
		_ => typeDoc
	};

static async Task<HttpResponseMessage> PatchSingleDocument(
	string documentId, 
	JsonNode documentPatch) {
		using var requestContent = JsonContent.Create(documentPatch);
		return await httpClient.PatchAsync(documentId, requestContent);
}

static readonly HttpClient httpClient =
	new HttpClient {
		BaseAddress = new Uri($"https://api-ged-intra.{environment}.maf.local/v2/Documents/")
	};
