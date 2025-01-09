<Query Kind="Expression">
  <Reference>C:\TeamProjects\donum\Donum.WebHost.Intranet\bin\Debug\net8.0\Donum.Domain.dll</Reference>
</Query>

typeof(Donum.Domain.Entities.CoteDocument)
	.GetProperties()
	.Select(p => p.Name)
	.OrderBy(_ => _)
	.Select(property => $"{property[0].ToString().ToLowerInvariant()}{property.Substring(1)}")
	.Aggregate(
		new StringBuilder(),
		(state, item) => state.Append($"{item},"),
		state => state.Remove(state.Length - 1, 1).ToString())