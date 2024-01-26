<Query Kind="Statements">
  <Namespace>System.Data.SqlClient</Namespace>
  <IncludeLinqToSql>true</IncludeLinqToSql>
</Query>

using var connection = new SqlConnection("Server=DNSINTBDDGECO01;Database=GEDMAF;Integrated Security=True");
connection.Open();
using var command = connection.CreateCommand();
command.CommandText = @"select C.COLUMN_NAME
from INFORMATION_SCHEMA.COLUMNS C
where C.TABLE_CATALOG = 'GEDMAF'
and C.TABLE_SCHEMA = 'GED'
and C.TABLE_NAME = 'NormalizedDocument'
order by C.COLUMN_NAME";
using var sqlDataAdapter = new SqlDataAdapter(command);
var dataTable = new DataTable();
sqlDataAdapter.Fill(dataTable);
dataTable.Dump();