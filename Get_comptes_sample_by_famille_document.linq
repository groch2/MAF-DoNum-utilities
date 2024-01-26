<Query Kind="Program">
  <Namespace>System.Data.SqlClient</Namespace>
  <IncludeLinqToSql>true</IncludeLinqToSql>
</Query>

void Main()
{
	GetCompteForFamilleDocumentId(FamilleDocument.CONTRAT).Dump();
}

static int[] GetCompteForFamilleDocumentId(FamilleDocument familleDocument) {
	var qualitesCompteIdList = new Func<int[]>(() => {
		switch (familleDocument) {
	        case FamilleDocument.CONTRAT:
	        case FamilleDocument.EMOA:
				return Enumerable.Range(1, 5).ToArray();
			default:
				return new [] { 6, 7 };
		}
	})();
	var commaSeparatedQualitesCompteIdList = string.Join(',', qualitesCompteIdList);
	using var connection = new SqlConnection("Data Source = INTPARBDD31; Integrated Security = True");
	connection.Open();
	var command = connection.CreateCommand();
	command.CommandText = @$"WITH [COMPTE] ([RowNumber], [CompteID]) AS (
	SELECT ROW_NUMBER() OVER(ORDER BY [CompteID] ASC),
		[CompteID]
	FROM [MAF_NSI].[Personnes].[Compte]
	JOIN [MAF_NSI].[RefMaf].[TypeCompte]
	ON [Compte].[TypeCompteID] = [TypeCompte].[TypeCompteId]
	AND [TypeCompte].[QualiteCompteId] IN ({commaSeparatedQualitesCompteIdList}))
SELECT [CompteID] FROM [COMPTE]
WHERE [RowNumber] BETWEEN 10000 AND 10100";
	using var sqlDataAdapter = new SqlDataAdapter(command);
	var dataTable = new DataTable();
	sqlDataAdapter.Fill(dataTable);
	return dataTable.Select().Select(row => (int)row["CompteID"]).ToArray();
}

enum FamilleDocument {
    CONTRAT = 19,
    EMOA = 22,
    PERSONNES = 24
};
