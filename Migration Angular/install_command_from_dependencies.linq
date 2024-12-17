<Query Kind="Statements">
  <Namespace>System.Text.Json</Namespace>
</Query>

var content =
	File
		.ReadAllText(@"C:\TeamProjects\donum\console-donum\package.json");
var jsonDoc = JsonDocument.Parse(content);
var _dependencies =
	jsonDoc
		.RootElement
		.GetProperty("dependencies")
		.EnumerateObject()
		.Select(
			property =>
				new {
					Name = property.Name,
					Version = property.Value.GetString(),
					Dependency = "dependency"
				});
var devDependencies =
	jsonDoc
		.RootElement
		.GetProperty("devDependencies")
		.EnumerateObject()
		.Select(
			property =>
				new {
					Name = property.Name,
					Version = property.Value.GetString(),
					Dependency = "devDependency"
				});
var dependencies =
	_dependencies
		.Concat(devDependencies)
		.Select(dependency =>
			new {
				dependency.Name,
				dependency.Version,
				dev = string.Equals(dependency.Dependency, "devDependency", StringComparison.OrdinalIgnoreCase),
			})
		.Where(
			dependency =>
				dependency.Name.StartsWith("@angular", StringComparison.OrdinalIgnoreCase) &&
				dependency.Version.StartsWith("^14", StringComparison.OrdinalIgnoreCase))
		.Select(dependency =>
			$"npm install {(dependency.dev ? "--save-dev" : "")} {dependency.Name}@15");
dependencies.Dump();
