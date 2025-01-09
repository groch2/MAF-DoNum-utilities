USE [GEDMAF]
GO

WITH NB_DOCUMENTS_BY_COMPTE_ID (COMPTE_ID, NB_DOCUMENTS)
AS (
	SELECT
		[compteId],
		COUNT([DocumentId]) AS 'nb documents'
	FROM [GED].[Document]
	WHERE
		statut = 'INDEXE' AND
		categoriesFamille IN ('DOCUMENTS CONTRAT', 'DOCUMENTS EMOA', 'DOCUMENTS PERSONNES')
	GROUP BY [compteId]
)
SELECT *
FROM NB_DOCUMENTS_BY_COMPTE_ID
WHERE NB_DOCUMENTS BETWEEN 3 AND 10
ORDER BY NB_DOCUMENTS
GO
