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
		.GetStringAsync($"?$filter=statut eq 'INDEXE' and assigneRedacteur eq '{userCode}'");
var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
JsonDocument
	.Parse(documents_json)
	.RootElement
	.GetProperty("value")
	.EnumerateArray()
	.Select(json =>
		JsonSerializer.Deserialize<MAF.GED.Domain.Model.Document>(json, jsonSerializerOptions))
	.Select(document => 
		new {
			document.DocumentId,
			document.Libelle,
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
		})
	.Dump();
static DateOnly? GetDateOnly(DateTime? date) =>
	date.HasValue ? DateOnly.FromDateTime(date.Value) : null;