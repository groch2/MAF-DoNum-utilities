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
	var selectDocumentProperties =
		documentTypeProperties
			.Select(p => p.Name)
			.Where(propertyName => propertyName != nameof(DocumentType.QueueStatus));
	var actual_documents =
		await httpClient.GetStringAsync(
			$"?$filter=assigneRedacteur eq '{code_rédacteur}'&$select={string.Join(',', selectDocumentProperties)}");
	var documents =
		JsonDocument
			.Parse(actual_documents)
			.RootElement.GetProperty("value")
			.EnumerateArray()
			.Select(document => JsonSerializer.Deserialize<DocumentType>(document, new JsonSerializerOptions { PropertyNameCaseInsensitive = true } ))
			.Select(document =>
				new {
					document.DocumentId,
					document.Libelle,
					document.CompteId,
					queueStatus = GetDocumentQueueStatus(document)
				});
	documents.Dump();
}

DocumentQueueStatus GetDocumentQueueStatus(DocumentType document) =>
    document switch {
        _ when document.TraiteLe != null => DocumentQueueStatus.TRAITE,
        _ when (document.VuLe ?? document.QualiteValideeLe) == null => DocumentQueueStatus.NOUVEAU,
        _ when document.VuLe != null && document.QualiteValideeLe != null && document.QualiteValideeValide != true => DocumentQueueStatus.INVALIDE,
        _ => DocumentQueueStatus.A_TRAITER
    };

enum DocumentQueueStatus
{
    NOUVEAU,
    INVALIDE,
    A_TRAITER,
    TRAITE
}

record DocumentType(
	string DocumentId,
	string Libelle,
	string DeposeLe,
	string TraiteLe,
	string VuLe,
	string QualiteValideeLe,
	bool? QualiteValideeValide,
	DocumentQueueStatus? QueueStatus,
	int? CompteId);

static readonly CustomTypeProperty[] documentTypeProperties =
	typeof(DocumentType)
		.GetProperties()
		.Select(p =>
			new CustomTypeProperty(
				Name: p.Name,
				Type: p.PropertyType.Name,
				UnderlyingType:
					p.PropertyType.IsGenericType &&
					p.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ?
						p.PropertyType.GenericTypeArguments[0].Name : null
			)).ToArray();

record CustomTypeProperty(string Name, string Type, string UnderlyingType);
