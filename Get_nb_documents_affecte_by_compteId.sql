USE [GEDMAF]
GO

WITH NB_DOCUMENTS_BY_COMPTE_ID (COMPTE_ID, NB_DOCUMENTS)
AS (
	SELECT
		[compteId],
		count([DocumentId]) as 'nb documents'
	FROM [GED].[Document]
	where
		statut = 'INDEXE' and
		categoriesFamille in ('DOCUMENTS CONTRAT', 'DOCUMENTS EMOA', 'DOCUMENTS PERSONNES')
	group by [compteId]
)
SELECT *
FROM NB_DOCUMENTS_BY_COMPTE_ID
WHERE NB_DOCUMENTS between 3 and 10
GO
