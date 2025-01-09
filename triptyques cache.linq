<Query Kind="Statements">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Text.Json</Namespace>
</Query>

//typeof(Famille).GetProperties().Select(p => $"Famille{p.Name} = famille.{p.Name},").Dump();
//typeof(Cote).GetProperties().Select(p => $"Cote{p.Name} = cote.{p.Name},").Dump();
//typeof(TypeDocument).GetProperties().Select(p => $"TypeDocument{p.Name} = typeDocument.{p.Name},").Dump();
//Environment.Exit(0);

var httpClient = new HttpClient { BaseAddress = new Uri("https://api-ged-intra.int.maf.local/v2/") };
var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

var famillesIdListJsonResponse =
	await httpClient.GetStringAsync("Familles?$select=familleDocumentId&$filter=isActif eq true and code in ('DOCUMENTS CONTRAT','DOCUMENTS EMOA','DOCUMENTS PERSONNES')");
var famillesIdList =
	JsonDocument
		.Parse(famillesIdListJsonResponse)
		.RootElement
		.GetProperty("value")
		.EnumerateArray()
		.Select(item => item.GetProperty("familleDocumentId").GetInt32());

var famillesListJsonResponse =
	await httpClient.GetStringAsync($"Familles?$select=familleDocumentId,code,libelle&$filter=isActif eq true and familleDocumentId in ({ string.Join(',', famillesIdList.Select(id => $"{ id }")) })");
var famillesList =
	JsonDocument
		.Parse(famillesListJsonResponse)
		.RootElement.GetProperty("value")
		.EnumerateArray()
		.Select(item =>
			JsonSerializer.Deserialize<FamilleGED>(
			item.GetRawText(),
			jsonSerializerOptions));

var cotesListJsonResponse =
	await httpClient.GetStringAsync($"Cotes?$select=coteDocumentId,code,libelle,familleDocumentId&$filter=isActif eq true and familleDocumentId in ({ string.Join(',', famillesIdList.Select(id => $"{ id }")) })");
var cotesList =
	JsonDocument
		.Parse(cotesListJsonResponse)
		.RootElement.GetProperty("value")
		.EnumerateArray()
		.Select(item =>
			JsonSerializer.Deserialize<CoteGED>(
				item.GetRawText(),
				jsonSerializerOptions));

var typesDocumentListJsonResponse =
	await httpClient.GetStringAsync($"TypesDocuments?$select=typeDocumentId,code,libelle,coteDocumentId&$filter=isActif eq true and coteDocumentId in ({ string.Join(',', cotesList.Select(cote => $"{ cote.CoteDocumentId }")) })");
var typesDocumentList =
	JsonDocument
		.Parse(typesDocumentListJsonResponse)
		.RootElement.GetProperty("value")
		.EnumerateArray()
		.Select(item =>
			JsonSerializer.Deserialize<TypeDocumentGED>(
				item.GetRawText(),
				jsonSerializerOptions));
var allTritpyques =
	famillesList
		.Select(
			famille => {
				var _cotesList =
					cotesList
						.Where(cote => cote.FamilleDocumentId == famille.FamilleDocumentId)
						.Select(cote => {
							var _typesDocumentList =
								typesDocumentList
									.Where(typeDocument => typeDocument.CoteDocumentId == cote.CoteDocumentId)
									.Select(typeDocument =>
										new TypeDocument(typeDocument.TypeDocumentId, typeDocument.Code, typeDocument.Libelle, typeDocument.CoteDocumentId));
							return new Cote(cote.CoteDocumentId, cote.Code, cote.Libelle, cote.FamilleDocumentId, _typesDocumentList);
						});
				return new Famille(famille.FamilleDocumentId, famille.Code, famille.Libelle, _cotesList);
		});

allTritpyques.Dump();

record FamilleGED(int FamilleDocumentId, string Code, string Libelle);
record CoteGED(int CoteDocumentId, string Code, string Libelle, int FamilleDocumentId);
record TypeDocumentGED(int TypeDocumentId, string Code, string Libelle, int CoteDocumentId);

record Famille(int FamilleDocumentId, string Code, string Libelle, IEnumerable<Cote> CotesList);
record Cote(int CoteDocumentId, string Code, string Libelle, int FamilleDocumentId, IEnumerable<TypeDocument> TypesDocumentList);
record TypeDocument(int TypeDocumentId, string Code, string Libelle, int CoteDocumentId);
