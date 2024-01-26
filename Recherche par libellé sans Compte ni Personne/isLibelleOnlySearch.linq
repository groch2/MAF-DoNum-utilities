<Query Kind="Expression">
  <Reference>C:\TeamProjects\donum\Donum.Domain\bin\Debug\netcoreapp3.1\Donum.Domain.dll</Reference>
</Query>

typeof(Donum.Domain.SearchFilters.DonumSearchCriteria).GetProperties()
	.OrderBy(p => p.Name)
	.Select(p => {
		return (p.PropertyType == typeof(string) ? $"string.IsNullOrEmpty(searchCriteria.{p.Name})" : $"searchCriteria.{p.Name} == null") + "&&" ;
	})