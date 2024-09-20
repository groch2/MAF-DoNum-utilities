<Query Kind="Statements">
  <Reference>C:\TeamProjects\GED API\MAF.GED.API.Host\bin\Debug\net6.0\MAF.GED.Domain.Model.dll</Reference>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Text.Json</Namespace>
</Query>

var httpClient =
	new HttpClient { BaseAddress = new Uri("https://api-ged-intra.int.maf.local/v2/Documents/") };
const string userCode = "ROD";
var documents_json =
	await httpClient
		.GetStringAsync($"?$filter=statut eq 'INDEXE' and (categoriesFamille eq 'DOCUMENTS CONTRAT' or categoriesFamille eq 'DOCUMENTS EMOA' or categoriesFamille eq 'DOCUMENTS PERSONNES') and assigneRedacteur eq '{userCode}'&$orderby=docn desc");
var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
var documents =
	JsonDocument
		.Parse(documents_json)
		.RootElement
		.GetProperty("value")
		.EnumerateArray()
		.Select(json =>
			JsonSerializer.Deserialize<MAF.GED.Domain.Model.Document>(json, jsonSerializerOptions))
		.Select(document => 
			new {
				document.AssigneRedacteur,
				document.DocumentId,
				document.Libelle,
				document.FichierNom,
				Famille = document.CategoriesFamille,
				Cote = document.CategoriesCote,
				TypeDocument = document.CategoriesTypeDocument,
				DeposeLe = GetDateOnly(document.DeposeLe),
				document.DeposePar,
				VuLe = GetDateOnly(document.VuLe),
				document.VuPar,
				QualiteValideeLe = GetDateOnly(document.QualiteValideeLe),
				document.QualiteValideePar,
				document.QualiteValideeValide,
				TraiteLe = GetDateOnly(document.TraiteLe),
				document.TraitePar,
				ModifieLe = GetDateOnly(document.ModifieLe),
				document.ModifiePar,
				document.NumeroContrat,
				document.ChantierId,
				document.TypeGarantie,
				document.AssureurId,
				document.CompteId,
				document.Sens,
			});
File
	.WriteAllLines(
		path: @"C:\Users\deschaseauxr\AppData\Local\Temp\doc_id_a_supprimer.txt",
		contents: documents.Select(document => document.DocumentId));
documents.Dump();
static DateOnly? GetDateOnly(DateTime? date) =>
	date.HasValue ? DateOnly.FromDateTime(date.Value) : null;