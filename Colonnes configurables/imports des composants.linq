<Query Kind="Expression" />

File.ReadAllLines(@"C:\TeamProjects\donum\console-donum\generate columns templates\gestionDuDoNumResultsColumns.txt")
	.Select((l, index) => {
		var componentClassName = $"{l[0].ToString().ToUpperInvariant()}{l.Substring(1)}";
		var componentFileName = Regex.Replace(l, @"[A-Z]", m => $"-{m.Value.ToLowerInvariant()}");
		return $@"import {{ {componentClassName}ColumnHeaderComponent }} from '../column-components/{index + 1:00}-{componentFileName}/header/{componentFileName}-column-header.component';
import {{ {componentClassName}ColumnRowComponent }} from '../column-components/{index + 1:00}-{componentFileName}/row/{componentFileName}-column-row.component';";
	})