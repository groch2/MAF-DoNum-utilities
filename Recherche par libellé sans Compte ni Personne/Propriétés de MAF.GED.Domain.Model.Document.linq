<Query Kind="Expression">
  <Reference>C:\TeamProjects\donum\Donum.Services.External\bin\Debug\netcoreapp3.1\Donum.Services.External.dll</Reference>
</Query>

typeof(MAF.GED.Domain.Model.Document)
	.GetProperties()
	.Select(p => {
		var type = Nullable.GetUnderlyingType(p.PropertyType) != null ? $"?{p.PropertyType.GenericTypeArguments[0].Name}" : p.PropertyType.Name;
		return new { p.Name, Type = type };
	})
	.OrderBy(n => n.Name, StringComparer.OrdinalIgnoreCase)