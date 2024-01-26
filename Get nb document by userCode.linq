<Query Kind="Statements">
  <Namespace>System.Data.SqlClient</Namespace>
  <IncludeLinqToSql>true</IncludeLinqToSql>
</Query>

using var connection = new SqlConnection("Server = bdd-ged.int.maf.local; Database = GEDMAF; Integrated Security = True");
connection.Open();
using var command = connection.CreateCommand();
command.CommandText = @"WITH NB_DOCUMENTS_BY_REDACTEUR (REDACTEUR, NB_DOCUMENTS)
AS (
	SELECT AssigneRedacteur, count([DocumentId])
	FROM [GED].[Document]
	WHERE AssigneRedacteur IS NOT NULL AND TRIM(AssigneRedacteur) <> ''
	group by AssigneRedacteur
)
SELECT TOP 5 *
FROM NB_DOCUMENTS_BY_REDACTEUR
ORDER BY NB_DOCUMENTS ASC";
using var reader = command.ExecuteReader();
var dataTable = new DataTable();
dataTable.Load(reader);
var redacteursCodes = dataTable.Select().Select(r => r["REDACTEUR"].ToString());
//redacteursCodes.Dump();
//Environment.Exit(0);
dataTable.Columns.Add("RedacteurNom", typeof(string));
dataTable.Columns.Add("RedacteurPrenom", typeof(string));
dataTable.Dump();
