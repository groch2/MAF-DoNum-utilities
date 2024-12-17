<Query Kind="Statements">
  <Reference>C:\TeamProjects\donum\Donum.WebHost.Intranet\bin\Debug\net9.0\Donum.DataAccess.dll</Reference>
  <Namespace>Donum.DataAccess.Context</Namespace>
  <Namespace>Microsoft.EntityFrameworkCore</Namespace>
</Query>

const string connectionString = "Server=bdd-donum.int.maf.local; Database=contrat; Trusted_Connection=True; TrustServerCertificate=True;";
var contextOptions =
	new DbContextOptionsBuilder<ContratContext>()
		.UseSqlServer(connectionString, providerOptions => providerOptions.CommandTimeout(3))
	    .Options;
using var context = new ContratContext(contextOptions);
context.Chantier.First().Dump();
"OK".Dump();
