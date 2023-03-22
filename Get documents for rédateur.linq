<Query Kind="Program">
  <Namespace>System.Data.SqlClient</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <IncludeLinqToSql>true</IncludeLinqToSql>
</Query>

async Task Main()
{
	var httpClient =
		new HttpClient {
			BaseAddress = new Uri("https://api-ged-intra.int.maf.local/v1/Documents/")
		};
	const string code_rédacteur = "ROD";
	var actual_documents =
		await httpClient.GetStringAsync(
			$"?$filter=assigneRedacteur eq '{code_rédacteur}'&$select=documentId,libelle,deposeLe,traiteLe,qualiteValideeLe,qualiteValideeValide,vuLe");
	var documentType = new {
		documentId = "",
		libelle = "",
		deposeLe = "",
		traiteLe = "",
		vuLe = "",
		qualiteValideeLe = "",
		qualiteValideeValide = (bool?)null,
		queueStatus = (DocumentQueueStatus?)null
	}.GetType();
	var documents =
		JsonDocument
			.Parse(actual_documents)
			.RootElement.GetProperty("value")
			.EnumerateArray()
			.Select(document => JsonSerializer.Deserialize(document, documentType))
			.Select((dynamic document) =>
				new {
					documentId = document.documentId,
					libelle = document.libelle,
					queueStatus = GetDocumentQueueStatus(document)
				});
	documents.Dump();
}

DocumentQueueStatus GetDocumentQueueStatus(dynamic document) =>
    document switch {
        _ when document.traiteLe != null => DocumentQueueStatus.TRAITE,
        _ when (document.vuLe ?? document.qualiteValideeLe) == null => DocumentQueueStatus.NOUVEAU,
        _ when document.vuLe != null && document.qualiteValideeLe != null && document.qualiteValideeValide != true => DocumentQueueStatus.INVALIDE,
        _ => DocumentQueueStatus.A_TRAITER
    };

public enum DocumentQueueStatus
{
    NOUVEAU,
    INVALIDE,
    A_TRAITER,
    TRAITE
}
