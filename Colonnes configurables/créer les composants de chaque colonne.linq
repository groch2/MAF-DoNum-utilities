<Query Kind="Statements" />

var componentTemplate = File.ReadAllText(@"C:\Users\deschaseauxr\Documents\Donum\Colonnes configurables\column.component.ts");
File.ReadAllLines(@"C:\Users\deschaseauxr\Documents\Donum\gestionDuDoNumResultsColumns.txt")
	.Select((l, index) => new { l, index })
	.ToList()
	.ForEach(item => {
		var l = item.l;
		var componentClassName = $"{l[0].ToString().ToUpperInvariant()}{l.Substring(1)}";
		var componentFileName = Regex.Replace(l, @"[A-Z]", m => $"-{m.Value.ToLowerInvariant()}");
		var directoryPath =
			Path.Combine(
				@"C:\TeamProjects\donum\console-donum\src\app\features\donum\components\column-components",
				$"{item.index + 1:00}-{componentFileName}");
		Directory.CreateDirectory(directoryPath);
		var rowDirectoryPath = Path.Combine(directoryPath, "row");
		Directory.CreateDirectory(rowDirectoryPath);
		var headerDirectoryPath = Path.Combine(directoryPath, "header");
		Directory.CreateDirectory(headerDirectoryPath);
		
		var componentRowClass =
			componentTemplate
				.Replace("column.component.html", $"{componentFileName}-column-row.component.html")
				.Replace("ColumnComponent", $"{componentClassName}ColumnRowComponent");
		var componentRowClassFilePath = Path.Combine(rowDirectoryPath, $"{componentFileName}-column-row.component.ts");
		File.WriteAllText(componentRowClassFilePath, componentRowClass);
		var componentRowTemplateFilePath = Path.Combine(rowDirectoryPath, $"{componentFileName}-column-row.component.html");
		File.WriteAllText(componentRowTemplateFilePath, $"<p>{componentFileName} row</p>");

		var componentHeaderClass =
			componentTemplate
				.Replace("column.component.html", $"{componentFileName}-column-header.component.html")
				.Replace("ColumnComponent", $"{componentClassName}ColumnHeaderComponent");
		var componentHeaderClassFilePath = Path.Combine(headerDirectoryPath, $"{componentFileName}-column-header.component.ts");
		File.WriteAllText(componentHeaderClassFilePath, componentHeaderClass);
		var componentHeaderTemplateFilePath = Path.Combine(headerDirectoryPath, $"{componentFileName}-column-header.component.html");
		File.WriteAllText(componentHeaderTemplateFilePath, $"<p>{componentFileName} header</p>");		
	});