<Query Kind="Statements">
  <Namespace>System.Text.Json</Namespace>
</Query>

var protoDependencies =
	GetDependencies(@"C:\Users\deschaseauxr\Documents\Donum\affichage conditionnel de composant\angular-ivy-jkcwrd\package.json");
var donumDependencies =
	GetDependencies(@"C:\TeamProjects\donum\console-donum\package.json");
var protoDevDependencies =
	protoDependencies
		.Where(dep => !dep.IsDev);
var donumDevDependencies =
	donumDependencies
		.Where(dep => !dep.IsDev);
var dependenciesInProtoAndInDonum =
	protoDevDependencies
		.Select(dep => dep.Name)
		.Intersect(
			donumDevDependencies.Select(dep => dep.Name),
			StringComparer.OrdinalIgnoreCase)
		.ToHashSet(StringComparer.OrdinalIgnoreCase);
//new { dependenciesInProtoAndInDonum }.Dump();
var filteredDonumDeps =
	donumDependencies.Where(dep => dependenciesInProtoAndInDonum.Contains(dep.Name));
//new { filteredDonumDeps }.Dump();
filteredDonumDeps.Select(dep => $@"""{dep.Name}"": ""{dep.Version}"",").Dump();;

IEnumerable<Dependency> GetDependencies(string packageFile) {
	var content =
		File.ReadAllText(packageFile);
	var jsonDocRoot = JsonDocument.Parse(content).RootElement;
	var _dependencies =
		jsonDocRoot
			.GetProperty("dependencies")
			.EnumerateObject()
			.Select(
				property =>
					new Dependency(
						Name: property.Name,
						Version: property.Value.GetString(),
						IsDev: false));
	var devDependencies =
		jsonDocRoot
			.GetProperty("devDependencies")
			.EnumerateObject()
			.Select(
				property =>
					new Dependency(
						Name: property.Name,
						Version: property.Value.GetString(),
						IsDev: true));
	var dependencies = _dependencies.Concat(devDependencies);
	return dependencies;
}

public record Dependency(string Name, string Version, bool IsDev);