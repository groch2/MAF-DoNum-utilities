<Query Kind="Statements">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

var butApiClient = new HttpClient {
	BaseAddress = new Uri("https://api-but-intra.int.maf.local/api/v2/Utilisateurs/codes/")
};
var userCodes = File.ReadAllLines(@"C:\Users\deschaseauxr\Documents\DONUM\Users_codes_with_documents.txt");
var queryString = string.Join(',', userCodes);
var response = await butApiClient.GetStringAsync(queryString);
var _user = new { Login = "", CodeUtilisateur = "", Prenom = "", Nom = "" };
var array = JsonDocument.Parse(response).RootElement;
var arrayLength = array.GetArrayLength();
var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
for(var index = 0; index < arrayLength; index++) {
	var rawUser = array[index].GetRawText();
	dynamic user = JsonSerializer.Deserialize(rawUser, _user.GetType(), options);
	Console.WriteLine(user.ToString());
}