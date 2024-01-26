<Query Kind="Expression">
  <Reference>C:\TeamProjects\donum\Donum.Domain\bin\Debug\netcoreapp3.1\Donum.Domain.dll</Reference>
</Query>

typeof(Donum.Domain.Entities.Document).GetProperties().Select(p => p.Name).OrderBy(n => n)