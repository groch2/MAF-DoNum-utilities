<Query Kind="Statements">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Net.Http.Json</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Nodes</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

const string filePath =
	@"C:\Users\deschaseauxr\Documents\Donum\test.pdf";
var fileName = Path.GetFileName(filePath);
const string gedApiVersion = "v2";

await using var stream = File.OpenRead(filePath);
using var requestUpload =
	new HttpRequestMessage(HttpMethod.Post, new Uri($"/{gedApiVersion}/upload", UriKind.Relative));
using var content = new MultipartFormDataContent { { new StreamContent(stream), "file", fileName } };
requestUpload.Content = content;

var client = new HttpClient { BaseAddress = new Uri("https://api-ged-intra.int.maf.local/") };
var httpResponseUpload = await client.SendAsync(requestUpload);
var uploadResponseContent = await httpResponseUpload.Content.ReadAsStringAsync();
var uploadId = JsonNode.Parse(uploadResponseContent)["guidFile"].GetValue<string>();

var jsonContent = File.ReadAllText(@"C:\Users\deschaseauxr\Documents\Donum\Copy document between environments\tmpF4B8.json");
var jsonNode = JsonNode.Parse(jsonContent);
jsonNode["fileId"] = uploadId;
jsonNode["canalId"] = 1;
//jsonNode.Dump();
//Environment.Exit(0);
var finalizeDocumentUploadJson =
	new StringContent(jsonNode.ToString(), Encoding.UTF8, "application/json");
var finalizeUploadAddress = new Uri($"/{gedApiVersion}/finalizeUpload", UriKind.Relative);
var finalizeUploadResponse =
	await client.PostAsync(finalizeUploadAddress, finalizeDocumentUploadJson);
var finalizeUploadResponseContent =
	await finalizeUploadResponse.Content.ReadAsStringAsync();
finalizeUploadResponseContent.Dump();
var documentId = JsonNode.Parse(finalizeUploadResponseContent)["documentId"].GetValue<string>();
documentId.Dump();
