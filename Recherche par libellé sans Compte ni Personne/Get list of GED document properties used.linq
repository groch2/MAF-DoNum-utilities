<Query Kind="Statements" />

// Vérifier les propriétés de document utilisées lors de la récupération d'un document depuis la web api GED
var code = File.ReadAllText(@"C:\Users\deschaseauxr\Documents\Donum\Recherche par libellé sans Compte ni Personne\Corps de la fonction GecoMapper.CreateDocument.txt");
var nbOccurenceDto = Regex.Count(code, @"\bdto\b");
new { nbOccurenceDto }.Dump();
var dtoPropertyUsed = 
	Regex
		.Matches(code, @"(?<=\bdto\.)\b\w+\b")
		.Cast<Match>()
		.Select(m => m.Value)
		.ToArray();
new { nbDtoPropertyUsed = dtoPropertyUsed.Length }.Dump();
System.Diagnostics.Debug.Assert(dtoPropertyUsed.Length == nbOccurenceDto);
dtoPropertyUsed
	.Distinct(StringComparer.OrdinalIgnoreCase)
	.OrderBy(documentProperty => documentProperty, StringComparer.OrdinalIgnoreCase)
	//.Select(documentProperty => $",[{documentProperty}]")
	.Dump();