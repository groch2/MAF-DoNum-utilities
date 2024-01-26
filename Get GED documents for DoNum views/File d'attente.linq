<Query Kind="Program">
  <Reference>C:\TeamProjects\GED API\MAF.GED.API.Host\bin\Debug\net6.0\MAF.GED.Domain.Model.dll</Reference>
  <Reference Relative="..\Json130r3\Bin\net6.0\Newtonsoft.Json.dll">&lt;MyDocuments&gt;\Json130r3\Bin\net6.0\Newtonsoft.Json.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <IncludeLinqToSql>true</IncludeLinqToSql>
</Query>

async Task Main()
{
	var httpClient =
		new HttpClient {
			BaseAddress = new Uri("https://api-ged-intra.int.maf.local/v2/Documents/")
		};
	const string code_rédacteur = "ROD";
	var actual_documents =
		await httpClient.GetStringAsync(
			$"?$filter=assigneRedacteur eq '{code_rédacteur}' and statut eq 'INDEXE' and (CategoriesFamille eq 'DOCUMENTS CONTRAT' or CategoriesFamille eq 'DOCUMENTS EMOA') and (traitePar eq null or traitePar eq '') and TraiteLe eq null");
	JsonDocument
		.Parse(actual_documents)
		.RootElement.GetProperty("value")
		.EnumerateArray()
		.Select(document => JsonConvert.DeserializeObject<MAF.GED.Domain.Model.Document>($"{document}"))
		.Select(document =>
			new {
				document.DocumentId,
				DateDocument = ToDateOnly(document.DateDocument),
				document.Libelle,
				document.CompteId,
				document.PersonneId,
				queueStatus = GetDocumentStatus(document),
				DeposeLe = ToDateOnly(document.DeposeLe),
				famille = document.CategoriesFamille,
				cote = document.CategoriesCote,
				typeDocument = document.CategoriesTypeDocument					
			})
		.Where(document => document.queueStatus == DocumentQueueStatus.NOUVEAU)
		.OrderBy(document => document.queueStatus)
		.ThenBy(document => document.famille, StringComparer.OrdinalIgnoreCase)
		.ThenBy(document => document.cote, StringComparer.OrdinalIgnoreCase)
		.ThenBy(document => document.typeDocument, StringComparer.OrdinalIgnoreCase)
		.ThenBy(document => document.DateDocument)
		.Dump();
}

DocumentQueueStatus GetDocumentStatus(MAF.GED.Domain.Model.Document document) {
	return 
		GetDocumentStatus(
		    documentTraiteDate: document.TraiteLe,
		    documentVuDate: document.VuLe,
		    documentQualiteValideeDate: document.QualiteValideeLe,
		    documentIsQualiteValidated: document.QualiteValideeValide);

	DocumentQueueStatus GetDocumentStatus(
	            DateTime? documentTraiteDate,
	            DateTime? documentVuDate,
	            DateTime? documentQualiteValideeDate,
	            bool? documentIsQualiteValidated) =>
	            0 switch {
	                _ when documentTraiteDate != null =>
	                    DocumentQueueStatus.TRAITE,
	                _ when (documentVuDate ?? documentQualiteValideeDate) == null =>
	                    DocumentQueueStatus.NOUVEAU,
	                _ when documentVuDate != null && documentQualiteValideeDate != null && documentIsQualiteValidated != true =>
	                    DocumentQueueStatus.INVALIDE,
	                _ => DocumentQueueStatus.A_TRAITER
	            };
}

enum DocumentQueueStatus
{
    NOUVEAU,
    INVALIDE,
    A_TRAITER,
    TRAITE
}

static DateOnly? ToDateOnly(DateTime? dateTime) =>
	dateTime.HasValue ?
	DateOnly.FromDateTime(dateTime.Value) :
	(DateOnly?)null;

