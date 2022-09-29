<Query Kind="Statements">
  <Namespace>System.Data.SqlClient</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
  <IncludeLinqToSql>true</IncludeLinqToSql>
</Query>

using var connection = new SqlConnection("Server = DNSINTBDDGECO01; Database = GEDMAF; Integrated Security = True");
connection.Open();
using var command = connection.CreateCommand();
command.CommandText = @"WITH NB_DOCUMENTS_BY_REDACTEUR (REDACTEUR, NB_DOCUMENTS)
AS
(
	SELECT AssigneRedacteur, count([DocumentId])
	FROM [GED].[Document]
	WHERE AssigneRedacteur IS NOT NULL AND TRIM(AssigneRedacteur) <> ''
	group by AssigneRedacteur
)
SELECT TRIM(REDACTEUR) AS REDACTEUR, NB_DOCUMENTS
FROM NB_DOCUMENTS_BY_REDACTEUR
ORDER BY NB_DOCUMENTS ASC";
using var reader = command.ExecuteReader();
var dataTable = new DataTable();
dataTable.Load(reader);
"données récupérées de la base".Dump();
var redacteursCodes = dataTable.Select().Select(r => r["REDACTEUR"].ToString()).ToArray();
//redacteursCodes.Dump();
//Environment.Exit(0);
dataTable.Columns.Add("Nom", typeof(string));
dataTable.Columns.Add("Prenom", typeof(string));
var butApiClient = new HttpClient {
	BaseAddress = new Uri("https://api-but-intra.int.maf.local/api/v2/Utilisateurs/codes/")
};
var response = await butApiClient.GetStringAsync(string.Join(',', redacteursCodes));
var _user = new { Login = "", CodeUtilisateur = "", Prenom = "", Nom = "" };
var array = JsonDocument.Parse(response).RootElement;
var arrayLength = array.GetArrayLength();
var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
for(var index = 0; index < arrayLength; index++) {
	var rawUser = array[index].GetRawText();
	dynamic user = JsonSerializer.Deserialize(rawUser, _user.GetType(), options);
	// Console.WriteLine(user.ToString());
	var userRows = dataTable.Select($"REDACTEUR = '{user.CodeUtilisateur}'");
	foreach (var userRow in userRows) {
		userRow["Nom"] = user.Nom;
		userRow["Prenom"] = user.Prenom;
	}
}
dataTable.Dump();
