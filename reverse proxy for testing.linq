<Query Kind="Statements">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <Namespace>Microsoft.Extensions.DependencyInjection</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Json</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

var builder = WebApplication.CreateBuilder();
const string MyPolicy = "allow localhost 4200";
builder
	.Services
	.AddCors(
		options =>
		    options
				.AddPolicy(
					name: MyPolicy,
					builder => {
						builder
							.WithOrigins(
								"http://localhost:4200")
							.AllowAnyHeader()
							.AllowAnyMethod();
					}));
var app = builder.Build();

var donumWebApiClient =
	new HttpClient {
		BaseAddress = new Uri("https://localhost:44363/")
	};

Enumerable
	.Range(0, 7)
	.Reverse()
	.Select(n =>
		string.Join('/',
			Enumerable
				.Range(0, n)
				.Select(n => $"{{level_{n + 1}}}")))	
	.ToList()
	.ForEach(
		path =>
			app.MapGet(
				$"/{path}",
				async (HttpContext context) => {
					var originalRequestAddress =
						getRelativeUriFromHttpRequest(context.Request);
					using var response =
						await donumWebApiClient.GetAsync(originalRequestAddress);
					response.EnsureSuccessStatusCode();
					var responseContent =
						await response.Content.ReadAsStringAsync();
					await context.Response.WriteAsync(responseContent);
				}
			));
	
app.MapPut(
	"/api/documents/updatedocument",
	async (HttpContext context) => {
		var uri = getRelativeUriFromHttpRequest(context.Request);
		using var reader = new StreamReader(context.Request.Body);
		var requestContentRaw = 
			await reader.ReadToEndAsync();
		var requestContentJson =
			System.Text.Json.JsonDocument.Parse(requestContentRaw);
		var jsonContent = JsonContent.Create(requestContentJson);
		using var response =
			await donumWebApiClient.PutAsync(uri, jsonContent);
		response.EnsureSuccessStatusCode();
		var responseContent =
			await response.Content.ReadAsStringAsync();
		await context.Response.WriteAsync(responseContent);
	});

app.MapPost(
	"/api/documents/createdocument",
	async (HttpContext context) => {
		context.Request.Query.Dump();
		var uri = getRelativeUriFromHttpRequest(context.Request);

		var multipartFormDataContent = new MultipartFormDataContent();
		
		new { context.Request.Form.Files }.Dump();
		var file = context.Request.Form.Files[0];
		using var readStream = file.OpenReadStream();
	    using var fileContent = new StreamContent(readStream);
	    fileContent.Headers.ContentType =
			System.Net.Http.Headers.MediaTypeHeaderValue.Parse(file.ContentType);
		new { fileContent, file.FileName }.Dump();
	    multipartFormDataContent.Add(
			content: fileContent,
			name: "file",
			fileName: file.FileName);
  
		var formUrlEncodedContent =
			new FormUrlEncodedContent(
				context.Request.Query.Select(
					item => new KeyValuePair<string,string>(item.Key, item.Value)));	  
	    multipartFormDataContent.Add(formUrlEncodedContent);
		
		new { multipartFormDataContent }.Dump();

		using var response =
			await donumWebApiClient.PostAsync(uri, multipartFormDataContent);
		response.EnsureSuccessStatusCode();
		var responseContent =
			await response.Content.ReadAsStringAsync();
		await context.Response.WriteAsync(responseContent);
	});

app.UseCors(MyPolicy);

app.Run();

Uri getRelativeUriFromHttpRequest(HttpRequest request) {
	var uriString =
		string.Concat(
			request.Path,
			request.QueryString);
	var uri = 
		new Uri(
			uriString: uriString,
			uriKind: UriKind.Relative);
	return uri;
}