<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

async void Main()
{
	var targetPipelineVariablesList = await ToArrayAsync(
		ConvertTextFileLinesToStreamOfLinesPairs(
			@"C:\Users\deschaseauxr\Documents\Donum\Correction des variables de release suite à MàJ de MafNgToolbox\variables de pipeline à corriger\target.txt"));
	var sourcePipelineVariablesList = await ToArrayAsync(
		ConvertTextFileLinesToStreamOfLinesPairs(
			@"C:\Users\deschaseauxr\Documents\Donum\Correction des variables de release suite à MàJ de MafNgToolbox\variables de pipeline à corriger\source.txt"));
	var targetPipelineVariablesWithMatchingsourcePipelineVariables =
		targetPipelineVariablesList
			.Select(
				targetPipelineVariable => {
					var sourcePipelineVariable =
						sourcePipelineVariablesList.FirstOrDefault(
							sourcePipelineVariable =>
								string.Equals(
									targetPipelineVariable.Item1,
									sourcePipelineVariable.Item1,
									StringComparison.InvariantCulture) &&
								string.Equals(
									targetPipelineVariable.Item2,
									sourcePipelineVariable.Item2,
									StringComparison.InvariantCulture));
					return new VariableMapping(
						pipelineVariableName: targetPipelineVariable.Item1,
						//variablesGroupsVariableName: angularVariable?.Item1,
						variablesGroupsVariableValue: sourcePipelineVariable?.Item2);
				})
				.OrderBy(variableMapping => variableMapping.variablesGroupsVariableValue == null)
				.ThenBy(variableMapping => variableMapping.pipelineVariableName, StringComparer.OrdinalIgnoreCase);
	targetPipelineVariablesWithMatchingsourcePipelineVariables.Dump();
}

static async IAsyncEnumerable<Tuple<string, string>> ConvertTextFileLinesToStreamOfLinesPairs(string filePath) {
	using var fileStreamReader = File.OpenText(filePath);
	var item1 = "";
	var count = 0;
	while (true) {
		var line = await fileStreamReader.ReadLineAsync();
		if (line == null) {
			yield break;
		}
		if (count++ % 2 == 0) {
			item1 = line;
			continue;
		}
		yield return Tuple.Create<string, string>(item1, line);
	}
}

static async Task<T[]> ToArrayAsync<T>(IAsyncEnumerable<T> stream) {
	var list = new List<T>();
	await foreach (var item in stream) {
		list.Add(item);
	}
	return list.ToArray();
}

record VariableMapping(
	string pipelineVariableName,
	//string variablesGroupsVariableName,
	string variablesGroupsVariableValue);