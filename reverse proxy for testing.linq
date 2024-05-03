<Query Kind="Statements">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <Namespace>Microsoft.Extensions.DependencyInjection</Namespace>
  <Namespace>System.Net.Http</Namespace>
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
								"http://localhost:4200");
					}));
var app = builder.Build();

var donumWebApiClient =
	new HttpClient {
		BaseAddress = new Uri("https://localhost:44363/")
	};

RequestDelegate requestDelegate =
	async (HttpContext context) => {
		var uriString =
			string.Concat(
				context.Request.Path,
				context.Request.QueryString);
		var uri = 
			new Uri(
				uriString: uriString,
				uriKind: UriKind.Relative);
		using var response =
			await donumWebApiClient.GetAsync(uri);
		var responseContent =
			await response.Content.ReadAsStringAsync();
		await context.Response.WriteAsync(responseContent);
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
	.ForEach(path => app.MapGet($"/{path}", requestDelegate));

app.UseCors(MyPolicy);

app.Run();