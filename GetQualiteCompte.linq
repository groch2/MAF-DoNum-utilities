<Query Kind="Statements">
  <Namespace>System.Data.SqlClient</Namespace>
  <IncludeLinqToSql>true</IncludeLinqToSql>
</Query>

using var connection = new SqlConnection("Data Source = DnsIntBddGeco01; Integrated Security = True");
connection.Open();
var command = connection.CreateCommand();
command.CommandText = @"SELECT [QualiteCompteId]
FROM [MAF_NSI].[RefMaf].[QualiteCompte]";
using var sqlDataAdapter = new SqlDataAdapter(command);
var dataTable = new DataTable();
sqlDataAdapter.Fill(dataTable);
dataTable.Select().Select(row => (int)row["QualiteCompteId"]).Dump();
