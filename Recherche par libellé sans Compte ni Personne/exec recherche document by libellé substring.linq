<Query Kind="Statements">
  <Namespace>System.Data.SqlClient</Namespace>
  <IncludeLinqToSql>true</IncludeLinqToSql>
</Query>

using var connection = new SqlConnection("Server=DNSINTBDDGECO01;Database=GEDMAF;Integrated Security=True");
connection.Open();
using var command = connection.CreateCommand();
command.CommandType = CommandType.StoredProcedure;
command.CommandText = @"[dbo].[RechercheDocumentByLibelleSubstring]";
command.Parameters.Add(new SqlParameter("@LibelleSubstring", "F0704D3FB2E"));
using var sqlDataAdapter = new SqlDataAdapter(command);
var dataTable = new DataTable();
sqlDataAdapter.Fill(dataTable);
dataTable.Rows.Dump();
