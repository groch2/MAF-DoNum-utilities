<Query Kind="Statements">
  <Namespace>System.Data.SqlClient</Namespace>
  <IncludeLinqToSql>true</IncludeLinqToSql>
</Query>

using var connection = new SqlConnection("Data Source = bdd-ged.hom.maf.local; Database = GEDMAF; Integrated Security = True");
connection.Open();
var command = connection.CreateCommand();
command.CommandText = @"SELECT documentId from GED.document where libelle = 'Lettre Accompagnement Bordereau Courtier Num  7910023 vendredi 2 juin 2023';";
var result = await command.ExecuteScalarAsync();
result.Dump();
