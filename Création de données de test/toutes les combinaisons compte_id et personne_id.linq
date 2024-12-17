<Query Kind="Program">
  <Reference>C:\TeamProjects\GED API\MAF.GED.API.Host\bin\Debug\net6.0\MAF.GED.Domain.Model.dll</Reference>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Json</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Nodes</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <IncludeLinqToSql>true</IncludeLinqToSql>
</Query>

const string ENVIRONMENT_CODE = "int";
async Task Main() {
	var compte_id_by_case = new []{ (int?)null, 0, 3 };
	var personne_id_by_case = new []{ (int?)null, 0, 353440 };
	var documents_id_list = File.ReadAllLines(@"C:\Users\deschaseauxr\Documents\Donum\documents_id_list.txt");
	var patch_documents_requests =
		await Task.WhenAll(
			CountInBase(@base: 3, nb_digits: 2)
				.Select(async (n, index) => {
					var document_id = documents_id_list[index];
					var documentPatch =
						JsonSerializer.SerializeToNode(
							new {
								ModifiePar = "X",
								CompteId = compte_id_by_case[n[0]],
								PersonneId = personne_id_by_case[n[1]]
							});
					var patchDocumentResponse =
						await PatchDocument(
							documentId: document_id,
							documentPatch: documentPatch);
					return new { document_id, patchDocumentResponse };
				}));
	//.EnsureSuccessStatusCode();	
}

HttpClient httpClient =
	new HttpClient {
		BaseAddress = new Uri($"http://api-ged-intra.{ENVIRONMENT_CODE}.maf.local/v2/Documents/") };

async Task<HttpResponseMessage> PatchDocument(string documentId, JsonNode documentPatch) {
	//documentPatch["@odata.type"] = "MAF.GED.Domain.Model.Document";
	var requestContent = JsonContent.Create(documentPatch);
	return await httpClient.PatchAsync(documentId, requestContent);
}

JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
async Task<MAF.GED.Domain.Model.Document> GetDocumentById(string documentId) =>
	await httpClient.GetFromJsonAsync<MAF.GED.Domain.Model.Document>(documentId, jsonSerializerOptions);
	
/*
AssigneDepartement
AssigneGroup
AssigneRedacteur
AssureurId
CanalPrincipal
CanalSecondaire
CategoriesCote
CategoriesFamille
CategoriesTypeDocument
ChantierId
CodeBarreId
CodeOrigine
Commentaire
CompteId
DateDocument
DateNumerisation
DeposeLe
DeposePar
Docn
DocumentId
DocumentId
DocumentValide
DuplicationId
Extension
FichierNom
FichierNombrePages
FichierTaille
HeureNumerisation
Horodatage
Important
IsHorsWorkFlowSinapps
Libelle
Link
ModifieLe
ModifiePar
MultiCompteId
Nature
NumeroAvenant
NumeroContrat
NumeroGc
NumeroProposition
NumeroSinistre
PeriodeValiditeDebut
PeriodeValiditeFin
PersonneId
PresenceAr
Preview
PreviewLink
Priorite
Provenance
QualiteValideeLe
QualiteValideePar
QualiteValideeValide
ReferenceAttestation
ReferenceSecondaire
RefTiers
RegroupementId
Sens
SousDossierSinistre
Statut
Tenant
TraiteLe
TraitePar
TypeContact
TypeGarantie
VisibiliteExterne
VisibilitePapsExtranet
VuLe
VuPar
*/

IEnumerable<int[]> CountInBase(int @base, int nb_digits) {
	var state = Enumerable.Repeat(element: 0, count: nb_digits).ToArray();
	yield return state;
	var max = Convert.ToInt32(Math.Pow(@base, nb_digits)) - 1;
	for (int i = max - 1; i >= 0; i--) {
		var position = state.Length - 1;
		while (state[position] == @base - 1) {
			state[position] = 0;
			position--;
		}
		state[position]++;
		yield return state;		
	}
}
