USE [GEDMAF]
GO

WITH NB_DOCUMENTS_BY_REDACTEUR (REDACTEUR, NB_DOCUMENTS)
AS
(
	SELECT AssigneRedacteur
		  ,count([DocumentId]) as 'nb documents'
	FROM [GED].[Document]
	group by AssigneRedacteur
)
SELECT *
FROM NB_DOCUMENTS_BY_REDACTEUR
ORDER BY NB_DOCUMENTS ASC
GO
