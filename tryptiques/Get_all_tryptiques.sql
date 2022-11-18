USE [GEDMAF]
GO

SELECT
	[FamilleDocument].[Code] AS 'Famille',
	[CoteDocument].[Code] AS 'Cote',
	[TypeDocument].[Code] AS 'Type'
FROM [Ref].[FamilleDocument]
JOIN [Ref].[CoteDocument]
	ON [FamilleDocument].[FamilleDocumentId] = [CoteDocument].[FamilleDocumentId]
JOIN [Ref].[TypeDocument]
	ON [CoteDocument].[CoteDocumentId] = [TypeDocument].[CoteDocumentId]
WHERE [FamilleDocument].[Code] IN ('DOCUMENTS CONTRAT', 'DOCUMENTS EMOA', 'DOCUMENTS PERSONNES')
AND [CoteDocument].[IsActif] = 1
AND [TypeDocument].[IsActif] = 1
ORDER BY
	[FamilleDocument].[Code],
	[CoteDocument].[Code],
	[TypeDocument].[Code]
GO
