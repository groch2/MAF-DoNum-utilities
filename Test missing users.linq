<Query Kind="Statements">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

var butApiClient = new HttpClient {
	BaseAddress = new Uri("https://api-but-intra.int.maf.local/api/v2/Utilisateurs/codes/")
};
var _user = new { Login = "", CodeUtilisateur = "", Prenom = "", Nom = "" };
var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
foreach (var userCode in File.ReadAllLines(@"C:\Users\deschaseauxr\Documents\DONUM\Users_codes_with_documents.txt")) {
	var rawResponse = await butApiClient.GetStringAsync(userCode);
	var reponseArray = JsonDocument.Parse(rawResponse).RootElement;
	if (reponseArray.GetArrayLength() == 0) {
		$"missing {userCode}".Dump();
		continue;
	}
	var rawUser = JsonDocument.Parse(rawResponse).RootElement[0].GetRawText();
	dynamic user = JsonSerializer.Deserialize(rawUser, _user.GetType(), options);
	Console.WriteLine(user.ToString());
}