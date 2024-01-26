<Query Kind="Statements">
  <Namespace>System.Data.SqlClient</Namespace>
  <IncludeLinqToSql>true</IncludeLinqToSql>
</Query>

using var connection = new SqlConnection("Server=bdd-ged.int.maf.local;Database=GEDMAF;Trusted_Connection=True;");
connection.Open();
var command = connection.CreateCommand();
command.CommandText = @"SELECT TOP 1 [ID_DOC] FROM [dbo].[ARCHEAMAF]";
var reader = await command.ExecuteReaderAsync();
while (reader.Read()) {
	reader.GetString(0).Dump();
}
