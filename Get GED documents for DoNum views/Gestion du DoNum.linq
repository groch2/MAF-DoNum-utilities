<Query Kind="Statements">
  <Reference>C:\TeamProjects\GED API\MAF.GED.API.Host\bin\Debug\net6.0\MAF.GED.Domain.Model.dll</Reference>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Nodes</Namespace>
</Query>

var httpClient =
	new HttpClient { BaseAddress = new Uri("https://api-ged-intra.int.maf.local/v2/Documents/") };
const string userCode = "ROD";
var documents_json =
	await httpClient
		.GetStringAsync($"?$filter=statut eq 'INDEXE' and (categoriesFamille eq 'DOCUMENTS CONTRAT' or categoriesFamille eq 'DOCUMENTS EMOA' or categoriesFamille eq 'DOCUMENTS PERSONNES') and assigneRedacteur eq '{userCode}'&$orderby=documentId desc");
var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
var documents =
	JsonDocument
		.Parse(documents_json)
		.RootElement
		.GetProperty("value")
		.EnumerateArray()
		.Select(jsonElement => JsonNode.Parse(jsonElement.ToString()))
		.Select(jsonNode =>
			new {
				AssigneRedacteur = jsonNode["assigneRedacteur"]?.ToString(),
				DocumentId = jsonNode["documentId"]?.ToString(),
				Libelle = jsonNode["libelle"]?.ToString(),
				TypeGarantie = jsonNode["typeGarantie"]?.ToString(),
				FichierNom = jsonNode["fichierNom"]?.ToString(),
				Famille = jsonNode["categoriesFamille"]?.ToString(),
				Côte = jsonNode["categoriesCote"]?.ToString(),
				TypeDocument = jsonNode["categoriesTypeDocument"]?.ToString(),
				DeposeLe = GetDateOnly(jsonNode["deposeLe"]?.GetValue<DateTime>()),
				DeposePar = jsonNode["deposePar"]?.ToString(),
				VuLe = GetDateOnly(jsonNode["vuLe"]?.GetValue<DateTime>()),
				VuPar = jsonNode["vuPar"]?.ToString(),
				QualiteValideeLe = GetDateOnly(jsonNode["qualiteValideeLe"]?.GetValue<DateTime>()),
				QualiteValideePar = jsonNode["qualiteValideePar"]?.ToString(),
				QualiteValideeValide = jsonNode["qualiteValideeValide"]?.ToString(),
				TraiteLe = GetDateOnly(jsonNode["traiteLe"]?.GetValue<DateTime>()),
				TraitePar = jsonNode["traitePar"]?.ToString(),
				ModifieLe = GetDateOnly(jsonNode["modifieLe"]?.GetValue<DateTime>()),
				ModifiePar = jsonNode["modifiePar"]?.ToString(),
				NumeroContrat = jsonNode["numeroContrat"]?.ToString(),
				ChantierId = jsonNode["chantierId"]?.ToString(),
				AssureurId = jsonNode["assureurId"]?.ToString(),
				CompteId = jsonNode["compteId"]?.ToString(),
				PersonneId = jsonNode["personneId"]?.ToString(),
				Sens = jsonNode["sens"]?.ToString(),
				Important = jsonNode["important"]?.ToString(),
				PeriodeValiditeDebut = GetDateOnly(jsonNode["periodeValiditeDebut"]?.GetValue<DateTime>()),
				PeriodeValiditeFin = GetDateOnly(jsonNode["periodeValiditeFin"]?.GetValue<DateTime>()),
				Statut = jsonNode["statut"]?.ToString()
			});
documents.Dump();

static DateOnly? GetDateOnly(DateTime? date) =>
	date.HasValue ? DateOnly.FromDateTime(date.Value) : null;