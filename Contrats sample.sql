USE [Contrat]
GO

;WITH CONTRAT_1 ([NumeroContrat]) AS (
	SELECT TOP 1000 [NumeroContrat]
	FROM [Geco].[Contrat]
	WHERE [NumeroContrat] IS NOT NULL
	AND RIGHT([NumeroContrat], 1) IN ('A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z')
),
CONTRAT_2 ([NumeroContrat]) AS (
	SELECT DISTINCT([NumeroContrat])
	FROM CONTRAT_1
)
SELECT [Contrat].ContratID, [Contrat].NumeroContrat
FROM CONTRAT_2
JOIN [Geco].[Contrat]
ON CONTRAT_2.[NumeroContrat] = [Contrat].NumeroContrat
GO
