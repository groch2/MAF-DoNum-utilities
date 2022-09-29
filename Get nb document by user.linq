<Query Kind="Statements">
  <Namespace>System.Data.SqlClient</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <IncludeLinqToSql>true</IncludeLinqToSql>
</Query>

using var connection = new SqlConnection("Server = DNSINTBDDGECO01; Database = GEDMAF; Integrated Security = True");
connection.Open();
using var command = connection.CreateCommand();
command.CommandText = @"WITH NB_DOCUMENTS_BY_REDACTEUR (REDACTEUR, NB_DOCUMENTS)
AS
(
	SELECT AssigneRedacteur, count([DocumentId])
	FROM [GED].[Document]
	group by AssigneRedacteur
)
SELECT *
FROM NB_DOCUMENTS_BY_REDACTEUR
ORDER BY NB_DOCUMENTS ASC";
using var reader = command.ExecuteReader();
var dataTable = new DataTable();
dataTable.Load(reader);
dataTable.Dump();
