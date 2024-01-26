<Query Kind="Statements" />

var colonnes = File.ReadAllLines(@"C:\Users\deschaseauxr\Documents\Donum\Colonnes configurables\search results columns.txt");
File.ReadAllLines(@"C:\Users\deschaseauxr\Documents\Donum\Colonnes configurables\search results columns rows.txt")
	.Select((line, index) => new { line = Regex.Replace(line, "^                      ", ""), index })
	.Aggregate(new List<List<string>>(),
		(state, item) => {
			if (item.line.Trim().StartsWith("<td") && item.index != 4) {
				state.Add(new List<string>{item.line});
			} else {
				state.Last().Add(item.line);
			}
			return state;
		})
	.Select(lines => string.Join(Environment.NewLine, lines))
	.Select((lines, index) => {
		var endOfOpeningTag = lines.IndexOf(">");
		var colonneCode = colonnes[index];
		return
			lines
				.Insert(lines.Length, "</ng-container>")
				.Insert(endOfOpeningTag, " pReorderableColumn")
				.Insert(0, @$"<ng-container *ngSwitchCase=""'{colonneCode}'"">")
				;
	})
	.Dump();