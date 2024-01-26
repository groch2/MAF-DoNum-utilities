<Query Kind="Statements">
  <Reference>C:\TeamProjects\donum\Donum.Services.External\bin\Debug\netcoreapp3.1\Donum.Services.External.dll</Reference>
</Query>

var propertiesOfGEDDocument =
	typeof(MAF.GED.Domain.Model.Document)
		.GetProperties()
		.Select(p => {
			var type = Nullable.GetUnderlyingType(p.PropertyType) != null ? $"{p.PropertyType.GenericTypeArguments[0].Name}?" : p.PropertyType.Name;
			return new { p.Name, Type = type };
		})
		.OrderBy(n => n.Name, StringComparer.OrdinalIgnoreCase);
var code = File.ReadAllText(@"C:\Users\deschaseauxr\Documents\Donum\Recherche par libellé sans Compte ni Personne\Corps de la fonction GecoMapper.CreateDocument.txt");
var dtoPropertyUsed = 
	Regex
		.Matches(code, @"(?<=\bdto\.)\b\w+\b")
		.Cast<Match>()
		.Select(m => m.Value)
		.Distinct(StringComparer.OrdinalIgnoreCase)
		.OrderBy(documentProperty => documentProperty, StringComparer.OrdinalIgnoreCase)
		.ToHashSet();
propertiesOfGEDDocument.Select(p => p.Name).ToHashSet().IsSupersetOf(dtoPropertyUsed).Dump();
propertiesOfGEDDocument
	.Where(p => dtoPropertyUsed.Contains(p.Name))
	.OrderBy(p => p.Name)
	.Select(p => $@"{p.Name} = !(await reader.IsDBNullAsync(""{p.Name}"")) ? await reader.GetFieldValueAsync<{p.Type}>(""{p.Name}"") : default,")
	.Dump();