<Query Kind="Statements">
  <Namespace>System.Data.SqlClient</Namespace>
  <IncludeLinqToSql>true</IncludeLinqToSql>
</Query>

using var connection = new SqlConnection("Server=DNSINTBDDGECO01;Database=GEDMAF;Integrated Security=True");
connection.Open();
new { ConnectionState = connection.State }.Dump();
var command = connection.CreateCommand();
command.CommandText = @"SELECT TOP 1 [ID_DOC] FROM [dbo].[ARCHEAMAF]";
var reader = await command.ExecuteReaderAsync();
while (reader.Read()) {
	reader.GetString(0).Dump();
}
