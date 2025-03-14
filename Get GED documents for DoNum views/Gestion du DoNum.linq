<Query Kind="Statements">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Nodes</Namespace>
</Query>

const string ENVIRONMENT_CODE = "hom";
var httpClient =
	new HttpClient { BaseAddress = new Uri($"https://api-ged-intra.{ENVIRONMENT_CODE}.maf.local/v2/Documents/") };
const string userCode = "ROD";
var documents_json =
	await httpClient
		.GetStringAsync($"?$filter=statut eq 'INDEXE' and categoriesFamille in ('DOCUMENTS CONTRAT','DOCUMENTS EMOA','DOCUMENTS PERSONNES') and assigneRedacteur eq '{userCode}'&$orderby=documentId desc");
var documents =
	JsonDocument
		.Parse(documents_json)
		.RootElement
		.GetProperty("value")
		.EnumerateArray()
		.Select(jsonElement => JsonNode.Parse(jsonElement.ToString()))
		.Select(jsonNode => {
			const int libelleTruncateLength = 5;
			var libelle = new Func<string>(() => {
				var libelle = jsonNode["libelle"]?.ToString();
				return libelle == null ? null : libelle[0..Math.Min(libelle.Length, libelleTruncateLength)];
			})();
			return
				new {
					DocumentId = jsonNode["documentId"]?.ToString(),
					Libelle = libelle,
					//CompteId = jsonNode["compteId"]?.ToString(),
					//PersonneId = jsonNode["personneId"]?.ToString(),
					//Statut = GetDocumentStatut(jsonNode),
					//Famille = jsonNode["categoriesFamille"]?.ToString(),
					//Côte = jsonNode["categoriesCote"]?.ToString(),
					//TypeDocument = jsonNode["categoriesTypeDocument"]?.ToString(),
					//AssigneRedacteur = jsonNode["assigneRedacteur"]?.ToString(),
					//DeposeLe = GetDateOnly(jsonNode["deposeLe"]?.GetValue<DateTime>()),
					//DeposePar = jsonNode["deposePar"]?.ToString(),
					//ModifieLe = GetDateOnly(jsonNode["modifieLe"]?.GetValue<DateTime>()),
					//ModifiePar = jsonNode["modifiePar"]?.ToString(),
					//VuLe = GetDateOnly(jsonNode["vuLe"]?.GetValue<DateTime>()),
					//VuPar = jsonNode["vuPar"]?.ToString(),
					//QualiteValideeLe = GetDateOnly(jsonNode["qualiteValideeLe"]?.GetValue<DateTime>()),
					//QualiteValideePar = jsonNode["qualiteValideePar"]?.ToString(),
					//QualiteValideeValide = jsonNode["qualiteValideeValide"]?.ToString(),
					//TraiteLe = GetDateOnly(jsonNode["traiteLe"]?.GetValue<DateTime>()),
					//TraitePar = jsonNode["traitePar"]?.ToString(),
					//PeriodeValiditeDebut = GetDateOnly(jsonNode["periodeValiditeDebut"]?.GetValue<DateTime>()),
					//PeriodeValiditeFin = GetDateOnly(jsonNode["periodeValiditeFin"]?.GetValue<DateTime>()),
					//FichierNom = jsonNode["fichierNom"]?.ToString(),
					//TypeGarantie = jsonNode["typeGarantie"]?.ToString(),
					//NumeroContrat = jsonNode["numeroContrat"]?.ToString(),
					//ChantierId = jsonNode["chantierId"]?.ToString(),
					//Commentaire = jsonNode["commentaire"]?.ToString(),
					//AssureurId = jsonNode["assureurId"]?.ToString(),
					//Statut = jsonNode["statut"]?.ToString(),
					//Sens = jsonNode["sens"]?.ToString(),
					//Important = jsonNode["important"]?.ToString(),
			};
	});
documents.Dump();

static DateOnly? GetDateOnly(DateTime? date) =>
	date.HasValue ? DateOnly.FromDateTime(date.Value) : null;

static DocumentQueueStatus GetDocumentStatut(JsonNode document) {
	return _GetDocumentStatus(
		documentTraiteDate: document["traiteLe"]?.GetValue<DateTime>(),
		documentVuDate: document["vuLe"]?.GetValue<DateTime>(),
		documentQualiteValideeDate: document["qualiteValideeLe"]?.GetValue<DateTime>(),
		documentIsQualiteValidated: bool.Parse(document["qualiteValideeValide"]?.ToString() ?? "false"));

	static DocumentQueueStatus _GetDocumentStatus(
	    DateTimeOffset? documentTraiteDate,
	    DateTimeOffset? documentVuDate,
	    DateTimeOffset? documentQualiteValideeDate,
	    bool? documentIsQualiteValidated) {
		if (documentTraiteDate != null) {
			return DocumentQueueStatus.TRAITE;
		}
		if ((documentVuDate ?? documentQualiteValideeDate) == null) {
			return DocumentQueueStatus.NOUVEAU;
		}
		if (documentVuDate != null && documentQualiteValideeDate != null) {
			return documentIsQualiteValidated != true ? DocumentQueueStatus.INVALIDE : DocumentQueueStatus.VALIDE;
		}
		return DocumentQueueStatus.A_TRAITER;
	}
}

enum DocumentQueueStatus {
    NOUVEAU,
    A_TRAITER,
    INVALIDE,
	VALIDE,
    TRAITE,
}
