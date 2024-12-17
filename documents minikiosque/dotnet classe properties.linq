<Query Kind="Statements">
  <Reference>C:\TeamProjects\donum\Donum.WebHost.Intranet\bin\Debug\net6.0\Donum.DataAccess.dll</Reference>
</Query>

typeof(Donum.DataAccess.Entities.DocumentMNKNonTransfere)
	.GetProperties()
	.OrderBy(p => p.Name)
	.Select(p => {
		var nullableUnderlyingType = System.Nullable.GetUnderlyingType(p.PropertyType);
		var isNullable = nullableUnderlyingType != null;
		var propertyType = $"{(isNullable ? nullableUnderlyingType : p.PropertyType)}{(isNullable ? "?" : string.Empty)}";
		propertyType =Regex.Replace(input: propertyType, pattern: @"^System\.", replacement: "");
		propertyType = propertyType.Replace("String", "string").Replace("Int32", "int");
		var propertyDeclaration = $"public {propertyType} {p.Name} {{ get; set; }}";
		return propertyDeclaration;
	})
	.Dump();
