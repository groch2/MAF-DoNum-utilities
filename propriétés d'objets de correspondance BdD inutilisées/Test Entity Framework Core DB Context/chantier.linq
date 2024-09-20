<Query Kind="Statements">
  <Reference>C:\TeamProjects\donum\Donum.WebHost.Intranet\bin\Debug\net6.0\Donum.DataAccess.dll</Reference>
  <Reference>C:\TeamProjects\donum\Donum.WebHost.Intranet\bin\Debug\net6.0\Microsoft.EntityFrameworkCore.dll</Reference>
  <Reference>C:\TeamProjects\donum\Donum.WebHost.Intranet\bin\Debug\net6.0\Microsoft.EntityFrameworkCore.SqlServer.dll</Reference>
  <Namespace>Donum.DataAccess.Context</Namespace>
  <Namespace>Microsoft.EntityFrameworkCore</Namespace>
  <Namespace>System.Text.Json</Namespace>
</Query>

var connectionString = 
	JsonDocument
		.Parse(
			File.ReadAllText(@"C:\TeamProjects\donum\Donum.WebHost.Intranet\appsettings.Development.json"))
		.RootElement
		.GetProperty("AppSettings")
		.GetProperty("ConnectionStrings")
		.GetProperty("DefaultConnection")
		.GetString();
//connectionString.Dump();
//Environment.Exit(0);
var dbContextOptions =
	new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<ContratContext>()
		.UseSqlServer(connectionString, providerOptions => providerOptions.CommandTimeout(3))
	    .Options;
using var context = new ContratContext(dbContextOptions);
var chantier =
	context
		.Chantier
		.Where(
			c =>
				c.NomOperation != null &&
				c.NomOperation != "" &&				
				c.ReferenceGC != null &&
				c.ReferenceGC != "")
		.First();
chantier.Dump();
