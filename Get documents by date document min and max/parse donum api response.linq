<Query Kind="Program">
  <Reference>C:\TeamProjects\donum\Donum.Services.External\bin\Debug\netcoreapp3.1\Donum.Services.External.dll</Reference>
  <Namespace>System.Text.Json</Namespace>
</Query>

void Main()
{
	var raw = File.ReadAllText(@"C:\Users\deschaseauxr\AppData\Local\Temp\test.txt");
	JsonDocument.Parse(raw).RootElement.GetProperty("value").EnumerateArray()
		.Select(jsonElement => {
			var document =
				JsonSerializer.Deserialize<DocumentType>(
				jsonElement,
				jsonSerializerOptions);
			return new {
				document.DocN,
				document.DocumentId,
				DateDocument = ParseDate(document.DateDocument),
				document.Libelle,
				document.CompteId,
				DeposeLe = ParseDate(document.DeposeLe)
			};
		})
		//.OrderBy(document => document.Libelle)
		.Dump();	
}

record DocumentType(
	string DocN,
	string DocumentId,
	string Libelle,
	string DateDocument,
	string DeposeLe,
	int? CompteId);
static DateOnly? ParseDate(string date) =>
	!string.IsNullOrEmpty(date) ?
	DateOnly.FromDateTime(DateTime.Parse(date)) :
	(DateOnly?)null;
static JsonSerializerOptions jsonSerializerOptions =
	new JsonSerializerOptions { PropertyNameCaseInsensitive = true };