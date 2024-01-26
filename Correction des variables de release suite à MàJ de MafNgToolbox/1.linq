<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

async void Main()
{
	var angularVariablesFromVariablesGroups = await ToArrayAsync(
		ConvertTextFileLinesToStreamOfLinesPairs(
			@"C:\Users\deschaseauxr\Documents\Donum\Correction des variables de release suite à MàJ de MafNgToolbox\variable_groups.txt"));
	//new { angularVariablesFromVariablesGroups }.Dump();
	var pipelineVariables = await ToArrayAsync(
		ConvertTextFileLinesToStreamOfLinesPairs(
			@"C:\Users\deschaseauxr\Documents\Donum\Correction des variables de release suite à MàJ de MafNgToolbox\pipeline_variable.txt"));
	//new { pipelineVariables }.Dump();
	var pipelineVariableWithMatchingAngularVariable =
		pipelineVariables
			.Where(pipelineVariable =>  pipelineVariable.Item1.StartsWith("mafToolboxConfig."))
			.Select(
				pipelineVariable => {
					var angularVariable =
						angularVariablesFromVariablesGroups.FirstOrDefault(
							angularVariable =>
								pipelineVariable.Item1.EndsWith($".{angularVariable.Item1}"));
					return new VariableMapping(
						pipelineVariableName: pipelineVariable.Item1,
						//variablesGroupsVariableName: angularVariable?.Item1,
						variablesGroupsVariableValue: angularVariable?.Item2);
				})
				.OrderBy(variableMapping => variableMapping.variablesGroupsVariableValue == null)
				.ThenBy(variableMapping => variableMapping.pipelineVariableName, StringComparer.OrdinalIgnoreCase);
	pipelineVariableWithMatchingAngularVariable.Dump();
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